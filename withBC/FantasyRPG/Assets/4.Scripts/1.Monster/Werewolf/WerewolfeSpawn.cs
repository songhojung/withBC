using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfeSpawn : MonoBehaviour {

    public GameObject Werewolf;
    public int EnemyCount = 3;

    private int nowEnemyCount = 0;
    private float CreatDelayTime = 0;

    // Use this for initialization
    void Start()
    {
        //Werewolf = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CreatDelayTime += Time.deltaTime;
        if (EnemyCount > 0)
        {
            if (CreatDelayTime > 0.5f)
            {
                CreatDelayTime = 0.0f;
                EnemyCount--;
                Vector3 position = this.transform.position;
                Instantiate(Werewolf, position, Quaternion.identity);
            }
        }
    }
}
