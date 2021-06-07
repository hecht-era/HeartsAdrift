using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookmark : MonoBehaviour
{
    public GameObject journal;
    private Journal obj;
    public int pageNum;
    public bool isLeft;

    void Start()
    {
        obj = journal.GetComponent<Journal>();
    }

    void OnMouseDown()
    {
        if(pageNum == 9)
        {
            obj.OpenJournal(false);
        }
        else
        {
            if (isLeft)
                obj.ClickBookmarkLeft(pageNum);
            else obj.ClickBookmarkRight(pageNum);
        }

    }
}
