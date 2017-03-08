using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster2DetectTarget : MonoBehaviour {

    public GameObject target;
    private Monster2Animation M2Animation;

    private MonsterFindPatroll PatrollPt;

   //private Monster2Move Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    public float RayDistance = 5.0f;
    private bool isDie = false;

    private GameObject PrefMove = null;

    private MonsterDetectCollider DetectColl;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Move = GetComponent<Monster2Move>();
        M2Animation = GetComponent<Monster2Animation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
        DetectColl = GetComponent<MonsterDetectCollider>();
        //StartCoroutine(Checkeverything());
    }
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
                if (M2Animation.NowState != Monster2Animation.M2_STATE.M2_ATTACK)
                {
                    if (Ray.collider.tag == "Player")
                    {
                        if (M2Animation.NowState == Monster2Animation.M2_STATE.M2_WALK || M2Animation.NowState == Monster2Animation.M2_STATE.M2_RUN)
                        {
                            M2Animation.NowState = Monster2Animation.M2_STATE.M2_ATTACK;
                        }
                    }
                    else
                    {
                        //agent.destination = target.transform.position;
                        if (agent.velocity.magnitude > 0.0f && M2Animation.NowState != Monster2Animation.M2_STATE.M2_RUN)
                        {
                            M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                        }
                    }
                }
                else
                {
                    if (Ray.collider.tag != "Player")
                    {
                        M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                    }
                    //if (M2Animation.Mt2.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    //{
                    //    agent.destination = target.transform.position;
                    //    M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                    //}
                }
            }
            else
            {
                if (M2Animation.Mt2.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
                {
                    //agent.destination = target.transform.position;
                    M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                }
                if (M2Animation.NowState != Monster2Animation.M2_STATE.M2_ATTACK)
                {
                    //agent.destination = target.transform.position;
                    if (M2Animation.NowState != Monster2Animation.M2_STATE.M2_RUN)
                    {
                        M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                    }
                }
            }
            if (M2Animation.Mt2.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run"))
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
                        M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                        //}
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
                        M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                        //}
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
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Monster")) |
            (1 << LayerMask.NameToLayer("PatrollPoint")));
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
                M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
            }
        }
        if (PatrollPt.ActPatroll)
        {
            target = PatrollPt.PatrollPoint;
            agent.destination = target.transform.position;
            M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
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
