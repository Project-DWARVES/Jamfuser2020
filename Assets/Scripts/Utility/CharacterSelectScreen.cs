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

    public int playersReady;

    public void StartGame()
    {
        PlayerPrefs.SetInt("PlayerCount", playersReady);
        SceneManager.LoadScene(2);
    }

}
