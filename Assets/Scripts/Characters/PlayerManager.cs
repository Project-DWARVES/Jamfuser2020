using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Range (1,4)]
    public int numberOfPlayers;

    public PlayerController[] playerPrefab;

    public Transform[] spawnPoints;


    void Start()
    {
        Initialise();
    }

    void Initialise()
    {
        //Spawn Players
        int playerNumber = 0;

        for (int i = 0; i < numberOfPlayers; i++)
        {
            PlayerController _player = Instantiate(playerPrefab[playerNumber], spawnPoints[playerNumber]);
            _player.playerID = playerNumber;
            _player.spawnTransform = spawnPoints[playerNumber].position;

            playerNumber++;
        }

        if (numberOfPlayers == 1)
        {
            SpawnAIPlayers();
        }
    }

    void SpawnAIPlayers()
    {
        //Do some funky AI shit here
    }

}
