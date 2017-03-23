using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetectCollider : MonoBehaviour {

    public GameObject target = null;
    public SphereCollider MyCollider;
    public bool FindPlayer = false;
    public bool FindPatrollNow = false;

    private MonsterFindPatroll MonsterFindPt;
	// Use this for initialization
	void Start () {
        MonsterFindPt = GetComponent<MonsterFindPatroll>();

	}
	
	// Update is called once per frame
	void Update () {
        if(target)
        {
            if (FindPlayer)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance > MyCollider.radius * 2.5f)
                {
                    FindPlayer = false;
                    MonsterFindPt.findPlayer = false;
                    MonsterFindPt.ActPatroll = false;
                    FindPatrollNow = true;
                    target = null;
                }
            }
        }

	}

    public float DetectZone()
    {
        
        if(target)
        {
            if (FindPlayer)
            {
                //Vector3 fwd = transform.forward;// - transform.position;
                //Vector3 targetDir = target.transform.position - transform.position;
                //
                //Vector3 v = fwd - targetDir;
                //float angle = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
                //Debug.Log(angle);
                //return angle;

               ///LookFor.z = transform.position.z + MyCollider.radius;
               ///float angle = Vector3.Angle(LookFor - transform.position, target.transform.position - transform.position);
               ///return angle;

                Vector3 fwd = transform.forward;
                Vector3 targetDir = target.transform.position - transform.position;
                //
                float Angle = Vector3.Angle(fwd, targetDir);
                //
                //
                //return Angle;
                if (AngleDir(fwd, targetDir, Vector3.up) == -1)
                {
                    Angle = 360.0f - Angle;
                    if (Angle > 359.9999f)
                        Angle -= 360.0f;
                    //Debug.Log(Angle);
                    return Angle;
                }
                else
                {
                    //Debug.Log(Angle);
                    return Angle;
                }
                

                //float Dot = Vector3.Dot((transform.position - transform.forward), 
                //                        (transform.position - target.transform.position));
                //float Angle = Mathf.Acos(Dot) * Mathf.Rad2Deg;

                //Vector3 LookFor = transform.forward;
                //Vector3 v = (target.transform.position - transform.position) - (transform.forward - transform.position);
                
            }
        }
        return 180;
        //
    }


    public int AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0)
            return 1;
        else if (dir < 0.0)
            return -1;
        else
            return 0;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!FindPlayer)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!other.GetComponentInParent<CharacterInformation>().isDie)
                {//MonsterFindPatroll PatrollPt = other.gameObject.GetComponentInParent<MonsterFindPatroll>();
                    target = other.gameObject;
                    FindPlayer = true;
                    FindPatrollNow = false;
                }
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!other.GetComponentInParent<CharacterInformation>().isDie)
                {
                    if (target != other.gameObject)
                    {
                        float Now = Vector3.Distance(target.transform.position, this.transform.position);
                        float You = Vector3.Distance(other.transform.position, this.transform.position);
                        if (You <= Now)
                        {
                            target = other.gameObject;
                        }
                    }
                }
            }
        }
    }
}
