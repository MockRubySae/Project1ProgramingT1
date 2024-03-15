using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public PlayerData player = new PlayerData();
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveToFile();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadFromFile();
        }
    }
    public void SaveToFile()
    {
        player.SetData();
        string savedPlayerData = JsonUtility.ToJson(player);
        string filePath = Application.persistentDataPath + "/GameData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, savedPlayerData);
        Debug.Log("PlayerOneData Saved");
    }

    public void LoadFromFile()
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        string savedPlayerData = System.IO.File.ReadAllText(filePath);
        player = JsonUtility.FromJson<PlayerData>(savedPlayerData);
        player.TransferData();
        Debug.Log("Loaded");
    }
}

[System.Serializable]
public class PlayerData
{
    public GameManager manager;
    public CharacterController playerOne;
    public CharacterController playerTwo;

    public GameObject ball;
    public int oneScore;
    public int twoScore;
    public Vector3 playerOnePos;
    public Vector3 playerTwoPos;
    public Quaternion playerOneRotation;
    public Quaternion playerTwoRotation;
    public Vector3 ballPos;
    public Quaternion ballRotation;
    public Vector3 ballSpeed;

public void SetData()
    {
        ball = GameObject.FindWithTag("SoccerBall");
        oneScore = manager.playerOneScore;
        twoScore = manager.playerTwoScore;
        playerOnePos = playerOne.transform.position;
        playerTwoPos = playerTwo.transform.position;
        playerOneRotation = playerOne.transform.rotation;
        playerTwoRotation = playerTwo.transform.rotation;
        if(ball != null)
        {
            ballPos = ball.transform.position;
            ballRotation = ball.transform.rotation;
            ballSpeed = ball.GetComponent<Rigidbody>().velocity;
        }     
    }

    public void TransferData()
    {
        ball = GameObject.FindWithTag("SoccerBall");
        manager.playerOneScore = oneScore;
        manager.playerTwoScore = twoScore;
        manager.uiManager.UpdateScores(manager.playerOneScore, manager.playerTwoScore);
        playerOne.transform.position = playerOnePos;
        playerTwo.transform.position = playerTwoPos;
        playerOne.transform.rotation = playerOneRotation;
        playerTwo.transform.rotation = playerTwoRotation;
        if(ball != null)
        {
            ball.transform.position = ballPos;
            ball.transform.rotation = ballRotation;
            ball.GetComponent<Rigidbody>().velocity = ballSpeed;
        }
        else if(ballPos != null && ballRotation != null && ballSpeed != null && ball == null)
        {
            manager.SpawnOrMoveSoccerBall(ballPos);
            ball = GameObject.FindWithTag("SoccerBall");
            ball.transform.rotation = ballRotation;
            ball.GetComponent<Rigidbody>().velocity = ballSpeed;
        }
    }
}