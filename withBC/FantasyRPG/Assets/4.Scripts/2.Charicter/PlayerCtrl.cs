using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    // Use this for initialization
    public enum PlayerJob { NONE,WARRIOR,ARCHER,WIZARD};
    public PlayerJob Job = PlayerJob.NONE;

    private Rigidbody rigidbody;
   
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

    public GameObject camera;

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;

    void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
      


    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //Debug.Log("h : " + h + ", v: " + v);
       
        direction = new Vector3(h, 0.0f, v);
        Vector3 playerTr = Vector3.zero;
        playerTr = rigidbody.transform.TransformDirection(direction); // *중요 -로컬좌표를 월드좌표로 변환해줌



        IsLeftMouseDown = Input.GetMouseButtonDown(0);
        IsLeftMouseUp = Input.GetMouseButtonUp(0);
        IsLeftMouseStay = Input.GetMouseButton(0);
        IsRightMouseDown = Input.GetMouseButtonDown(1);
        IsJump = Input.GetKeyDown(KeyCode.Space);

        
        Vector3 vecRotate = Vector3.zero;
        vecRotate = new Vector3(0, camera.transform.eulerAngles.y, 0);

        Quaternion TurnRotation = Quaternion.Euler(vecRotate);

        Vector3 forward = Vector3.Slerp(rigidbody.transform.forward,
                   playerTr
                   , rotateSpeed * Time.deltaTime);
        
        //rotate.eulerAngles = h * Vector3.up * rotateSpeed;

        //Vector3 forward = Vector3.Slerp(rigidbody.transform.forward,
        //        direction
        //        , rotateSpeed * Time.deltaTime);
        //rigidbody.transform.LookAt(rigidbody.transform.position + forward);


        if (Job == PlayerJob.ARCHER && IsLeftMouseStay) // 마우수 왼쪽누를떄 캐릭터 회전가능 ... 조준용
        {
           rigidbody.MoveRotation(TurnRotation);
        }
        else // 아닐떄는 키보드로만 캐릭터 회전
        {
            rigidbody.transform.LookAt(rigidbody.transform.position + forward);
        }

        rigidbody.MovePosition(rigidbody.transform.position + (playerTr * moveSpeed * Time.deltaTime));
       
    }
}
