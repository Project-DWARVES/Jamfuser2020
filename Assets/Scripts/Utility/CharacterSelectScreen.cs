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

    void Start()
    {
        PlayerPrefs.DeleteKey("Player0");
        PlayerPrefs.DeleteKey("Player1");
        PlayerPrefs.DeleteKey("Player2");
        PlayerPrefs.DeleteKey("Player3");
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayerReady(int playerNumber)
    {
        PlayerPrefs.SetInt("Player" + playerNumber, 1);
    }

}
