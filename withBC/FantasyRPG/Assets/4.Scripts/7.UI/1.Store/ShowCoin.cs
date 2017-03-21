using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCoin : MonoBehaviour
{

    private UILabel coinLabel;

    private void Start()
    {
        coinLabel = GetComponent<UILabel>();
        coinLabel.text = GameManager.Instance.Gold.ToString();
    }

    private void Update()
    {
        coinLabel.text = GameManager.Instance.Gold.ToString();
    }


}
