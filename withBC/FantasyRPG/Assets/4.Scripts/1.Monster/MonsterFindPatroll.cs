using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFindPatroll : MonoBehaviour {

    public MakePatroll Point = null;
    public bool findPlayer = false;
    public bool ActPatroll = false;
    public GameObject PatrollPoint = null;

    //0은 땅 1은 하늘
    public int Rand_Fly = 0;

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
                    this.Rand_Fly = Point.Rand_Fly;
                    ActPatroll = true;
                }
            }
        }
	}
}
