using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour {

    public enum Z_STATE
    {
        Z_WALK, Z_ATTACK, Z_R_FALL,Z_B_FALL,Z_L_FALL
    };

    public Animator Zombie;
    public Z_STATE NowState;

    public int _health = 10;

    private MonsterInformation Information;

    public int damage;
    // Use this for initialization
    void Start () {
        Zombie = GetComponent<Animator>();
        NowState = Z_STATE.Z_WALK;
        //_health = Zombie.GetInteger("Health");

        Zombie.SetInteger("Health", _health);
        Information = GetComponent<MonsterInformation>();

    }
	
	// Update is called once per frame
	void Update () {
        Zombie.SetInteger("Check_State", (int)NowState);
        InformationCheck();
	}


    private void InformationCheck()
    {
        if (Information)
        {
            Information.hp = _health;

            Information.damage = damage;

            if (NowState == Z_STATE.Z_ATTACK)
            {
                if (Information.MonsterState != MonsterInformation.STATE.ATTACK)
                    Information.MonsterState = MonsterInformation.STATE.ATTACK;
                if (!Information.isAttack)
                {
                    Information.isAttack = true;
                    Information.isOnceAttack = true;
                }
            }
            else if (NowState == Z_STATE.Z_B_FALL || NowState == Z_STATE.Z_R_FALL ||
                        NowState == Z_STATE.Z_L_FALL)
            {
                if (Information.MonsterState != MonsterInformation.STATE.HIT)
                    Information.MonsterState = MonsterInformation.STATE.HIT;
            }
            else
            {
                if (Information.MonsterState != MonsterInformation.STATE.STAY)
                    Information.MonsterState = MonsterInformation.STATE.STAY;
            }
        }
    }

}
