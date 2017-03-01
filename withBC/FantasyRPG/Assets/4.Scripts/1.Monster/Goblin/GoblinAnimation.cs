using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimation : MonoBehaviour {

    public enum G_STATE
    {
        S_IDLE, S_ATT1, S_ATT2, S_ATT3,
        S_BLOCK, S_BLOCK_HIT, S_COMBAT_IDLE,
        S_DEATH, S_REMOVE_WEAPON, S_RUN, S_WALK
    };

    public Animation Goblin;

    //CharacterController pcControll;

    public float runSpeed = 6.0f;

    public G_STATE NowState = G_STATE.S_ATT1;

    public int _health;
	// Use this for initialization
	void Start () {
        NowState = G_STATE.S_IDLE;
        Goblin.wrapMode = WrapMode.Loop;
        Goblin.CrossFade("idle", 0.3f);
        //Debug.Log("클립갯수:" + ClipNum.ToString());

        //pcControll = this.gameObject.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        //ChangeMotion();
        Animation_Play3();

    }
    private void ChangeMotion()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if (NowState == G_STATE.S_WALK)
                NowState = G_STATE.S_IDLE;
            else
                NowState++;
            Animation_Play3();
        }
    }
    private void Animation_Play3()
    {
        switch (NowState)
        {
            case G_STATE.S_IDLE:
                Goblin.wrapMode = WrapMode.Loop;
                Goblin.CrossFade("idle", 0.3f);
                break;
            case G_STATE.S_ATT1:
                Goblin.wrapMode = WrapMode.Once;
                Goblin.CrossFade("attack1", 0.3f);
                break;
            case G_STATE.S_ATT2:
                Goblin.wrapMode = WrapMode.Once;
                Goblin.CrossFade("attack2", 0.3f);
                break;
            case G_STATE.S_ATT3:
                Goblin.wrapMode = WrapMode.Once;
                Goblin.CrossFade("attack3", 0.3f);
                break;
            case G_STATE.S_BLOCK:
                Goblin.wrapMode = WrapMode.Once;
                Goblin.CrossFade("block", 0.3f);
                break;
            case G_STATE.S_BLOCK_HIT:
                Goblin.wrapMode = WrapMode.Once;
                Goblin.CrossFade("block_hit", 0.3f);
                break;
            case G_STATE.S_COMBAT_IDLE:
                Goblin.wrapMode = WrapMode.Loop;
                Goblin.CrossFade("combat_idle", 0.3f); ;
                break;
            case G_STATE.S_DEATH:
                Goblin.wrapMode = WrapMode.Once;
                Goblin.CrossFade("death", 0.3f); ;
                break;
            case G_STATE.S_REMOVE_WEAPON:
                Goblin.wrapMode = WrapMode.Once;
                Goblin.CrossFade("remove_weapons", 0.3f); ;
                break;
            case G_STATE.S_RUN:
                Goblin.wrapMode = WrapMode.Loop;
                Goblin.CrossFade("run", 0.3f); ;
                break;
            case G_STATE.S_WALK:
                Goblin.wrapMode = WrapMode.Loop;
                Goblin.CrossFade("walk", 0.3f); ;
                break;
        }
    }


}
