using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3Animation : MonoBehaviour {
    public enum M3_STATE
    {
        M3_STAY,M3_ATTACK,M3_RUN,M3_WALK,
        M3_TAUNT,M3_ROAR,M3_HIT,M3_AIR
    };

    public enum M3_WAY
    {
        M3_F,M3_B,M3_R,M3_L
    };

    public M3_STATE    NowState;
    public M3_WAY      NowWay;
    public int         AttackNum;
    public int         _health;
    public Animator    Mt3;


    public int damage = 10;
    private MonsterInformation Information;

    private MonsterSoundManager SoundManager;

    private bool OnceHit = false;
    private bool OnceAttack = false;
    private bool OnceDie = false;
    // Use this for initialization
    void Start () {
        Mt3 = GetComponent<Animator>();
        NowState = M3_STATE.M3_STAY;
        Information = GetComponent<MonsterInformation>();
        //_health = Mt3.GetInteger("Health");

        Mt3.SetInteger("Health", Information.hp);
        NowWay = (M3_WAY)Mt3.GetInteger("Way");
        AttackNum = 0;
        SoundManager = GetComponent<MonsterSoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //ChangeAnimation();
        if (Information.hp <= 0)
        {
            if (NowState != M3_STATE.M3_HIT)
                NowState = M3_STATE.M3_HIT;
        }
        InformationCheck();
        AttackCheck();
        CheckWay();
        Sound_Play();
        //HitbyChar();
    }
    private void InformationCheck()
    {
        if (Mt3.GetInteger("State") != (int)NowState)
            Mt3.SetInteger("State", (int)NowState);
        if (Information)
        {
            if (Mt3.GetInteger("Health") != Information.hp)
                Mt3.SetInteger("Health", Information.hp);
            damage = Information.damage;

            if (NowState == M3_STATE.M3_ATTACK)
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
                    NowState = M3_STATE.M3_HIT;
                    OnceHit = true;
                }
            }
            else if (NowState == M3_STATE.M3_HIT)
            {
                if (OnceAttack)
                    OnceAttack = false;
                if (Information.MonsterState != MonsterInformation.STATE.HIT)
                    Information.MonsterState = MonsterInformation.STATE.HIT;
                if (!Information.isHit)
                {
                    if (Information.MonsterState != MonsterInformation.STATE.STAY)
                        Information.MonsterState = MonsterInformation.STATE.STAY;
                    NowState = M3_STATE.M3_STAY;
                }
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
                    NowState = M3_STATE.M3_HIT;
                    OnceHit = true;
                }
            }
        }
    }
    void AttackCheck()
    {
        if(NowState == M3_STATE.M3_ATTACK)
        {
            if(AttackNum != Mt3.GetInteger("AttackCount"))
            {
                Mt3.SetInteger("AttackCount", AttackNum);
            }
        }
    }

    void ChangeAnimation()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (NowState == M3_STATE.M3_AIR)
                NowState = M3_STATE.M3_STAY;
            else
                NowState++;
            Mt3.SetInteger("State", (int)NowState);
        }
        
    }

    void CheckWay()
    {
        if(NowState == M3_STATE.M3_WALK)
        {
            if ((int)NowWay != Mt3.GetInteger("Way"))
            {
                Mt3.SetInteger("Way", (int)NowWay);
            }
        }
    }

    void HitbyChar()
    {
        _health--;
        Mt3.SetInteger("Health", _health);
    }

    private void Sound_Play()
    {
        if(!Information.isDie)
        {
            switch(NowState)
            {
                case M3_STATE.M3_STAY:
                    break;
                case M3_STATE.M3_RUN:
                    break;
                case M3_STATE.M3_HIT:
                    if(Information.hp <= 0)
                    {

                    }
                    else
                    {

                    }
                    break;
                case M3_STATE.M3_ATTACK:
                    if (Mt3.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
                    {
                        if (Mt3.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f &&
                            Mt3.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.45f)
                        {
                            Information.isOnceAttack = true;
                        }
                    }
                            break;
            }
        }
    }
}
