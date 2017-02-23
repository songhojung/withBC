using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WerewolfDetectTarget : MonoBehaviour {

    public GameObject target;
    private WerewolfeAnimation WolfAnimation;

    private WerewolfeMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;
    private float RayDistance = 14.0f;
    private bool isDie = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Move = GetComponent<WerewolfeMove>();
        WolfAnimation = GetComponent<WerewolfeAnimation>();
        //StartCoroutine(Checkeverything());
    }

    //IEnumerator Checkeverything()
    //{
    //    while (!isDie)
    //    {
    //        RayCast();
    //        if (Ray.collider != null && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_ATTACK20)
    //        {
    //            if (Ray.collider.tag == "Player")
    //            {
    //                if (WolfAnimation.NowState == WerewolfeAnimation.W_STATE.S_WALK || WolfAnimation.NowState == WerewolfeAnimation.W_STATE.S_RUN)
    //                {
    //                    WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_ATTACK20;
    //                }
    //            }
    //            else
    //            {
    //                agent.destination = target.transform.position;
    //                if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
    //                {
    //                    WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (!WolfAnimation.Werewolf.IsPlaying("Roar"))
    //            {
    //                agent.destination = target.transform.position;
    //                if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
    //                {
    //                    WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
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
            if (Ray.collider != null && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_ATT20)
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
                    agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
                    {
                        WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                    }
                }
            }
            else
            {
                if (WolfAnimation.Werewolf["Attack20"].normalizedTime >= 0.9f)
                {
                    agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
                    {
                        WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                    }
                }
                if (WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_ATT20)
                {
                    agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
                    {
                        WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                    }
                }
                //if (!WolfAnimation.Werewolf.IsPlaying("Roar"))
                //{
                //    agent.destination = target.transform.position;
                //    if (agent.velocity.magnitude > 0.0f && WolfAnimation.NowState != WerewolfeAnimation.W_STATE.S_RUN)
                //    {
                //        WolfAnimation.NowState = WerewolfeAnimation.W_STATE.S_RUN;
                //    }
                //}
            }
        }
        else
        {
            if(Ray.collider != null)
            {
                if(Ray.collider.tag == "Player")
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
