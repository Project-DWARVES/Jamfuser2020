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
    bool startScreenProgressed = false;
    bool controlScreenProgressed = false;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
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
        if (hasJoined)
            Animate();


        timer += Time.deltaTime;


        if (player.GetButtonDown("Boost"))
        {
            if(playerID != 0)
            {
                foreach(InputActionSourceData i in player.GetCurrentInputSources("Boost"))
                {
                    if(i.controller.type == ControllerType.Keyboard)
                        return;
                }
            }

            if (!startScreenProgressed && playerID == 0)
            {
                startScreenProgressed = true;
                animator.SetBool("StartScreen", true);
                return;
            }

            if (!controlScreenProgressed && playerID == 0)
            {
                controlScreenProgressed = true;
                //animate to Player Selected
                animator.SetBool("ControlScreen", true);
                return;
            }

            if (!hasJoined && animator.GetBool("ControlScreen") == true)
            {
                hasJoined = true;
                graphics.SetActive(true);
                CharacterSelectScreen.instance.PlayerJoined();
                return;
            }

            if (!isReady && animator.GetBool("ControlScreen") == true)
            {

                GetReady();
                return;
            }

            if (isReady && CharacterSelectScreen.instance.CheckIfGameStart())
            {
                CharacterSelectScreen.instance.StartGame();
            }

        }

        if (player.GetAxis("MoveHorizontal") != 0 && timer > 0.5f && !isReady)
        {
            timer = 0f;
            CycleHat(Mathf.CeilToInt(player.GetAxis("MoveHorizontal")));
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
