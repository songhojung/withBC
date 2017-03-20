using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfeAnimation : MonoBehaviour {

    public enum W_STATE
    {
        S_STAND,S_WALK,S_RUN,S_ATT1,S_BEATEN,
        S_VICTORY,S_ROAR,S_ATT20,S_DEATH,S_JUMP,
        S_STRAFE_R,S_STRAFE_L,S_ATT2,S_ATT_STAND,
        S_ATT3,S_RECOVER
    };

    //public AnimationClip[] PlayerAni;

    public Animation Werewolf;

    //CharacterController pcControll;

    public float runSpeed = 6.0f;

    public W_STATE NowState = W_STATE.S_STAND;

    public int _health = 10;

    private MonsterInformation Information;

    public int damage;
    // Use this for initialization
    void Start()
    {
        Werewolf = this.GetComponent<Animation>();
        Information = GetComponent<MonsterInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Information.hp <=0)
        {
            if (NowState != W_STATE.S_DEATH)
                NowState = W_STATE.S_DEATH;
        }
        //ChangeMotion();
        Animation_Play3();
        InformationCheck();
        
    }

    private void InformationCheck()
    {
        if(Information)
        {
            _health = Information.hp;

            Information.damage = damage;

            if(NowState == W_STATE.S_ATT1 || NowState == W_STATE.S_ATT2 ||
                NowState == W_STATE.S_ATT3 || NowState == W_STATE.S_ATT20 ||
                NowState == W_STATE.S_ATT_STAND)
            {
                if(Information.MonsterState != MonsterInformation.STATE.ATTACK)
                    Information.MonsterState = MonsterInformation.STATE.ATTACK;
                if(!Information.isAttack)
                {
                    Information.isAttack = true;
                    Information.isOnceAttack = true;
                }

                if(Information.isHit)
                {
                    NowState = W_STATE.S_BEATEN;
                }
            }
            else if(NowState == W_STATE.S_BEATEN)
            {
                if (Information.MonsterState != MonsterInformation.STATE.HIT)
                    Information.MonsterState = MonsterInformation.STATE.HIT;
                if (!Information.isHit)
                {
                    if (Information.MonsterState != MonsterInformation.STATE.STAY)
                        Information.MonsterState = MonsterInformation.STATE.STAY;
                    NowState = W_STATE.S_STAND;
                }
            }
            else if(NowState == W_STATE.S_DEATH)
            {

            }
            else
            {
                if (!Information.isHit)
                {
                    if (Information.MonsterState != MonsterInformation.STATE.STAY)
                        Information.MonsterState = MonsterInformation.STATE.STAY;
                }
                else
                    NowState = W_STATE.S_BEATEN;
            }
        }
    }
    private void ChangeMotion()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (NowState == W_STATE.S_RECOVER)
                NowState = W_STATE.S_STAND;
            else
                NowState++;
        }
    }
    private void Animation_Play3()
    {
        //Werewolf.wrapMode = WrapMode.Loop;
        if(!Information.isDie)
        {
            switch (NowState)
            {
                case W_STATE.S_STAND:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Standby", 0.3f);
                    break;
                case W_STATE.S_ATT1:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Attack1", 0.3f);
                    break;
                case W_STATE.S_ATT2:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Attack2", 0.3f);
                    break;
                case W_STATE.S_ATT3:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Attack3", 0.3f);
                    break;
                case W_STATE.S_ATT20:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Attack20", 0.3f);
                    break;
                case W_STATE.S_ATT_STAND:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Attack_standby", 0.3f);
                    break;
                case W_STATE.S_BEATEN:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Beaten", 0.3f);
                    break;
                case W_STATE.S_DEATH:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Death", 0.3f);
                    break;
                case W_STATE.S_JUMP:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Jump", 0.3f);
                    break;
                case W_STATE.S_RUN:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("run", 0.3f);
                    break;
                case W_STATE.S_WALK:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("walk", 0.3f);
                    break;
                case W_STATE.S_ROAR:
                    //if (!Werewolf.Play())
                    //{
                    //    NowState = W_STATE.S_STAND;
                    //}
                    //if(Werewolf["Roar"].normalizedTime >= 0.99f)
                    //{
                    //    NowState = W_STATE.S_RUN;
                    //}
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Roar", 0.3f);
                    break;
                case W_STATE.S_STRAFE_L:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("strafe_right", 0.3f);
                    break;
                case W_STATE.S_STRAFE_R:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("strafe_left", 0.3f);
                    break;
                case W_STATE.S_VICTORY:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("Victory", 0.3f);
                    break;
                case W_STATE.S_RECOVER:
                    Werewolf.wrapMode = WrapMode.Once;
                    Werewolf.CrossFade("recover", 0.3f);
                    break;
            }
        }
    }
}
