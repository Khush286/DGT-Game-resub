using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************** Project Header ******************************\
Script Name:  CoinScript
Project:      DGT-Game Dungeon Runner
Author:       Khushwant Singh

The script for Coins to check if they collid with the player

\***************************************************************************/

public class CoinScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") // Checks if colidded with player
        {
            CoinTextScript.coinAmount += 1; // Add another coin to the Player
            Destroy(gameObject);
            
        }
    }
}
 