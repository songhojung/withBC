﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour {

    public enum S_STATE
    {
        S_ATT,S_ATT_L,S_ATT_R,S_DEATH,
        S_IDLE,S_RUN,S_WALK
    };

    public Animation Spider;
    public S_STATE NowState = S_STATE.S_IDLE;

    public int _health = 10;

    private MonsterInformation Information;
    private MonsterSoundManager Sound_M;
    public bool isHit = false;

    public int damage;

    private bool OnceHit = false;
    private bool OnceAttack = false;
    private bool OnceDie = false;
    // Use this for initialization
    void Start () {
        Spider = GetComponent<Animation>();

        Information = GetComponent<MonsterInformation>();
        Sound_M = GetComponent<MonsterSoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Information.hp <= 0)
        {
            if (NowState != S_STATE.S_DEATH)
                NowState = S_STATE.S_DEATH;
        }

        //ChangeState();
        PlayAnimation();
        InformationCheck();
	}

    private void InformationCheck()
    {
        if (Information)
        {
            _health = Information.hp;

            damage = Information.damage;

            if (NowState == S_STATE.S_ATT || NowState == S_STATE.S_ATT_L ||
                NowState == S_STATE.S_ATT_R)
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
                    NowState = S_STATE.S_IDLE;
                    OnceHit = true;
                }
            }
            else if (NowState == S_STATE.S_IDLE)
            {
                if (OnceAttack)
                    OnceAttack = false;
                if (Information.isHit)
                {
                    if (Information.MonsterState != MonsterInformation.STATE.HIT)
                        Information.MonsterState = MonsterInformation.STATE.HIT;
                }
                else
                {
                    if (Information.MonsterState != MonsterInformation.STATE.STAY)
                        Information.MonsterState = MonsterInformation.STATE.STAY;
                    NowState = S_STATE.S_IDLE;
                }
            }
            else if (NowState == S_STATE.S_DEATH)
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
                    NowState = S_STATE.S_IDLE;
                    OnceHit = true;
                }
            }
        }
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
        if (!Information.isDie)
        {
            switch (NowState)
            {
                case S_STATE.S_ATT:
                    Spider.wrapMode = WrapMode.Once;
                    Spider.CrossFade("Attack", 0.3f);
                    if(Spider["Attack"].normalizedTime >0.4f &&
                        Spider["Attack"].normalizedTime < 0.45f)
                    {
                        Information.isOnceAttack = true;
                    }
                    break;
                case S_STATE.S_ATT_L:
                    Spider.wrapMode = WrapMode.Once;
                    Spider.CrossFade("Attack_Left", 0.3f);
                    break;
                case S_STATE.S_ATT_R:
                    Spider.wrapMode = WrapMode.Once;
                    Spider.CrossFade("Attack_Right", 0.3f);
                    break;
                case S_STATE.S_DEATH:
                    Spider.wrapMode = WrapMode.Once;
                    Spider.CrossFade("Death", 0.3f);
                    break;
                case S_STATE.S_IDLE:
                    Spider.wrapMode = WrapMode.Once;
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

    private void Sound_Play()
    {
        if (!Information.isDie)
        {
            if(!Sound_M.MyAudio.isPlaying)
            {
                Sound_M.MyAudio.PlayOneShot(Sound_M.SpiderStay, Sound_M.NowVolum);
                Sound_M.MyAudio.loop = true;
            }
        }
    }
}
