using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour {

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

    public int damage;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
