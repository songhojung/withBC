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
        if (GameManager.Instance.Gold < 0)
            GameManager.Instance.Gold = 0;
        coinLabel.text = GameManager.Instance.Gold.ToString();
    }


}
