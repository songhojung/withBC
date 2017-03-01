using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnimationCtrl : MonoBehaviour {

    //=========== 에니메이션 이름
    readonly string DaggerIdle = "Dagger Stance";
    readonly string StaffIdle = "Staff Stance";
    readonly string Run = "Run";
    readonly string DrawDagger = "Draw Dagger";
    readonly string DrawStaff = "Draw Staff";
    readonly string Jump = "Jumping";
    readonly string Die = "Die 1";
    readonly string Hit1 = "Receibe Damage";
    readonly string Hit2 = "Receibe Great Damage";
    readonly string DaggerAttack1 = "Dagger Strike 2";
    readonly string DaggerAttack2 = "Dagger Strike 3";
    readonly string DaggerAttack3 = "Dagger Strike 4";
    readonly string StaffAttack = "Staff Swing";
    readonly string StaffSpell1 = "Staff Spell Foward";
    readonly string StaffSpell2 = "Staff Spell Circle";
    readonly string StaffSpell3 = "Staff Spell Ground";

    public enum WizardState
    {
        NONE, DAGGERIDLE, STAFFIDLE, RUN, DRAWDAGGER, DRAWSTAFF, JUMP, DIE,
        HIT_1, HIT_2, DAGGERATTACT_1, DAGGERATTACT_2, DAGGERATTACT_3,
        STAFFATTACK,STAFFSPELL_1, STAFFSPELL_2, STAFFSPELL_3
    };
    
    public Animation WizardAnimation;

    [System.NonSerialized]
    public WizardState wizardState = WizardState.STAFFIDLE;
    [System.NonSerialized]
    public WizardState BeforeState = WizardState.NONE;



    private bool IsDie = false;
    private bool IsJump = false;
    private bool IsLeftMouseDown = false;
    private bool IsLeftMouseUp = false;
    private bool IsLeftMouseStay = false;
    private bool IsRightMouseDown = false;

    private Vector3 direction = Vector3.zero;
    //AnimationState anistate;
    //AnimationBlendMode

    void Start()
    {
        StartCoroutine(CheckArcherState());
        StartCoroutine(WizardAction());
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

           

            if (direction.magnitude >= 0.1f)
            {
               
            }
            else if (direction.magnitude <= 0.0f)
            {
                
            }

      
            if (IsJump)
            {
            }

            if (IsLeftMouseDown)
            {
               
            }
            else if (IsLeftMouseUp)
            {
                
            }
            
            //오른쪽 공격
            if (IsRightMouseDown)
            {
               
            }

          

            //Debug.Log(archerState);
            yield return null;
        }
    }

    IEnumerator WizardAction()
    {

        while (!IsDie)
        {

            switch (wizardState)
            {
                case WizardState.STAFFIDLE:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(StaffIdle, 0.3f);
                    break;


                

                default:
                    break;

            }
            yield return null;
        }
    }



}

