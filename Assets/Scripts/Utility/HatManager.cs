using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HatManager : MonoBehaviour
{
    #region Singleton
    public static HatManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    public List<GameObject> hatCollection = new List<GameObject>();

    public int player1Hat;
    public int player2Hat;
    public int player3Hat;
    public int player4Hat;

    public void ConfirmHat(int player, int hat)
    {
        switch (player)
        {
            case 0:
                player1Hat = hat;
                break;
            case 1:
                player2Hat = hat;
                break;
            case 2:
                player3Hat = hat;
                break;
            case 3:
                player4Hat = hat;
                break;


            default:
                break;
        }
    }

    public GameObject LoadHat(int playerNumber)
    {
        switch (playerNumber)
        {
            case 0:
                return hatCollection[player1Hat];
            case 1:
                return hatCollection[player2Hat];
            case 2:
                return hatCollection[player3Hat];
            case 3:
                return hatCollection[player4Hat];
            default:
                return null;
        }
    }
}
