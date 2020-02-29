using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    AudioSource audioSource;
    protected Rigidbody rbody;

    [Header("Setup")]
    public Transform hatTransform;
    public Transform spawnTransform;
    public float respawnDelay;

    Animator animator;

    [Header("Movement Variables")]
    public float tilt;
    public float speed;
    public float boostForce;
    public float boostCooldown;
    float boostCooldownTimer;
    Vector3 movement;

    [Header("Game Variables")]
    ICollectable currentCollectable = null;

    public int playerID = 0;
    [SerializeField] private Player player;


    [Header("AI bits")]
    [SerializeField] bool isAI = true; // assume its an AI
    PlayerController[] players = new PlayerController[4];
    NUT nut;


    protected virtual void Start()
    {
        //Component Assignment
        audioSource = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if(animator)
            animator.SetTrigger("Jumped");

        //Init
        try // try to initialise as a player
        {
            player = ReInput.players.GetPlayer(playerID);

            GameObject _hat = Instantiate(HatManager.instance.LoadHat(playerID), hatTransform.position, hatTransform.rotation, hatTransform);
            //_hat.transform.localPosition = Vector3.zero;
            
            
            isAI = false; // is not AI
        }
        catch
        {
            // must be AI if playerID doesnt exists, so let the AI script do its thing
            // do get a random hat
            players = FindObjectsOfType<PlayerController>(); 
        }
        nut = FindObjectOfType<NUT>();
    }

    protected virtual void FixedUpdate()
    {
        //BoostCooldown
        boostCooldownTimer += Time.deltaTime;

        float moveHorizontal = 0.0f;
        float moveVertical = 0.0f;

        if(!isAI) // if is a player, do player things
        {
            // standard move bits
            moveHorizontal = player.GetAxis("MoveHorizontal");
            moveVertical = player.GetAxis("MoveVertical");
            movement = new Vector3(moveHorizontal, 0, moveVertical);

            // extra abilities 
            if (player.GetButtonDown("Boost") && (boostCooldown <= boostCooldownTimer))
            {
                doBoost();
            }

            if (player.GetButtonDown("UseItem"))
            {
                doUseItem();
            }
        }
        else // if AI, do things by itself
        {   
            Vector3 target = this.transform.position;
            #region AI bully script
            /*
            foreach (PlayerController t in players)
            {
                float currentClosestDist = 999;
                if (t != this)
                {
                    float distance = (t.transform.position - this.transform.position).magnitude;
                    if (distance < currentClosestDist)
                    {
                        currentClosestDist = distance;
                        target = t.transform.position;
                    }
                }
            }*/
            #endregion

            #region AI nut hunter script

            if (nut.player != null)
            {
                target = nut.player.transform.position;
            }
            else if(nut.player == this)
            {
                target = nut.spawnPosition.transform.position;
            }
            else
            {
                target = nut.transform.position;
            }
            /* 
            is it that simple?? always aim for the nut?
            when a player has it, everyone will swarm that player,
            when the AI has it, evade??? currently just sits still 
            when it gets drop, the nut zooms back to the center, the ai will chase that
            */
            #endregion

            // try to boost on cooldown
            // Let's onyl boost every now and then
            if(Random.Range(0, 100) < 1)
                doBoost();

            Vector3 dirToTarget = (target - this.transform.position).normalized;

            movement = new Vector3(dirToTarget.x, 0, dirToTarget.z);
        }



        rbody.AddForce(movement * speed);
        rbody.rotation = Quaternion.Euler(0.0f, 0.0f, rbody.velocity.x * -tilt);


    }

    void doBoost()
    {
        if(boostCooldownTimer >= boostCooldown)
        {
            //Debug.Log("Player " + playerID + " used Boost");
            if(animator)
                animator.SetTrigger("Dash");

            boostCooldownTimer = 0;

            rbody.AddForce(movement * boostForce, ForceMode.Impulse);
        }
    }

    void doUseItem()
    {
        Debug.Log("Player " + playerID + " used Item");
    }


    public void Respawn()
    {
        if(animator)
            animator.SetTrigger("SwoopIn");
        
        //Play Death Sound
        if (nut.player == this)
        {
            nut.OnDrop();
        }
        StartCoroutine(CO_Respawn(respawnDelay));
    }

    IEnumerator CO_Respawn(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);

        transform.position = spawnTransform.position;
        rbody.velocity = Vector3.zero;
    }

    public float respawnTimer = 1f;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Branch")) // probs wont end up this way
        {
            if(animator)
                animator.SetTrigger("Impact");

            Invoke("Respawn", 1f);
        }
    }
}
