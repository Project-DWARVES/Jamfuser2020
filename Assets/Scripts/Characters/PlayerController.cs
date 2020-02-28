using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rbody;

    [Header("Setup")]
    public Vector3 spawnTransform;
    public float respawnDelay;

    [Header("Movement Variables")]
    public float tilt;
    public float speed;
    public float boostForce;
    public float boostCooldown;
    float boostCooldownTimer;

    [Header("Game Variables")]
    ICollectable currentCollectable = null;

    public int playerID = 0;
    [SerializeField] private Player player;

    void Start()
    {
        //Component Assignment
        audioSource = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody>();

        //Init
        player = ReInput.players.GetPlayer(playerID);
    }

    void FixedUpdate()
    {
        //BoostCooldown
        boostCooldownTimer += Time.deltaTime;

        float moveHorizontal = player.GetAxis("MoveHorizontal");
        float moveVertical = player.GetAxis("MoveVertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rbody.AddForce(movement * speed);

        rbody.rotation = Quaternion.Euler(0.0f, 0.0f, rbody.velocity.x * -tilt);


        if (player.GetButtonDown("Boost") && (boostCooldown <= boostCooldownTimer))
        {
            Debug.Log("Player " + playerID + " used Boost");

            boostCooldownTimer = 0;

            rbody.AddForce(movement * boostForce, ForceMode.Impulse);
        }

        if (player.GetButtonDown("UseItem"))
        {
            Debug.Log("Player " + playerID + " used Item");
        }
    }

    public void Respawn()
    {
        //Play Death Sound

        StartCoroutine(CO_Respawn(respawnDelay));
    }

    IEnumerator CO_Respawn(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);

        transform.position = spawnTransform;
        rbody.velocity = Vector3.zero;
    }
}
