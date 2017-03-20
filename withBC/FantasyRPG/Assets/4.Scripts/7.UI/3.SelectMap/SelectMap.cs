using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    public GameObject missionLabel;
	// Use this for initialization
	

    void OnSelectMapWindow()
    {
        gameObject.active = true;
    }

    void OffSelectMapWindow()
    {
        gameObject.active = false;
    }

    void OnOffMissionScript()
    {
       if(missionLabel && !missionLabel.activeSelf)
        {
            missionLabel.SetActive(true);
        }
       else if(missionLabel && missionLabel.activeSelf)
        {
            missionLabel.SetActive(false);
        }
    }

    void CheckConfirmButton()
    {
        if(missionLabel && missionLabel.activeSelf)
        {
            SceneManager.LoadScene("map_1");
        }
    }

    void CloseButton()
    {
        gameObject.SetActive(false);
    }

}
