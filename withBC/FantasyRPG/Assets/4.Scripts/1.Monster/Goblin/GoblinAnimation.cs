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

    public G_STATE NowState = G_STATE.S_IDLE;

    public int _health = 10;

    private MonsterInformation Information;

    public int damage;
    // Use this for initialization

    private MonsterSoundManager SoundManager;

    private bool OnceHit = false;
    private bool OnceAttack = false;
    private bool OnceDie = false;
    void Start () {
        Goblin = this.GetComponent<Animation>();

        Information = GetComponent<MonsterInformation>();
        SoundManager = GetComponent<MonsterSoundManager>();
        //Debug.Log("클립갯수:" + ClipNum.ToString());

        //pcControll = this.gameObject.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Information.hp <= 0)
        {
            if (NowState != G_STATE.S_DEATH)
                NowState = G_STATE.S_DEATH;
        }
        //ChangeMotion();
        Animation_Play3();
        InformationCheck();
        Sound_Play();
    }

    private void InformationCheck()
    {
        if (Information)
        {
            _health = Information.hp;

            damage = Information.damage;

            if (NowState == G_STATE.S_ATT1 || NowState == G_STATE.S_ATT2 ||
                NowState == G_STATE.S_ATT3)
            {
                if (Information.MonsterState != MonsterInformation.STATE.ATTACK)
                    Information.MonsterState = MonsterInformation.STATE.ATTACK;
                if (!Information.isAttack)
                {
                    Information.isAttack = true;
                    //Information.isOnceAttack = true;
                }

                if (Information.isHit)
                {
                    NowState = G_STATE.S_BLOCK_HIT;
                    OnceHit = true;
                }
            }
            else if (NowState == G_STATE.S_BLOCK_HIT)
            {
                if (OnceAttack)
                    OnceAttack = false;
                if (Information.MonsterState != MonsterInformation.STATE.HIT)
                    Information.MonsterState = MonsterInformation.STATE.HIT;
                if (!Information.isHit)
                {
                    if (Information.MonsterState != MonsterInformation.STATE.STAY)
                        Information.MonsterState = MonsterInformation.STATE.STAY;
                    NowState = G_STATE.S_IDLE;
                }
            }
            else if (NowState == G_STATE.S_DEATH)
            {

            }
            else
            {
                if (OnceAttack)
                    OnceAttack = false;
                if (!Information.isHit)
                {
                    if (Information.MonsterState != MonsterInformation.STATE.STAY)
                        Information.MonsterState = MonsterInformation.STATE.STAY;
                }
                else
                {
                    NowState = G_STATE.S_BLOCK_HIT;
                    OnceHit = true;
                }
            }
        }
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
        if (!Information.isDie)
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
                    if (Goblin["attack1"].normalizedTime > 0.4f &&
                        Goblin["attack1"].normalizedTime < 0.45f)
                    {
                        Information.isOnceAttack = true;
                    }
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

    private void Sound_Play()
    {
        if (!Information.isDie)
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
}
