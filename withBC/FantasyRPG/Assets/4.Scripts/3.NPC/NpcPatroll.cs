using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPatroll : MonoBehaviour {

    public MoveNPC.PlayerJob PointJob;

    public MoveNPC NpcThis = null;

    public GameObject TargetNpc = null;


    private Vector3 Me;
	// Use this for initialization
	void Start () {
        //NpcThis.TargetPoint = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		if(NpcThis)
        {
            if(!NpcThis.isMonster)
            {
                StartCoroutine(CheckPoint());
                if(Vector3.Distance(Me,this.gameObject.transform.position)>=10.0f)
                    NpcThis.TargetPoint = this.gameObject;
            }
        }

	}

    IEnumerator CheckPoint()
    {
        yield return new WaitForSeconds(5.0f);
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
