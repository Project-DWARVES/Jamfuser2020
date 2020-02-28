using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Range(1, 4)]
    public int numberOfPlayers;

    public PlayerController[] playerPrefab;

    public Transform[] spawnPoints;

    CameraFollower cameraFollower;

    void Start()
    {
        cameraFollower = FindObjectOfType<CameraFollower>();
        Initialise();
    }

    void Initialise()
    {
        if (PlayerPrefs.GetInt("Player0") == 1)
            SpawnPlayer(0);
        if (PlayerPrefs.GetInt("Player1") == 1)
            SpawnPlayer(1);
        if (PlayerPrefs.GetInt("Player2") == 1)
            SpawnPlayer(2);
        if (PlayerPrefs.GetInt("Player3") == 1)
            SpawnPlayer(3);



        if (numberOfPlayers == 1)
        {
            SpawnAIPlayers();
        }
    }

    void SpawnPlayer(int player)
    {
        PlayerController _player = Instantiate(playerPrefab[player], spawnPoints[player]);
        _player.playerID = player;
        _player.spawnTransform = spawnPoints[player];
        cameraFollower?.playerTransforms.Add(_player.transform);
    }

    void SpawnAIPlayers()
    {
        //Do some funky AI shit here
    }

}
