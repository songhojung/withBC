using System.Collections;
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

    public float RayDistance = 5.0f;
    private bool isDie = false;

    private GameObject PrefMove = null;

    private MonsterDetectCollider DetectColl;

    private MonsterInformation Information;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       // Move = GetComponent<Monster3Move>();
        M3Animation = GetComponent<Monster3Animation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
        DetectColl = GetComponent<MonsterDetectCollider>();

        Information = GetComponent<MonsterInformation>();
        //StartCoroutine(Checkeverything());
    }

    void Update()
    {
        if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Die"))
        {
            if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                MonsterParentsCollider CheckPt = GetComponent<MonsterParentsCollider>();
                CheckPt.isDie = true;
                Information.isDie = true;
            }
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
        }
        else
        {
            StartCoroutine(YouDie());
        }

        if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.GetHit"))
        {
            if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                MonsterParentsCollider CheckPt = GetComponent<MonsterParentsCollider>();
                CheckPt.isHit = false;
                M3Animation.NowState = Monster3Animation.M3_STATE.M3_STAY;
            }
        }
                //Move.Wolf.transform.LookAt(transform.position + transform.forward);

    }

    IEnumerator YouDie()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    private void RandDetect()
    {
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
                        //if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
                        //{
                            //agent.destination = target.transform.position;
                            //if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                                M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
                        //}

                    }
                }
            }
            else
            {
                if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
                {
                    //agent.destination = target.transform.position;
                    if(M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
                        M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
                }
                if (M3Animation.NowState != Monster3Animation.M3_STATE.M3_ATTACK)
                {
                    //agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f && M3Animation.NowState != Monster3Animation.M3_STATE.M3_RUN)
                    {
                        M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
                    }
                }
            }

            if (M3Animation.Mt3.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run"))
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
                        M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
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
                        M3Animation.NowState = Monster3Animation.M3_STATE.M3_RUN;
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
    }
    private void RayCast()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 3.0f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Monster")) |
            (1 << LayerMask.NameToLayer("PatrollPoint")) |
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
        ObjPos.y += 3.0f;

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
