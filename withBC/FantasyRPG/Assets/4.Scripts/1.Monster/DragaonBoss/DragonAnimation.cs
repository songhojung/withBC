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
    public D_STATE NowState = D_STATE.D_STAY;

    public D_FLYRAND NowFlyRand;

    public D_ATTACKSTATE NowAttack;


    public int AttackNum;
    public int _health = 20;
    public int _hitNum;

    public Animation Dragon;


    private MonsterInformation Information;

    public int damage;

    //private bool isDie = false;

    private DragonSound SoundManager;

    private bool OnceHit = false;
    public bool OnceAttack = false;
    private bool OnceDie = false;
    // Use this for initialization
    void Start () {
        Dragon = GetComponent<Animation>();
        NowFlyRand = D_FLYRAND.NOWRAND;

        Information = GetComponent<MonsterInformation>();
        SoundManager = GetComponent<DragonSound>();
        AttackNum = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Information.hp <= 0)
        {
            if (NowState != D_STATE.D_DEATH)
                NowState = D_STATE.D_DEATH;
        }

            //ChangeState();
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

            if (NowState == D_STATE.D_FLY_ATT || NowState == D_STATE.D_ATT2 ||
                NowState == D_STATE.D_ATT1 || NowState == D_STATE.D_FIRE ||
                NowState == D_STATE.D_FLY_FIRE)
            {
                if (Information.MonsterState != MonsterInformation.STATE.ATTACK)
                    Information.MonsterState = MonsterInformation.STATE.ATTACK;
                if (!Information.isAttack)
                {
                    Information.isAttack = true;
                    Information.isOnceAttack = true;
                }
                if (NowAttack != D_ATTACKSTATE.ATTACK)
                    NowAttack = D_ATTACKSTATE.ATTACK;
                if(Information.isHit)
                {
                    int a = Random.Range(0, 1);
                    switch(a)
                    {
                        case 0:
                            NowState = D_STATE.D_HIT1;
                            break;
                        case 1:
                            NowState = D_STATE.D_HIT2;
                            break;
                    }
                    
                }
            }
            else if (NowState == D_STATE.D_HIT1 || NowState == D_STATE.D_HIT2 ||
                    NowState == D_STATE.D_FLY_HIT)
            {
                if (NowAttack != D_ATTACKSTATE.NONATTACK)
                    NowAttack = D_ATTACKSTATE.NONATTACK;

                if (Information.MonsterState != MonsterInformation.STATE.HIT)
                    Information.MonsterState = MonsterInformation.STATE.HIT;
                if(!Information.isHit)
                {
                    if (Information.MonsterState != MonsterInformation.STATE.STAY)
                        Information.MonsterState = MonsterInformation.STATE.STAY;
                    NowState = D_STATE.D_IDLE;
                }
            }
            else if(NowState == D_STATE.D_DEATH)
            {
                if (NowAttack != D_ATTACKSTATE.NONATTACK)
                    NowAttack = D_ATTACKSTATE.NONATTACK;
            }
            else
            {
                if (NowAttack != D_ATTACKSTATE.NONATTACK)
                    NowAttack = D_ATTACKSTATE.NONATTACK;

                if (!Information.isHit)
                {
                    if (Information.MonsterState != MonsterInformation.STATE.STAY)
                        Information.MonsterState = MonsterInformation.STATE.STAY;
                }
                else
                {
                    int a = Random.Range(0, 1);
                    switch (a)
                    {
                        case 0:
                            NowState = D_STATE.D_HIT1;
                            break;
                        case 1:
                            NowState = D_STATE.D_HIT2;
                            break;
                    }
                }
            }
        }
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
        if (!Information.isDie)
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

    private void Sound_Play()
    {
        if (!Information.isDie)
        {
            switch (NowState)
            {
                case D_STATE.D_STAY:
                    if (!SoundManager.MyAudio.isPlaying)
                    {
                        SoundManager.MyAudio.clip = SoundManager.Stay;
                        SoundManager.MyAudio.PlayOneShot(SoundManager.Stay, SoundManager.NowVolum);
                    }
                    else
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Stay)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Stay;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Stay, SoundManager.NowVolum);
                        }
                    }
                    break;
                case D_STATE.D_IDLE:
                    if (!SoundManager.MyAudio.isPlaying)
                    {
                        SoundManager.MyAudio.clip = SoundManager.Stay;
                        SoundManager.MyAudio.PlayOneShot(SoundManager.Stay, SoundManager.NowVolum);
                    }
                    else
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Stay)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Stay;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Stay, SoundManager.NowVolum);
                        }
                    }
                    break;
                case D_STATE.D_ATT1:
                    if (OnceAttack)
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Attack)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Attack;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Attack, SoundManager.NowVolum);
                            OnceAttack = false;
                        }
                    }
                    break;
                case D_STATE.D_ATT2:
                    if (OnceAttack)
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Attack)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Attack;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Attack, SoundManager.NowVolum);
                            OnceAttack = false;
                        }
                    }
                    break;
                case D_STATE.D_FIRE:
                    if (OnceAttack)
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Fire)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Fire;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Fire, SoundManager.NowVolum);
                            OnceAttack = false;
                        }
                    }
                    break;
                case D_STATE.D_TAIL:
                    break;
                case D_STATE.D_HIT1:
                    if (OnceHit)
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Hit)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Hit;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Hit, SoundManager.NowVolum);
                            OnceHit = false;
                        }
                    }
                    break;
                case D_STATE.D_HIT2:
                    if (OnceHit)
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Hit)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Hit;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Hit, SoundManager.NowVolum);
                            OnceHit = false;
                        }
                    }
                    break;
                case D_STATE.D_DEATH:
                    if (!OnceDie)
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Death)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Death;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Death, SoundManager.NowVolum);
                            OnceDie = true;
                        }
                    }
                    break;
                case D_STATE.D_WALK:
                    if (!SoundManager.MyAudio.isPlaying)
                    {
                        SoundManager.MyAudio.clip = SoundManager.Move;
                        SoundManager.MyAudio.PlayOneShot(SoundManager.Move, SoundManager.NowVolum);
                    }
                    else
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Move)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Move;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Move, SoundManager.NowVolum);
                        }
                    }
                    break;
                case D_STATE.D_RUN:
                    if (!SoundManager.MyAudio.isPlaying)
                    {
                        SoundManager.MyAudio.clip = SoundManager.Move;
                        SoundManager.MyAudio.PlayOneShot(SoundManager.Move, SoundManager.NowVolum);
                    }
                    else
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Move)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Move;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Move, SoundManager.NowVolum);
                        }
                    }
                    break;
                case D_STATE.D_FLY:
                    break;
                case D_STATE.D_FLY_FIRE:
                    if (OnceAttack)
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Fire)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Fire;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Fire, SoundManager.NowVolum);
                            OnceAttack = false;
                        }
                    }
                    break;
                case D_STATE.D_FLY_FAST:
                    if (!SoundManager.MyAudio.isPlaying)
                    {
                        SoundManager.MyAudio.clip = SoundManager.Move;
                        SoundManager.MyAudio.PlayOneShot(SoundManager.Move, SoundManager.NowVolum);
                    }
                    else
                    {
                        if (SoundManager.MyAudio.clip != SoundManager.Move)
                        {
                            SoundManager.MyAudio.Stop();
                            SoundManager.MyAudio.clip = SoundManager.Move;
                            SoundManager.MyAudio.PlayOneShot(SoundManager.Move, SoundManager.NowVolum);
                        }
                    }
                    break;
                case D_STATE.D_FLY_HIT:
                    break;
            }
        }
    }
}
