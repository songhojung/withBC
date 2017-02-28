using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFindPatroll : MonoBehaviour {
    public MakePatroll Point = null;
    public bool findPlayer = false;
    public bool ActPatroll = false;
    public GameObject PatrollPoint = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!findPlayer)
        {
            if (!ActPatroll)
            {
                if (Point)
                {
                    PatrollPoint = Point.gameObject;
                    ActPatroll = true;
                }
            }
        }
	}
}
