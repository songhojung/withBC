using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMonster : MonoBehaviour {

    //몬스터 찾았는지 못찾았는지
    public bool isMonster = false;

    public bool FollowMonster = false;

    public GameObject[] Monster;
    public GameObject NearestMonster;
    private MoveNPC _move;

    public float RayDistance = 15.0f;

    private RaycastHit Ray;

    // Use this for initialization
    void Start () {
        _move = GetComponent<MoveNPC>();
    }
	
	// Update is called once per frame
	void Update () {
		if(!isMonster)
        {
            if(Monster.Length>0)
            {
                isMonster = true;
            }
        }
        else
        {
            if (Monster.Length <= 0)
            {
                isMonster = false;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!isMonster)
        {
            if (other.CompareTag("Monster"))
            {
                if (Monster.Length <= 0)
                {
                    Monster[0] = other.gameObject;
                    isMonster = true;
                    _move.TargetMonster[0] = Monster[0].gameObject;
                }
                else
                {
                    Monster[Monster.Length + 1] = other.gameObject;
                    isMonster = true;
                    _move.TargetMonster[Monster.Length+1] = Monster[0].gameObject;
                }
            }
        }
        else
        {
            if (other.CompareTag("Monster"))
            {
                if (Monster.Length <= 0)
                {
                    Monster[0] = other.gameObject;
                    _move.TargetMonster[0] = Monster[0].gameObject;
                }
                else
                {
                    Monster[Monster.Length + 1] = other.gameObject;
                    _move.TargetMonster[Monster.Length + 1] = Monster[0].gameObject;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(isMonster)
        {
            if (!FollowMonster)
            {
                if (other.CompareTag("Monster"))
                {
                    for (int i = 0; i < Monster.Length; i++)
                    {
                        if (other == Monster[i])
                        {
                            RayCast();
                            if (Ray.collider != null)
                            {
                                if (Ray.collider.tag == "Monster")
                                {
                                    //Monster = Ray.collider.gameObject;
                                    FollowMonster = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isMonster)
        {
            for (int i = 0; i < Monster.Length; i++)
            {
                if (other.gameObject == Monster[i])
                {
                    Monster = null;
                    isMonster = false;
                    _move.TargetMonster = null;
                    FollowMonster = false;
                }
            }
        }
    }

    private void RayCast()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = NearestMonster.transform.position - transform.position;
        ObjPos.y += 10.0f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Player")) |
                    (1 << LayerMask.NameToLayer("PatrollPoint")));        //layerMask = ~layerMask;
        Physics.Raycast(ObjPos, ObjForward, out Ray, RayDistance, layerMask);
    }

    private void CheckNearMonster()
    {
        if(isMonster)
        {
            if(Monster.Length>0)
            {
                NearestMonster = Monster[0];
                for(int i=0; i<Monster.Length; i++)
                {
                    float Now = Vector3.Distance(NearestMonster.transform.position,
                                                 this.transform.position);
                    float Next = Vector3.Distance(Monster[i].transform.position,
                                                 this.transform.position);
                    if(Now>Next)
                    {
                        NearestMonster = Monster[i];
                    }
                }
            }
        }
    }
}
