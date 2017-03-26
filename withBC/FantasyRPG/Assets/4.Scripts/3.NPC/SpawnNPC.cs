using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour {

    public GameObject NPC;
    public int NpcCount = 1;

    private int nowNpcCount = 0;
    private float CreatDelayTime = 0;

    public float CreatTime = 5.0f;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.NowScene == GameManager.SCENE.InGameScene)
        {
            if (nowNpcCount <= 0)
            {
                CreatDelayTime += Time.deltaTime;
                if (NpcCount > 0)
                {
                    if (CreatDelayTime > CreatTime)
                    {
                        CreatDelayTime = 0.0f;
                        NpcCount--;
                        nowNpcCount++;
                        Vector3 position = this.transform.position;
                        GameObject NpcObject = (GameObject)Instantiate(NPC, position, Quaternion.identity);
                        if(GameManager.Instance.Npc1 == null)
                        {
                            GameManager.Instance.Npc1 = NpcObject;
                        }
                        else if(GameManager.Instance.Npc2 == null)
                        {
                            GameManager.Instance.Npc2 = NpcObject;
                        }
                    }
                }
            }
        }
    }
}
