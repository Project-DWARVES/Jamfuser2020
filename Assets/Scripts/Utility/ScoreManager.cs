using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Dictionary<int, float> timeHoldingNut = new Dictionary<int, float>();

    NUT nut;

    public List<float> playerPercentages = new List<float>();
    public List<Color> playerColours = new List<Color>();
    public List<Slider> playerSliders = new List<Slider>();
    public Material scoreMaterial;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        nut = FindObjectOfType<NUT>();
        if(scoreMaterial)
        {
            scoreMaterial.SetColorArray("Colours", playerColours);
            scoreMaterial.SetFloatArray("Percentages", playerPercentages);
        }
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
            playerSliders[i].value = timeHoldingNut[i] / totalTime;
            playerPercentages[i] = playerSliders[i].value;
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

        Debug.Log(timeHoldingNut.Count);

        if(nut.player)
            timeHoldingNut[nut.player.playerID] += Time.deltaTime;

        UpdateUI();
    }
}
