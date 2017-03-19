﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour {

    public SpiderAnimation spiderAni;
    public Rigidbody Spider;
    public float WalkSpeed = 1.0f;

    private SpiderDetectTarget DetectTarget;

    private MonsterDetectCollider DetectColl;
    // Use this for initialization
    void Start () {
        Spider = GetComponent<Rigidbody>();
        spiderAni = GetComponent<SpiderAnimation>();

        DetectTarget = GetComponent<SpiderDetectTarget>();
        DetectColl = GetComponent<MonsterDetectCollider>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(spiderAni.NowState == SpiderAnimation.S_STATE.S_WALK || spiderAni.NowState == SpiderAnimation.S_STATE.S_RUN)
        {
            CheckMove();
        }
	}

    void CheckMove()
    {
        switch(spiderAni.NowState)
        {
            case SpiderAnimation.S_STATE.S_WALK:
                if (!(DetectColl.FindPlayer &&
                    Vector3.Distance(DetectTarget.target.transform.position, this.transform.position) <= 5.0f))
                {
                    Vector3 VecSpider = (Spider.transform.forward * WalkSpeed * Time.deltaTime) + Spider.transform.position;
                    Spider.MovePosition(VecSpider);
                }

                //Spider.transform.LookAt(VecSpider);
                break;
            case SpiderAnimation.S_STATE.S_RUN:
                if (!(DetectColl.FindPlayer &&
                    Vector3.Distance(DetectTarget.target.transform.position, this.transform.position) <= 5.0f))
                {
                    Vector3 VecSpider2 = (Spider.transform.forward * WalkSpeed * 2.0f * Time.deltaTime) + Spider.transform.position;
                    Spider.MovePosition(VecSpider2);
                }

                //Spider.transform.LookAt(VecSpider2);
                break;
        }
    }
}
