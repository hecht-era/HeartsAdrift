using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class boat : MonoBehaviour
{
    public float turnSpeed = 1000f;
    public float moveSpeed = 1000f;
    private GameState _state;
    private GameState _lastState;
    private GameObject _otherObj;

    private Rigidbody _rBody;
    [SerializeField] public GameObject compass;
    [SerializeField] public Canvas canvas;
    [SerializeField] public GameObject player;
    [SerializeField] public Camera cam;
    [SerializeField] public Transform cameraPos;
    [SerializeField] public Transform playerPos;
    [SerializeField] public Transform crow;
    [SerializeField] public GameObject book;
    [SerializeField] public GameObject bookCover;
    //[SerializeField] public GameObject camFront;
    [SerializeField] public GameObject bookPos;
    [SerializeField] public GameObject mapPos;
    [SerializeField] public GameObject map;
    [SerializeField] public GameObject frontFace;
    [SerializeField] public GameObject crane;
    [SerializeField] public GameObject tether;
    [SerializeField] public LineRenderer rope;

    private bool _isHighlighted;
    private GameObject _hitBook;
    private GameObject _hitMap;
    private bool _inTrigger;
    private bool _isTreasure;
    private bool _gotTreasure;
    private bool _pickUpTreasure;
    private bool _isAutoSail;

    private Vector3 _treasureTarget;

    private Journal journal;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        _rBody = GetComponent<Rigidbody>();
        rope = rope.GetComponent<LineRenderer>();
        _state = StateManager.Instance.GetState();
        _isHighlighted = false;
        _hitBook = null;
        _hitMap = null;
        _inTrigger = false;
        _isTreasure = false;
        _gotTreasure = false;
        _pickUpTreasure = false;
        _isAutoSail = false;
        journal = book.GetComponent<Journal>();
    }

    void FixedUpdate()
    {
        if (StateManager.Instance.GetState() != GameState.DOCKED && StateManager.Instance.GetState() != GameState.WALKING)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _isAutoSail = !_isAutoSail;
            }
            if (_isAutoSail)
            {
                if (Input.GetAxis("Vertical") == 0)
                    v = 1;
                else
                    _isAutoSail = false;
            }
            Vector3 m_EulerAngleVelocity = new Vector3(0f, h * turnSpeed * Time.deltaTime, 0f);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);

            if (StateManager.Instance.GetState() == GameState.SAILING)
            {
                _rBody.MoveRotation(_rBody.rotation * deltaRotation);
                _rBody.AddForce(transform.forward * v * moveSpeed * Time.deltaTime);
            }

            Quaternion compassRotation = deltaRotation;
            compassRotation.x = compass.transform.rotation.x;
            compassRotation.z = compass.transform.rotation.z;
            compassRotation.y += 1.3f; //stupid quaternions
            compass.transform.rotation = compassRotation;
        }
    }
    private void Update()
    {
        if (StateManager.Instance.GetState() != GameState.READING && StateManager.Instance.GetState() != GameState.PAUSED && StateManager.Instance.GetState() != GameState.WALKING)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        //Cast a ray forward and highlight interactable object
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Interactable")))
        {
            if (hit.collider.gameObject.transform.tag == "Book")
            {
                _hitBook = hit.collider.gameObject;
                bookCover.GetComponent<Highlight>().AddHighlight();
                _isHighlighted = true;
                canvas.transform.GetChild(6).gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && StateManager.Instance.GetState() != GameState.READING)
                {
                    canvas.transform.GetChild(6).gameObject.SetActive(false);
                    _lastState = StateManager.Instance.GetState();
                    StateManager.Instance.SetState(GameState.READING);
                    bookCover.GetComponent<Highlight>().RemoveHighlight();
                    Cursor.lockState = CursorLockMode.None;
                    journal.OpenJournal(true);
                }
            }
            else if (hit.collider.gameObject.transform.tag == "Map" && StateManager.Instance.GetState() != GameState.READING)
            {
                _hitMap = hit.collider.gameObject;
                _hitMap.GetComponent<Highlight>().AddHighlight();
                _isHighlighted = true;
                canvas.transform.GetChild(6).gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    canvas.transform.GetChild(6).gameObject.SetActive(false);
                    _lastState = StateManager.Instance.GetState();
                    StateManager.Instance.SetState(GameState.READING);
                }
            }
        }
        else if (_isHighlighted)
        {
            if (_hitBook != null)
                bookCover.GetComponent<Highlight>().RemoveHighlight();
            if (_hitMap != null)
                _hitMap.GetComponent<Highlight>().RemoveHighlight();
            _isHighlighted = false;
            _hitBook = null;
            _hitMap = null;
            canvas.transform.GetChild(6).gameObject.SetActive(false);
        }

        if (_isTreasure)
        {
            GatherTreasure();
        }
        if (_pickUpTreasure)
        {
            _pickUpTreasure = false;
            _isTreasure = false;
            crane.GetComponent<Treasure>().TreasureDone();
            _otherObj.SetActive(false);
            canvas.transform.GetChild(9).gameObject.SetActive(false);
            StateManager.Instance.SetState(GameState.SAILING);
        }

        Vector3[] points = new Vector3[2];
        points[0] = tether.transform.position;
        points[1] = crane.transform.position;
        rope.SetPositions(points);

        StateHandler();
    }

    private void StateHandler()
    {
        if (StateManager.Instance.GetState() == GameState.DOCKING)
        {
            transform.position = Vector3.MoveTowards(transform.position, _otherObj.transform.position, Time.deltaTime * 3);
            transform.forward = Vector3.RotateTowards(transform.forward, _otherObj.transform.right * -1, Time.deltaTime, Time.deltaTime);
            GetComponent<Collider>().enabled = false;
            if (transform.forward == _otherObj.transform.right * -1)
            {
                GetComponent<Collider>().enabled = true;
                StateManager.Instance.SetState(GameState.DOCKED);
            }
        }
        if (StateManager.Instance.GetState() == GameState.UNDOCKING)
        {
            transform.position = Vector3.MoveTowards(transform.position, _otherObj.transform.GetChild(0).transform.position, Time.deltaTime * 2);
        }
        if (StateManager.Instance.GetState() == GameState.READING)
        {
            canvas.transform.GetChild(7).gameObject.SetActive(false);
            canvas.transform.GetChild(8).gameObject.SetActive(true);
            if (_hitBook != null || journal.ReturnJournal())
            {
                book.transform.position = Vector3.MoveTowards(book.transform.position, frontFace.transform.position, 1.1f * Time.deltaTime);
                book.transform.forward = Vector3.RotateTowards(book.transform.forward, -frontFace.transform.forward, .05f, .05f);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    bookCover.GetComponent<Highlight>().RemoveHighlight();
                    StateManager.Instance.SetState(_lastState);
                    _lastState = GameState.READING;
                    Cursor.lockState = CursorLockMode.Locked;
                    journal.OpenJournal(false);
                }
            }
            else if (!journal.ReturnJournal() && _hitMap == null)
            {
                StateManager.Instance.SetState(_lastState);
                _lastState = GameState.READING;
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (_hitMap != null)
            {
                map.GetComponent<Highlight>().RemoveHighlight();
                map.transform.position = Vector3.MoveTowards(map.transform.position, frontFace.transform.position, 1f * Time.deltaTime);
                map.transform.forward = Vector3.RotateTowards(map.transform.forward, frontFace.transform.forward, .05f, .05f);
                map.transform.localScale = Vector3.MoveTowards(map.transform.localScale, frontFace.transform.localScale, 1f * Time.deltaTime);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    map.GetComponent<Highlight>().RemoveHighlight();
                    StateManager.Instance.SetState(_lastState);
                    _lastState = GameState.READING;
                }
            }
        }
        if (_lastState == GameState.READING)
        {
            canvas.transform.GetChild(8).gameObject.SetActive(false);
            canvas.transform.GetChild(7).gameObject.SetActive(true);
            bookCover.GetComponent<Collider>().enabled = false;
            map.GetComponent<Collider>().enabled = false;
            book.transform.position = Vector3.MoveTowards(book.transform.position, bookPos.transform.position, 1f * Time.deltaTime);
            book.transform.forward = Vector3.RotateTowards(book.transform.forward, bookPos.transform.forward, .05f, .05f);
            if (book.transform.position == bookPos.transform.position)
                bookCover.GetComponent<Collider>().enabled = true;
            map.transform.position = Vector3.MoveTowards(map.transform.position, mapPos.transform.position, 1f * Time.deltaTime);
            map.transform.forward = Vector3.RotateTowards(map.transform.forward, mapPos.transform.forward, .05f, .05f);
            map.transform.localScale = Vector3.MoveTowards(map.transform.localScale, mapPos.transform.localScale, 1f * Time.deltaTime);
            if (map.transform.position == mapPos.transform.position)
                map.GetComponent<Collider>().enabled = true;
        }

        if (StateManager.Instance.GetState() != GameState.WALKING && _inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E) && StateManager.Instance.GetState() == GameState.SAILING)
            {
                Debug.Log(_otherObj.tag);
                if (_otherObj.tag == "Treasure")
                {
                    _rBody.velocity = new Vector3(0, 0, 0);
                    StateManager.Instance.SetState(GameState.TREASURE);
                    _treasureTarget = new Vector3(crane.transform.position.x, crane.transform.position.y - 6f, crane.transform.position.z);
                    _isTreasure = true;
                }
                else
                {
                    canvas.transform.GetChild(0).gameObject.SetActive(false);
                    canvas.transform.GetChild(1).gameObject.SetActive(true);
                    StateManager.Instance.SetState(GameState.DOCKING);
                }

                _lastState = GameState.SAILING;
            }
            if (Input.GetKeyDown(KeyCode.R) && StateManager.Instance.GetState() == GameState.DOCKED)
            {
                canvas.transform.GetChild(1).gameObject.SetActive(false);
                canvas.transform.GetChild(0).gameObject.SetActive(true);
                StateManager.Instance.SetState(GameState.UNDOCKING);
                //_lastState = GameState.DOCKED;
            }
            if (Input.GetKeyDown(KeyCode.E) && StateManager.Instance.GetState() == GameState.DOCKED && _lastState != GameState.WALKING)
            {
                canvas.transform.GetChild(1).gameObject.SetActive(false);
                canvas.transform.GetChild(0).gameObject.SetActive(false);
                cam.transform.position = crow.position;
                player.transform.position = _otherObj.transform.parent.GetChild(0).position;
                player.transform.rotation = _otherObj.transform.parent.GetChild(0).rotation;
                player.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                StateManager.Instance.SetState(GameState.WALKING);
                player.transform.SetParent(null);
                canvas.transform.GetChild(7).gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E) && StateManager.Instance.GetState() == GameState.DOCKED)
                _lastState = GameState.DOCKED;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Docking" && StateManager.Instance.GetState() == GameState.SAILING)
        {
            canvas.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (other.tag == "Exit" && StateManager.Instance.GetState() == GameState.UNDOCKING)
        {
            StateManager.Instance.SetState(GameState.SAILING);
            _lastState = GameState.UNDOCKING;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Docking")
        {
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            _inTrigger = false;
        }
        else if (other.tag == "Treasure")
        {
            canvas.transform.GetChild(9).gameObject.SetActive(false);
            _inTrigger = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Docking")
        {
            _inTrigger = true;
            _otherObj = other.gameObject;
        }
        else if (other.tag == "Treasure")
        {
            canvas.transform.GetChild(9).gameObject.SetActive(true);
            _inTrigger = true;
            _otherObj = other.gameObject;
        }
    }

    public void ResetState()
    //Resets the camera to the boat, player is put back on boat and disabled
    {
        canvas.transform.GetChild(7).gameObject.SetActive(true);
        cam.transform.position = cameraPos.position;
        player.transform.position = playerPos.position;
        player.transform.rotation = playerPos.rotation;
        player.transform.SetParent(gameObject.transform);
        player.SetActive(false);
        //StartCoroutine(WaitForDocked());
        StateManager.Instance.SetState(GameState.DOCKED);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        _lastState = GameState.WALKING;
    } 


    IEnumerator WaitForDocked() //not needed?
    {
        StateManager.Instance.SetState(GameState.DOCKED);
        yield return new WaitForSeconds(1);
    }

    private void GatherTreasure()
    {
        Vector3 returnTreasure = new Vector3(_treasureTarget.x, _treasureTarget.y + 6f, _treasureTarget.z);

        if(!_gotTreasure)
            crane.transform.position = Vector3.MoveTowards(crane.transform.position, _treasureTarget, 0.05f);
        else
            crane.transform.position = Vector3.MoveTowards(crane.transform.position, returnTreasure, 0.05f);
        if (crane.transform.position == _treasureTarget)
        {
            _gotTreasure = crane.GetComponent<Treasure>().CollectTreasure();
        }
        if(_gotTreasure && crane.transform.position == returnTreasure)
        {
            Debug.Log("test");
            _pickUpTreasure = true;
        }
    }
}
