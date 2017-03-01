using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimation : MonoBehaviour {

    public enum D_STATE
    {
        D_STAY, D_IDLE, D_ATT1,D_ATT2,D_FIRE,
        D_TAIL, D_HIT1, D_HIT2, D_DEATH, D_WALK, D_RUN, D_FLY,
        D_FLY_ATT, D_FLY_FIRE, D_FLY_FAST, D_FLY_HIT, D_FLY_DEATH,
        D_WALK_ROOT,D_WALK_ROTATE,D_EAT

    };

    public enum D_ATTACKSTATE
    {
        ATTACK, NONATTACK
    };

    public enum D_FLYRAND
    {
        NOWRAND,NOWFLY
    };
    public D_STATE NowState;

    public D_FLYRAND NowFlyRand;

    public D_ATTACKSTATE NowAttack;


    public int AttackNum;
    public int _health = 20;
    public int _hitNum;

    public Animation Dragon;

    // Use this for initialization
    void Start () {
        Dragon = GetComponent<Animation>();
        NowState = D_STATE.D_STAY;
        NowFlyRand = D_FLYRAND.NOWRAND;

        Dragon.wrapMode = WrapMode.Loop;
        Dragon.CrossFade("stand", 0.3f);

        AttackNum = 0;
    }
	
	// Update is called once per frame
	void Update () {
        ChangeState();
        Animation_Play3();

    }
    void ChangeState()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (NowState == D_STATE.D_EAT)
                NowState = D_STATE.D_STAY;
            else
                NowState++;
        }

    }
    private void Animation_Play3()
    {
        switch (NowState)
        {
            case D_STATE.D_STAY:
                Dragon.wrapMode = WrapMode.Loop;
                Dragon.CrossFade("stand", 0.3f);
                break;
            case D_STATE.D_IDLE:
                Dragon.wrapMode = WrapMode.Loop;
                Dragon.CrossFade("idle", 0.3f);
                break;
            case D_STATE.D_ATT1:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("breath fire", 0.3f);
                break;
            case D_STATE.D_ATT2:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("attack2", 0.3f);
                break;
            case D_STATE.D_FIRE:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("breath fire", 0.3f);
                break;
            case D_STATE.D_TAIL:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("whip tail", 0.3f);
                break;
            case D_STATE.D_HIT1:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("hit1", 0.3f);
                break;
            case D_STATE.D_HIT2:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("hit2", 0.3f); 
                break;
            case D_STATE.D_DEATH:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("death", 0.3f); 
                break;
            case D_STATE.D_WALK:
                Dragon.wrapMode = WrapMode.Loop;
                Dragon.CrossFade("walk", 0.3f); 
                break;
            case D_STATE.D_RUN:
                Dragon.wrapMode = WrapMode.Loop;
                Dragon.CrossFade("run", 0.3f); 
                break;
            case D_STATE.D_FLY:
                Dragon.wrapMode = WrapMode.Loop;
                Dragon.CrossFade("fly", 0.3f);
                break;
            case D_STATE.D_FLY_ATT:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("fly attack", 0.3f);
                break;
            case D_STATE.D_FLY_FIRE:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("fly breath fire", 0.3f);
                break;
            case D_STATE.D_FLY_FAST:
                Dragon.wrapMode = WrapMode.Loop;
                Dragon.CrossFade("fly fast", 0.3f);
                break;
            case D_STATE.D_FLY_HIT:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("fly hit", 0.3f);
                break;
            case D_STATE.D_FLY_DEATH:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("fly death", 0.3f);
                break;
            case D_STATE.D_WALK_ROOT:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("walk root motion", 0.3f);
                break;
            case D_STATE.D_WALK_ROTATE:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("fly", 0.3f);
                break;
            case D_STATE.D_EAT:
                Dragon.wrapMode = WrapMode.Once;
                Dragon.CrossFade("eat", 0.3f);
                break;
        }
    }
}
