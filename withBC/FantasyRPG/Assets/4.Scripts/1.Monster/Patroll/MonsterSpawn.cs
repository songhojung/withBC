using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour {

    public GameObject Monster;
    public int EnemyCount = 1;

    private int nowEnemyCount = 0;
    private float CreatDelayTime = 0;

    public float CreatTIme = 5.0f;

    // Use this for initialization
    void Start()
    {
        nowEnemyCount = EnemyCount;
        //Monster = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CreatDelayTime += Time.deltaTime;
        if (EnemyCount > 0)
        {
            if (CreatDelayTime > CreatTIme)
            {
                CreatDelayTime = 0.0f;
                EnemyCount--;
                Vector3 position = this.transform.position;
                Instantiate(Monster, position, Quaternion.identity);
            }
        }
    }
}
