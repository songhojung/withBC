using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectCtrl : MonoBehaviour {



    private Transform tr;
    public float moveSpeed = 20.0f;

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
       

    }

    // Update is called once per frame
    void Update()
    {
       

        Vector3 moveVec = (-Vector3.up * moveSpeed * Time.deltaTime);

        tr.Translate(moveVec);
    }
}
