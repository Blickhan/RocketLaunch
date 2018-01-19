using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    RocketMovement rocket;
    static public int currency;
    public int currencyThisRound;
    int totalCurrency;
    bool currencySet;

    // Use this for initialization
    void Start()
    {
        GameObject player_go = GameObject.FindGameObjectWithTag("Player");
        if (player_go == null)
        {
            Debug.LogError("Could not find an object with tag 'Player'.");
        }
        rocket = player_go.GetComponent<RocketMovement>();
        currencySet = false;
        currencyThisRound = 0;
        currency = PlayerPrefs.GetInt("currency", 0);
    }

    void OnDestroy()
    {
        currency = totalCurrency;
        PlayerPrefs.SetInt("currency", currency);
    }

    // Update is called once per frame
    void Update()
    {
        if (!rocket.dead)
        {
            currencySet = false;
            currencyThisRound = Score.height / 10;
            totalCurrency = currency + currencyThisRound;
        }

        if (rocket.dead && !currencySet)
        {
            currency = totalCurrency;
            currencySet = true;
        }        
        
        GameObject.FindGameObjectWithTag("Currency").GetComponent<Text>().text = "$" + totalCurrency.ToString();
        //currency += score.height;
    }
}
