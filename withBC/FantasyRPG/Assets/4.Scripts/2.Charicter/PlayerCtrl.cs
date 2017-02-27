using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    // Use this for initialization
    public enum PlayerJob { NONE,WARRIOR,ARCHER,WIZARD};
    public PlayerJob Job = PlayerJob.NONE;

    private Rigidbody rigidbody;
    private GameObject camera;
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
    Quaternion rotate = Quaternion.identity;

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;

    void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = GameObject.Find("Main Camera");


    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //Debug.Log("h : " + h + ", v: " + v);
        direction = new Vector3(h, 0.0f, v);

        IsLeftMouseDown = Input.GetMouseButtonDown(0);
        IsLeftMouseUp = Input.GetMouseButtonUp(0);
        IsLeftMouseStay = Input.GetMouseButton(0);
        IsRightMouseDown = Input.GetMouseButtonDown(1);
        IsJump = Input.GetKeyDown(KeyCode.Space);
        

        
        //rotate.eulerAngles = h*Vector3.up * rotateSpeed;
        Vector3 forward = Vector3.Slerp(rigidbody.transform.forward,
                direction
                , rotateSpeed * Time.deltaTime);
        rigidbody.transform.LookAt(rigidbody.transform.position + forward);
        rigidbody.MovePosition((direction* moveSpeed*Time.deltaTime)+rigidbody.transform.position );
        //rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, rotate, Time.deltaTime*5.0f ));
        //rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, rotate, Time.deltaTime * 5.0f);
    }
}
