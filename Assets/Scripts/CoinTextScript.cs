using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/****************************** Project Header ******************************\
Script Name:  CoinTextScript
Project:      DGT-Game Dungeon Runner
Author:       Khushwant Singh

Displays the amount of coins the Player has as text.

\***************************************************************************/

public class CoinTextScript : MonoBehaviour
{
    Text text;
    [SerializeField] public static int coinAmount; // The Players coins

    void Start()
    {
        text = GetComponent<Text>(); // Retrieve the text component
    }

    void Update()
    {
        text.text = "Coins: " + coinAmount.ToString() + "/30"; // Dislays the Players coin amount
    }
}
