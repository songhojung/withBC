using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDetectTarget : MonoBehaviour {

    //타깃
    public GameObject target;
    private ZombieAnimation ZombieAnimation;

    //패트롤포인트찾기
    private MonsterFindPatroll PatrollPt;

    //private ZombieMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    //레이캐스트거리
    public float RayDistance = 5.0f;
    private bool isDie = false;

    //유저 추적시스템
    private GameObject PrefMove = null;
    private MonsterDetectCollider DetectColl;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Move = GetComponent<ZombieMove>();
        ZombieAnimation = GetComponent<ZombieAnimation>();
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
                if (Ray.collider.tag == "Player")
                {
                    if (ZombieAnimation.NowState == ZombieAnimation.Z_STATE.Z_WALK)
                    {
                        ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_ATTACK;
                    }
                }
                else
                {
                    //agent.destination = target.transform.position;
                    if (ZombieAnimation.NowState != ZombieAnimation.Z_STATE.Z_WALK)
                    {
                        ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
                    }
                }
            }
            else
            {
                if (ZombieAnimation.Zombie.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
                }
                if (ZombieAnimation.NowState != ZombieAnimation.Z_STATE.Z_WALK)
                {
                    //agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f)
                    {
                        ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
                    }
                }
            }
            if (ZombieAnimation.NowState == ZombieAnimation.Z_STATE.Z_WALK)
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
                        ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
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
                        ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
                        //}
                    }
                }
            }

        }
        else
        {
            findAndMove();
        }
        //Move.Zombie.transform.LookAt(transform.position + transform.forward);
        //Move.transform.LookAt(transform.position + transform.forward);
        //transform.LookAt(transform.position + transform.forward);
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
                ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
            }
        }
        if (PatrollPt.ActPatroll)
        {

            target = PatrollPt.PatrollPoint;
            agent.destination = target.transform.position;
            ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
        }
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
