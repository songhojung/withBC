using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSetting : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(GameManager.Instance.NowScene == GameManager.SCENE.TitleScene)
        {
            GameManager.Instance.SendMessage("SettingUISelectWindow");
        }
	}
}
