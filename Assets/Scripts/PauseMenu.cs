using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/****************************** Project Header ******************************\
Script Name:  PauseMenu
Project:      DGT-Game Dungeon Runner
Author:       Khushwant Singh

Handles all of the User Interface and saving/loading.

\***************************************************************************/

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // UI for pausing
    public GameObject mainMenuUI; // UI for the main menu
    public GameObject gameOverMenuUI; // UI for the game over screen
    public GameObject settingsUI;
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;
    public static bool SettingsIsDisplayed = false;
    public static bool CheatsEnabled = false;

void Start()
    {
        settingsUI.SetActive(false);
        SettingsIsDisplayed = false;
        GameIsOver = (false);
        mainMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
        Resume();

        if (SceneManager.GetActiveScene().name == "MainMenu") // Checks if the main menu is loaded
        {
            mainMenuUI.SetActive(true);
        }
    }
    void Update()
    {   
        checkForGameOver();

        if (Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().name != "MainMenu")) // Checks if the escape key is pressed during the game
        {
            if (GameIsPaused) // If the game is paused, resume
            {
                Resume();
            }
            else if (!GameIsOver) // If the game is playing, pause
            {
                Pause();
            }
        }
    }
    public void Resume() // Function for resuming the game if it was paused
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() // Function for pausing the game if it was playing
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    public void checkForGameOver() // Checks if the player health is less than or equal too zero
    {
        if (Player.playerHealth <= 0 && GameIsOver != true && GameIsPaused != true && SceneManager.GetActiveScene().name != "MainMenu")
        {
            GameOver();
        }
    }

    public void GameOver() // Function for running game over tasks, like activating the game over menu
    {
        gameOverMenuUI.SetActive(true);
        GameIsOver = true;
        GameIsPaused = false;
    }

    public void LoadMenu() // Function for loading the main menu
    {
        Time.timeScale = 1f;
        Debug.Log("Loading Menu...");
        GameIsOver = (false);
        gameOverMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() // The command to exit the .exe compiled version of the game, does nothing if the game isn't properly compiled
    {
        Debug.Log("Game was quit");
        Application.Quit();
    }

    public void StartGame() // Function for starting the game
    {
        SettingsIsDisplayed = false;
        GameIsPaused = false;
        Player.playerHealth = 3; // Sets player health to default; 3
        KeyTextScript.keyAmount = 0; // Sets player keys to 0
        CoinTextScript.coinAmount = 0; // Sets player coins to 0
        SceneManager.LoadScene("Room0"); // Loads the first room
    }

    public void EnableCheats() // Function for handling the UI for enabling cheats
    {
        if (CheatsEnabled)
        {
            CheatsEnabled = false;
            settingsUI.transform.Find("EnableCheatsButton").transform.Find("Text").GetComponent<Text>().text = "Enable Cheats: OFF";
        }
        else
        {
            CheatsEnabled = true;
            settingsUI.transform.Find("EnableCheatsButton").transform.Find("Text").GetComponent<Text>().text = "Enable Cheats: ON";
        }
    }

    public void DisplaySettings() // Function for handlin the UI for displaying the settings menu
    {
        if (CheatsEnabled)
        {
            settingsUI.transform.Find("EnableCheatsButton").transform.Find("Text").GetComponent<Text>().text = "Enable Cheats: ON";
        }
        else
        {
            settingsUI.transform.Find("EnableCheatsButton").transform.Find("Text").GetComponent<Text>().text = "Enable Cheats: OFF";
        }
        if (SettingsIsDisplayed)
        {
            settingsUI.SetActive(false);
            SettingsIsDisplayed = false;
        }
        else
        {
            SettingsIsDisplayed = true;
            settingsUI.SetActive(true);
        }
    }

    public void DeleteSaveFile() // Function for handling the button to delete an save file
    {
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("Keys", 0);
        PlayerPrefs.SetInt("CurrentHealth", 3);
        PlayerPrefs.SetString("CurrentRoom", "Room0");
        PlayerPrefs.Save();
    }

    public void SaveGame() // Function for handling the button to save the current game state
    {
        PlayerPrefs.SetInt("Coins", CoinTextScript.coinAmount);
        PlayerPrefs.SetInt("Keys", KeyTextScript.keyAmount);
        PlayerPrefs.SetInt("CurrentHealth", Player.playerHealth);
        PlayerPrefs.SetString("CurrentRoom", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }
    public void LoadGame() // Function that retrieves a saved game state and loads it
    {
        if(PlayerPrefs.GetString("CurrentRoom") != "MainMenu")
        {
            CoinTextScript.coinAmount = PlayerPrefs.GetInt("Coins");
            KeyTextScript.keyAmount = PlayerPrefs.GetInt("Keys");
            Player.playerHealth = PlayerPrefs.GetInt("CurrentHealth");
            SceneManager.LoadScene(PlayerPrefs.GetString("CurrentRoom"));
            Resume();
        }
    }
}
