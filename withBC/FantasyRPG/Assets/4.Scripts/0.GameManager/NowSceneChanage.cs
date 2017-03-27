using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowSceneChanage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(GameManager.Instance.NowScene != GameManager.SCENE.BossScene)
                GameManager.Instance.NowScene = GameManager.SCENE.BossScene;
        }
    }
}
