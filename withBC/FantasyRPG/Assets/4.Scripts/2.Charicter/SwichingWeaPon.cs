using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichingWeaPon : MonoBehaviour
{

    public delegate void SwitchWeaponEvent();
    // Use this for initialization
    void Start()
    {
        // SwithcingWeapon();
        
    }

    public static void SwithcingWeapon()
    {
        new WaitForSeconds(12.0f);
        
        GameObject Weapon_2 = GameObject.Find("Bip01 R Hand").transform.FindChild("Dagger").gameObject;
        GameObject Weapon_1 = GameObject.Find("Bip01 R Hand").transform.FindChild("Magic Staff").gameObject;

        if (Weapon_1 || Weapon_2)
        {
            if(Weapon_1.active)
            {
                Weapon_1.SetActive(false);
                Weapon_2.SetActive(true);
            }
            else if (Weapon_2.active)
            {
                Weapon_2.SetActive(false);
                Weapon_1.SetActive(true);
            }
            Debug.Log("할당됨");
           
        }
    }

 

    public static IEnumerator siwtchweapon()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject Weapon_2 = GameObject.Find("Bip01 R Hand").transform.FindChild("Dagger").gameObject;
        GameObject Weapon_1 = GameObject.Find("Bip01 R Hand").transform.FindChild("Magic Staff").gameObject;

        if (Weapon_1 || Weapon_2)
        {
            if (Weapon_1.active)
            {
                Weapon_1.SetActive(false);
                Weapon_2.SetActive(true);
            }
            else if (Weapon_2.active)
            {
                Weapon_2.SetActive(false);
                Weapon_1.SetActive(true);
            }
            Debug.Log("할당됨");

        }
    } 

    
}
