using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPatroll : MonoBehaviour {

    public MoveNPC.PlayerJob PointJob;

    public MoveNPC NpcThis = null;

    public GameObject TargetNpc = null;


	// Use this for initialization
	void Start () {
        //NpcThis = GetComponent<MoveNPC>();
	}
	
	// Update is called once per frame
	void Update () {

		if(NpcThis)
        {
            if(!NpcThis.isMonster)
            {
                NpcThis.TargetPoint = this.gameObject;
            }
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MoveNPC>().Job == PointJob)
        {
            NpcThis = other.GetComponent<MoveNPC>();
        }
    }
}
