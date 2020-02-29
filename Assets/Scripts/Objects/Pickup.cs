using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, ICollectable
{
    void OnPickup()
    {

    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        Debug.Log(other.gameObject.name + " picked up " + name);

    }
}
