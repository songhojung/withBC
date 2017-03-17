using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMonster : MonoBehaviour {

    //몬스터 찾았는지 못찾았는지
    public bool isMonster = false;

    public bool FollowMonster = false;

    //public GameObject[] Monster = null;
    //public GameObject[] Monster;
    public List<GameObject> Monster = new List<GameObject>();
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
            if(Monster.Count>0)
            {
                isMonster = true;
            }
        }
        else
        {
            if (Monster.Count <= 0)
            {
                isMonster = false;
                FollowMonster = false;
            }
            StartCoroutine(CheckNearest());
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!isMonster)
        {
            if (other.CompareTag("Monster"))
            {
                
                isMonster = true;
                Monster.Add(other.gameObject);
                //_move.TargetMonster.Add(other.gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Monster"))
            {
                if (Monster.Count <= 0)
                {
                    Monster.Add(other.gameObject);
                    //_move.TargetMonster.Add(other.gameObject);
                }
                else
                {
                    Monster.Add(other.gameObject);
                    //_move.TargetMonster.Add(other.gameObject);
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
                    if(Monster.Count>0)
                    {
                        FollowMonster = true;
                    }
                    //for (int i = 0; i < Monster.Count; i++)
                    //{
                    //    if (other == Monster[i])
                    //    {
                    //        //RayCast();
                    //        //if (Ray.collider != null)
                    //        {
                    //            //if (Ray.collider.tag == "Monster")
                    //            {
                    //                //if (NearestMonster)
                    //                {//Monster = Ray.collider.gameObject;
                    //                    FollowMonster = true;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isMonster)
        {
            for (int i = 0; i < Monster.Count; i++)
            {
                if (other.gameObject == Monster[i])
                {
                    Monster[i] = null;
                    Monster.Remove(Monster[i]);
                }
            }
            
        }
    }

    private void RayCast()
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = NearestMonster.transform.position;
        ObjPos.y += 1.0f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Player")) |
                    (1 << LayerMask.NameToLayer("PatrollPoint")) |
                    (1 << LayerMask.NameToLayer("NPC")) |
                    (1 << LayerMask.NameToLayer("Default")));        //layerMask = ~layerMask;
        Physics.Raycast(ObjPos, ObjForward, out Ray, RayDistance, layerMask);
    }

    private void OnDrawGizmos()
    {
        if (NearestMonster)
        {
            Vector3 ObjPos = transform.position;
            Vector3 ObjForward = NearestMonster.transform.position;
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
                Gizmos.color = Color.black;
                Gizmos.DrawLine(ObjPos,
                     ObjPos + this.transform.forward * RayDistance);
            }
        }
    }

    IEnumerator CheckNearest()
    {
        yield return new WaitForSeconds(3.0f);
        CheckNearMonster();
        UpdateMoveNpcMonster();
    }

    private void CheckNearMonster()
    {
        if(isMonster)
        {
            if(Monster.Count>0)
            {
                NearestMonster = Monster[0];
                for(int i=0; i<Monster.Count; i++)
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
            else
            {
                FollowMonster = false;
                NearestMonster = null;
                isMonster = false;
            }
        }
    }

    private void UpdateMoveNpcMonster()
    {
        if(isMonster)
        {
            if(_move)
            {
                _move.TargetMonster = Monster;
                _move.NearestMonster = NearestMonster;
            }
        }
    }
}
