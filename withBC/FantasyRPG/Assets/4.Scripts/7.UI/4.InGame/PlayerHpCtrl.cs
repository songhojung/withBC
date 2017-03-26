using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpCtrl : MonoBehaviour
{
    private UISlider UIHp;
    private CharacterInformation playerCharacterInfo;
    private float MaxHp;
	void Start ()
    {
        UIHp = GetComponent<UISlider>();
        playerCharacterInfo = GameManager.Instance.PlayerObject.GetComponent<CharacterInformation>();
        MaxHp = playerCharacterInfo.hp;
    }
	
	
	void Update ()
    {
        UIHp.sliderValue = playerCharacterInfo.hp / MaxHp;
      
    }
}
