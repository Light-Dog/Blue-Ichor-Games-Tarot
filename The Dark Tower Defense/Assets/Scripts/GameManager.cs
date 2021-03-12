using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool playerDied = false;

    public GameObject gameOverUI;

    private void Start()
    {
        playerDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDied)
            return;

        if (Input.GetKeyDown("e"))
            PlayerStats.Lives = 0;

        if(PlayerStats.Lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        playerDied = true;
        gameOverUI.SetActive(true);
    }
}
