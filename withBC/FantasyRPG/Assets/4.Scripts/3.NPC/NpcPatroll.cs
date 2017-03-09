using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPatroll : MonoBehaviour {

    public MoveNPC.PlayerJob PointJob;

    private MoveNPC NpcThis;

    public GameObject TargetNpc;


	// Use this for initialization
	void Start () {
        NpcThis = GetComponent<MoveNPC>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!NpcThis.isMonster)
        {
            
        }
	}
}
