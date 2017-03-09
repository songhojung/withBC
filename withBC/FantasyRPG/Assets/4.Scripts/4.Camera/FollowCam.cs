using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject Target;
    private Transform cameraTr;

    public float distance = 5.0f;
    public float height = 5.0f;
    public float zoomHeight = 0;
    public float trace = 5.0f;
    public float cameraAngle = 25.0f;


    private float moveRight = 0.0f;
    private float Height_fix = 15.0f;
    private Vector3 rotate = Vector3.zero;
    public Quaternion CameraXRot;
    Quaternion b;
    // Use this for initialization
    void Start ()
    {
        cameraTr = GetComponent<Transform>();
        //rotate = new Vector3(cameraAngle, 0.0f, 0);
        // cameraTr.Rotate(rotate);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        PlayerCtrl pPlayerCtrl = Target.GetComponent<PlayerCtrl>();
        PlayerCtrl.PlayerJob Job = pPlayerCtrl.Job;

        if (Job == PlayerCtrl.PlayerJob.ARCHER)
        {
            if (pPlayerCtrl.IsLeftMouseStay)
            {
                //Vector3 vecRotate = new Vector3(0, Target.transform.eulerAngles.y, 0);
                //Quaternion TurnRotation = Quaternion.Euler(vecRotate);
                //cameraTr.rotation = TurnRotation;
                distance = 0.1f;
                height = 5.0f;
                zoomHeight = 0.0f;
                
            }
            else if (pPlayerCtrl.IsLeftMouseUp)
            {
                distance = 17.0f;
                height = 9.0f;
                zoomHeight = 0.0f;

            }
        }

        // lookat 으로 캐릭터 볼떄
        //cameraTr.position = Vector3.Lerp(cameraTr.position,
        //    Target.transform.position - (Target.transform.forward * distance) + (Target.transform.up * (height+zoomHeight))
        //   , Time.deltaTime * trace);

        cameraTr.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 10.0f, Space.World);
        CameraXRot = cameraTr.rotation;
        cameraTr.Rotate(Vector3.left    * Input.GetAxis("Mouse Y") * 10.0f, Space.Self);


        // 위아래 회전 제한
        if (cameraTr.eulerAngles.x > 0 && cameraTr.eulerAngles.x < 75)
        {
            if (cameraTr.eulerAngles.x >= 65)
            {
                cameraTr.eulerAngles = new Vector3(65.0f, cameraTr.eulerAngles.y, 0);
            }
        }
        if (cameraTr.eulerAngles.x < 360 && cameraTr.eulerAngles.x > 290)
        {
            if (cameraTr.eulerAngles.x <= 300)
            {
                cameraTr.eulerAngles = new Vector3(300.0f, cameraTr.eulerAngles.y, 0);
            }
        }

        // height-(Height_fix * (cameraTr.eulerAngles.x/360) : x축회전시 화면상에 캐릭터가 중심으로 가게 하기위함
        cameraTr.position = Target.transform.position - (cameraTr.forward * distance) +
            (cameraTr.up * (height /*- (Height_fix * (cameraTr.eulerAngles.x / 360))*/));

        //============================
        // rotateAround로 캐릭터를 볼떄

        //cameraTr.position = Vector3.Lerp(cameraTr.position,
        //   Target.transform.position - (cameraTr.forward * distance) + (cameraTr.up * (height + zoomHeight))
        //  , Time.deltaTime * trace);

        //cameraTr.RotateAround(Target.transform.position, Vector2.up, Time.deltaTime * 40.0f * Input.GetAxis("Mouse X"));
        //cameraTr.RotateAround(Target.transform.position - (Target.transform.forward * distance)*2
        //    , -cameraTr.right, Time.deltaTime * 40.0f * Input.GetAxis("Mouse Y"));


        //cameraTr.LookAt(Target.transform.position + (Target.transform.up * 5) );



        //cameraTr.rotation = Target.transform.rotation;
        //cameraTr.rotation = Quaternion.Slerp(cameraTr.rotation,
        //    Target.transform.rotation/* * Quaternion.Euler(20,0,0)*/,
        //   trace * Time.deltaTime);






        //cameraTr.RotateAround(Target.position, Vector3.up,30.0f);
        //cameraTr.localRotation = Quaternion.AngleAxis(45.0f, Vector3.right);
        //cameraTr.localRotation = Quaternion.Euler(45.0f, 1.0f * Time.deltaTime * 20.0f * Input.GetAxis("Mouse X"),0.0f);
        // cameraTr.rotation = Quaternion.Euler(45.0f, Time.deltaTime * 20.0f * Input.GetAxis("Mouse X"), 0);


    }
}
