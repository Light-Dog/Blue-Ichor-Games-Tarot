using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool playerDied = false;

    // Update is called once per frame
    void Update()
    {
        if (!playerDied)
            return;

        if(PlayerStats.Lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        playerDied = true;
        Debug.Log("Game Over!");
    }
}
