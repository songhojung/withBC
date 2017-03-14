using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPatroll : MonoBehaviour {

    public MoveNPC.PlayerJob PointJob;

    public MoveNPC NpcThis = null;

    public GameObject TargetNpc = null;

    private Vector3 Me;

    public enum FamilyCheck
    {
        PARENTS, CHILD
    };

    public FamilyCheck family = FamilyCheck.PARENTS;

    public NpcPatroll family_Point = null;

    // Use this for initialization
    void Start () {
        //NpcThis.TargetPoint = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        switch (family)
        {
            case FamilyCheck.PARENTS:
                Parent();
                break;
            case FamilyCheck.CHILD:
                Child();
                break;
        }
    }

    private void Parent()
    {
        if (NpcThis)
        {
            if(family_Point)
            {
                float DistanceMe = Vector3.Distance(this.gameObject.transform.position,
                                                NpcThis.gameObject.transform.position);
                float DistanceYou = Vector3.Distance(family_Point.gameObject.transform.position,
                                                    NpcThis.gameObject.transform.position);
                if (DistanceMe < DistanceYou)
                {
                    StartCoroutine(CheckPoint());
                    if (Vector3.Distance(Me, this.gameObject.transform.position) >= 10.0f)
                        NpcThis.TargetPoint = this.gameObject;
                }
                else
                {
                    //StartCoroutine(CheckPoint());
                    //if (Vector3.Distance(Me, this.gameObject.transform.position) >= 10.0f)
                        NpcThis.TargetPoint = family_Point.gameObject;
                }
            }
            else
            {
                StartCoroutine(CheckPoint());
                if (Vector3.Distance(Me, this.gameObject.transform.position) >= 10.0f)
                    NpcThis.TargetPoint = this.gameObject;
            }
        }
    }

    private void Child()
    {
        if(NpcThis)
        {
            float DistanceMe = Vector3.Distance(this.gameObject.transform.position,
                                                NpcThis.gameObject.transform.position);
            float DistanceYou = Vector3.Distance(family_Point.gameObject.transform.position,
                                                NpcThis.gameObject.transform.position);
            if (DistanceMe < DistanceYou)
            {
                StartCoroutine(CheckPoint());
                if (Vector3.Distance(Me, this.gameObject.transform.position) >= 10.0f)
                    NpcThis.TargetPoint = this.gameObject;
            }
            else
            {
                //StartCoroutine(CheckPoint());
                //if (Vector3.Distance(Me, this.gameObject.transform.position) >= 10.0f)
                NpcThis.TargetPoint = family_Point.gameObject;
            }
        }
        else
        {
            NpcThis = family_Point.NpcThis;
        }
    }

    IEnumerator CheckPoint()
    {
        yield return new WaitForSeconds(0.5f);
        Me = this.gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MoveNPC>())
        {
            if (other.GetComponent<MoveNPC>().Job == PointJob)
            {
                NpcThis = other.GetComponent<MoveNPC>();
            }
        }
    }
}
