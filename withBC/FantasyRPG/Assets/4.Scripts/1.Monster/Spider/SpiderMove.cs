using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour {

    public SpiderAnimation spiderAni;
    public Rigidbody Spider;
    public float WalkSpeed = 1.0f;
	// Use this for initialization
	void Start () {
        Spider = GetComponent<Rigidbody>();
        spiderAni = GetComponent<SpiderAnimation>();

    }
	
	// Update is called once per frame
	void Update () {
		
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
                Vector3 VecSpider = (Vector3.forward * WalkSpeed * Time.deltaTime) + Spider.transform.position;
                Spider.MovePosition(VecSpider);
                break;
            case SpiderAnimation.S_STATE.S_RUN:
                Vector3 VecSpider2 = (Vector3.forward * WalkSpeed* 2.0f * Time.deltaTime) + Spider.transform.position;
                Spider.MovePosition(VecSpider2);
                break;
        }
    }
}
