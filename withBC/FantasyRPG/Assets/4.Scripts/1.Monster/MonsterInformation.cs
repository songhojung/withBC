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

    public bool isDie;
    public bool isHit;
    public bool isAttack;

    public STATE MonsterState;

    public bool isOnceAttack;


    //public bool isFindPlayer;


    public int damage;



	// Use this for initialization
	void Start () {
        MonsterFind = GetComponent<MonsterFindPatroll>();
	}
	
	// Update is called once per frame
	void Update ()
    {

    
	}
}
