using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Journal : MonoBehaviour
{
    Dictionary<int, List<string>> journalData = new Dictionary<int, List<string>>();
    [HideInInspector]
    public List<string> journalNames = new List<string>();
    [HideInInspector]
    public List<string> journalLikes1 = new List<string>();
    [HideInInspector] 
    public List<string> journalLikes2 = new List<string>();
    [HideInInspector] 
    public List<string> journalLikes3 = new List<string>();
    [HideInInspector] 
    public List<string> journalJobs = new List<string>();

    public TextMeshProUGUI client;
    public TextMeshProUGUI likes1;
    public TextMeshProUGUI likes2;
    public TextMeshProUGUI likes3;
    public TextMeshProUGUI job;
    public Image photo;

    public List<Sprite> journalPhotos = new List<Sprite>();
    private int pageNum;

    void Start()
    {
        pageNum = 0;
        List<string> wendy = new List<string>();
        List<string> pete = new List<string>();
        List<string> bayou = new List<string>();
        List<string> p4 = new List<string>();
        List<string> p5 = new List<string>();
        List<string> p6 = new List<string>();
        List<string> p7 = new List<string>();
        List<string> p8 = new List<string>();
        List<string> p9 = new List<string>();
        List<string> p10 = new List<string>();
        wendy.Add("Wendy");
        wendy.Add("Windmill Island");
        wendy.Add("Cosplay");
        wendy.Add("Fishing");
        wendy.Add("Swimming");
        wendy.Add("Miller");
        journalData.Add(0, wendy);
        pete.Add("Pete");
        pete.Add("Lighthouse Island");
        pete.Add("Cosplay");
        pete.Add("Fishing");
        pete.Add("Swimming");
        pete.Add("Lighthouse Obrero");
        journalData.Add(1, pete);
        bayou.Add("Bayou");
        bayou.Add("Swamp Island");
        bayou.Add("Aquascaping");
        bayou.Add("Hiking");
        bayou.Add("Fishing");
        bayou.Add("Fisherman");
        journalData.Add(2, bayou);
        p4.Add("Name");
        p4.Add("Island");
        p4.Add("Likes1");
        p4.Add("Likes2");
        p4.Add("Likes3");
        p4.Add("Job");
        journalData.Add(3, p4);
        p5.Add("Name");
        p5.Add("Island");
        p5.Add("Likes1");
        p5.Add("Likes2");
        p5.Add("Likes3");
        p5.Add("Job");
        journalData.Add(4, p5);
        p6.Add("Name");
        p6.Add("Island");
        p6.Add("Likes1");
        p6.Add("Likes2");
        p6.Add("Likes3");
        p6.Add("Job");
        journalData.Add(5, p6);
        p7.Add("Name");
        p7.Add("Island");
        p7.Add("Likes1");
        p7.Add("Likes2");
        p7.Add("Likes3");
        p7.Add("Job");
        journalData.Add(6, p7);
        p8.Add("Name");
        p8.Add("Island");
        p8.Add("Likes1");
        p8.Add("Likes2");
        p8.Add("Likes3");
        p8.Add("Job");
        journalData.Add(7, p8);
        p9.Add("Name");
        p9.Add("Island");
        p9.Add("Likes1");
        p9.Add("Likes2");
        p9.Add("Likes3");
        p9.Add("Job");
        journalData.Add(8, p9);
        p10.Add("Name");
        p10.Add("Island");
        p10.Add("Likes1");
        p10.Add("Likes2");
        p10.Add("Likes3");
        p10.Add("Job");
        journalData.Add(9, p10);
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 6; j++)
                Debug.Log(journalData[i][j]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            pageNum--;
            if (pageNum < 0)
                pageNum = journalData.Count - 1;
            UpdatePage();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            pageNum++;
            if (pageNum == journalData.Count)
                pageNum = 0;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        client.SetText(journalData[pageNum][0]);
        likes1.SetText(journalData[pageNum][2]);
        likes2.SetText(journalData[pageNum][3]);
        likes3.SetText(journalData[pageNum][4]);
        job.SetText(journalData[pageNum][5]);
        photo.sprite = journalPhotos[pageNum];
    }
}
