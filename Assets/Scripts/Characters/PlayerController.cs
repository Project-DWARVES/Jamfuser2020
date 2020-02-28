using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbody;

    public float speed;
    public float boostForce;
    public float boostCooldown;
    float boostCooldownTimer;

    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    void Start()
    {
        //Component Assignment
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


}
