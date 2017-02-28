using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    PlayerCtrl playerCtrl;
    // Use this for initialization
    Dictionary<string, GameObject> WeaponResource = new Dictionary<string, GameObject>();
    void Start ()
    {
        playerCtrl = GetComponent<PlayerCtrl>();
        PlayerCtrl.PlayerJob Job = playerCtrl.Job;

        //Transform particleObj = (Transform)Instantiate(
        //    Resources.Load("Tank_tutorial/Prefabs/TankExplosion",
        //            typeof(Transform)), pos, Quaternion.identity);

       // Resources.UnloadUnusedAssets();
       //gameObject.transform.parent = 
    } 
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void ResourceLode()
    {
        object[] resource = Resources.LoadAll("Prefabs");

        for(int i = 0; i <resource.Length; i++)
        {
            GameObject item = (GameObject)(resource[i]);

            WeaponResource[item.name] = item;
        }
    }

    GameObject GetweaPon(string key)
    {
        return WeaponResource[key];
    }
}
