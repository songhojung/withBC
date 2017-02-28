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

    private bool IsDie = false;
    private bool IsJump = false;
    private bool IsLeftMouseDown = false;
    private bool IsLeftMouseUp = false;
    private bool IsLeftMouseStay = false;
    private bool IsRightMouseDown = false;
   
    private bool IsCombat = false;
    private float ReadyCombatTime = 0.0f;
    private Vector3 direction = Vector3.zero;
    //AnimationState anistate;
    //AnimationBlendMode

	void Start ()
    {
        StartCoroutine(CheckArcherState());
        StartCoroutine(ArcherAction());
        ArcherAnimation[Jump].speed = 1.4f;
        ArcherAnimation[GetArrow].speed = 2.5f;
        ArrowObject.active = false;


    }

    IEnumerator CheckArcherState()
    {

        while (!IsDie)
        {
            PlayerCtrl pPlayerCtrl = GetComponent<PlayerCtrl>();
            direction = pPlayerCtrl.direction;
            IsJump = pPlayerCtrl.IsJump;
            IsLeftMouseDown = pPlayerCtrl.IsLeftMouseDown;
            IsLeftMouseUp = pPlayerCtrl.IsLeftMouseUp;
            IsLeftMouseStay = pPlayerCtrl.IsLeftMouseStay;
            IsRightMouseDown = pPlayerCtrl.IsRightMouseDown;

            if (!ArcherAnimation.IsPlaying(Jump) && !ArcherAnimation.IsPlaying(BowShoot)
                && !ArcherAnimation.IsPlaying(GetArrow)&& !ArcherAnimation.IsPlaying(Aim)
                && !ArcherAnimation.IsPlaying(Attack1) && !ArcherAnimation.IsPlaying(Attack2))
            {
                
                if (direction.magnitude >= 0.1f)
                {
                    archerState = ArcherState.RUN;
                }
                else if(IsCombat)
                {
                    archerState = ArcherState.BOWIDLE;
                }
                else if (direction.magnitude <= 0.0f)
                {
                    archerState = ArcherState.SWORDIDLE;
                }
                
            }
            
            if (IsJump)
            {
                archerState = ArcherState.JUMP;
            }

            if (IsLeftMouseDown)
            {
                archerState = ArcherState.GETARROW;
                ArrowObject.active = true;


            }
            else if(IsLeftMouseUp)
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
                            Debug.Log("활당기기준비완료");
                            archerState = ArcherState.AIM;
                        }
                    }
                }
                else if (archerState == ArcherState.AIM)
                {
                    Debug.Log("활당기는중");
                    archerState = ArcherState.AIM;
                    if (ArcherAnimation.IsPlaying(Aim))
                    {
                        Debug.Log("쏘기준비완료");
                        IsReadyForShoot = true;
                        IsCombat = true;
                    }
                }
            }

            //오른쪽 공격
            if(IsRightMouseDown)
            {
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

            //Debug.Log(archerState);
            yield return null;
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

                default:
                    break;

            }
            yield return null;
        }
    }

   

}
