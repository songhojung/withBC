using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPC : MonoBehaviour {


    public enum PlayerJob { NONE, WARRIOR, ARCHER, WIZARD};
    public PlayerJob Job = PlayerJob.NONE;

    private Rigidbody rigibody;

    private float h = 0.0f;
    private float v = 0.0f;

    [System.NonSerialized]
    public Vector3 direction = Vector3.zero;
    [System.NonSerialized]
    public bool IsLeftMouseDown = false;
    [System.NonSerialized]
    public bool IsLeftMouseUp = false;
    [System.NonSerialized]
    public bool IsLeftMouseStay = false;
    [System.NonSerialized]
    public bool IsRightMouseDown = false;
    [System.NonSerialized]
    public bool IsJump = false;
    [System.NonSerialized]
    public bool IsNumKey_1 = false;
    [System.NonSerialized]
    public bool IsNumKey_2 = false;
    [System.NonSerialized]
    public bool IsKey_E = false;
    [System.NonSerialized]
    public bool IsKey_Q = false;
    [System.NonSerialized]
    public bool IsKey_Shift = false;
    [System.NonSerialized]
    public bool AttackState = false;

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;

    
    // Use this for initialization
    void Start () {
        rigibody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
