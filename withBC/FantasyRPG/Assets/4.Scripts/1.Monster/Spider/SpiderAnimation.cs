using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour {

    public enum S_STATE
    {
        S_ATT,S_ATT_L,S_ATT_R,S_DEATH,
        S_IDLE,S_RUN,S_WALK
    };

    public Animation Spider;
    public S_STATE NowState;
    
	// Use this for initialization
	void Start () {
        NowState = S_STATE.S_IDLE;
        Spider.wrapMode = WrapMode.Loop;
        Spider.CrossFade("Idle", 0.3f);
    }
	
	// Update is called once per frame
	void Update () {
        ChangeState();
        //PlayAnimation();
	}

    void ChangeState()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if (NowState == S_STATE.S_WALK)
                NowState = S_STATE.S_ATT;
            else
                NowState++;
            PlayAnimation();
        }

    }

    void PlayAnimation()
    {
        switch(NowState)
        {
            case S_STATE.S_ATT:
                Spider.wrapMode = WrapMode.Loop;
                Spider.CrossFade("Attack", 0.3f);
                break;
            case S_STATE.S_ATT_L:
                Spider.wrapMode = WrapMode.Loop;
                Spider.CrossFade("Attack_Left", 0.3f);
                break;
            case S_STATE.S_ATT_R:
                Spider.wrapMode = WrapMode.Loop;
                Spider.CrossFade("Attack_Right", 0.3f);
                break;
            case S_STATE.S_DEATH:
                Spider.wrapMode = WrapMode.Loop;
                Spider.CrossFade("Death", 0.3f);
                break;
            case S_STATE.S_IDLE:
                Spider.wrapMode = WrapMode.Loop;
                Spider.CrossFade("Idle", 0.3f);
                break;
            case S_STATE.S_RUN:
                Spider.wrapMode = WrapMode.Loop;
                Spider.CrossFade("Run", 0.3f);
                break;
            case S_STATE.S_WALK:
                Spider.wrapMode = WrapMode.Loop;
                Spider.CrossFade("Walk", 0.3f);
                break;
        }
    }
}
