using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrollMove : MonoBehaviour {
    public MonsterFindPatroll MonsterPt = null;
    public MakePatroll MyPoint;
	// Use this for initialization
	void Start () {
        MyPoint = GetComponent<MakePatroll>();
    }
	
	// Update is called once per frame
	void Update () {

	}
}
