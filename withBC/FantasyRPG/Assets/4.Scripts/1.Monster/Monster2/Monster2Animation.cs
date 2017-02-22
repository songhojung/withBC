using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2Animation : MonoBehaviour {

    public enum M2_STATE
    {
        M2_STAY, M2_ATTACK, M2_WALK, M2_ROAR,
        M2_RUN, M2_HIT
    };

    public enum M2_WAY
    {
        M2_F, M2_B, M2_L, M2_R
    };

    public M2_STATE NowState;
    public M2_WAY NowWay;
    public int AttackNum;
    public int _health;
    public int _hitNum;
    Animator Mt2;
    // Use this for initialization
    void Start()
    {
        Mt2 = GetComponent<Animator>();
        NowState = M2_STATE.M2_STAY;
        _health = Mt2.GetInteger("Health");
        NowWay = (M2_WAY)Mt2.GetInteger("Way");
        AttackNum = 0;
        _hitNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnimation();
        AttackCheck();
        CheckWay();
        //HitbyChar();
    }

    void AttackCheck()
    {
        if (NowState == M2_STATE.M2_ATTACK)
        {
            if (AttackNum != Mt2.GetInteger("AttCheck"))
            {
                Mt2.SetInteger("AttCheck", AttackNum);
            }
        }
    }

    void ChangeAnimation()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (NowState == M2_STATE.M2_HIT)
                NowState = M2_STATE.M2_STAY;
            else
                NowState++;
            Mt2.SetInteger("State", (int)NowState);
        }
    }

    void CheckWay()
    {
        if (NowState == M2_STATE.M2_WALK)
        {
            if ((int)NowWay != Mt2.GetInteger("Way"))
            {
                Mt2.SetInteger("Way", (int)NowWay);
            }
        }
    }

    void HitbyChar()
    {
        _health--;
        Mt2.SetInteger("Health", _health);
    }
}
