using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] public GameObject treasure;
    Animator anim;

    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CollectTreasure()
    {
        treasure.SetActive(true);
        anim.Play("Control");
        return true;
    }

    public void TreasureDone()
    {
        treasure.SetActive(false);
        anim.Play("Release");
    }
}
