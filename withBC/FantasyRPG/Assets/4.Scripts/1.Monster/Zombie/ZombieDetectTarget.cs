using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDetectTarget : MonoBehaviour {

    public GameObject target;
    private ZombieAnimation ZombieAnimation;

    private ZombieMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;
    private float RayDistance = 14.0f;
    private bool isDie = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Move = GetComponent<ZombieMove>();
        ZombieAnimation = GetComponent<ZombieAnimation>();
        //StartCoroutine(Checkeverything());
    }

    //IEnumerator Checkeverything()
    //{
    //    while (!isDie)
    //    {
    //        RayCast();
    //        if (Ray.collider != null && ZombieAnimation.NowState != ZombieAnimation.Z_STATE.Z_ATTACK20)
    //        {
    //            if (Ray.collider.tag == "Player")
    //            {
    //                if (ZombieAnimation.NowState == ZombieAnimation.Z_STATE.Z_WALK || ZombieAnimation.NowState == ZombieAnimation.Z_STATE.Z_RUN)
    //                {
    //                    ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_ATTACK20;
    //                }
    //            }
    //            else
    //            {
    //                agent.destination = target.transform.position;
    //                if (agent.velocity.magnitude > 0.0f && ZombieAnimation.NowState != ZombieAnimation.Z_STATE.Z_RUN)
    //                {
    //                    ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_RUN;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (!ZombieAnimation.WereZombie.IsPlaying("Roar"))
    //            {
    //                agent.destination = target.transform.position;
    //                if (agent.velocity.magnitude > 0.0f && ZombieAnimation.NowState != ZombieAnimation.Z_STATE.Z_RUN)
    //                {
    //                    ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_RUN;
    //                }
    //            }
    //        }
    //        //Move.Zombie.transform.LookAt(transform.position + transform.forward);
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
                if (Ray.collider.tag == "Player")
                {
                    if (ZombieAnimation.NowState == ZombieAnimation.Z_STATE.Z_WALK)
                    {
                        ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_ATTACK;
                    }
                }
                else
                {
                    agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f && ZombieAnimation.NowState != ZombieAnimation.Z_STATE.Z_WALK)
                    {
                        ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
                    }
                }
            }
            else
            {
                if (ZombieAnimation.Zombie.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f)
                    {
                        ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
                    }
                }
                if (ZombieAnimation.NowState != ZombieAnimation.Z_STATE.Z_WALK)
                {
                    agent.destination = target.transform.position;
                    if (agent.velocity.magnitude > 0.0f)
                    {
                        ZombieAnimation.NowState = ZombieAnimation.Z_STATE.Z_WALK;
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
        //Move.Zombie.transform.LookAt(transform.position + transform.forward);
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
