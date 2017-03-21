﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderDetectTarget : MonoBehaviour {

    //타깃
    public GameObject target;
    private SpiderAnimation SpiderAni;

    //패트롤포인트찾기
    private MonsterFindPatroll PatrollPt;

    //private SpiderMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    //레이캐스트거리
    public float RayDistance = 5.0f;

    //죽었는지 살았는지
    private bool isDie = false;

    //유저 추적하다가 유저가 사라지면 다시 제자리로 돌아갈위치
    private GameObject PrefMove = null;

    private MonsterDetectCollider DetectColl;

    private MonsterInformation Information;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Move = GetComponent<SpiderMove>();
        SpiderAni = GetComponent<SpiderAnimation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
        DetectColl = GetComponent<MonsterDetectCollider>();

        Information = GetComponent<MonsterInformation>();
        //StartCoroutine(Checkeverything());
    }

    void Update()
    {
        if (SpiderAni.Spider["Death"].normalizedTime >= 0.9f)
        {
            MonsterParentsCollider CheckPt = GetComponent<MonsterParentsCollider>();
            CheckPt.isDie = true;
            Information.isDie = true;
        }

        isDie = Information.isDie;

        if (!isDie)
        {
            if (!Information.isHit)
            {
                if (Information.MonsterState == MonsterInformation.STATE.ATTACK)
                {
                    if (agent.enabled)
                        agent.enabled = false;
                }
                else
                {
                    if (!agent.enabled)
                        agent.enabled = true;
                }
                RayCast();
                RandDetect();
            }
            else
            {
                if(SpiderAni.Spider["Idle"].normalizedTime >= 0.5f)
                {
                    MonsterParentsCollider CheckPt = GetComponent<MonsterParentsCollider>();
                    CheckPt.isHit = false;
                    SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
                }
            }
        }
        else
        {
            StartCoroutine(YouDie());
        }
        

    }
    // Update is called once per frame
    IEnumerator YouDie()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
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
                if (agent.enabled)
                {
                    if (target)
                    {
                        if (Vector3.Distance(agent.destination, target.transform.position) >= 2.0f)
                        {
                            agent.destination = target.transform.position;
                        }
                    }
                }
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

            if (target.gameObject.CompareTag("Player"))
            {
                if (DetectColl.FindPatrollNow)
                {
                    target = PrefMove;
                    PatrollPt.Point = target.GetComponent<MakePatroll>();
                    DetectColl.FindPatrollNow = false;
                }
            }

            if (DetectColl.FindPlayer)
            {
                if ((DetectColl.DetectZone() <= 45) ||
                    (DetectColl.DetectZone() >= 315))
                {
                    if (!PatrollPt.findPlayer)
                    {
                        //if (Ray.collider.tag == "Player")
                        //{
                        PrefMove = target;
                        target = null;
                        target = DetectColl.target;
                        agent.destination = target.transform.position;
                        PatrollPt.ActPatroll = false;
                        PatrollPt.findPlayer = true;
                        SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
                        //}
                    }
                    else if (target != DetectColl.target)
                    {
                        target = null;
                        target = DetectColl.target;
                    }
                }
                else if (Vector3.Distance(DetectColl.target.transform.position, transform.position) <= 10.0f)
                {
                    if (!PatrollPt.findPlayer)
                    {
                        //if (Ray.collider.tag == "Player")
                        //{
                        PrefMove = target;
                        target = null;
                        target = DetectColl.target;
                        agent.destination = target.transform.position;
                        PatrollPt.ActPatroll = false;
                        PatrollPt.findPlayer = true;
                        SpiderAni.NowState = SpiderAnimation.S_STATE.S_RUN;
                        //}
                    }
                    else if (target != DetectColl.target)
                    {
                        target = null;
                        target = DetectColl.target;
                    }
                }
            }

        }
        else
        {
            findAndMove();
        }
        //Move.Wolf.transform.LookAt(transform.position + transform.forward);
        //Move.transform.LookAt(transform.position + transform.forward);
        //transform.LookAt(transform.position + transform.forward);
    }
    private void RayCast()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 1.0f;
        ObjPos.y += 2.0f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Monster")) |
            (1 << LayerMask.NameToLayer("PatrollPoint")) |
             (1 << LayerMask.NameToLayer("Default")));       //layerMask = ~layerMask;
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
        ObjPos.y += 2.0f;

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
