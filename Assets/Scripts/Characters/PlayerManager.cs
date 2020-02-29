using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
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
            SpawnPlayer(0, false);
        else
            SpawnPlayer(0, true);

        if (PlayerPrefs.GetInt("Player1") == 1)
            SpawnPlayer(1, false);
        else
            SpawnPlayer(1, true);

        if (PlayerPrefs.GetInt("Player2") == 1)
            SpawnPlayer(2, false);
        else
            SpawnPlayer(2, true);

        if (PlayerPrefs.GetInt("Player3") == 1)
            SpawnPlayer(3, false);
        else
            SpawnPlayer(3, true);

    }

    void SpawnPlayer(int player, bool isAi)
    {
        PlayerController _player = Instantiate(playerPrefab[player], spawnPoints[player]);
        _player.playerID = player;
        _player.isAI = isAi;
        _player.spawnTransform = spawnPoints[player];
        cameraFollower?.playerTransforms.Add(_player.transform);
    }


}
