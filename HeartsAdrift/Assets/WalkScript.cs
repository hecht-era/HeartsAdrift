﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    private Transform nextTransform;
    private bool revert;

    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        revert = false;
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.SetDestination(waypoint1.position);
        agent.speed = 5f;
    }

    private void Update()
    {
        if(TimeCycle.Instance.GetHours() == 8f && !revert) 
        {
            agent.destination = waypoint2.position;
            revert = true;
        }
        else if (TimeCycle.Instance.GetHours() == 14f && revert)
        {
            agent.destination = waypoint1.position;
            revert = false;
        }
    }
}
