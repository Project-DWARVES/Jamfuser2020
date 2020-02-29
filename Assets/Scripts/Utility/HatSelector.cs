using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class HatSelector : MonoBehaviour
{
    Animator animator;

    public List<GameObject> hatCollection = new List<GameObject>();
    public int playerID;
    public int selectedHat = 0;
    public GameObject ready;
    public GameObject graphics;

    //ReWired
    private Player player;

    //SelectionTimer
    float timer;
    float animTimer;
    float animRandom;

    //Ready
    bool hasJoined = false;
    bool isReady = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = ReInput.players.GetPlayer(playerID);

        foreach (GameObject hat in hatCollection)
        {
            hat.gameObject.SetActive(false);
        }

        graphics.SetActive(false);

        NewRandomInterval();

        hatCollection[Mathf.CeilToInt(selectedHat)].SetActive(true);
    }

    public void CycleHat(int cycle)
    {
        hatCollection[selectedHat].SetActive(false);

        selectedHat += cycle;

        if (selectedHat > (hatCollection.Count - 1))
            selectedHat = 0;
        else if (selectedHat < 0)
            selectedHat = (hatCollection.Count - 1);

        hatCollection[selectedHat].SetActive(true);
    }

    void Update()
    {
        Animate();

        timer += Time.deltaTime;

        if (player.GetAxis("MoveHorizontal") != 0 && timer > 0.5f)
        {
            timer = 0f;
            CycleHat(Mathf.CeilToInt(player.GetAxis("MoveHorizontal")));
        }

        if (player.GetButtonDown("Boost"))
        {
            if (!hasJoined)
            {
                hasJoined = true;
                graphics.SetActive(true);
                return;
            }

            GetReady();
        }

        if (player.GetButtonDown("StartButton"))
        {
            if (isReady)
            {
                CharacterSelectScreen.instance.StartGame();
            }
        }
    }

    void GetReady()
    {
        ready.SetActive(true);
        isReady = true;
        CharacterSelectScreen.instance.PlayerReady(playerID);
        HatManager.instance.ConfirmHat(playerID, selectedHat);
    }

    void Animate()
    {
        animTimer += Time.deltaTime;

        if (animTimer > animRandom)
        {
            animator.SetTrigger("Fidget");
            NewRandomInterval();
        }
    }

    void NewRandomInterval()
    {
        animTimer = 0;
        animRandom = Random.Range(2f, 6f);
    }


}
