using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GoblinDetectTarget : MonoBehaviour {

    public GameObject target;
    private GoblinAnimation GoblinAni;

    private MonsterFindPatroll PatrollPt;

    private GoblinMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    private float RayDistance = 15.0f;
    private bool isDie = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Move = GetComponent<GoblinMove>();
        GoblinAni = GetComponent<GoblinAnimation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
        //StartCoroutine(Checkeverything());
    }

    void Update()
    {
        RayCast();
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
                    if (GoblinAni.Goblin["attack1"].normalizedTime >= 0.95f)
                    {
                        GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
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

            if (Ray.collider != null)
            {
                if (!PatrollPt.findPlayer)
                {
                    if (Ray.collider.tag == "Player")
                    {
                        target = null;
                        target = Ray.collider.gameObject;
                        agent.destination = target.transform.position;
                        PatrollPt.ActPatroll = false;
                        PatrollPt.findPlayer = true;
                        GoblinAni.NowState = GoblinAnimation.G_STATE.S_RUN;
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
    // Update is called once per frame

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
