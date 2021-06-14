using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class Wendy : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    private Transform nextTransform;
    private bool revert;

    private Animator anim;

    private UnityEngine.AI.NavMeshAgent agent;

    private QuestState questState;

    private bool isQuestGiven;

    void Start()
    {
        revert = false;
        isQuestGiven = false;
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.SetDestination(waypoint1.position);
        agent.speed = 5f;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //QuestLog.SetQuestState("Wendy's Love", QuestState.Active);
        //questState = QuestLog.GetQuestState("Wendy's Love");
        if (questState != QuestState.Unassigned)
        {
            /*if (!isQuestGiven)
            {
                Journal.Instance.UpdateQuest(0);
                Journal.Instance.UpdateClientName(0);
                Journal.Instance.UpdateClient(0, 0);
                Journal.Instance.UpdateClient(0, 1);
                Journal.Instance.UpdateClient(0, 3);
                Journal.Instance.UpdateIsland(2, 0);
                Journal.Instance.UpdateIsland(2, 1);
                Journal.Instance.UpdateQuest(0);
            }
            isQuestGiven = true;*/
            if (TimeCycle.Instance.GetHours() == 8f && !revert)
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
        if (agent.velocity == new Vector3(0f, 0f, 0f)) // Triggers Idle
        {
            anim.SetBool("isWalking", false);
        }

        else // Triggers Walking
        {
            anim.SetBool("isWalking", true);
        }
    }
}
