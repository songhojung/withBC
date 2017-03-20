using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;

    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                
                GameObject newGameManager = new GameObject("GameManager");
                sInstance = newGameManager.GetComponent<GameManager>();
            }
            return
                sInstance;
        }
    }

    private void Awake()
    {
        sInstance = this;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log("aaaaaaa");
        }
    }

    // ===============  선언부 =====================//
}
