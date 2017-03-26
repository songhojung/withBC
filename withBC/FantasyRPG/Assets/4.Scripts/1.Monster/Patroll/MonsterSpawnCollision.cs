using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnCollision : MonoBehaviour
{
    public GameObject[] MonsterSpawnPoints;

    private void Start()
    {
       // MonsterSpawnPoints = GetComponentsInChildren<Transform>();
    }


    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player") && collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            for(int i = 0; i < MonsterSpawnPoints.Length; i++)
            {
                if (!MonsterSpawnPoints[i].gameObject.activeSelf)
                {
                    MonsterSpawnPoints[i].gameObject.SetActive(true);
                }
            }
        }
    }

}
