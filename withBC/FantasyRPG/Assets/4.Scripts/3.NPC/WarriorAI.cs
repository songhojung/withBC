using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAI : MonoBehaviour
{

    private WarriorAnimationCtrl warriorAniCtrl;
    public float RayDistance = 5.0f;
    private RaycastHit my_ray;
    private MoveNPC Npc_Move;
    //private DetectMonster _detectMonster;
    //private MoveNPC _move;

    // Use this for initialization
    void Start()
    {
        warriorAniCtrl = GetComponent<WarriorAnimationCtrl>();
        Npc_Move = GetComponent<MoveNPC>();
        //_detectMonster = GetComponent<DetectMonster>();
        //_move = GetComponent<MoveNPC>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Npc_Move)
        {
            if (Npc_Move.isMonster)
            {
                Raycast();
                if (my_ray.collider != null)
                {
                    warriorAniCtrl.IsLeftMouseDown = true;
                    if (Npc_Move.NowState != MoveNPC.PlayerState.Attack)
                        Npc_Move.NowState = MoveNPC.PlayerState.Attack;
                }
                else if(Npc_Move.NowState != MoveNPC.PlayerState.Attack)
                {
                    Npc_Move.NowState = MoveNPC.PlayerState.Detect;
                    warriorAniCtrl.IsLeftMouseDown = false;
                }
            }
            else
            {
                if (Npc_Move.NowState != MoveNPC.PlayerState.Follow)
                    Npc_Move.NowState = MoveNPC.PlayerState.Follow;
                warriorAniCtrl.IsLeftMouseDown = false;
            }
        }
    }

    private void Raycast()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 2.0f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Player")) |
            (1 << LayerMask.NameToLayer("PatrollPoint")) |
            (1 << LayerMask.NameToLayer("NPC")) |
            (1 << LayerMask.NameToLayer("Default")) |
            (1 << LayerMask.NameToLayer("Map")));          //layerMask = ~layerMask;
        Physics.Raycast(ObjPos, ObjForward, out my_ray, RayDistance, layerMask);
    }
    private void OnDrawGizmos()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = transform.forward;
        ObjPos.y += 2.0f;

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
