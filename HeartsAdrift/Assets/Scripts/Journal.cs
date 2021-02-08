using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Journal : MonoBehaviour
{
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
        journalNames.Add("Wendy");
        journalNames.Add("Pete");
        journalNames.Add("Bayou");
        journalLikes1.Add("Cosplay");
        journalLikes1.Add("Cosplay");
        journalLikes1.Add("Aquascaping");
        journalLikes2.Add("Fishing");
        journalLikes2.Add("Fishing");
        journalLikes2.Add("Hiking");
        journalLikes3.Add("Swimming");
        journalLikes3.Add("Swimming");
        journalLikes3.Add("Fishing");
        journalJobs.Add("Miller");
        journalJobs.Add("Lighthouse Obrero");
        journalJobs.Add("Fisherman");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            pageNum--;
            if (pageNum < 0)
                pageNum = journalNames.Count - 1;
            UpdatePage();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            pageNum++;
            if (pageNum == journalNames.Count)
                pageNum = 0;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        client.SetText(journalNames[pageNum]);
        likes1.SetText(journalLikes1[pageNum]);
        likes2.SetText(journalLikes2[pageNum]);
        likes3.SetText(journalLikes3[pageNum]);
        job.SetText(journalJobs[pageNum]);
        photo.sprite = journalPhotos[pageNum];
    }
}
