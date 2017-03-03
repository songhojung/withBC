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
                if (distance > MyCollider.radius * 3)
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
                Vector3 LookFor = transform.forward;
                Vector3 v = (target.transform.position - transform.position) - (transform.forward - transform.position);
                Debug.Log(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
                return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                //LookFor.z = transform.position.z + MyCollider.radius;
                //float angle = Vector3.Angle(LookFor - transform.position, target.transform.position - transform.position);
                //return angle;
            }
        }
        return 180;
        //
    }

    private void OnTriggerStay(Collider other)
    {
        if (!FindPlayer)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //MonsterFindPatroll PatrollPt = other.gameObject.GetComponentInParent<MonsterFindPatroll>();
                target = other.gameObject;
                FindPlayer = true;
                FindPatrollNow = false;
            }
        }
    }
}
