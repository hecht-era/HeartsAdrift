using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [SerializeField] Canvas pauseMenu;
    [SerializeField] Canvas regCanvas;
    [SerializeField] Sprite[] images;
    private bool[] activeCanvas = new bool[3];

    private bool _isPaused;
    private GameState _lastState;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _isPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            _lastState = StateManager.Instance.GetState();
            StateManager.Instance.SetState(GameState.PAUSED);
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.gameObject.SetActive(true);
            int rand = Random.Range(0, images.Length);
            pauseMenu.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = images[rand];
            Time.timeScale = 0;
            _isPaused = true;
            for(int i = 6; i < 9; i++)
            {
                if (regCanvas.transform.GetChild(i).gameObject.activeSelf)
                {
                    activeCanvas[i - 6] = true;
                }
                else activeCanvas[i - 6] = false;
                regCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            ResumeGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        _isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        StateManager.Instance.SetState(_lastState);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        _isPaused = false;
        for(int i = 0; i < 3; i++)
        {
            if (activeCanvas[i])
            {
                regCanvas.transform.GetChild(i+6).gameObject.SetActive(true);
            }
        }
    }
}
