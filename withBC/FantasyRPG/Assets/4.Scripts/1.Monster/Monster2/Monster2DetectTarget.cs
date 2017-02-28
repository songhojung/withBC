using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Monster2DetectTarget : MonoBehaviour {

    public GameObject target;
    private Monster2Animation M2Animation;

    private Monster2Move Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;
    private float RayDistance = 14.0f;
    private bool isDie = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Move = GetComponent<Monster2Move>();
        M2Animation = GetComponent<Monster2Animation>();
        //StartCoroutine(Checkeverything());
    }

    //IEnumerator Checkeverything()
    //{
    //    while (!isDie)
    //    {
    //        RayCast();
    //        if (Ray.collider != null && M2Animation.NowState != Monster2Animation.M2_STATE.M2_ATTACK20)
    //        {
    //            if (Ray.collider.tag == "Player")
    //            {
    //                if (M2Animation.NowState == Monster2Animation.M2_STATE.M2_WALK || M2Animation.NowState == Monster2Animation.M2_STATE.M2_RUN)
    //                {
    //                    M2Animation.NowState = Monster2Animation.M2_STATE.M2_ATTACK20;
    //                }
    //            }
    //            else
    //            {
    //                agent.destination = target.transform.position;
    //                if (agent.velocity.magnitude > 0.0f && M2Animation.NowState != Monster2Animation.M2_STATE.M2_RUN)
    //                {
    //                    M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (!M2Animation.Werewolf.IsPlaying("Roar"))
    //            {
    //                agent.destination = target.transform.position;
    //                if (agent.velocity.magnitude > 0.0f && M2Animation.NowState != Monster2Animation.M2_STATE.M2_RUN)
    //                {
    //                    M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
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
        RayCast();
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
                        agent.destination = target.transform.position;
                        if (agent.velocity.magnitude > 0.0f && M2Animation.NowState != Monster2Animation.M2_STATE.M2_RUN)
                        {
                            M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                        }
                    }
                }
                else
                {
                    if (M2Animation.Mt2.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        agent.destination = target.transform.position;
                        M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                    }
                }
            }
            else
            {
                if (M2Animation.NowState != Monster2Animation.M2_STATE.M2_ATTACK)
                {
                    //agent.destination = target.transform.position;
                    M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                }
                if (M2Animation.NowState != Monster2Animation.M2_STATE.M2_ATTACK)
                {
                    //agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f && M2Animation.NowState != Monster2Animation.M2_STATE.M2_RUN)
                    {
                        M2Animation.NowState = Monster2Animation.M2_STATE.M2_RUN;
                    }
                }
            }


        }
        else
        {
            if (Ray.collider != null)
            {
                if (Ray.collider.tag == "Player")
                {
                    target = Ray.collider.gameObject;
                    agent.destination = target.transform.position;
                }
            }
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
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Monster"));
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
