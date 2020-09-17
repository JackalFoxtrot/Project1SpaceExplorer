using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    public PlayerShip playerShip;
    // Update is called once per frame
    void Update()
    {
        if (playerShip.getRocketsBool() && Input.GetKeyDown(KeyCode.F))
        {
            playerShip.Powerups();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndProg();       
        }
    }

    void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(activeSceneIndex);
    }

    void EndProg()
    {
        Application.Quit();
    }
}
