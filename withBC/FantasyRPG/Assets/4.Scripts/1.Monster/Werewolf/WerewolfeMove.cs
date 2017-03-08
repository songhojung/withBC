using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfeMove : MonoBehaviour {

    private WerewolfeAnimation WolfAni;
    public float WalkSpeed = 1.0f;
    private Rigidbody Wolf;
	// Use this for initialization
	void Start () {
        WolfAni = GetComponent<WerewolfeAnimation>();
        Wolf = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(WolfAni.NowState == WerewolfeAnimation.W_STATE.S_WALK ||
            WolfAni.NowState == WerewolfeAnimation.W_STATE.S_RUN)
        {
            Move();
        }
        if (WolfAni.NowState == WerewolfeAnimation.W_STATE.S_STRAFE_L ||
            WolfAni.NowState == WerewolfeAnimation.W_STATE.S_STRAFE_R)
        {
            sterfe();
        }
    }

    void Move()
    {
        switch (WolfAni.NowState)
        {
            case WerewolfeAnimation.W_STATE.S_WALK:
                Vector3 VecSpider = (Wolf.transform.forward * WalkSpeed * Time.deltaTime) + Wolf.transform.position;
                Wolf.MovePosition(VecSpider);
                //Wolf.transform.LookAt(transform.forward);
                break;
            case WerewolfeAnimation.W_STATE.S_RUN:
                Vector3 VecSpider2 = (Wolf.transform.forward * WalkSpeed * 2.0f * Time.deltaTime) + Wolf.transform.position;
                Wolf.MovePosition(VecSpider2);
                //Wolf.transform.LookAt(transform.forward);
                break;
        }
    }

    void sterfe()
    {
        switch (WolfAni.NowState)
        {
            case WerewolfeAnimation.W_STATE.S_STRAFE_L:
                Vector3 VecSpider = (Vector3.left * WalkSpeed*0.5f * Time.deltaTime) + Wolf.transform.position;
                Wolf.MovePosition(VecSpider);
                Wolf.transform.LookAt((Vector3.forward * WalkSpeed * 2.0f * Time.deltaTime) + Wolf.transform.position);
                break;
            case WerewolfeAnimation.W_STATE.S_STRAFE_R:
                Vector3 VecSpider2 = (Vector3.right * WalkSpeed * 0.5f * Time.deltaTime) + Wolf.transform.position;
                Wolf.MovePosition(VecSpider2);
                Wolf.transform.LookAt((Vector3.forward * WalkSpeed * 2.0f * Time.deltaTime) + Wolf.transform.position);
                break;
        }
    }
}
