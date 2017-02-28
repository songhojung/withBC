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
    // Use this for initialization
    void Start () {
        Mt3 = GetComponent<Animator>();
        NowState = M3_STATE.M3_STAY;
        _health = Mt3.GetInteger("Health");
        NowWay = (M3_WAY)Mt3.GetInteger("Way");
        AttackNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Mt3.SetInteger("State", (int)NowState);
        ChangeAnimation();
        AttackCheck();
        CheckWay();
        //HitbyChar();
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
}
