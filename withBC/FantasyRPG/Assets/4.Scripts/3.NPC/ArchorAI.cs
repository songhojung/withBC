using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchorAI : MonoBehaviour
{

    private ArcherAnimationCtrl ArchorAniCtrl;
    public float RayDistance = 5.0f;
    private RaycastHit my_ray;
    private MoveNPC Npc_Move;
    private CharacterInformation Information;

    // Use this for initialization
    void Start ()
    {
        ArchorAniCtrl = GetComponent<ArcherAnimationCtrl>();
        Npc_Move = GetComponent<MoveNPC>();
        Information = GetComponent<CharacterInformation>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Npc_Move)
        {
            if (Npc_Move.isMonster)
            {
                if (!Information.isHit)
                {
                    Raycast();
                    if (my_ray.collider != null)
                    {

                        if (Npc_Move.NowState != MoveNPC.PlayerState.Attack)
                            Npc_Move.NowState = MoveNPC.PlayerState.Attack;
                        else
                        {
                            if (ArchorAniCtrl.archerState == ArcherAnimationCtrl.ArcherState.AIM)
                            {
                                if (!ArchorAniCtrl.IsReadyForShoot)
                                {
                                    //Debug.Log("들어옴");
                                    ArchorAniCtrl.IsLeftMouseDown = false;
                                    //ArchorAniCtrl.IsLeftMouseStay = false;
                                }
                                else
                                {
                                    ArchorAniCtrl.IsLeftMouseUp = true;
                                    ArchorAniCtrl.IsLeftMouseStay = false;
                                    ArchorAniCtrl.IsLeftMouseDown = false;
                                }
                            }
                            else
                            {
                                if (ArchorAniCtrl.archerState != ArcherAnimationCtrl.ArcherState.BOWSHOOT)
                                {
                                    if (!ArchorAniCtrl.IsLeftMouseUp)
                                    {
                                        ArchorAniCtrl.IsLeftMouseDown = true;
                                        ArchorAniCtrl.IsLeftMouseStay = true;
                                        ArchorAniCtrl.IsLeftMouseUp = false;
                                    }
                                }
                                //Debug.Log("여기먼저");
                            }
                        }
                    }
                    else
                    {
                        if (ArchorAniCtrl.archerState != ArcherAnimationCtrl.ArcherState.AIM)
                        {
                            if (Npc_Move.NowState != MoveNPC.PlayerState.Follow &&
                                Npc_Move.NowState != MoveNPC.PlayerState.Detect)
                            {
                                Npc_Move.NowState = MoveNPC.PlayerState.Detect;
                            }
                        }
                        else
                        {
                            if (!Npc_Move.isMonster)
                            {
                                if (Npc_Move.NowState != MoveNPC.PlayerState.Follow &&
                                Npc_Move.NowState != MoveNPC.PlayerState.Detect)
                                {
                                    Npc_Move.NowState = MoveNPC.PlayerState.Detect;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (Npc_Move.NowState != MoveNPC.PlayerState.Follow)
                {
                    Npc_Move.NowState = MoveNPC.PlayerState.Follow;
                    ArchorAniCtrl.IsLeftMouseDown = false;
                    ArchorAniCtrl.IsLeftMouseStay = false;
                    ArchorAniCtrl.IsLeftMouseUp = false;
                    ArchorAniCtrl.archerState = ArcherAnimationCtrl.ArcherState.RUN;
                }
            }
        }
    }

    private void Raycast()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 3.0f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Player")) |
            (1 << LayerMask.NameToLayer("PatrollPoint")) |
            (1 << LayerMask.NameToLayer("NPC")) |
            (1 << LayerMask.NameToLayer("Default")) |
            (1 << LayerMask.NameToLayer("Map")));        //layerMask = ~layerMask;
        Physics.Raycast(ObjPos, ObjForward, out my_ray, RayDistance, layerMask);
    }
    private void OnDrawGizmos()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 3.0f;

        if (this.my_ray.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.my_ray.point, 1.0f);

            Gizmos.color = Color.black;
            Gizmos.DrawLine(ObjPos,
               ObjPos + ObjForward * RayDistance);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(ObjPos,
                 ObjPos + ObjForward * RayDistance);
        }
    }
    private void RemoveTo()
    {
        if (Npc_Move.NowState == MoveNPC.PlayerState.Attack)
            Npc_Move.NowState = MoveNPC.PlayerState.Detect;
    }
}
