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
    readonly string Jump = "Jumping"; //Jumping
    readonly string Die = "Die 1";
    readonly string Hit1 = "Receibe Damage";
    readonly string Hit2 = "Receibe Great Damage";
    readonly string DaggerAttack1 = "Dagger Strike 2";
    readonly string DaggerAttack2 = "Dagger Strike 3";
    readonly string DaggerAttack3 = "Dagger Strike 4";
    readonly string StaffAttack = "Staff Swing";
    readonly string StaffSpell1 = "Staff Spell Forward";
    readonly string StaffSpell2 = "Staff Spell Circle";
    readonly string StaffSpell3 = "Staff Spell Ground";
    readonly string Land = "Crouch Idle"; //Jumping
    
    public enum WizardState
    {
        NONE, DAGGERIDLE, STAFFIDLE, RUN, DRAWDAGGER, DRAWSTAFF, JUMP, DIE,
        HIT_1, HIT_2, DAGGERATTACT_1, DAGGERATTACT_2, DAGGERATTACT_3,
        STAFFATTACK,STAFFSPELL_1, STAFFSPELL_2, STAFFSPELL_3,LAND
    };
    
    public Animation WizardAnimation;
    private PlayerCtrl pPlayerCtrl;
    private MoveNPC Move_Npc;

    // 무기바꾸기 델리게이트 선언
    private SwichingWeaPon.SwitchWeaponEvent switchDel;

    [System.NonSerialized]
    public WizardState wizardState = WizardState.STAFFIDLE;
    [System.NonSerialized]
    public WizardState BeforeState = WizardState.NONE;

    private CharacterInformation.MODE Mode;
    private CharacterInformation CharaterInfo;


    private bool IsDie = false;
    private bool IsJump = false;
    private bool IsLeftMouseDown = false;
    private bool IsLeftMouseUp = false;
    private bool IsLeftMouseStay = false;
    private bool IsRightMouseDown = false;
    private bool IsUseAnotherWeaPon = false;
    private bool IsNumKey_1 = false;
    private bool IsNumKey_2 = false;
    private bool IsKey_E = false;
    private bool IsKey_Q = false;
    private bool IsKey_Shift = false;
    private int  ComboCount = 0;
    private float NowComboTime = 0.0f;

    private Vector3 direction = Vector3.zero;
    //AnimationState anistate;
    //AnimationBlendMode
   
    void Start()
    {
        Mode = GetComponent<CharacterInformation>()._mode;
        CharaterInfo = GetComponent<CharacterInformation>();
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
        StartCoroutine(WizardAction());
        StartCoroutine(CheckArcherState());
        
        //무기바꾸기 콜백함수
        switchDel = new SwichingWeaPon.SwitchWeaponEvent(SwichingWeaPon.SwithcingWeapon);
        

        

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
                    NpcMode();
                    break;
            }


           // Debug.Log(wizardState);
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
            IsNumKey_1 = pPlayerCtrl.IsNumKey_1;
            IsNumKey_2 = pPlayerCtrl.IsNumKey_2;
            IsKey_E = pPlayerCtrl.IsKey_E;
            IsKey_Q = pPlayerCtrl.IsKey_Q;
            IsKey_Shift = pPlayerCtrl.IsKey_Shift;
        }
        AttackCombo();

        if (!WizardAnimation.IsPlaying(Jump) && FinishAnimation(DrawStaff, 0.7f) &&
            FinishAnimation(DrawDagger, 0.7f) && FinishAnimation(StaffAttack, 0.7f) &&
            FinishAnimation(StaffSpell1, 0.7f) && FinishAnimation(StaffSpell2, 0.7f) &&
            FinishAnimation(StaffSpell3, 0.7f) && FinishAnimation(DaggerAttack1, 0.7f) &&
            FinishAnimation(DaggerAttack2, 0.7f) && FinishAnimation(DaggerAttack3, 0.7f) &&
            FinishAnimation(Hit1, 0.7f) && FinishAnimation(Hit2, 0.7f) &&
            FinishAnimation(Die, 0.7f))
        {
            if (direction.magnitude >= 0.1f)
            {
                wizardState = WizardState.RUN;
            }
            else if (direction.magnitude <= 0.0f)
            {
                if (IsUseAnotherWeaPon)
                    wizardState = WizardState.DAGGERIDLE;
                else
                    wizardState = WizardState.STAFFIDLE;
            }
            CharaterInfo.isOnceAttack = false;
        }

        if (IsJump)
        {
            if (wizardState == WizardState.JUMP)
            {
                wizardState = WizardState.LAND;
            }
            else
                wizardState = WizardState.JUMP;
        }

        if (IsLeftMouseDown)
        {
            if (!IsUseAnotherWeaPon)
                wizardState = WizardState.STAFFSPELL_1;
        }
        else if (IsLeftMouseUp)
        {

        }
        else if (IsNumKey_1)
        {
            // Debug.Log("1키");
            wizardState = WizardState.DRAWDAGGER;
            if (IsUseAnotherWeaPon)
                StartCoroutine(SwitchWeaponCoroutine(0.4f)); // 무기도 바꾸기
            IsUseAnotherWeaPon = false;

        }
        else if (IsNumKey_2)
        {
            // Debug.Log("1키");
            wizardState = WizardState.DRAWSTAFF;
            if (!IsUseAnotherWeaPon)
                StartCoroutine(SwitchWeaponCoroutine(0.4f));
            IsUseAnotherWeaPon = true;
        }
        else if (IsKey_E)
        {
            if (!IsUseAnotherWeaPon)
                wizardState = WizardState.STAFFATTACK;
            CharaterInfo.isOnceAttack = true;
        }
        else if (IsKey_Q)
        {
            if (!IsUseAnotherWeaPon)
                wizardState = WizardState.STAFFSPELL_2;
        }
        else if (IsKey_Shift)
        {
            if (!IsUseAnotherWeaPon)
                wizardState = WizardState.STAFFSPELL_3;
        }

        //오른쪽 공격
        if (IsRightMouseDown)
        {
            if (IsUseAnotherWeaPon)
            {

                if (ComboCount == 2 && NowComboTime < 1.0f)
                {

                    wizardState = WizardState.DAGGERATTACT_2;
                    NowComboTime = 0.0f;
                }
                else if (ComboCount == 3 && NowComboTime < 1.0f)
                {

                    wizardState = WizardState.DAGGERATTACT_3;
                    NowComboTime = 0.0f;
                }
                else
                {
                    if (ComboCount == 1)
                    {
                        ComboCount = 2;
                        NowComboTime = 0.0f;
                    }
                    else
                    {
                        
                        wizardState = WizardState.DAGGERATTACT_1;
                        ComboCount = 1;
                        NowComboTime = 0.0f;
                    }
                }
            }
        }

       

        NowComboTime += Time.deltaTime;
        if (NowComboTime > 1.2f)
        {
            NowComboTime = 0.0f;
            ComboCount = 0;
        }
    }

    private void NpcMode()
    {
        if (Mode == CharacterInformation.MODE.NPC)
        {
            direction = Move_Npc.direction;
            //IsJump = Move_Npc.IsJump;
            ////IsLeftMouseDown = Move_Npc.IsLeftMouseDown;
            //IsLeftMouseUp = Move_Npc.IsLeftMouseUp;
            //IsLeftMouseStay = Move_Npc.IsLeftMouseStay;
            //IsRightMouseDown = Move_Npc.IsRightMouseDown;
            //IsNumKey_1 = Move_Npc.IsNumKey_1;
            //IsNumKey_2 = Move_Npc.IsNumKey_2;
            //IsKey_E = Move_Npc.IsKey_E;
            //IsKey_Q = Move_Npc.IsKey_Q;
            //IsKey_Shift = Move_Npc.IsKey_Shift;
        }
        AttackCombo();

        if (!WizardAnimation.IsPlaying(Jump) && FinishAnimation(DrawStaff, 0.7f) &&
            FinishAnimation(DrawDagger, 0.7f) && FinishAnimation(StaffAttack, 0.7f) &&
            FinishAnimation(StaffSpell1, 0.7f) && FinishAnimation(StaffSpell2, 0.7f) &&
            FinishAnimation(StaffSpell3, 0.7f) && FinishAnimation(DaggerAttack1, 0.7f) &&
            FinishAnimation(DaggerAttack2, 0.7f) && FinishAnimation(DaggerAttack3, 0.7f) &&
            FinishAnimation(Hit1, 0.7f) && FinishAnimation(Hit2, 0.7f) &&
            FinishAnimation(Die, 0.7f))
        {
            if (direction.magnitude >= 0.1f)
            {
                wizardState = WizardState.RUN;
            }
            else if (direction.magnitude <= 0.0f)
            {
                if (IsUseAnotherWeaPon)
                    wizardState = WizardState.DAGGERIDLE;
                else
                    wizardState = WizardState.STAFFIDLE;
            }
        }

        if (IsJump)
        {
            if (wizardState == WizardState.JUMP)
            {
                wizardState = WizardState.LAND;
            }
            else
                wizardState = WizardState.JUMP;
        }

        if (IsLeftMouseDown)
        {
            if (!IsUseAnotherWeaPon)
                wizardState = WizardState.STAFFSPELL_1;
        }
        else if (IsLeftMouseUp)
        {

        }
        else if (IsNumKey_1)
        {
            // Debug.Log("1키");
            wizardState = WizardState.DRAWDAGGER;
            if (IsUseAnotherWeaPon)
                StartCoroutine(SwitchWeaponCoroutine(0.4f)); // 무기도 바꾸기
            IsUseAnotherWeaPon = false;

        }
        else if (IsNumKey_2)
        {
            // Debug.Log("1키");
            wizardState = WizardState.DRAWSTAFF;
            if (!IsUseAnotherWeaPon)
                StartCoroutine(SwitchWeaponCoroutine(0.4f));
            IsUseAnotherWeaPon = true;
        }
        else if (IsKey_E)
        {
            if (!IsUseAnotherWeaPon)
                wizardState = WizardState.STAFFATTACK;
        }
        else if (IsKey_Q)
        {
            if (!IsUseAnotherWeaPon)
                wizardState = WizardState.STAFFSPELL_2;
        }
        else if (IsKey_Shift)
        {
            if (!IsUseAnotherWeaPon)
                wizardState = WizardState.STAFFSPELL_3;
        }

        //오른쪽 공격
        if (IsRightMouseDown)
        {
            if (IsUseAnotherWeaPon)
            {
                if (ComboCount == 2 && NowComboTime < 1.0f)
                {

                    wizardState = WizardState.DAGGERATTACT_2;
                    NowComboTime = 0.0f;
                }
                else if (ComboCount == 3 && NowComboTime < 1.0f)
                {

                    wizardState = WizardState.DAGGERATTACT_3;
                    NowComboTime = 0.0f;
                }
                else
                {
                    if (ComboCount == 1)
                    {
                        ComboCount = 2;
                        NowComboTime = 0.0f;
                    }
                    else
                    {
                        wizardState = WizardState.DAGGERATTACT_1;
                        ComboCount = 1;
                        NowComboTime = 0.0f;
                    }
                }
            }
        }



        NowComboTime += Time.deltaTime;
        if (NowComboTime > 1.2f)
        {
            NowComboTime = 0.0f;
            ComboCount = 0;
        }
    }

    IEnumerator WizardAction()
    {

        while (!IsDie)
        {

            switch (wizardState)
            {
                case WizardState.RUN:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(Run, 0.3f);
                    break;

                case WizardState.STAFFIDLE:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(StaffIdle, 0.3f);
                    break;

                case WizardState.DAGGERIDLE:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(DaggerIdle, 0.3f);
                    break;

                case WizardState.JUMP:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(Jump, 0.3f);
                    break;

                case WizardState.LAND:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(Land, 0.3f);
                    break;

                case WizardState.DRAWSTAFF:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(DrawStaff, 0.3f);
                    break;

                case WizardState.DRAWDAGGER:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(DrawDagger, 0.3f);
                    break;

                case WizardState.STAFFATTACK:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(StaffAttack, 0.3f);
                    break;

                case WizardState.STAFFSPELL_1:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(StaffSpell1, 0.3f);
                    break;

                case WizardState.STAFFSPELL_2:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(StaffSpell2, 0.3f);
                    break;

                case WizardState.STAFFSPELL_3:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(StaffSpell3, 0.3f);
                    break;

                case WizardState.DAGGERATTACT_1:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(DaggerAttack1, 0.3f);
                    break;

                case WizardState.DAGGERATTACT_2:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(DaggerAttack2, 0.3f);
                    break;

                case WizardState.DAGGERATTACT_3:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(DaggerAttack3, 0.3f);
                    break;

                case WizardState.HIT_1:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(Hit1, 0.3f);
                    break;

                case WizardState.HIT_2:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(Hit2, 0.3f);
                    break;

                case WizardState.DIE:
                    WizardAnimation.wrapMode = WrapMode.Loop;
                    WizardAnimation.CrossFade(Die, 0.3f);
                    break;

                default:
                    break;

            }
            yield return null;
        }
    }

    IEnumerator SwitchWeaponCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        switchDel();
    }

    bool FinishAnimation(string aniName, float NormalizedTime)
    {
        // 해당이름의 애니메이션이 정해진 노말타임이상이면 애니메이션 완료햇으므로 트루리턴
        bool isFinish = false;
        
        if (WizardAnimation[aniName].normalizedTime >= NormalizedTime)
        {
            //Debug.Log("완료");
            return isFinish = true;
        }
        else if (!WizardAnimation.IsPlaying(aniName))
        {
            //Debug.Log("플레이중 아님");
            return isFinish = true;
        }

        return isFinish;
    }

    void AttackCombo()
    {
        if (WizardAnimation[DaggerAttack1].normalizedTime >= 0.7f)
        {
            ComboCount=2;
        }
        else if (WizardAnimation[DaggerAttack2].normalizedTime >= 0.7f)
        {
            ComboCount=3;
        }
        else if (WizardAnimation[DaggerAttack3].normalizedTime >= 0.7f)
        {
            ComboCount = 0;
        }
    }
    


}

