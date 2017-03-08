using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GoblinDetectTarget : MonoBehaviour {

    //타깃
    public GameObject target;

    private GoblinAnimation GoblinAni;

    //패트롤포인트 찾기
    private MonsterFindPatroll PatrollPt;

    //private GoblinMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    //레이케스트 레이저 거리
    public float RayDistance = 5.0f;

    //죽었는지 살았는지
    private bool isDie = false;

    //유저 따라가다가 사라지면 다시 지정위치로 돌아갈 위치
    private GameObject PrefMove = null;

    //유저가 일정범위 내로 들어올경우 확인해서 각도가
    //일정 각도 이내일경우 추적을 시작하게함
    private MonsterDetectCollider DetectColl;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Move = GetComponent<GoblinMove>();
        GoblinAni = GetComponent<GoblinAnimation>();
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
                if (GoblinAni.NowState != GoblinAnimation.G_STATE.S_ATT1)
                {
                    if (Ray.collider.tag == "Player")
                    {
                        if (GoblinAni.NowState == GoblinAnimation.G_STATE.S_WALK || GoblinAni.NowState == GoblinAnimation.G_STATE.S_RUN)
                        {
                            GoblinAni.NowState = GoblinAnimation.G_STATE.S_ATT1;
                        }
                    }
                    else
                    {
                        //agent.destination = target.transform.position;
                        if (agent.velocity.magnitude > 0.0f && GoblinAni.NowState != GoblinAnimation.G_STATE.S_RUN)
                        {
                            GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
                        }
                    }
                }
                else
                {
                    if (Ray.collider.tag != "Player")
                    {
                        if (GoblinAni.Goblin["attack1"].normalizedTime >= 0.95f)
                        {
                            GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
                        }
                    }
                }
            }
            else
            {
                if (GoblinAni.Goblin["attack1"].normalizedTime >= 0.95f)
                {
                    GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
                }
                if (GoblinAni.NowState != GoblinAnimation.G_STATE.S_ATT1)
                {
                    //agent.destination = target.transform.position;
                    if (GoblinAni.NowState != GoblinAnimation.G_STATE.S_RUN)
                    {
                        GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
                    }
                }
            }
            if (GoblinAni.NowState == GoblinAnimation.G_STATE.S_RUN)
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
                        GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
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
                        GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
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
                    (1 << LayerMask.NameToLayer("PatrollPoint")));        //layerMask = ~layerMask;
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
                GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
            }
        }
        if (PatrollPt.ActPatroll)
        {
            target = PatrollPt.PatrollPoint;
            agent.destination = target.transform.position;
            GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
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
