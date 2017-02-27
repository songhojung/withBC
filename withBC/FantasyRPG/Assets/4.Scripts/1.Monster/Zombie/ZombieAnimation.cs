﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour {

    public enum Z_STATE
    {
        Z_WALK, Z_ATTACK, Z_R_FALL,Z_B_FALL,Z_L_FALL
    };

    public Animator Zombie;
    public Z_STATE NowState;

    public int _health;

	// Use this for initialization
	void Start () {
        Zombie = GetComponent<Animator>();
        NowState = Z_STATE.Z_WALK;
        _health = Zombie.GetInteger("Health");
    }
	
	// Update is called once per frame
	void Update () {
            Zombie.SetInteger("Check_State", (int)NowState);
	}
}
