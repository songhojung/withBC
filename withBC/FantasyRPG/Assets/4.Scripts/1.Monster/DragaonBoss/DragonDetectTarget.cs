using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonDetectTarget : MonoBehaviour {

    public GameObject target;
    private DragonAnimation D_Animation;

    private MonsterFindPatroll PatrollPt;

    private DragonMove Move;
    private NavMeshAgent agent;
    private RaycastHit Ray;

    private float RayDistance = 14.0f;
    private bool isDie = false;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        Move = GetComponent<DragonMove>();
        D_Animation = GetComponent<DragonAnimation>();
        PatrollPt = GetComponent<MonsterFindPatroll>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
