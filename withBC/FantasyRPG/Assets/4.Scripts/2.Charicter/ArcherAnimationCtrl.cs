using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimationCtrl : MonoBehaviour {

    //=========== 에니메이션 이름
    readonly string SwordIdle = "SwordCombatReady";
    readonly string BowIdle = "BowCombatReady";
    readonly string Run = "GENRun";
    readonly string BowShoot = "BowShootArrow";
    readonly string GetArrow = "BowGetArrowFromBack";
    readonly string Aim = "BowAim";
    readonly string Jump = "GENjump";
    readonly string Die = "NPCDyingB";
    readonly string HitFront = "GENHitFromFront";
    readonly string HitLeft = "GENHitFromLeft";
    readonly string HitRight = "GENHitFromRight";
    readonly string Attack1 = "SwordSwingHightLeft";
    readonly string Attack2 = "NPCArmKickRight";

    public enum ArcherState { NONE,SWORDIDLE, BOWIDLE,RUN,BOWSHOOT,GETARROW, AIM, JUMP,DIE,
                    HITFRONT, HITLEFT, HITRIGHT,ATTACK1,ATTACK2};
    //
    public Animation ArcherAnimation;
    [System.NonSerialized]
    public ArcherState archerState = ArcherState.SWORDIDLE;
    [System.NonSerialized]
    public ArcherState BeforeState = ArcherState.NONE;
    public GameObject ArrowObject;
    public  bool IsReadyForShoot = false;

    private CharacterInformation.MODE Mode;
    private CharacterInformation characterInfo;

    private bool IsDie = false;
    private bool IsJump = false;
    public bool IsLeftMouseDown = false;
    public bool IsLeftMouseUp = false;
    public bool IsLeftMouseStay = false;
    private bool IsRightMouseDown = false;
   
    private bool IsCombat = false;
    private float ReadyCombatTime = 0.0f;
    private Vector3 direction = Vector3.zero;

    private PlayerCtrl pPlayerCtrl;
    private MoveNPC Move_Npc;

    //AnimationState anistate;
    //AnimationBlendMode

    void Start ()
    {
        Mode = GetComponent<CharacterInformation>()._mode;
        characterInfo = GetComponent<CharacterInformation>();

        switch (Mode)
        {
            case CharacterInformation.MODE.PLAYER:
                pPlayerCtrl = GetComponent<PlayerCtrl>();
                Move_Npc = null;
                break;
            case CharacterInformation.MODE.NPC:
                pPlayerCtrl = null;
                Move_Npc = GetComponent<MoveNPC>();
                break;
        }

        StartCoroutine(ArcherAction());
        StartCoroutine(CheckArcherState());
        StartCoroutine(PlaySound());

        ArcherAnimation[Jump].speed = 1.4f;
        ArcherAnimation[GetArrow].speed = 2.5f;
        ArrowObject.active = false;

        


    }

    IEnumerator CheckArcherState()
    {

        while (!IsDie)
        {
            switch(Mode)
            {
                case CharacterInformation.MODE.PLAYER:
                    PlayerMode();
                    break;
                case CharacterInformation.MODE.NPC:
                    AIMode();
                    break;
            }

            //Debug.Log(archerState);
            yield return null;
        }
    }

    private void PlayerMode()
    {
        if (Mode == CharacterInformation.MODE.PLAYER)
        {

            direction = pPlayerCtrl.direction;
            IsJump = pPlayerCtrl.IsJump;
            IsLeftMouseDown = pPlayerCtrl.IsLeftMouseDown;
            IsLeftMouseUp = pPlayerCtrl.IsLeftMouseUp;
            IsLeftMouseStay = pPlayerCtrl.IsLeftMouseStay;
            IsRightMouseDown = pPlayerCtrl.IsRightMouseDown;
        }

        if(FinishAnimation(Jump,0.7f) && FinishAnimation(BowShoot, 0.7f)
            && FinishAnimation(GetArrow, 0.7f) && FinishAnimation(Aim, 0.7f)
            && FinishAnimation(Attack1, 0.7f) && FinishAnimation(Attack2, 0.7f)
            && FinishAnimation(HitFront, 0.7f))
        {
            if (direction.magnitude >= 0.1f)
            {
                archerState = ArcherState.RUN;
            }
            else if (IsCombat)
            {
                archerState = ArcherState.BOWIDLE;
            }
            else if (direction.magnitude <= 0.0f)
            {
                archerState = ArcherState.SWORDIDLE;
            }
            characterInfo.isOnceAttack = false;
        }
       
        

        if (IsJump)
        {
            archerState = ArcherState.JUMP;
        }

        if (IsLeftMouseDown)
        {
            archerState = ArcherState.GETARROW;
            ArrowObject.SetActive(true);


        }
        else if (IsLeftMouseUp)
        {
            if (IsReadyForShoot)
            {
                archerState = ArcherState.BOWSHOOT;
                IsReadyForShoot = false;
                ArrowObject.SetActive(false);
            }
        }

        if (IsLeftMouseStay)
        {
            if (archerState == ArcherState.GETARROW)
            {
                if (ArcherAnimation.IsPlaying(GetArrow))
                {
                    if (ArcherAnimation[GetArrow].normalizedTime >= 0.50f)
                    {

                        archerState = ArcherState.AIM;
                    }
                }
            }
            else if (archerState == ArcherState.AIM)
            {

                archerState = ArcherState.AIM;
                if (ArcherAnimation.IsPlaying(Aim))
                {
 
                    IsReadyForShoot = true;
                    IsCombat = true;
                }
            }
        }

        //오른쪽 공격
        if (IsRightMouseDown)
        {
            characterInfo.isOnceAttack = true;
            archerState = ArcherState.ATTACK1;
        }

        //공격자세 온!
        if (IsCombat)
        {
            ReadyCombatTime += Time.deltaTime;
            if (ReadyCombatTime >= 4.0f)
            {
                ReadyCombatTime = 0.0f;
                IsCombat = false;
            }
        }
        
    }

    private void AIMode()
    {
        if (Mode == CharacterInformation.MODE.NPC)
        {

            direction = Move_Npc.direction;
            //IsJump = Move_Npc.IsJump;
            //IsLeftMouseDown = Move_Npc.IsLeftMouseDown;
            //IsLeftMouseUp = Move_Npc.IsLeftMouseUp;
            //IsLeftMouseStay = Move_Npc.IsLeftMouseStay;
            //IsRightMouseDown = Move_Npc.IsRightMouseDown;
        }

        if (FinishAnimation(Jump, 0.7f) && FinishAnimation(BowShoot, 0.7f)
            && FinishAnimation(GetArrow, 0.7f) && FinishAnimation(Aim, 0.7f)
            && FinishAnimation(Attack1, 0.7f) && FinishAnimation(Attack2, 0.7f)
            && FinishAnimation(HitFront, 0.7f))
        {
            if (direction.magnitude >= 0.1f)
            {
                archerState = ArcherState.RUN;
            }
            else if (IsCombat)
            {
                archerState = ArcherState.BOWIDLE;
            }
            else if (direction.magnitude <= 0.0f)
            {
                archerState = ArcherState.SWORDIDLE;
            }
            characterInfo.isOnceAttack = false;
            if (characterInfo.isHit)
                characterInfo.isHit = false;
        }

        if (IsJump)
        {
            archerState = ArcherState.JUMP;
        }

        if (IsLeftMouseDown)
        {
            archerState = ArcherState.GETARROW;
            if(Move_Npc.TargetNav.enabled)
                Move_Npc.TargetNav.enabled = false;
            ArrowObject.active = true;


        }
        else if (IsLeftMouseUp)
        {
            if (IsReadyForShoot)
            {
                archerState = ArcherState.BOWSHOOT;
                IsReadyForShoot = false;
                ArrowObject.active = false;
            }
        }

        if (IsLeftMouseStay)
        {
            if (archerState == ArcherState.GETARROW)
            {
                if (ArcherAnimation.IsPlaying(GetArrow))
                {
                    if (ArcherAnimation[GetArrow].normalizedTime >= 0.80f)
                    {

                        if (Move_Npc.TargetNav.enabled)
                            Move_Npc.TargetNav.enabled = false;
                        archerState = ArcherState.AIM;
                    }
                }
            }
            else if (archerState == ArcherState.AIM)
            {

                //archerState = ArcherState.AIM;
                if (Move_Npc.TargetNav.enabled)
                    Move_Npc.TargetNav.enabled = false;
                if (ArcherAnimation[Aim].normalizedTime >= 0.50f)
                {
                    IsReadyForShoot = true;
                    IsCombat = true;
                }
            }
        }

        //오른쪽 공격
        if (IsRightMouseDown)
        {
            characterInfo.isOnceAttack = true;
            archerState = ArcherState.ATTACK1;
        }

        //공격자세 온!
        if (IsCombat)
        {
            ReadyCombatTime += Time.deltaTime;
            if (ReadyCombatTime >= 4.0f)
            {
                ReadyCombatTime = 0.0f;
                IsCombat = false;
            }
        }
    }

    IEnumerator ArcherAction()
    {
   
        while (!IsDie)
        {
      
            switch (archerState)
            {
                case ArcherState.SWORDIDLE:
                    ArcherAnimation.wrapMode = WrapMode.Loop;
                    ArcherAnimation.CrossFade(SwordIdle, 0.3f);
                    break;

                case ArcherState.RUN:
                    ArcherAnimation.CrossFade(Run, 0.3f);
                    break;

                case ArcherState.BOWIDLE:
                    ArcherAnimation.wrapMode = WrapMode.Loop;
                    ArcherAnimation.CrossFade(BowIdle, 0.5f);
                    break;

                case ArcherState.GETARROW:
                    ArcherAnimation.wrapMode = WrapMode.Once;
                    ArcherAnimation.CrossFade(GetArrow, 0.5f);
                    break;
                case ArcherState.AIM:
                    ArcherAnimation.wrapMode = WrapMode.Loop;
                    ArcherAnimation.CrossFade(Aim, 3.0f);
                    break;

                case ArcherState.BOWSHOOT:
                    ArcherAnimation.wrapMode = WrapMode.Once;
                    ArcherAnimation.CrossFade(BowShoot, 0.0f);
                    if(ArcherAnimation[BowShoot].normalizedTime >= 0.8f)
                    {
                        IsLeftMouseUp = false;
                        
                    }
                    break;

                case ArcherState.JUMP:
                    ArcherAnimation.wrapMode = WrapMode.Once;
                    ArcherAnimation.CrossFade(Jump,0.3f);
                    break;

                case ArcherState.ATTACK1:
                    ArcherAnimation.wrapMode = WrapMode.Once;
                    ArcherAnimation.CrossFade(Attack1, 0.3f);
                    break;

                case ArcherState.ATTACK2:
                    ArcherAnimation.wrapMode = WrapMode.Loop;
                    ArcherAnimation.CrossFade(Attack2, 0.3f);
                    break;

                case ArcherState.HITFRONT:
                    ArcherAnimation.wrapMode = WrapMode.Once;
                    ArcherAnimation.CrossFade(HitFront, 0.3f);
                    break;

                default:
                    break;

            }
            yield return null;
        }
    }

    IEnumerator PlaySound()
    {

        while (!IsDie)
        {

            switch (archerState)
            {
                case ArcherState.SWORDIDLE:
                
                    break;

                case ArcherState.RUN:
                   
                    break;

                case ArcherState.BOWIDLE:
                    
                    break;

                case ArcherState.GETARROW:
                    
                    break;
                case ArcherState.AIM:
                   
                    break;

                case ArcherState.BOWSHOOT:
                    
                    break;

                case ArcherState.JUMP:
                    
                    break;

                case ArcherState.ATTACK1:
                   
                    break;

                case ArcherState.ATTACK2:
                   
                    break;

                case ArcherState.HITFRONT:
                    
                    break;

                default:
                    break;

            }
            yield return null;
        }
    }

    bool FinishAnimation(string aniName, float NormalizedTime)
    {
        // 해당이름의 애니메이션이 정해진 노말타임이상이면 애니메이션 완료햇으므로 트루리턴
        bool isFinish = false;

        if (ArcherAnimation[aniName].normalizedTime >= NormalizedTime)
        {
            //Debug.Log("완료");
            return isFinish = true;
        }
        else if (!ArcherAnimation.IsPlaying(aniName))
        {
            //Debug.Log("플레이중 아님");
            return isFinish = true;
        }

        return isFinish;
    }


}
