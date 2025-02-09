﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****************************** Project Header ******************************\
Script Name:  Player
Project:      DGT-Game Dungeon Runner
Author:       Khushwant Singh

Handles the players interaction with the gamespace and Cheating if it's enabled.

\***************************************************************************/

public class Player : MonoBehaviour
{
    public static int playerHealth = 3; // The Players current health
    public Rigidbody2D rb;
    public GameObject menuUI;
    private GameObject[] checkForBots;
    public bool botsExist;
    public GameObject particleEffect;

    private void checkForHealth() // Checks if the player has 0 health
    {
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void SpawnParticleEffect(Vector2 spawnPosition) // Created particle effect
    {
        GameObject Particle = Instantiate(particleEffect, spawnPosition, Quaternion.identity) as GameObject;
        Destroy(Particle, 3);
    }

    private void DoorInteraction(int roomToAccess) // Load room of the door the Player interacted with
    {
        SceneManager.LoadScene("Room" + roomToAccess);
        KeyTextScript.keyAmount -= 1; // Player loses 1 key
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Door" && KeyTextScript.keyAmount >= 1 && IsThisArrayEmpty(checkForBots)) // Checks for door interaction and if there's any bots stll alive
        {
            DoorInteraction(other.GetComponent<Door>().roomToAccess);
        }
        else if (other.tag == "LockedDoor" && CoinTextScript.coinAmount >= 30 && KeyTextScript.keyAmount >= 1 && IsThisArrayEmpty(checkForBots)) // Check for final door in room 18
        {
            DoorInteraction(other.GetComponent<Door>().roomToAccess);

        }
        else if (other.name == "PlayerCollider")
        {
            playerHealth -= 1;
            SpawnParticleEffect(other.gameObject.transform.position);
            other.GetComponentInParent<EnemyFollow>().health = 0;
            other.GetComponentInParent<EnemyFollow>().changeHealth();
            checkForHealth();
        }
        else if (other.name == "HeartItem" && playerHealth != 3) // Check if collided object is a heart item
        {
            playerHealth += 1;
            Destroy(other.gameObject);
        }
    }

    private bool IsThisArrayEmpty(Array array) // Function to check if inputted array is empy
    {
        if (array == null || array.Length == 0)
        {
            return true;
        }
        else { return false; }
    }

    void Update()
    {
        Cheats();
        checkForBots = GameObject.FindGameObjectsWithTag("Bot");
    }

    private void Cheats() // Check if cheats are enabled and watch for hotkeys that give the player coins,hearts,keys
    {
        if(PauseMenu.CheatsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.J) && playerHealth <= 2 && playerHealth >= 1)
            {
                playerHealth += 1;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                KeyTextScript.keyAmount += 1;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                CoinTextScript.coinAmount += 1;
            }
            if (Input.GetKeyDown(KeyCode.H) && IsThisArrayEmpty(checkForBots))
            {
                Debug.Log("if");
            }
            else if (Input.GetKeyDown(KeyCode.H) && !IsThisArrayEmpty(checkForBots))
            {
                Debug.Log("else if");
            }
        }

    }
}
