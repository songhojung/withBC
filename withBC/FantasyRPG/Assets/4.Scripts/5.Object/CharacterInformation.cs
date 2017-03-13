using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour {

    public int hp;

    public enum STATE
    {
        ATTACK, STAY, HIT
    };

    public bool isDie = false;
    public bool isHit = false;
    public bool isAttack = false;

    public STATE MonsterState = STATE.STAY;

    public bool isOnceAttack = false;

    public int damage;

    public enum MODE
    {
        NPC, PLAYER
    };

    public enum PlayerJob { NONE, WARRIOR, ARCHER, WIZARD };


    public PlayerJob Job = PlayerJob.NONE;
    public MODE _mode;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
