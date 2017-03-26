using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpCtrl : MonoBehaviour
{
    private UISlider UIHp;
    private CharacterInformation playerCharacterInfo;
    float hp = 1000;
	void Start ()
    {
        UIHp = GetComponent<UISlider>();
        playerCharacterInfo = GameManager.Instance.PlayerObject.GetComponent<CharacterInformation>();

    }
	
	
	void Update ()
    {
        UIHp.sliderValue = playerCharacterInfo.hp * 0.01f;
      
    }
}
