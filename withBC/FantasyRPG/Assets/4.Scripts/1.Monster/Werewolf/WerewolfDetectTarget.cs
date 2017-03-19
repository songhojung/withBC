using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WerewolfDetectTarget : MonoBehaviour {

    public GameObject target;
    private WerewolfeAnimation WolfAnimation;

    private MonsterFindPatroll PatrollPt;

    //private WerewolfeMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    public float RayDistance = 5.0f;
    private bool isDie = false;

    private GameObject PrefMove = null;

    private MonsterDetectCollider DetectColl;

    private MonsterInformation Information;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Move = GetComponent<WerewolfeMove>();
        WolfAnimation = GetComponent<WerewolfeAnimation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
        DetectColl = GetComponent<MonsterDetectCollider>();

        Information = GetComponent<MonsterInformation>();
        //StartCoroutine(Checkeverything());
    }

    void Update()
    {
        if (!isDie)
        {
            if (Information.MonsterState == MonsterInformation.STATE.ATTACK)
            {
                if(agent.enabled)
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
        Information.isDie = isDie;
    }
    // Update is called once per frame
   
    private void RandDetect()
    {
        if (target)
        {
            if (Ray.collider != null)
            {
                if (WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_ATT20)
                {
                    if (Ray.collider.tag == "Player")
                    {
                        if (WolfAnimation.NowState == WerewolfeAnimation.W_STATE.S_WALK || WolfAnimation.NowState == WerewolfeAnimation.W_STATE.S_RUN)
                        {
                            WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_ATT20;
                        }
                    }
                    else
                    {
                        //agent.destination = target.transform.position;
                        if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
                        {
                            WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                        }
                    }
                }
                else
                {
                    if (Ray.collider.tag != "Player")
                    {
                        if (WolfAnimation.Werewolf["Attack20"].normalizedTime >= 0.95f)
                        {
                            //agent.destination = target.transform.position;
                            WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                        }
                    }
                }
            }
            else
            {
                if (WolfAnimation.Werewolf["Attack20"].normalizedTime >= 0.95f)
                {
                    //agent.destination = target.transform.position;
                    WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                }
                if (WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_ATT20)
                {
                    //agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
                    {
                        WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                    }
                }
            }
            if (WolfAnimation.NowState == WerewolfeAnimation.W_STATE.S_RUN)
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

            if(target.gameObject.CompareTag("Player"))
            {
                if(DetectColl.FindPatrollNow)
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
                        WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                        //}
                    }
                    else if(target != DetectColl.target)
                    {
                        target = null;
                        target = DetectColl.target;
                    }
                }
                else if(Vector3.Distance(DetectColl.target.transform.position,transform.position) <= 10.0f)
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
                        WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                        //}
                    }
                    else if (target != DetectColl.target)
                    {
                        target = null;
                        target = DetectColl.target;
                    }
                }
            }

            
            //if (Ray.collider != null)
            //{
            //    if (!PatrollPt.findPlayer)
            //    {
            //        if (Ray.collider.tag == "Player")
            //        {
            //            target = null;
            //            target = Ray.collider.gameObject;
            //            agent.destination = target.transform.position;
            //            PatrollPt.ActPatroll = false;
            //            PatrollPt.findPlayer = true;
            //            WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
            //        }
            //    }
            //}
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
        ObjPos.y += 2.0f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Monster"))|
            (1 << LayerMask.NameToLayer("PatrollPoint"))|
             (1 << LayerMask.NameToLayer("Default")));
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
                WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
            }
        }
        if (PatrollPt.ActPatroll)
        {
            target = PatrollPt.PatrollPoint;
            agent.destination = target.transform.position;
            WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
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
