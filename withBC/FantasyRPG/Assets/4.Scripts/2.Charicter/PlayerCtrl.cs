using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    // Use this for initialization
    public enum PlayerJob { NONE,WARRIOR,ARCHER,WIZARD};
    private CharacterInformation.PlayerJob Job;

    private Rigidbody rigidbody;
   
    private float h = 0.0f;
    private float v = 0.0f;
    private bool IsShot = false;
    private Vector3 PrevPosition = Vector3.zero;

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

    public GameObject cameraLookAt;


    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;

    private Transform BipTr;

    void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();

        Job = GetComponent<CharacterInformation>().Job;
        BipTr = GameObject.Find("Bip01").gameObject.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        IsLeftMouseDown = Input.GetMouseButtonDown(0);
        IsLeftMouseUp = Input.GetMouseButtonUp(0);
        IsLeftMouseStay = Input.GetMouseButton(0);
        IsRightMouseDown = Input.GetMouseButtonDown(1);
        IsJump = Input.GetKeyDown(KeyCode.Space);
        IsNumKey_1 = Input.GetKeyDown(KeyCode.Alpha1);
        IsNumKey_2 = Input.GetKeyDown(KeyCode.Alpha2);
        IsKey_E = Input.GetKeyDown(KeyCode.E);
        IsKey_Q = Input.GetKeyDown(KeyCode.Q);
        IsKey_Shift = Input.GetKeyDown(KeyCode.LeftShift);


        h = Input.GetAxis("Horizontal");//a,d
        v = Input.GetAxis("Vertical");// w,s

        direction  = new Vector3(h, 0.0f, v);

        
        Vector3 cameraFoward = new Vector3(cameraLookAt.transform.forward.x, 0.0f, cameraLookAt.transform.forward.z).normalized;
        Vector3 moveDir = (cameraFoward * v) + (cameraLookAt.transform.right * h);


      



        Vector3 forward = Vector3.Slerp(rigidbody.transform.forward,
                moveDir , rotateSpeed * Time.deltaTime);


        if (Job == CharacterInformation.PlayerJob.ARCHER && IsLeftMouseStay) // 마우수 왼쪽누를떄 캐릭터 회전가능 ... 조준용
        {
            Vector3 vecRotate = Vector3.zero;
            vecRotate = new Vector3(cameraLookAt.transform.eulerAngles.x, cameraLookAt.transform.eulerAngles.y, 0);
            Quaternion TurnRotation = Quaternion.Euler(vecRotate);

            rigidbody.MoveRotation(TurnRotation);
            
            IsShot = false;
        }
        else if(Job == CharacterInformation.PlayerJob.ARCHER && IsLeftMouseUp)
        {
            IsShot = true;
            //vecRotate = new Vector3(0, cameraLookAt.transform.eulerAngles.y, 0);
            //TurnRotation = Quaternion.Euler(vecRotate);
            //rigidbody.MoveRotation(TurnRotation);
           
        }
        else // 아닐떄는 키보드로만 캐릭터 회전
        {

            //rigidbody.transform.LookAt(rigidbody.transform.position + camera.transform.forward);
            if(IsShot)
            {

            }
            PrevPosition = rigidbody.transform.eulerAngles;
            rigidbody.transform.LookAt(rigidbody.transform.position + forward); 

        }

        //캐릭터 봐라보는방향 변경
    
        
        //마우스로 캐릭터회전
        //rigidbody.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * Input.GetAxis("Mouse X") * 20);
        // tr.forward 방향기준으로 이동
        rigidbody.MovePosition(rigidbody.transform.position + (moveDir * moveSpeed * Time.deltaTime));


    }
}
