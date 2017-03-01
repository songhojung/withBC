﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderDetectTarget : MonoBehaviour {

    public GameObject target;
    private SpiderAnimation SpiderAni;

    private MonsterFindPatroll PatrollPt;

    private SpiderMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    private float RayDistance = 15.0f;
    private bool isDie = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Move = GetComponent<SpiderMove>();
        SpiderAni = GetComponent<SpiderAnimation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
        //StartCoroutine(Checkeverything());
    }

    //IEnumerator Checkeverything()
    //{
    //    while (!isDie)
    //    {
    //        RayCast();
    //        if (Ray.collider != null && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_ROAR)
    //        {
    //            if (Ray.collider.tag == "Player")
    //            {
    //                if (WolfAnimation.NowState == WerewolfeAnimation.W_STATE.S_WALK || WolfAnimation.NowState == WerewolfeAnimation.W_STATE.S_RUN)
    //                {
    //                    WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_ROAR;
    //                }
    //            }
    //            else
    //            {
    //                agent.destination = target.transform.position;
    //                if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
    //                {
    //                    WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (!WolfAnimation.Werewolf.IsPlaying("Roar"))
    //            {
    //                agent.destination = target.transform.position;
    //                if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
    //                {
    //                    WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
    //                }
    //            }
    //        }
    //        //Move.Wolf.transform.LookAt(transform.position + transform.forward);
    //        //transform.LookAt(transform.position + transform.forward);
    //        Move.transform.LookAt(transform.position + transform.forward);

    //        yield return null;
    //    }
    //}
    void Update()
    {
        if (!isDie)
        {
            RayCast();
            RandDetect();
        }
        

    }
    // Update is called once per frame

    private void RandDetect()
    {
        if (target)
        {
            if (Ray.collider != null)
            {
                if (SpiderAni.NowState != SpiderAnimation.S_STATE.S_ATT)
                {
                    if (Ray.collider.tag == "Player")
                    {
                        if (SpiderAni.NowState == SpiderAnimation.S_STATE.S_WALK || SpiderAni.NowState == SpiderAnimation.S_STATE.S_RUN)
                        {
                            SpiderAni.NowState = SpiderAnimation.S_STATE.S_ATT;
                        }
                    }
                    else
                    {
                        //agent.destination = target.transform.position;
                        if (agent.velocity.magnitude > 0.0f && SpiderAni.NowState != SpiderAnimation.S_STATE.S_RUN)
                        {
                            SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
                        }
                    }
                }
                else
                {
                    if (Ray.collider.tag != "Player")
                    {
                        if (SpiderAni.Spider["Attack"].normalizedTime >= 0.95f)
                        {
                            SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
                        }
                    }
                }
            }
            else
            {
                //if (!SpiderAni.Spider.IsPlaying("Attack"))
                if (SpiderAni.Spider["Attack"].normalizedTime >= 0.95f)
                {
                    SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
                }
                if (SpiderAni.NowState != SpiderAnimation.S_STATE.S_ATT)
                {
                    //agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f && SpiderAni.NowState != SpiderAnimation.S_STATE.S_RUN)
                    {
                        SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
                    }
                }
            }

            if (SpiderAni.NowState == SpiderAnimation.S_STATE.S_RUN)
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
                        SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
                    }
                }
            }

        }
        else
        {
            findAndMove();
        }
        //Move.Wolf.transform.LookAt(transform.position + transform.forward);
        Move.transform.LookAt(transform.position + transform.forward);
        transform.LookAt(transform.position + transform.forward);
    }
    private void RayCast()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 1.0f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Monster")) |
                    (1 << LayerMask.NameToLayer("PatrollPoint")));        //layerMask = ~layerMask;
        Physics.Raycast(ObjPos, ObjForward, out Ray, RayDistance,layerMask);

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
                SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
            }
        }
        if (PatrollPt.ActPatroll)
        {

            target = PatrollPt.PatrollPoint;
            agent.destination = target.transform.position;
            SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
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
