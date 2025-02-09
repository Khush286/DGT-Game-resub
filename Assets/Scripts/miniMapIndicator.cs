﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/****************************** Project Header ******************************\
Script Name:  miniMapIndicator
Project:      DGT-Game Dungeon Runner
Author:       Khushwant Singh

Displays the Mini-Map, Holds the panels in an Array and finds the corresponding panel
for what room the Player is currently in, then changes the colour of that panel.

\***************************************************************************/

public class miniMapIndicator : MonoBehaviour
{
    public string currentRoom = ""; // The current game Scene
    int value = 0;
    public Transform[] childs;
    public GameObject[] childObjects; // Array to hold the panels

    void Start()
    {
        childs = gameObject.GetComponentsInChildren<Transform>(); // Put all children in an array
        childObjects = new GameObject[childs.Length];

        foreach (Transform trans in childs)
        {
            value++;
            childObjects.SetValue(trans.gameObject, value - 1);
        }
    }

    void Update()
    {
        currentRoom = SceneManager.GetActiveScene().name; // Retrieve the active scenes name
        foreach (GameObject go in childObjects)
        {
            if (go.name == currentRoom) // If current scene name is the same as a menu ui, change the menu ui's color to green
            {
                go.GetComponent<Image>().color = Color.green;
                break;
            }
        }

    }
}
