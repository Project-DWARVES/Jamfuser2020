using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    
    public Transform[] podiums = new Transform[4];
    bool[] placedPlayers = new bool[4];
    public Transform cameraPosition;
    PlayerController[] players = new PlayerController[4];
    public ParticleSystem confetti;

    ScoreManager scoreManager;

    public GameObject endGameUI;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // Assign players
        foreach(PlayerController pc in FindObjectsOfType<PlayerController>())
            players[pc.playerID] = pc;

        scoreManager = FindObjectOfType<ScoreManager>();
        endGameUI = GameObject.Find("UI_EndGame");

        for(int i = 0; i < 4; i++)
            placedPlayers[i] = false;
    }

    public float intermission = 3;

    IEnumerator fadeScreen()
    { 
        while(endGameUI.GetComponent<Image>().color.a < 1)
        {
            endGameUI.GetComponent<Image>().color = new Color(0, 0, 0, endGameUI.GetComponent<Image>().color.a + 0.05f);
            yield return new WaitForEndOfFrame();
        }

        if(confetti)
        {
            confetti.gameObject.SetActive(true);
        }

        Camera.main.transform.position = cameraPosition.position;
        Camera.main.transform.rotation = cameraPosition.rotation;

        Camera.main.GetComponent<CameraFollower>().active = false;
        int[] _ranks = scoreManager.GetScorePositions();

        for(int i = 0; i < 4; i++)
        {
            players[_ranks[i]].flying = false;
            players[_ranks[i]].GetComponentInChildren<Collider>().enabled = false;
            players[_ranks[i]].rbody.isKinematic = true;
            players[_ranks[i]].transform.position = podiums[i].position;
            players[_ranks[i]].transform.rotation = podiums[i].rotation;

            if(i == 0)
                players[_ranks[i]].animator.SetTrigger("Victory");
            else
                players[_ranks[i]].animator.SetTrigger("Loss");
        }

        GameObject.Find("UI_Score").SetActive(false);

        yield return new WaitForSeconds(intermission);

        endGameUI.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    public void endLevel()
    {
        if(endGameUI)
            StartCoroutine(fadeScreen());
    }
}
