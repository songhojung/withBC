using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAnimationCtrl : MonoBehaviour {

    public enum WarriorState { IDLE, ATTACK1, ATTACK2,  WALK,RUN,DIE,JUMP,HIT };
    public enum WarriorComboState { NONE,COMBO1,COMBO2,COMBO3,SHIELDBLOCK };

    [System.NonSerialized]
    public WarriorState warriorState = WarriorState.IDLE;
    [System.NonSerialized]
    public WarriorComboState warriorComboState = WarriorComboState.NONE;

    private CharacterInformation.MODE Mode;
    private CharacterInformation characterInfo;

    private Animator WarriorAnimator;
    public bool isDie = false;
    public bool isRun = false;
    public bool isCombo1 = false;
    public bool isCombo2 = false;
    public Vector3 Direction = Vector3.zero;
    public bool IsLeftMouseDown = false;
    public bool IsRightMouseDown = false;
    public bool IsJump = false;
    private float NowComboTime = 0.0f;
    private float[] AttackClipLength;

    private PlayerCtrl pPlayerCtrl;
    private MoveNPC Move_Npc;
    // Use this for initialization
    void Start()
    {
        WarriorAnimator = GetComponent<Animator>();

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
        StartCoroutine(WarriorAction());
        StartCoroutine(CheckWarriorState());
        StartCoroutine(WarriorComboAttack());
        //pPlayerCtrl = GetComponent<PlayerCtrl>();
        
        
    }

    //void GetLengthData()
    //{
    //    if (WarriorAnimator != null)
    //    {
    //        RuntimeAnimatorController aniController = WarriorAnimator.runtimeAnimatorController;
    //        for (int i = 0; i < aniController.animationClips.Length; i++)
    //        {
    //            if (!aniLength.ContainsKey(aniController.animationClips[i].name))
    //            {
    //                Debug.Log(aniController.animationClips[i].length);
    //                aniLength.Add(aniController.animationClips[i].name, aniController.animationClips[i].length);
    //            }
    //        }
    //    }

    //}


    //워리어 상태변경
    IEnumerator CheckWarriorState()
    {
        while (!isDie)
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
            yield return null;
        }
    }

    private void PlayerMode()
    {
        if (Mode == CharacterInformation.MODE.PLAYER)
        {
            Direction = pPlayerCtrl.direction;
            IsLeftMouseDown = pPlayerCtrl.IsLeftMouseDown;
            IsRightMouseDown = pPlayerCtrl.IsRightMouseDown;
            IsJump = pPlayerCtrl.IsJump;
        }

        if (IsJump)
        {
            //Debug.Log("점프");
            warriorState = WarriorState.JUMP;
        }

        else if (Direction.sqrMagnitude > 0.01f)
        {
            //Debug.Log("런");
            warriorState = WarriorState.RUN;
        }
        else if (Direction.sqrMagnitude <= 0)
        {
            warriorState = WarriorState.IDLE;
        }


        if (!isCombo1 && WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack1 0"))
        {
            if (!isCombo1 && WarriorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                warriorComboState = WarriorComboState.NONE;
                characterInfo.isOnceAttack = false;

            }
        }
        else if (WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack2"))
        {

            if (!isCombo2 && WarriorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                //Debug.Log("어택2공격끝");
                warriorComboState = WarriorComboState.NONE;
                characterInfo.isOnceAttack = false;
            }
        }
        else if (isCombo1 && !WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack1 0") ||
            isCombo2 && !WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack2"))
        {
            warriorComboState = WarriorComboState.NONE;
            characterInfo.isOnceAttack = false;
        }

        if (WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Block"))
        {
            warriorComboState = WarriorComboState.NONE;
            characterInfo.isOnceAttack = false;
        }
        //else if(WarriorAnimator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody Layer.Block"))
        //{
        //    warriorComboState = WarriorComboState.NONE;
        //}

        //    if (WarriorAnimator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody Layer.Attack1 0"))
        //{
        //    if (!isCombo1 && WarriorAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1.0f)
        //    {
        //        warriorComboState = WarriorComboState.NONE;

        //    }
        //}


        if (IsLeftMouseDown)
        {
            characterInfo.isOnceAttack = true;
            if (!isCombo1 && warriorComboState == WarriorComboState.COMBO1 && NowComboTime < 0.9f)
            {

                warriorComboState = WarriorComboState.COMBO2;
                //Debug.Log("투콤시간" + NowComboTime);
                NowComboTime = 0.0f;

            }
            else if (!isCombo2 && warriorComboState == WarriorComboState.COMBO2 && NowComboTime < 1.5f)
            {
                warriorComboState = WarriorComboState.COMBO3;
                //Debug.Log("삼콤보시간" + NowComboTime);
                NowComboTime = 0.0f;
            }
            else
            {
                //Debug.Log("첫째공격");
                warriorComboState = WarriorComboState.COMBO1;
                isCombo1 = false;
                isCombo2 = false;
                NowComboTime = 0.0f;
            }

        }
        else if (IsRightMouseDown)
        {
            characterInfo.isOnceAttack = true;
            warriorComboState = WarriorComboState.SHIELDBLOCK;
            NowComboTime = 0.0f;
        }


        NowComboTime += Time.deltaTime;
        if (NowComboTime > 2.0f) NowComboTime = 0.0f;
        //Debug.Log(isCombo1);
    }


    private void AIMode()
    { 
        if (Mode == CharacterInformation.MODE.NPC)
        {
            
            Direction = Move_Npc.direction;
            //IsLeftMouseDown = Move_Npc.IsLeftMouseDown;
            //IsRightMouseDown = Move_Npc.IsRightMouseDown;
        }
        if (IsJump)
        {
            //Debug.Log("점프");
            warriorState = WarriorState.JUMP;
        }

        else if (Direction.sqrMagnitude > 0.01f)
        {
            //Debug.Log("런");
            warriorState = WarriorState.RUN;
        }
        else if (Direction.sqrMagnitude <= 0)
        {
            warriorState = WarriorState.IDLE;
        }


        if (!isCombo1 && WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack1 0"))
        {
            if (!isCombo1 && WarriorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                warriorComboState = WarriorComboState.NONE;
                characterInfo.isOnceAttack = false;

            }
        }
        else if (WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack2"))
        {

            if (!isCombo2 && WarriorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {

                warriorComboState = WarriorComboState.NONE;
                characterInfo.isOnceAttack = false;
            }
        }
        else if (isCombo1 && !WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack1 0") ||
            isCombo2 && !WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack2"))
        {
            warriorComboState = WarriorComboState.NONE;
            characterInfo.isOnceAttack = false;
        }

        if (WarriorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Block"))
        {
            warriorComboState = WarriorComboState.NONE;
            characterInfo.isOnceAttack = false;
        }
        //else if(WarriorAnimator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody Layer.Block"))
        //{
        //    warriorComboState = WarriorComboState.NONE;
        //}

        //    if (WarriorAnimator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody Layer.Attack1 0"))
        //{
        //    if (!isCombo1 && WarriorAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1.0f)
        //    {
        //        warriorComboState = WarriorComboState.NONE;

        //    }
        //}


        if (IsLeftMouseDown)
        {
            characterInfo.isOnceAttack = true;
            if (!isCombo1 && warriorComboState == WarriorComboState.COMBO1 && NowComboTime < 0.9f)
            {

                warriorComboState = WarriorComboState.COMBO2;
                //Debug.Log("투콤시간" + NowComboTime);
                NowComboTime = 0.0f;

            }
            else if (!isCombo2 && warriorComboState == WarriorComboState.COMBO2 && NowComboTime < 1.5f)
            {
                warriorComboState = WarriorComboState.COMBO3;
                //Debug.Log("삼콤보시간" + NowComboTime);
                NowComboTime = 0.0f;
            }
            else
            {
                //Debug.Log("첫째공격");
                warriorComboState = WarriorComboState.COMBO1;
                isCombo1 = false;
                isCombo2 = false;
               // Debug.Log(NowComboTime);
                NowComboTime = 0.0f;
            }

        }
        else if (IsRightMouseDown)
        {
            characterInfo.isOnceAttack = true;
            warriorComboState = WarriorComboState.SHIELDBLOCK;
            NowComboTime = 0.0f;
        }


        NowComboTime += Time.deltaTime;
        if (NowComboTime > 2.0f) NowComboTime = 0.0f;
        //Debug.Log(isCombo1);
    }
    //상태 변경에 따른 에니메이션 변경
    IEnumerator WarriorAction()
    {
        while (!isDie)
        {


            switch (warriorState)
            {
                case WarriorState.RUN:
                    isRun = true;
                    WarriorAnimator.SetBool("IsRun", true);
                    WarriorAnimator.SetBool("IsJump", false);
                    WarriorAnimator.SetFloat("RunSpeed", Direction.sqrMagnitude);
                    //WarriorAnimator.SetBool("IsAttack_1 0", false);
                    //WarriorAnimator.SetBool("IsAttack_2 0", false);
                    break;

                case WarriorState.IDLE:
                    isRun = false;
                    WarriorAnimator.SetBool("IsJump", false);
                    WarriorAnimator.SetBool("IsRun", false);
                    WarriorAnimator.SetFloat("RunSpeed", Direction.sqrMagnitude);
                    break;

                case WarriorState.JUMP:
                    isRun = false;
                    WarriorAnimator.SetBool("IsJump", true);
                    WarriorAnimator.SetBool("IsRun", false);
                    WarriorAnimator.SetFloat("RunSpeed", Direction.sqrMagnitude);
                    break;
            }
           
            yield return null;
        }
    }

    IEnumerator WarriorComboAttack()
    {
        while (!isDie)
        {


            switch (warriorComboState)
            {
                case WarriorComboState.COMBO1:
                    WarriorAnimator.SetBool("IsAttack_3", false);
                    WarriorAnimator.SetBool("IsAttack_2", false);
                    WarriorAnimator.SetBool("IsAttack_1", true);
                    //WarriorAnimator.SetTrigger("IsAttack_1");
                    break;

                case WarriorComboState.COMBO2:
                    WarriorAnimator.SetBool("IsAttack_1", false);
                    WarriorAnimator.SetBool("IsAttack_2", true);
                    isCombo1 = true;
                    break;

                case WarriorComboState.COMBO3:
                    WarriorAnimator.SetBool("IsAttack_2", false);
                    WarriorAnimator.SetBool("IsAttack_3", true);
                    isCombo2 = true;
                    break;

                case WarriorComboState.SHIELDBLOCK:
                    WarriorAnimator.SetBool("IsBlock", true);
                    break;

                case WarriorComboState.NONE:
                    WarriorAnimator.SetBool("IsAttack_1", false);
                    WarriorAnimator.SetBool("IsAttack_2", false);
                    WarriorAnimator.SetBool("IsAttack_3", false);
                    WarriorAnimator.SetBool("IsBlock", false);
                    break;

                default:
                    break;



            }
            yield return null;
        }
    }

}
