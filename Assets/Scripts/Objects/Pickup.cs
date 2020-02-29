using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour, ICollectable
{
    public bool _canBePickedUp = true;
    public bool canBePickedUp {get{return _canBePickedUp;} set{_canBePickedUp = value;}}

    public PlayerController player;
    public Transform spawnPosition;
    protected Rigidbody rBody;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public virtual void OnPickup()
    {
        
    }

    public virtual void OnDrop()
    {
        player = null;
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if(!player)
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            Debug.Log(other.gameObject.name + " picked up " + name);

            if(pc && canBePickedUp)
            {
                player = pc;
                OnPickup();
            }
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void Update()
    {
        
    }
}
