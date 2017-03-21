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
    public List<GameObject> ViewMonster = new List<GameObject>();
    private MoveNPC _move;

    public float RayDistance = 25.0f;

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
            if (Monster.Count > 0)
            {
                for (int i = 0; i < Monster.Count; i++)
                {
                    if (Monster[i].GetComponent<MonsterInformation>().isDie)
                    {
                        Monster.Remove(Monster[i]);
                    }
                }
            }
            if(ViewMonster.Count >0)
            {
                for(int j=0; j<ViewMonster.Count; j++)
                {
                    if (ViewMonster[j].GetComponent<MonsterInformation>().isDie)
                    {
                        ViewMonster.Remove(ViewMonster[j]);
                    }
                }
            }
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
                    int Check_Count = 0;
                    for(int i=0; i<Monster.Count; i++)
                    {
                        if (Monster[i] == other.gameObject)
                            Check_Count++;
                    }
                    if(Check_Count <=0)
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
            //if (!FollowMonster)
            //{
            //    FollowMonster = true;
            //}
            if (other.CompareTag("Monster"))
            {
                if (Monster.Count > 0)
                {
                    for (int i = 0; i < Monster.Count; i++)
                    {
                        RayCast(Monster[i]);
                        if (Ray.collider != null)
                        {
                            if(_move.NowState == MoveNPC.PlayerState.Follow)
                            {
                                _move.NowState = MoveNPC.PlayerState.Detect;
                            }

                            int Check_count = 0;
                            for (int j = 0; j < ViewMonster.Count; j++)
                            {
                                if (ViewMonster[j] == Monster[i])
                                {
                                    Check_count++;
                                }
                            }
                            if (Check_count <= 0)
                            {
                                ViewMonster.Add(Monster[i]);
                                UpdateMoveNpcMonster();
                            }
                            if (!FollowMonster)
                            {
                                UpdateMoveNpcMonster();
                                FollowMonster = true;
                            }
                            break;
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
            for (int i = 0; i < Monster.Count; i++)
            {
                if (other.gameObject == Monster[i])
                {
                    Monster[i] = null;
                    Monster.Remove(Monster[i]);
                }

               
            }
            for(int j=0; j<ViewMonster.Count; j++)
            {
                if (other.gameObject == ViewMonster[j])
                {
                    ViewMonster[j] = null;
                    ViewMonster.Remove(ViewMonster[j]);
                }
            }
            
        }
    }

    private void RayCast(GameObject Target)
    {
        Vector3 ObjPos = transform.position;
        Vector3 ObjForward = Target.transform.position - transform.position;
        ObjPos.y += 2.5f;
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Player")) |
            (1 << LayerMask.NameToLayer("PatrollPoint")) |
            (1 << LayerMask.NameToLayer("NPC")) |
            (1 << LayerMask.NameToLayer("Default")) |
            (1 << LayerMask.NameToLayer("Map")));          //layerMask = ~layerMask;
        Physics.Raycast(ObjPos, ObjForward, out Ray, RayDistance, layerMask);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < Monster.Count; i++)
        {
            if (Monster[i])
            {
                Vector3 ObjPos = transform.position;
                Vector3 ObjForward = Monster[i].transform.position - transform.position;
                ObjPos.y += 2.5f;

                if (this.Ray.collider != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(this.Ray.point, 1.0f);

                    Gizmos.color = Color.black;
                    Gizmos.DrawLine(ObjPos,
                       ObjPos + ObjForward * RayDistance);
                }
                else
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(ObjPos,
                         ObjPos + ObjForward * RayDistance);
                }
            }
        }
    }

    IEnumerator CheckNearest()
    {
        yield return new WaitForSeconds(1.0f);
        if (isMonster)
        {
            if (_move)
            {
                if(!_move.NearestMonster)
                    UpdateMoveNpcMonster();
                if (NearestMonster != _move.NearestMonster)
                {
                    UpdateMoveNpcMonster();
                }
            }
        }
        CheckNearMonster();
    }

    private void CheckNearMonster()
    {
        if(isMonster)
        {
            if(ViewMonster.Count>0)
            {
                NearestMonster = ViewMonster[0];
                for(int i=0; i< ViewMonster.Count; i++)
                {
                    float Now = Vector3.Distance(NearestMonster.transform.position,
                                                 this.transform.position);
                    float Next = Vector3.Distance(ViewMonster[i].transform.position,
                                                 this.transform.position);
                    if(Now>Next)
                    {
                        NearestMonster = ViewMonster[i];
                        UpdateMoveNpcMonster();
                    }
                }
                if (!FollowMonster)
                    FollowMonster = true;
            }
            else
            {
                FollowMonster = false;
                NearestMonster = null;
                //isMonster = false;
            }
        }
    }

    private void UpdateMoveNpcMonster()
    {
        if(isMonster)
        {
            if(_move)
            {
                _move.TargetMonster = ViewMonster;
                //_move.TargetMonster = ViewMonster;
                _move.NearestMonster = NearestMonster;
            }
        }
    }
}
