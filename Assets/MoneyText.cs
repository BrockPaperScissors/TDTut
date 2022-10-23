using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    public Text moneyDisplayed;

    // Update is called once per frame
    void Update()
    {
        moneyDisplayed.text = "$" + PlayerStats.Money.ToString();
    }
}
