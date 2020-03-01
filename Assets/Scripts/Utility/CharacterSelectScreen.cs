using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectScreen : MonoBehaviour
{
    #region Singleton
    public static CharacterSelectScreen instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    float playersJoined = 0;
    float playersReady = 0;

    public bool CheckIfGameStart()
    {
        if (playersReady == playersJoined)
            return true;
        else
            return false;
    }

    public void Start()
    {
        PlayerPrefs.DeleteKey("Player0");
        PlayerPrefs.DeleteKey("Player1");
        PlayerPrefs.DeleteKey("Player2");
        PlayerPrefs.DeleteKey("Player3");

        PlayerPrefs.SetInt("Player0", 0);
        PlayerPrefs.SetInt("Player1", 0);
        PlayerPrefs.SetInt("Player2", 0);
        PlayerPrefs.SetInt("Player3", 0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayerReady(int playerNumber)
    {
        PlayerPrefs.SetInt("Player" + playerNumber, 1);
        playersReady++;
    }

    public void PlayerJoined()
    {
        playersJoined++;
    }

}
