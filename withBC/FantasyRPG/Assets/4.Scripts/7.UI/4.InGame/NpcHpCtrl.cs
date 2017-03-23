using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHpCtrl : MonoBehaviour
{
    public enum HpTargetNpc { Wizard, Archor, Warrior};
    public HpTargetNpc hpTargetNpc = HpTargetNpc.Warrior;

    private UISlider UIHp;
    private CharacterInformation NpcCharacterInfo;


    void Start()
    {
        UIHp = GetComponent<UISlider>();

        if(hpTargetNpc == HpTargetNpc.Warrior)
            NpcCharacterInfo = GameManager.Instance.Npc1.GetComponent<CharacterInformation>();
        else if(hpTargetNpc == HpTargetNpc.Archor)
            NpcCharacterInfo = GameManager.Instance.Npc2.GetComponent<CharacterInformation>();
    }


    void Update()
    {
        UIHp.sliderValue = NpcCharacterInfo.hp * 0.01f;

    }
}
