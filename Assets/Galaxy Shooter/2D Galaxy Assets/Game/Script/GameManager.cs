using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // up on  start of game
    public bool GameOver = true;
    public GameObject Player;
    private UIManager _uiManager;

    //game over is true
    //space key pressed
    //game over should become false
    //title screen should disappear

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
       if (GameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(Player, Vector3.zero, Quaternion.identity);
                GameOver = false;
                _uiManager.HideTitleScreen();
            }
        }
    }

}
