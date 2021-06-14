using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PixelCrushers.DialogueSystem;

public class Journal : MonoBehaviour
{
    public static Journal Instance;

    Dictionary<int, string[]> journalData = new Dictionary<int, string[]>();
    Dictionary<int, string[]> journalCurrentData = new Dictionary<int, string[]>();

    [SerializeField] private GameObject[][] textList = new GameObject[4][];
    

    public GameObject CPR;
    public GameObject CPL;
    public GameObject PPR;
    public GameObject PPL;
    private GameObject _CPRData;
    private GameObject _CPLData;
    private GameObject _PPRData;
    private GameObject _PPLData;

    public GameObject BM_TreasureLeft;
    public GameObject BM_IslandsLeft;
    public GameObject BM_PeopleLeft;
    public GameObject BM_DeliveriesLeft;
    public GameObject BM_TreasureRight;
    public GameObject BM_IslandsRight;
    public GameObject BM_PeopleRight;
    public GameObject BM_DeliveriesRight;
    public GameObject BM_CloseRight;


    //public Image[] photo = new Image[4];

    private string[] _treasureList = new string[4] { "Treasure 1", "Treasure 2", "Treasure 3", "Treasure 4"};
    private string[] _clientList = new string[4] { "Wendy", "Pelican Pete", "Bayou", "Client 4" };
    private string[] _questList = new string[4] { "Wendy\'s Love", "The Wind Beneath Pete\'s Wings", "Pete - Treasure Hunting", "Quest 4" };
    private string[] _islandList = new string[4] { "Windmill Island", "Lighthouse Island", "Creole Island", "Island 4" };

    private string[] _wendyData = new string[5] { "Windmill Island", "Cosplay", "Fishing", "Swimming", "Miller" };
    private string[] _peteData = new string[5] { "Lighthouse Island", "Cosplay", "Fishing", "Swimming", "Lighthouse Obrero" };

    private string[] _lighthouseData = new string[4] { "Lighthouse Island", "Pelican Pete", "Client 5", "Client 6" };
    private string[] _windmillData = new string[4] { "Windmill Island", "Wendy", "Client 2", "Client 3" };
    private string[] _creoleData = new string[4] { "Creole Island", "Client 7", "Client 8", "Client 9" };
    private string[] _island4Data = new string[4] { "Island 4", "Client 10", "Client 11", "Client 12" };

    //=======================================================================================================================================

    private string[] _treasureCurrentList = new string[4] { "????", "????", "????", "????" };
    private string[] _treasureCurrentList2 = new string[4] { "????", "????", "????", "????" };
    private string[] _clientCurrentList = new string[4] { "????", "????", "????", "????" };
    private string[] _questCurrentList = new string[4] { "????", "????", "????", "????" };
    private string[] _questCurrentList2 = new string[4] { "????", "????", "????", "????" };
    private string[] _islandCurrentList = new string[4] { "????", "????", "????", "????" };

    private string[] _wendyCurrentData = new string[5] { "????", "????", "????", "????", "????" };
    private string[] _peteCurrentData = new string[5] { "????", "????", "????", "????", "????" };

    private string[] _lighthouseCurrentData = new string[4] { "????", "????", "????", "????" };
    private string[] _windmillCurrentData = new string[4] { "????", "????", "????", "????" };
    private string[] _creoleCurrentData = new string[4] { "????", "????", "????", "????" };
    private string[] _island4CurrentData = new string[4] { "????", "????", "????", "????" };

    public List<Sprite> journalPhotos = new List<Sprite>();

    private Animator anim;

    [HideInInspector]
    public int pageNum;
    
    private void Awake()
    {
        Instance = this;
        _PPLData = PPL.transform.GetChild(0).transform.GetChild(0).gameObject;
        _PPRData = PPR.transform.GetChild(0).transform.GetChild(0).gameObject;
        _CPLData = CPL.transform.GetChild(0).transform.GetChild(0).gameObject;
        _CPRData = CPR.transform.GetChild(0).transform.GetChild(0).gameObject;

        Lua.RegisterFunction("UpdateClient", this, typeof(Journal).GetMethod("UpdateClient"));
        Lua.RegisterFunction("UpdateClientName", this, typeof(Journal).GetMethod("UpdateClientName"));
        Lua.RegisterFunction("UpdateQuest", this, typeof(Journal).GetMethod("UpdateQuest"));
        Lua.RegisterFunction("UpdateIsland", this, typeof(Journal).GetMethod("UpdateIsland"));
        Lua.RegisterFunction("UpdateTreasure", this, typeof(Journal).GetMethod("UpdateTreasure"));
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        pageNum = 0;
        journalCurrentData.Add(0, _wendyCurrentData);
        journalCurrentData.Add(1, _peteCurrentData);
        journalCurrentData.Add(2, _windmillCurrentData);
        journalCurrentData.Add(3, _lighthouseCurrentData);
        journalCurrentData.Add(4, _creoleCurrentData);
        journalCurrentData.Add(5, _island4CurrentData);
        journalCurrentData.Add(6, _questCurrentList);
        journalCurrentData.Add(7, _questCurrentList2);
        journalCurrentData.Add(8, _treasureCurrentList);
        journalCurrentData.Add(9, _treasureCurrentList2);

        journalData.Add(0, _wendyData);
        journalData.Add(1, _peteData);
        journalData.Add(2, _windmillData);
        journalData.Add(3, _lighthouseData);
        journalData.Add(4, _creoleData);
        journalData.Add(5, _island4Data);
        journalData.Add(6, _questList);
        journalData.Add(7, _questList);
        journalData.Add(8, _treasureList);
        journalData.Add(9, _treasureList);

        textList[0] = new GameObject[20];
        textList[1] = new GameObject[20];
        textList[2] = new GameObject[20];
        textList[3] = new GameObject[20];


        for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 20; j++)
            {
                GameObject temp = null;
                switch (i)
                {
                    case 0:
                        temp = _PPLData;
                        break;
                    case 1:
                        temp = _PPRData;
                        break;
                    case 2:
                        temp = _CPLData;
                        break;
                    case 3:
                        temp = _CPRData;
                        break;
                    default:
                        break;
                }
                textList[i][j] = temp.transform.GetChild(j).gameObject;
            }
        }

        UpdatePageRight();
        anim.SetBool("isUsing", false);
    }


    void Update()
    {
        if (anim.GetBool("isUsing"))
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                StartCoroutine(TurnPage(false));
                pageNum -= 2;
                if (pageNum < 0)
                    pageNum = 8;
                UpdatePageLeft();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                StartCoroutine(TurnPage(true));
                pageNum += 2;
                if (pageNum == journalData.Count)
                    pageNum = 0;
                UpdatePageRight();
            }
        }

    }

    private void UpdatePageRight()
    {
        if (pageNum < 6 && pageNum > 1) //islands, pages 2-5 (4 pages) -----maybe i need to count by twos...so this would be pages 4-7
        {
            BM_PeopleLeft.SetActive(true);
            BM_IslandsRight.SetActive(false);
            BM_IslandsLeft.SetActive(false);
            BM_DeliveriesLeft.SetActive(false);
            BM_TreasureLeft.SetActive(false);
            BM_DeliveriesRight.SetActive(true);
            BM_TreasureRight.SetActive(true);
            UpdateCurrentPages(1);
            _CPLData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
            _CPLData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][3]);

            _CPRData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][0]);
            _CPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _CPRData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);
            _CPRData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][3]);

            if (pageNum == 2)
            {
                UpdatePreviousPages(0);
                _PPLData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum - 2]);
                //_PPLData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][0]);
                _PPLData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][1]);
                _PPLData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][2]);
                _PPLData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][3]);
                _PPLData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][4]);

                _PPRData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum - 1]);
                //_PPRData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
                _PPRData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
                _PPRData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);
                _PPRData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][3]);
                _PPRData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][4]);
            }
            else
            {
                UpdatePreviousPages(1);
                _PPLData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][0]);
                _PPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][1]);
                _PPLData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][2]);
                _PPLData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][3]);

                _PPRData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
                _PPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
                _PPRData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);
                _PPRData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][3]);
            }

        }
        else if (pageNum == 6) //quests, page 6 (1 page) ------pages 8-9
        {
            BM_PeopleLeft.SetActive(true);
            BM_IslandsRight.SetActive(false);
            BM_IslandsLeft.SetActive(true);
            BM_DeliveriesLeft.SetActive(false);
            BM_TreasureLeft.SetActive(false);
            BM_DeliveriesRight.SetActive(false);
            BM_TreasureRight.SetActive(true);
            UpdateCurrentPages(2);
            _CPLData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);

            _CPRData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][0]);
            _CPRData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _CPRData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);

            UpdatePreviousPages(1);
            _PPLData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][0]);
            _PPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][1]);
            _PPLData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][2]);
            _PPLData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][3]);

            _PPRData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
            _PPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
            _PPRData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);
            _PPRData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][3]);
        }
        else if (pageNum == 8) //treasures, page 7 (1 page) ----pages 10-11
        {
            BM_PeopleLeft.SetActive(true);
            BM_IslandsRight.SetActive(false);
            BM_IslandsLeft.SetActive(true);
            BM_DeliveriesLeft.SetActive(true);
            BM_TreasureLeft.SetActive(false);
            BM_DeliveriesRight.SetActive(false);
            BM_TreasureRight.SetActive(false);
            UpdateCurrentPages(3);
            _CPLData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);

            _CPRData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][0]);
            _CPRData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _CPRData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);

            UpdatePreviousPages(2);
            _PPLData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][0]);
            _PPLData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][1]);
            _PPLData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][2]);

            _PPRData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
            _PPRData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
            _PPRData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);
        }
        else if (pageNum > 8 || pageNum < 2) //clients, pages 0-1 (2 pages) ----pages 0-3
        {
            BM_PeopleLeft.SetActive(false);
            BM_DeliveriesLeft.SetActive(false);
            BM_IslandsLeft.SetActive(false);
            BM_TreasureLeft.SetActive(false);
            BM_IslandsRight.SetActive(true);
            BM_DeliveriesRight.SetActive(true);
            BM_TreasureRight.SetActive(true);
            UpdateCurrentPages(0);
            pageNum = 0;
            _CPLData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum]);
            _CPLData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
            _CPLData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][3]);
            _CPLData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][4]);

            _CPRData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum + 1]);
            _CPRData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _CPRData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);
            _CPRData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][3]);
            _CPRData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][4]);

            UpdatePreviousPages(3);
            _PPLData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[8][0]);
            _PPLData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[8][1]);
            _PPLData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[8][2]);

            _PPRData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[9][0]);
            _PPRData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[9][1]);
            _PPRData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[9][2]);
        }
    }

    private void UpdatePageLeft()
    {
        if (pageNum < 6 && pageNum > 1) //islands, pages 2-5 (4 pages) -----maybe i need to count by twos...so this would be pages 4-7
        {
            BM_PeopleLeft.SetActive(true);
            BM_IslandsRight.SetActive(false);
            BM_IslandsLeft.SetActive(false);
            BM_DeliveriesLeft.SetActive(false);
            BM_TreasureLeft.SetActive(false);
            BM_DeliveriesRight.SetActive(true);
            BM_TreasureRight.SetActive(true);
            UpdateCurrentPages(1);
            _CPLData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
            _CPLData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][3]);

            _CPRData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][0]);
            _CPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _CPRData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);
            _CPRData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][3]);

            UpdatePreviousPages(1);
            _PPLData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _PPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _PPLData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
            _PPLData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][3]);

            _PPRData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][0]);
            _PPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _PPRData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);
            _PPRData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][3]);

            /*if(pageNum == 2)
            {
                UpdatePreviousPages(0);
                _PPLData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum - 2]);
                //_PPLData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][0]);
                _PPLData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][1]);
                _PPLData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][2]);
                _PPLData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][3]);
                _PPLData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][4]);

                _PPRData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum - 1]);
                //_PPRData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
                _PPRData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
                _PPRData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);
                _PPRData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][3]);
                _PPRData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][4]);
            }
            else
            {
                UpdatePreviousPages(1);
                _PPLData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][0]);
                _PPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][1]);
                _PPLData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][2]);
                _PPLData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][3]);

                _PPRData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
                _PPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
                _PPRData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);
                _PPRData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][3]);
            }*/

        }
        else if (pageNum == 6) //quests, page 6 (1 page) ------pages 8-9
        {
            BM_PeopleLeft.SetActive(true);
            BM_IslandsRight.SetActive(false);
            BM_IslandsLeft.SetActive(true);
            BM_DeliveriesLeft.SetActive(false);
            BM_TreasureLeft.SetActive(false);
            BM_DeliveriesRight.SetActive(false);
            BM_TreasureRight.SetActive(true);
            UpdateCurrentPages(2);
            _CPLData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);

            _CPRData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][0]);
            _CPRData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _CPRData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);

            UpdatePreviousPages(2);

            _PPLData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _PPLData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _PPLData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);

            _PPRData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][0]);
            _PPRData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _PPRData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);
            /*
             *             UpdatePreviousPages(1);
             * _PPLData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][0]);
            _PPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][1]);
            _PPLData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][2]);
            _PPLData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][3]);

            _PPRData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
            _PPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
            _PPRData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);
            _PPRData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][3]);*/
        }
        else if (pageNum == 8) //treasures, page 7 (1 page) ----pages 10-11
        {
            BM_PeopleLeft.SetActive(true);
            BM_IslandsRight.SetActive(false);
            BM_IslandsLeft.SetActive(true);
            BM_DeliveriesLeft.SetActive(true);
            BM_TreasureLeft.SetActive(false);
            BM_DeliveriesRight.SetActive(false);
            BM_TreasureRight.SetActive(false);
            UpdateCurrentPages(3);
            _CPLData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);

            _CPRData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][0]);
            _CPRData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _CPRData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);

            UpdatePreviousPages(3);
            _PPLData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _PPLData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _PPLData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);

            _PPRData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][0]);
            _PPRData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _PPRData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);

            /*UpdatePreviousPages(2);
            _PPLData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][0]);
            _PPLData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][1]);
            _PPLData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 2][2]);

            _PPRData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
            _PPRData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
            _PPRData.transform.GetChild(15).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);*/
        }
        else if (pageNum > 8 || pageNum < 2) //clients, pages 0-1 (2 pages) ----pages 0-3
        {
            BM_PeopleLeft.SetActive(false);
            BM_DeliveriesLeft.SetActive(false);
            BM_IslandsLeft.SetActive(false);
            BM_TreasureLeft.SetActive(false);
            BM_IslandsRight.SetActive(true);
            BM_DeliveriesRight.SetActive(true);
            BM_TreasureRight.SetActive(true);
            UpdateCurrentPages(0);
            pageNum = 0;
            _CPLData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum]);
            _CPLData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
            _CPLData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][3]);
            _CPLData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][4]);

            _CPRData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum + 1]);
            _CPRData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _CPRData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);
            _CPRData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][3]);
            _CPRData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][4]);

            UpdatePreviousPages(0);
            _PPLData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum]);
            _PPLData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _PPLData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
            _PPLData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][3]);
            _PPLData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][4]);

            _PPRData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum + 1]);
            _PPRData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][1]);
            _PPRData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][2]);
            _PPRData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][3]);
            _PPRData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum + 1][4]);

            /*UpdatePreviousPages(3);
            _PPLData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[8][0]);
            _PPLData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[8][1]);
            _PPLData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[8][2]);

            _PPRData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[9][0]);
            _PPRData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[9][1]);
            _PPRData.transform.GetChild(19).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[9][2]);*/
        }
    }

    public void UpdateClient(double client, double index)
    {
        int i = (int)client;
        int j = (int)index;
        journalCurrentData[i][j] = journalData[i][j];
    }

    public void UpdateClientName(double index)
    {
        int i = (int)index;
        _clientCurrentList[i] = _clientList[i];
    }

    public void UpdateQuest(double index)  
    {
        int i = (int)index;
        journalCurrentData[6][i] = journalData[6][i];
    }

    public void UpdateTreasure(double index)
    {
        int i = (int)index;
        journalCurrentData[8][i] = journalData[8][i];
    }

    public void UpdateIsland(double island, double index)
    {
        int i = (int)island;
        int j = (int)index;
        journalCurrentData[i][j] = journalData[i][j];
    }

    private void UpdateCurrentPages(int section)
    {
        int min = 0;
        int max = 0;
        switch (section)
        {
            case 0:
                min = 0;
                max = 7;
                break;
            case 1:
                min = 8;
                max = 11;
                break;
            case 2:
                min = 12;
                max = 15;
                break;
            case 3:
                min = 16;
                max = 19;
                break;
            default:
                break;
        }
        for (int i = 0; i < textList[0].Length; i++)
        {
            if (i == 5)
            {
                textList[2][i].SetActive(false);
                textList[3][i].SetActive(false);
            }
            else if (i >= min && i <= max)
            {
                textList[2][i].SetActive(true);
                textList[3][i].SetActive(true);
            }
            else
            {
                textList[2][i].SetActive(false);
                textList[3][i].SetActive(false);
            }
        }
    }

    private void UpdatePreviousPages(int section)
    {
        int min = 0;
        int max = 0;
        switch (section)
        {
            case 0:
                min = 0;
                max = 7;
                break;
            case 1:
                min = 8;
                max = 11;
                break;
            case 2:
                min = 12;
                max = 15;
                break;
            case 3:
                min = 16;
                max = 19;
                break;
            default:
                break;
        }
        for (int i = 0; i < textList[0].Length; i++)
        {
            if (i == 5)
            {
                textList[0][i].SetActive(false);
                textList[1][i].SetActive(false);
            }
            else if (i >= min && i <= max)
            {
                textList[0][i].SetActive(true);
                textList[1][i].SetActive(true);
            }
            else
            {
                textList[0][i].SetActive(false);
                textList[1][i].SetActive(false);
            }
        }
    }

    public void OpenJournal(bool isOpen)
    {
        anim.SetBool("isUsing", isOpen);
    }

    public bool ReturnJournal()
    {
        return anim.GetBool("isUsing");
    }

    IEnumerator TurnPage(bool isForward)
    {
        if (isForward)
            anim.Play("journal page turn forward", -1, 0f);
        else anim.Play("journal page turn backward", -1, 0f);
        yield return new WaitForSeconds(.5f);
        anim.Play("journal open side", -1, 1);
        yield return null;
    }

    public void ClickBookmarkLeft(int page)
    {
        pageNum = page;
        StartCoroutine(TurnPage(false));
        UpdatePageLeft();
    }

    public void ClickBookmarkRight(int page)
    {
        pageNum = page;
        StartCoroutine(TurnPage(true));
        UpdatePageRight();
    }
}
