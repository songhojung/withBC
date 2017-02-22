using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3Move : MonoBehaviour {

    public Monster3Animation Monster3Ani;
    public Rigidbody Monster3_body;

    public float walkSpeed = 1.0f;
    // Use this for initialization
    void Start()
    {
        Monster3Ani = GetComponent<Monster3Animation>();
        Monster3_body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Monster3Ani.NowState == Monster3Animation.M3_STATE.M3_WALK ||
            Monster3Ani.NowState == Monster3Animation.M3_STATE.M3_RUN)
        {
            Move();
        }
    }

    void Move()
    {
        switch (Monster3Ani.NowState)
        {
            case Monster3Animation.M3_STATE.M3_WALK:
                Vector3 VecGoblin = (Vector3.forward * walkSpeed * Time.deltaTime) + Monster3_body.transform.position;
                Monster3_body.MovePosition(VecGoblin);
                break;
            case Monster3Animation.M3_STATE.M3_RUN:
                Vector3 VecGoblin2 = (Vector3.forward * walkSpeed * 1.5f * Time.deltaTime) + Monster3_body.transform.position;
                Monster3_body.MovePosition(VecGoblin2);
                break;
        }
    }
}
