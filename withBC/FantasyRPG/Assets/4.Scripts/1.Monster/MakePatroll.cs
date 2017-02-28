using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePatroll : MonoBehaviour {

    public MakePatroll Parent = null;
    public MakePatroll Child = null;
    public SphereCollider find_monster;
    // Use this for initialization

    //0은 땅 1은 하늘
    public int Rand_Fly = 0;

    void Start () {
        find_monster = GetComponent<SphereCollider>();
        if(Parent)
        {
            ParentsChildPoint();
        }
        if(Child)
        {
            MakeChildPoint();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(find_monster)
        {
            
        }
    
	}

    void MakeChildPoint()
    { 
        if (Child)
        {
            Child.Parent = this;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            MonsterFindPatroll PatrollPt = other.gameObject.GetComponentInParent<MonsterFindPatroll>();
            if (PatrollPt)
            {
                if (!PatrollPt.ActPatroll)
                {
                    if (!PatrollPt.findPlayer)
                    {
                        PatrollPt.Point = this;
                    }
                }
            }
        }
    }

    void MakeParentPoint(MakePatroll Pt)
    {
        Parent = Pt;
        Parent.Child = this;
    }

    void ParentsChildPoint()
    {
        if(Parent)
        {
            Parent.Child = this;
        }
    }

    private void OnDrawGizmos()
    {
        if(Child)
        {
            Vector3 ObjPos = this.transform.position;
            Vector3 ObjForward = Child.transform.position;
            ObjPos.y += 1.0f;

           Gizmos.color = Color.red;
           Gizmos.DrawWireSphere(this.Child.transform.position, 1.0f);

           Gizmos.color = Color.black;
           Gizmos.DrawLine(ObjPos,
              ObjForward);
            //else
            //{
            //    Gizmos.DrawLine(ObjPos,
            //         ObjPos + this.transform.forward * RayDistance);
            //}
        }
    }
}
