using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonDetectTarget : MonoBehaviour {

    public GameObject target;
    private DragonAnimation D_Animation;

    private MoveForDragon Flyagent;

    private MonsterFindPatroll PatrollPt;

    private DragonMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    private float RayDistance = 14.0f;
    private bool isDie = false;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        Move = GetComponent<DragonMove>();
        D_Animation = GetComponent<DragonAnimation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
        Flyagent = GetComponent<MoveForDragon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            RayCast();
            switch (D_Animation.NowFlyRand)
            {
                case DragonAnimation.D_FLYRAND.NOWRAND:
                    if(!agent.enabled)
                        agent.enabled = true;
                    if (Flyagent.OnOff)
                        Flyagent.OnOff = false;
                    RandDetect();
                    break;
                case DragonAnimation.D_FLYRAND.NOWFLY:
                    if(agent.enabled)
                        agent.enabled = false;
                    if (!Flyagent.OnOff)
                        Flyagent.OnOff = true;
                    FlyDetect();
                    break;
            }
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
                    if (D_Animation.NowAttack != DragonAnimation.D_ATTACKSTATE.ATTACK)
                    {
                        D_Animation.NowState = DragonAnimation.D_STATE.D_ATT1;
                    }
                }
                else
                {
                    if (D_Animation.Dragon["breath fire"].normalizedTime >= 0.95f)
                    {
                        D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
                    }
                }
            }
            else
            {
                //if (!D_Animation.Dragon.IsPlaying("breath fire"))
                if (D_Animation.Dragon["breath fire"].normalizedTime >= 0.95f)
                {
                    D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
                }
                if (D_Animation.NowState != DragonAnimation.D_STATE.D_ATT1)
                {
                    if (D_Animation.NowState != DragonAnimation.D_STATE.D_RUN)
                    {
                        D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
                    }
                }
            }

            if (D_Animation.NowState == DragonAnimation.D_STATE.D_RUN)
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
                    else
                    {
                        if (target.GetComponent<MakePatroll>().Parent)
                        {
                            target = target.GetComponent<MakePatroll>().Parent.gameObject;
                        }
                    }
                }
                if (target.GetComponent<MakePatroll>().Rand_Fly == 1)
                {
                    D_Animation.NowFlyRand = DragonAnimation.D_FLYRAND.NOWFLY;
                    Flyagent.OnOff = true;
                    Flyagent.target = target;
                }
                else
                {
                    D_Animation.NowFlyRand = DragonAnimation.D_FLYRAND.NOWRAND;
                    Flyagent.OnOff = false;
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
                        D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
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

    private void FlyDetect()
    {
        if (target)
        {
            if (Ray.collider != null)
            {
                if (Ray.collider.tag == "Player")
                {
                    if (D_Animation.NowAttack != DragonAnimation.D_ATTACKSTATE.ATTACK)
                    {
                        D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_ATT;
                        D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.ATTACK;
                    }
                }
                else
                {
                    if (D_Animation.Dragon["fly attack"].normalizedTime >= 0.95f)
                    {
                        D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                        D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.NONATTACK;
                    }
                }
            }
            else
            {
                //if (!D_Animation.Dragon.IsPlaying("breath fire"))
                if (D_Animation.Dragon["fly attack"].normalizedTime >= 0.95f)
                {
                    D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                    D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.NONATTACK;
                }
                if (D_Animation.NowAttack != DragonAnimation.D_ATTACKSTATE.ATTACK)
                {
                    if (D_Animation.NowState != DragonAnimation.D_STATE.D_FLY_FAST)
                    {
                        D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                    }
                }
            }

            if (D_Animation.NowState == DragonAnimation.D_STATE.D_FLY_FAST)
            {
                D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.NONATTACK;
            }


            if (target.gameObject.CompareTag("PatrollPoint"))
            {
                float distance = Vector3.Distance(this.transform.position, target.transform.position);
                if (distance <= 5.0f)
                {
                    if (target.GetComponent<MakePatroll>().Child)
                    {
                        target = target.GetComponent<MakePatroll>().Child.gameObject;
                        Flyagent.target = null;
                        Flyagent.target = target;
                    }
                    else
                    {
                        if (target.GetComponent<MakePatroll>().Parent)
                        {
                            target = target.GetComponent<MakePatroll>().Parent.gameObject;
                            Flyagent.target = null;
                            Flyagent.target = target;
                        }
                    }
                }

                if (target.GetComponent<MakePatroll>().Rand_Fly == 1)
                {
                    D_Animation.NowFlyRand = DragonAnimation.D_FLYRAND.NOWFLY;
                    Flyagent.OnOff = true;
                    Flyagent.target = target;
                }
                else
                {
                    D_Animation.NowFlyRand = DragonAnimation.D_FLYRAND.NOWRAND;
                    Flyagent.OnOff = false;
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
                        //agent.destination = target.transform.position;
                        PatrollPt.ActPatroll = false;
                        PatrollPt.findPlayer = true;
                        D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                        D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.NONATTACK;
                    }
                }
            }
            //Move.transform.LookAt(target.transform.position - transform.position);
            //transform.LookAt(target.transform.position - transform.position);
        }
        else
        {
            findAndMove();
            //Move.transform.LookAt(transform.position + transform.forward);
            //transform.LookAt(transform.position + transform.forward);
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
        switch(PatrollPt.Rand_Fly)
        {
            //땅
            case 0:
                D_Animation.NowFlyRand = DragonAnimation.D_FLYRAND.NOWRAND;
                break;

            //하늘
            case 1:
                D_Animation.NowFlyRand = DragonAnimation.D_FLYRAND.NOWFLY;
                break;
        }
        switch(D_Animation.NowFlyRand)
        {
            case DragonAnimation.D_FLYRAND.NOWRAND:
                if (Ray.collider != null)
                {
                    if (Ray.collider.tag == "Player")
                    {
                        target = Ray.collider.gameObject;
                        agent.destination = target.transform.position;
                        PatrollPt.ActPatroll = false;
                        PatrollPt.findPlayer = true;
                        D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
                        D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.NONATTACK;
                    }
                }
                if (PatrollPt.ActPatroll)
                {
                    target = PatrollPt.PatrollPoint;
                    agent.destination = target.transform.position;
                    D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
                    D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.NONATTACK;
                }
                break;
            case DragonAnimation.D_FLYRAND.NOWFLY:
                if (PatrollPt.ActPatroll)
                {
                    target = PatrollPt.PatrollPoint;
                    Flyagent.target = target;
                    D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                    D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.NONATTACK;
                }
                break;
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
