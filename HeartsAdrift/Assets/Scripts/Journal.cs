using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Journal : MonoBehaviour
{
    public static Journal Instance;

    Dictionary<int, string[]> journalData = new Dictionary<int, string[]>();
    Dictionary<int, string[]> journalCurrentData = new Dictionary<int, string[]>();

    [SerializeField] private GameObject[] textList = new GameObject[20];

    public GameObject CPR;
    public GameObject CPL;
    public GameObject PPR;
    public GameObject PPL;
    private GameObject _CPRData;
    private GameObject _CPLData;
    private GameObject _PPRData;
    private GameObject _PPLData;


    //public Image[] photo = new Image[4];

    private string[] _treasureList = new string[4] { "Treasure 1", "Treasure 2", "Treasure 3", "Treasure 4"};
    private string[] _clientList = new string[4] { "Wendy", "Pelican Pete", "Bayou", "Client 4" };
    private string[] _questList = new string[4] { "Wendy\'s Love", "The Wind Beneath Pete\'s Wings", "Pete - Treasure Hunting", "Quest 4" };
    private string[] _islandList = new string[4] { "Windmill Island", "Lighthouse Island", "Creole Island", "Island 4" };

    private string[] _wendyData = new string[5] { "Windmill Island", "Cosplay", "Fishing", "Swimming", "Miller" };
    private string[] _peteData = new string[5] { "Lighthouse Island", "Cosplay", "Fishing", "Swimming", "Lighthouse Obrero" };

    private string[] _lighthouseData = new string[3] { "Pelican Pete", "Client 5", "Client 6" };
    private string[] _windmillData = new string[3] { "Wendy", "Client 2", "Client 3" };
    private string[] _creoleData = new string[3] { "Client 7", "Client 8", "Client 9" };
    private string[] _island4Data = new string[3] { "Client 10", "Client 11", "Client 12" };

    //=======================================================================================================================================

    private string[] _treasureCurrentList = new string[4] { "????", "????", "????", "????" };
    private string[] _clientCurrentList = new string[4] { "????", "????", "????", "????" };
    private string[] _questCurrentList = new string[4] { "????", "????", "????", "????" };
    private string[] _islandCurrentList = new string[4] { "????", "????", "????", "????" };

    private string[] _wendyCurrentData = new string[5] { "????", "????", "????", "????", "????" };
    private string[] _peteCurrentData = new string[5] { "????", "????", "????", "????", "????" };

    private string[] _lighthouseCurrentData = new string[3] { "????", "????", "????" };
    private string[] _windmillCurrentData = new string[3] { "????", "????", "????" };
    private string[] _creoleCurrentData = new string[3] { "????", "????", "????" };
    private string[] _island4CurrentData = new string[3] { "????", "????", "????" };

    public List<Sprite> journalPhotos = new List<Sprite>();
    private int pageNum;

    private void Awake()
    {
        Instance = this;
        _PPLData = PPL.transform.GetChild(0).transform.GetChild(0).gameObject;
        _PPRData = PPR.transform.GetChild(0).transform.GetChild(0).gameObject;
        _CPLData = CPL.transform.GetChild(0).transform.GetChild(0).gameObject;
        _CPRData = CPR.transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    void Start()
    {
        pageNum = 0;
        journalCurrentData.Add(0, _wendyCurrentData);
        journalCurrentData.Add(1, _peteCurrentData);
        journalCurrentData.Add(2, _windmillCurrentData);
        journalCurrentData.Add(3, _lighthouseCurrentData);
        journalCurrentData.Add(4, _creoleCurrentData);
        journalCurrentData.Add(5, _island4CurrentData);
        journalCurrentData.Add(6, _questCurrentList);
        journalCurrentData.Add(7, _treasureCurrentList);

        journalData.Add(0, _wendyData);
        journalData.Add(1, _peteData);
        journalData.Add(2, _windmillData);
        journalData.Add(3, _lighthouseData);
        journalData.Add(4, _creoleData);
        journalData.Add(5, _island4Data);
        journalData.Add(6, _questList);
        journalData.Add(7, _treasureList);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            pageNum -= 1;
            if (pageNum < 0)
                pageNum = journalData.Count - 1;
            UpdatePageLeft();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            pageNum += 1;
            if (pageNum == journalData.Count)
                pageNum = 0;
            UpdatePageRight();
        }
    }

    private void UpdatePageRight()
    {
        if (pageNum < 6 && pageNum > 1) //islands
        {
            pageNum++;
            Debug.Log(journalCurrentData[pageNum - 1][0]);
            //_CPLData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(_islandCurrentList[pageNum - 1]);
            _CPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
            _CPLData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
            _CPLData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);

            //_CPRData.transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>().SetText(_islandCurrentList[pageNum]);
            _CPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPRData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPRData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);

        }
        else if (pageNum == 6) //quests
        {
            _CPLData.transform.GetChild(12).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
        }
        else if (pageNum == 7) //treasures
        {
            _CPLData.transform.GetChild(16).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]); 
        }
        else if (pageNum > 7 || pageNum < 2) //clients
        {
            UpdatePages(0);
            pageNum = 1;
            _CPLData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum - 1]);
            _CPLData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
            _CPLData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
            _CPLData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);
            _CPLData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][3]);
            _CPLData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][4]);

            _CPRData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum]);
            _CPRData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPRData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPRData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
            _CPRData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][3]);
            _CPRData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][4]);
        }
    }

    private void UpdatePageLeft()
    {
        if (pageNum < 6 && pageNum > 1) //islands
        {
            UpdatePages(1);
            pageNum++;
            _CPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(_islandCurrentList[pageNum - 2]);
            _CPLData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
            _CPLData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
            _CPLData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);

            _CPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(_islandCurrentList[pageNum - 1]);
            _CPRData.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPRData.transform.GetChild(10).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPRData.transform.GetChild(11).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);

        }
        else if (pageNum == 6) //quests
        {
            UpdatePages(2);
            _CPLData.transform.GetChild(12).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(13).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(14).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
        }
        else if (pageNum == 7) //treasures
        {
            UpdatePages(3);
            _CPLData.transform.GetChild(16).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPLData.transform.GetChild(17).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPLData.transform.GetChild(18).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
        }
        else if (pageNum > 7 || pageNum < 2) //clients
        {
            pageNum = 1;
            _CPLData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum - 1]);
            _CPLData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][0]);
            _CPLData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][1]);
            _CPLData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][2]);
            _CPLData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][3]);
            _CPLData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum - 1][4]);

            _CPRData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(_clientCurrentList[pageNum]);
            _CPRData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][0]);
            _CPRData.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][1]);
            _CPRData.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][2]);
            _CPRData.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][3]);
            _CPRData.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText(journalCurrentData[pageNum][4]);
            UpdatePages(0);
        }
    }

    public void UpdateClient(int client, int index)
    {
        journalCurrentData[client][index] = journalData[client][index];
    }

    public void UpdateQuest(int index)
    {
        journalCurrentData[6][index] = journalData[6][index];
    }

    public void UpdateTreasure(int index)
    {
        journalCurrentData[7][index] = journalData[7][index];
    }

    public void UpdateIsland(int index)
    {
        journalCurrentData[8][index] = journalData[8][index];
    }

    private void UpdatePages(int section)
    {
        switch (section)
        {
            case 0:
                for(int i = 0; i < textList.Length; i++)
                {
                    if (i < 8)
                    {
                        textList[i].SetActive(true);
                    }
                    else textList[i].SetActive(false);
                }
                break;
            case 1:
                for (int i = 0; i < textList.Length; i++)
                {
                    if (i < 12 && i > 7)
                    {
                        textList[i].SetActive(true);
                    }
                    else textList[i].SetActive(false);
                }
                break;
            case 2:
                for (int i = 0; i < textList.Length; i++)
                {
                    if (i > 11 && i < 16)
                    {
                        textList[i].SetActive(true);
                    }
                    else textList[i].SetActive(false);
                }
                break;
            case 3:
                for (int i = 0; i < textList.Length; i++)
                {
                    if (i > 15 && i < 20)
                    {
                        textList[i].SetActive(true);
                    }
                    else textList[i].SetActive(false);
                }
                break;
        }
    }
}
