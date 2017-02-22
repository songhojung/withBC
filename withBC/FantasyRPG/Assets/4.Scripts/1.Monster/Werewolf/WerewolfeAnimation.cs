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

    Vector3 velocity;

    private float h = 0;
    private float v = 0;
    private int ClipNum = 0;
    public W_STATE NowState = W_STATE.S_STAND;
    // Use this for initialization
    void Start()
    {
        Werewolf = this.gameObject.GetComponentInChildren<Animation>();
        ClipNum = Werewolf.GetClipCount();
        //Debug.Log("클립갯수:" + ClipNum.ToString());

        //pcControll = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMotion();
        Animation_Play3();
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
        //Werewolf.CrossFade(PlayerAni[(int)NowState].name, 0.3f);
        {
            switch (NowState)
            {
                case W_STATE.S_STAND:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Standby", 0.3f);
                    break;
                case W_STATE.S_ATT1:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Attack1", 0.3f);
                    break;
                case W_STATE.S_ATT2:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Attack2", 0.3f);
                    break;
                case W_STATE.S_ATT3:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Attack3", 0.3f);
                    break;
                case W_STATE.S_ATT20:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Attack20", 0.3f);
                    break;
                case W_STATE.S_ATT_STAND:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Attack_standby", 0.3f);
                    break;
                case W_STATE.S_BEATEN:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Beaten", 0.3f);
                    break;
                case W_STATE.S_DEATH:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Death", 0.3f);
                    break;
                case W_STATE.S_JUMP:
                    Werewolf.wrapMode = WrapMode.Loop;
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
                    Werewolf.wrapMode = WrapMode.Loop;
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
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("Victory", 0.3f);
                    break;
                case W_STATE.S_RECOVER:
                    Werewolf.wrapMode = WrapMode.Loop;
                    Werewolf.CrossFade("recover", 0.3f);
                    break;
            }
        }
    }
}
