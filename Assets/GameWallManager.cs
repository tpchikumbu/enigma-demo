using Unity.VisualScripting;
using UnityEngine;
using System;
using Platformer.Mechanics;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameWallManager : MonoBehaviour
{
    private GameWall playerWall;
    private GameWall computerWall;
    private const int WIN_NUMBER = 25;
    private static System.Random random = new();
    public int total = 0;
    public bool playerTurn = true;
    private bool gameOver = false;
    private AttackScript MageShooter;
    private Ground[] floorBricks = new Ground[6];
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private TMPro.TextMeshProUGUI scoreText;
    void Start()
    {
        // get torch object by name
        GameObject gameWallObject = GameObject.Find("GameWall1");
        playerWall = gameWallObject.GetComponent<GameWall>();
        playerWall.id = 1;
        gameWallObject = GameObject.Find("GameWall2");
        computerWall = gameWallObject.GetComponent<GameWall>();
        computerWall.id = 2;
        MageShooter = GameObject.Find("Mage Variant").GetComponent<AttackScript>();
        // Check if the MageShooter object is not null

        // Get floor object by name
        floorBricks[0] = GameObject.Find("FA1").GetComponent<Ground>();
        floorBricks[1] = GameObject.Find("FA2").GetComponent<Ground>();
        floorBricks[2] = GameObject.Find("FA3").GetComponent<Ground>();
        floorBricks[3] = GameObject.Find("FB1").GetComponent<Ground>();
        floorBricks[4] = GameObject.Find("FB2").GetComponent<Ground>();
        floorBricks[5] = GameObject.Find("FB3").GetComponent<Ground>();

        // Check if the floorBricks object is not null
        for (int i = 0; i < 6; i++)
        {
            if (floorBricks[i] == null)
            {
                Debug.LogError("Floor brick " + i + " not found");
            }
            else{
                Debug.Log("Floor brick " + i + " found");
            }
        }

        scoreText = GameObject.Find("scoreCard").GetComponent<TMPro.TextMeshProUGUI>();
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: 0";

        Debug.Log("GameWallManager started");
        if (scoreText == null)
        {
            Debug.LogError("scoreText not found");
        }
        else {
            Debug.Log("scoreText found");
        }

    }

    public void Update() {

    }

    public void Hit(int id){

        Debug.Log("Hit" + id);
        // Compare the tag of the object that collided with the wall
        if (gameOver)
            return;

        if (playerTurn && id == 1)
        {
            playerTurn = false;
            total += playerWall.move;
            // Run the next line on a separate thread
        }
        else if (!playerTurn && id == 2) {
            total += computerWall.move;
            playerTurn = true;
        }
        
        if (total >= WIN_NUMBER){
            gameOver = true;
            MoveFloor();
        } else if (!playerTurn){
            Debug.Log("Computer's turn");
            // Run the next line on a separate thread
            // Task.Run(() => MageShooter.Shoot(GetComputerMove() - 1));
            MageShooter.Shoot(GetComputerMove() - 1);
        }

        scoreText.text = "Score: " + total;
    }

    private int GetComputerMove() {
        int target = (4 - ((total - 1) % 4)) % 4;
        if (target == 0)
        {
            target = 4; // If we're already at a multiple of 4 plus 1, we want to add 4 to get to the next one
        }

        // Ensure we don't go over 21 and only use legal moves (1, 2, or 3)
        int maxMove = Math.Min(Math.Min(target, WIN_NUMBER - total), 3);

        if (maxMove < target)
        {
            // If we can't make the optimal move, introduce some variation
            return random.Next(1, maxMove + 1);
        }
        else
        {
            return maxMove;
        }
    }

    private void MoveFloor(){
        if (!playerTurn){
            floorBricks[0].Move(true);
            floorBricks[1].Move(true);
            floorBricks[2].Move(true);
            // lose the game and restart the level
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        else {
            floorBricks[3].Move(true);
            floorBricks[4].Move(true);
            floorBricks[5].Move(true);

        }
    }
}


