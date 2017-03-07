using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterInformation : MonoBehaviour {
    //몬스터 체력, 몬스터 상태, 죽었는지, 맞는중인지, 몬스터의 데미지

    private MonsterFindPatroll MonsterFind;

    public int hp;
    
    public enum STATE
    {
        ATTACK, STAY, HIT
    };

    public bool isDie = false;

    //한번 맞고서 아직 맞는상태일경우 트루
    //나머지 상태에서는 거짓
    public bool isHit = false;


    public bool isAttack = false;

    public STATE MonsterState = STATE.STAY;

    //공격중에 한번 상대가 맞을때까지는 트루
    //나머지 상태들과 상대가 나에게 한번 맞았을경우는 거짓
    public bool isOnceAttack = false;


    //public bool isFindPlayer;


    public int damage;



	// Use this for initialization
	void Start () {
        MonsterFind = GetComponent<MonsterFindPatroll>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch(MonsterState)
        {
            case STATE.ATTACK:
                break;
            case STATE.HIT:
                break;
            case STATE.STAY:
                break;
        }
    
	}
}
