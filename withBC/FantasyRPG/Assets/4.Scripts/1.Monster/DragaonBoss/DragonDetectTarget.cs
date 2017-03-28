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

    public float RayDistance = 14.0f;
    private bool isDie = false;

    private GameObject PrefMove = null;

    private MonsterDetectCollider DetectColl;

    private MonsterInformation Information;

    private float CheckFireTime = 0.0f;

    public GameObject DragonFlame;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        Move = GetComponent<DragonMove>();
        D_Animation = GetComponent<DragonAnimation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
        Flyagent = GetComponent<MoveForDragon>();

        DetectColl = GetComponent<MonsterDetectCollider>();

        Information = GetComponent<MonsterInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(D_Animation.Dragon["death"].normalizedTime >= 0.95f)
        {
            MonsterParentsCollider CheckPt = GetComponent<MonsterParentsCollider>();
            CheckPt.isDie = true;
            Information.isDie = true;
        }

        isDie = Information.isDie;

        if (!Information.isDie)
        {
            RayCast();
            switch (D_Animation.NowFlyRand)
            {
                case DragonAnimation.D_FLYRAND.NOWRAND:
                    if (!agent.enabled)
                        agent.enabled = true;
                    if (Flyagent.OnOff)
                        Flyagent.OnOff = false;
                    RandDetect();
                    break;
                case DragonAnimation.D_FLYRAND.NOWFLY:
                    if (agent.enabled)
                        agent.enabled = false;
                    if (!Flyagent.OnOff)
                        Flyagent.OnOff = true;
                    //FlyDetect();
                    FlyAttackEvent();
                    break;
            }
        }
        else
        {
            StartCoroutine(YouDie());
        }
        if((D_Animation.Dragon["hit1"].normalizedTime >=0.8f)||
            (D_Animation.Dragon["hit2"].normalizedTime >= 0.8f))
        {
            MonsterParentsCollider CheckPt = GetComponent<MonsterParentsCollider>();
            CheckPt.isHit = false;
            D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
        }
        

    }

    IEnumerator YouDie()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame

    private void RandDetect()
    {
        if (agent.velocity.magnitude > 0.0f && (D_Animation.Dragon["breath fire"].normalizedTime >= 0.95f ||
                        D_Animation.Dragon["attack1"].normalizedTime >= 0.95f ||
                        D_Animation.Dragon["attack2"].normalizedTime >= 0.95f))
        {
            D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
            D_Animation.OnceAttackCheck = false;
            D_Animation.OnceAttack = false;

        }
        //Debug.Log(this.name);
        if (target)
        {
            if (Ray.collider != null)
            {
                if (Ray.collider.tag == "Player")
                {
                    
                    CheckFireTime += Time.deltaTime;
                    if (CheckFireTime >= 5.0f)
                    {
                        //if (D_Animation.NowAttack != DragonAnimation.D_ATTACKSTATE.ATTACK)
                        //{
                        if (D_Animation.NowState != DragonAnimation.D_STATE.D_FIRE)
                        {
                            D_Animation.NowState = DragonAnimation.D_STATE.D_FIRE;
                            D_Animation.OnceAttackCheck = true;
                            D_Animation.OnceAttack = true;
                            CheckFireTime = 0.0f;
                        }
                        //}
                    }
                    else if((CheckFireTime >= 0.8f && CheckFireTime <= 1.2f) || (CheckFireTime >= 2.8f && CheckFireTime <= 3.2f))
                    {
                        if (!Information.isHit)
                        {
                            if (D_Animation.NowAttack != DragonAnimation.D_ATTACKSTATE.ATTACK)
                            {
                                if (D_Animation.NowState != DragonAnimation.D_STATE.D_ATT1)
                                {
                                    D_Animation.NowState = DragonAnimation.D_STATE.D_ATT1;
                                    D_Animation.OnceAttackCheck = true;
                                    D_Animation.OnceAttack = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if(D_Animation.NowState != DragonAnimation.D_STATE.D_ATT1 && D_Animation.NowState != DragonAnimation.D_STATE.D_ATT2 &&
                            D_Animation.NowState != DragonAnimation.D_STATE.D_FIRE && D_Animation.NowState != DragonAnimation.D_STATE.D_HIT1 &&
                            D_Animation.NowState != DragonAnimation.D_STATE.D_HIT2)
                        {
                            D_Animation.NowState = DragonAnimation.D_STATE.D_STAY;
                            D_Animation.OnceAttackCheck = false;
                            D_Animation.OnceAttack = false;
                        }
                    }
                }
                else
                {
                    if (agent.velocity.magnitude > 0.0f && (D_Animation.Dragon["breath fire"].normalizedTime >= 0.95f||
                        D_Animation.Dragon["attack1"].normalizedTime >= 0.95f ||
                        D_Animation.Dragon["attack2"].normalizedTime >= 0.95f))
                    {
                        D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
                        D_Animation.OnceAttackCheck = false;
                        D_Animation.OnceAttack = false;
                        
                    }
                }
            }
            else
            {
                //if (!D_Animation.Dragon.IsPlaying("breath fire"))
                if (D_Animation.Dragon["breath fire"].normalizedTime >= 0.95f)
                {
                    D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
                    D_Animation.OnceAttackCheck = false;
                    D_Animation.OnceAttack = false;
                    CheckFireTime = 0.0f;
                }

                if (D_Animation.Dragon["attack1"].normalizedTime >= 0.95f)
                {
                    D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
                    D_Animation.OnceAttackCheck = false;
                    D_Animation.OnceAttack = false;
                    //CheckFireTime = 0.0f;
                }
                if (D_Animation.NowState != DragonAnimation.D_STATE.D_ATT1 &&
                    D_Animation.NowState != DragonAnimation.D_STATE.D_FIRE)
                {
                    if (D_Animation.NowState != DragonAnimation.D_STATE.D_RUN)
                    {
                        D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;
                        D_Animation.OnceAttackCheck = false;
                        D_Animation.OnceAttack = false;
                    }
                }
            }

            if (D_Animation.NowState == DragonAnimation.D_STATE.D_RUN)
            {
                if (agent.enabled)
                {
                    if (target)
                    {
                        //if (Vector3.Distance(agent.destination, target.transform.position) >= 2.0f)
                        //{
                        agent.destination = target.transform.position;
                        //CheckFireTime = 0.0f;
                        //}
                    }
                }
            }


            if (target.gameObject.CompareTag("PatrollPoint"))
            {
                float distance = Vector3.Distance(this.transform.position, target.transform.position);
                if (distance <= 15.0f)
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
                    D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                    D_Animation.NowFlyRand = DragonAnimation.D_FLYRAND.NOWFLY;
                    Flyagent.OnOff = true;
                    Flyagent.target = target;
                }
                else
                {
                    if(D_Animation.NowFlyRand != DragonAnimation.D_FLYRAND.NOWRAND)
                        D_Animation.NowFlyRand = DragonAnimation.D_FLYRAND.NOWRAND;
                    if(!Flyagent.OnOff)
                        Flyagent.OnOff = false;
                }

            }

            if (DetectColl.FindPlayer)
            {
                if ((DetectColl.DetectZone() <= 30) ||
                    (DetectColl.DetectZone() >= 330))
                {
                    if (Vector3.Distance(target.transform.position, this.transform.position) <= 30.0f)
                    {
                        if (PatrollPt.findPlayer &&
                          (D_Animation.NowState == DragonAnimation.D_STATE.D_ATT1 ||
                          D_Animation.NowState == DragonAnimation.D_STATE.D_FIRE))
                        {
                            PrefMove = target;
                            target = null;
                            target = DetectColl.target;
                            agent.enabled = false;
                            //D_Animation.NowState = DragonAnimation.D_STATE.D_ATT1;
                        }

                    }
                }
                if ((DetectColl.DetectZone() <= 45) ||
                    (DetectColl.DetectZone() >= 315))
                {
                    if (!PatrollPt.findPlayer)
                    {
                        PrefMove = target;
                        target = null;
                        target = DetectColl.target;
                        agent.destination = target.transform.position;
                        PatrollPt.ActPatroll = false;
                        PatrollPt.findPlayer = true;
                        D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;                       //}
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
                        D_Animation.NowState = DragonAnimation.D_STATE.D_RUN;                        //}
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
                        PatrollPt.ActPatroll = false;
                        PatrollPt.findPlayer = true;
                        D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                        D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.NONATTACK;
                    }
                }
            }
        }
        else
        {
            findAndMove();
        }

    }

    private void FlyAttackEvent()
    {
        if (target)
        {
            if (D_Animation.Dragon["fly breath fire"].normalizedTime >= 0.95f)
            {
                if(target.gameObject.CompareTag("Player"))
                {
                    D_Animation.NowFlyRand = DragonAnimation.D_FLYRAND.NOWRAND;
                    Flyagent.OnOff = false;
                    D_Animation.OnceAttack = false;
                    D_Animation.OnceAttackCheck = false;
                }
                else
                {
                    D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                    D_Animation.NowAttack = DragonAnimation.D_ATTACKSTATE.NONATTACK;
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
                        if (target.GetComponent<MakePatroll>().Rand_Fly == 3)
                        {
                            //if(target.GetComponent<>)
                            D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FIRE;
                            D_Animation.OnceAttackCheck = true;
                            D_Animation.OnceAttack = true;
                            //D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                        }
                        else if (target.GetComponent<MakePatroll>().Rand_Fly == 1)
                        {
                            D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                        }
                        target = target.GetComponent<MakePatroll>().Child.gameObject;
                        Flyagent.target = null;
                        Flyagent.target = target;
                        
                    }
                    else
                    {
                        if (target.GetComponent<MakePatroll>().Parent)
                        {
                            if (target.GetComponent<MakePatroll>().Rand_Fly == 3)
                            {
                                D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FIRE;
                                D_Animation.OnceAttackCheck = true;
                                D_Animation.OnceAttack = true;
                                //D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                            }
                            else if(target.GetComponent<MakePatroll>().Rand_Fly == 1)
                            {
                                D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FAST;
                            }
                            target = target.GetComponent<MakePatroll>().Parent.gameObject;
                            Flyagent.target = null;
                            Flyagent.target = target;
                            
                        }
                    }
                }

                if (target.GetComponent<MakePatroll>().Rand_Fly == 1 ||
                    target.GetComponent<MakePatroll>().Rand_Fly == 3)
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
            else
            {
                if(target.gameObject.CompareTag("Player"))
                {
                    D_Animation.NowState = DragonAnimation.D_STATE.D_FLY_FIRE;
                    D_Animation.OnceAttackCheck = true;
                    D_Animation.OnceAttack = true;
                }
            }
        }
        else
        {
            findAndMove();
        }
    }

    private void RayCast()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 2.0f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Monster")) |
            (1 << LayerMask.NameToLayer("PatrollPoint")) |
             (1 << LayerMask.NameToLayer("Default")) |
             (1 << LayerMask.NameToLayer("Map")));        //layerMask = ~layerMask;
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

            case 3:
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
