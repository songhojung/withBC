using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectCtrl : MonoBehaviour {

    public enum ThrowObjectType { ARROW, FIREBALL, FIRE,LIGHTNING, DRAGONFIRE}
    public ThrowObjectType throwObjType; 

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
        Vector3 moveVec = (tr.forward* moveSpeed * Time.deltaTime);
        tr.position = tr.position+moveVec;
    }
    
}
