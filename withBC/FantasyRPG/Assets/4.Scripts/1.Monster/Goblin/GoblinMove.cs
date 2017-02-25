using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMove : MonoBehaviour {
    public GoblinAnimation GoblinAni;
    public Rigidbody G_body;

    public float walkSpeed = 1.0f;
    // Use this for initialization
    void Start()
    {
        GoblinAni = GetComponent<GoblinAnimation>();
        G_body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GoblinAni.NowState == GoblinAnimation.G_STATE.S_WALK ||
            GoblinAni.NowState == GoblinAnimation.G_STATE.S_RUN)
        {
            Move();
        }
    }

    void Move()
    {
        switch (GoblinAni.NowState)
        {
            case GoblinAnimation.G_STATE.S_WALK:
                Vector3 VecGoblin = (G_body.transform.forward * walkSpeed * Time.deltaTime) + G_body.transform.position;
                G_body.MovePosition(VecGoblin);

                //G_body.transform.LookAt(VecGoblin);
                break;
            case GoblinAnimation.G_STATE.S_RUN:
                Vector3 VecGoblin2 = (G_body.transform.forward * walkSpeed*1.5f * Time.deltaTime) + G_body.transform.position;
                G_body.MovePosition(VecGoblin2);

                //G_body.transform.LookAt(VecGoblin2);
                break;
        }
    }
}
