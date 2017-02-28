using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2Move : MonoBehaviour {

    public Monster2Animation Monster2Ani;
    public Rigidbody Monster2_body;

    public float walkSpeed = 1.0f;
    // Use this for initialization
    void Start()
    {
        Monster2Ani = GetComponent<Monster2Animation>();
        Monster2_body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Monster2Ani.NowState == Monster2Animation.M2_STATE.M2_WALK ||
            Monster2Ani.NowState == Monster2Animation.M2_STATE.M2_RUN)
        {
            Move();
        }
    }

    void Move()
    {
        switch (Monster2Ani.NowState)
        {
            case Monster2Animation.M2_STATE.M2_WALK:
                Vector3 VecGoblin = (Vector3.forward * walkSpeed * Time.deltaTime) + Monster2_body.transform.position;
                Monster2_body.MovePosition(VecGoblin);

                //Monster2_body.transform.LookAt(VecGoblin);

                break;
            case Monster2Animation.M2_STATE.M2_RUN:
                Vector3 VecGoblin2 = (Monster2_body.transform.forward * walkSpeed * 1.5f * Time.deltaTime) + Monster2_body.transform.position;
                Monster2_body.MovePosition(VecGoblin2);

                //Monster2_body.transform.LookAt(VecGoblin2);

                break;
        }
    }
}
