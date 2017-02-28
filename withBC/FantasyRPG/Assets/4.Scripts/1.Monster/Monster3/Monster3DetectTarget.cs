﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster3DetectTarget : MonoBehaviour {

    public GameObject target;
    private Monster3Animation M3Animation;

    private MonsterFindPatroll PatrollPt;

    //private Monster3Move Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    private float RayDistance = 14.0f;
    private bool isDie = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       // Move = GetComponent<Monster3Move>();
        M3Animation = GetComponent<Monster3Animation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
        //StartCoroutine(Checkeverything());
    }

    void Update()
    {
        //M3Animation = GetComponent<Monster3Animation>();
        RayCast();
        if (target)
        {
            if (Ray.collider != null)
            {
                if (M3Animation.NowState != Monster3Animation.M3_STATE.M3_ATTACK)
                {
                    if (Ray.collider.tag == "Player")
                    {
                        if (M3Animation.NowState == Monster3Animation.M3_STATE.M3_WALK || M3Animation.NowState == Monster3Animation.M3_STATE.M3_RUN)
                        {
                            M3Animation.NowState = Monster3Animation.M3_STATE.M3_ATTACK;
                        }
                    }
                    else
                    {
                        //agent.destination = target.transform.position;
                        if (agent.velocity.magnitude > 0.0f && M3Animation.NowState != Monster3Animation.M3_STATE.M3_RUN)
                        {
                            M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
                        }
                    }
                }
                else
                {
                    if (Ray.collider.tag != "Player")
                    {
                        M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
                    }
                    //if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    //{
                    //    //agent.destination = target.transform.position;
                    //    M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
                    //}
                }
            }
            else
            {
                if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
                {
                    //agent.destination = target.transform.position;
                    M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
                }
                if (M3Animation.NowState != Monster3Animation.M3_STATE.M3_ATTACK)
                {
                    //agent.destination = target.transform.position;
                    if (M3Animation.NowState != Monster3Animation.M3_STATE.M3_RUN)
                    {
                        M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
                    }
                }
            }

            if(M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run"))
            {
                agent.destination = target.transform.position;
            }

            if (target.gameObject.CompareTag("PatrollPoint"))
            {
                float distance = Vector3.Distance(this.transform.position, target.transform.position);
                if (distance <= 5.0f)
                {
                    if (target.GetComponent<MakePatroll>().Child)
                    {
                        target = target.GetComponent<MakePatroll>().Child.gameObject;
                    }
                }

            }

            if (Ray.collider != null)
            {
                if (Ray.collider.tag == "Player")
                {
                    if (!PatrollPt.findPlayer)
                    {
                        target = null;
                        target = Ray.collider.gameObject;
                        agent.destination = target.transform.position;
                        PatrollPt.ActPatroll = false;
                        PatrollPt.findPlayer = true;
                        M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
                    }
                }
            }
        }
        else
        {
            findAndMove();
        }
        //Move.Wolf.transform.LookAt(transform.position + transform.forward);

    }
    // Update is called once per frame

    private void RayCast()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 1.0f;
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Monster"));
        //layerMask = ~layerMask;
        Physics.Raycast(ObjPos, ObjForward, out Ray, RayDistance, layerMask);
    }

    private void findAndMove()
    {
        if (Ray.collider != null)
        {
            if (Ray.collider.tag == "Player")
            {
                target = Ray.collider.gameObject;
                agent.destination = target.transform.position;
                PatrollPt.ActPatroll = false;
                PatrollPt.findPlayer = true;
                M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
            }
        }
        if (PatrollPt.ActPatroll)
        {
            target = PatrollPt.PatrollPoint;
            agent.destination = target.transform.position;
            M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 1.0f;

        if (this.Ray.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.Ray.point, 1.0f);

            Gizmos.color = Color.black;
            Gizmos.DrawLine(ObjPos,
               ObjPos + this.transform.forward * RayDistance);
        }
        else
        {
            Gizmos.DrawLine(ObjPos,
                 ObjPos + this.transform.forward * RayDistance);
        }
    }
}
