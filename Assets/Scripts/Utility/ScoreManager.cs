using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Dictionary<int, float> timeHoldingNut = new Dictionary<int, float>();

    NUT nut;

    public float[] playerPercentages = {0.25f, 0.25f, 0.25f, 0.25f};
    public Color[] playerColours;
    public Material material;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        nut = FindObjectOfType<NUT>();
    }

    public int GetScorePosition(int player)
    {
        int index = 1;
        float percent = playerPercentages[player];

        for(int  i = 0; i < 4; i++)
        {
            if(i != player)
            {
                if(playerPercentages[i] < percent)
                    index++;
                else
                    index--;
            }
        }

        return Mathf.Clamp(index, 1, 4);
    }

    public int[] GetScorePositions()
    {
        int[] ranks = {0, 1, 2, 3};

        for(int i = 0; i < 3; i++)
        {
            for(int j = i + 1; j < 4; j++)
            {
                if(playerPercentages[i] < playerPercentages[j])
                {
                    float aux = playerPercentages[i];
                    playerPercentages[i] = playerPercentages[j];
                    playerPercentages[j] = aux;

                    int _aux = ranks[i];
                    ranks[i] = ranks[j];
                    ranks[j] = _aux;
                }
            }
        }

        Debug.Log(ranks[0] + " " + ranks[1] + " " + ranks[2] + " " + ranks[3]);

        return ranks;
    }

    void UpdateUI()
    {
        float totalTime = 0;

        for(int i = 0; i < timeHoldingNut.Count; i++)
        {
            totalTime += timeHoldingNut[i];
        }

        for(int i = 0; i < timeHoldingNut.Count; i++)
        {
            playerPercentages[i] = timeHoldingNut[i] / totalTime;
        }

        if(material)
        {
            Shader.EnableKeyword("_Colours");
            material.SetColorArray(Shader.PropertyToID("_Colours"), playerColours);
            Shader.DisableKeyword("_Colours");
            Shader.EnableKeyword("_Percentages");
            material.SetFloatArray(Shader.PropertyToID("_Percentages"), playerPercentages);
            Shader.DisableKeyword("_Percentages");
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(timeHoldingNut.Count == 0)
            foreach(PlayerController pc in FindObjectsOfType<PlayerController>())
                timeHoldingNut.Add(pc.playerID, 0);

        if(nut.player)
            timeHoldingNut[nut.player.playerID] += Time.deltaTime;

        UpdateUI();
    }
}
