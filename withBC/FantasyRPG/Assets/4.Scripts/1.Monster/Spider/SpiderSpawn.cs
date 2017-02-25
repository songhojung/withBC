using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawn : MonoBehaviour {

    public GameObject spider;
    public int EnemyCount = 3;

    private int nowEnemyCount = 0;
    private float CreatDelayTime = 0;

    // Use this for initialization
    void Start()
    {
        //spider = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CreatDelayTime += Time.deltaTime;
        if (EnemyCount>0)
        {
            if (CreatDelayTime > 0.5f)
             {
                 CreatDelayTime = 0.0f;
                EnemyCount--;
                 Vector3 position = this.transform.position;
                 Instantiate(spider, position, Quaternion.identity);
             }
        }
        //CreatDelayTime += Time.deltaTime;
        //if (nowEnemyCount < EnemyCount) // 적이 일정수 이하이면 생성허라
        //{
        //    if (CreatDelayTime > 0.5f)
        //    {
        //        CreatDelayTime = 0.0f;
        //        ++nowEnemyCount;
        //        Vector3 position = this.transform.position;
        //        Instantiate(spider, position, Quaternion.identity);
        //    }
        //}
        //else //아니면 생성하지마라
        //{
        //    CreatDelayTime = 0.0f;
        //    nowEnemyCount = 0;
        //}
        //if(Input.GetKeyDown(KeyCode.Space))
        //      {

        //          Vector3 position = new Vector3(300, Random.Range(-220, 220), 0);
        //          Instantiate(enemy, position, Quaternion.identity);
        //      }
    }
}
