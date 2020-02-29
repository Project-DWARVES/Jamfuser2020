using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NUT : Pickup
{
    public float nutSpeed = 5f;
    public float pickupCooldown = 3f;

    public override void OnDrop()
    {
        transform.SetParent(spawnPosition);
        player = null;
        TogglePickupCooldown();
    }

    void TogglePickupCooldown()
    {
        canBePickedUp = false;
        Invoke("EnablePickup", pickupCooldown);
    }

    void EnablePickup()
    {
        GetComponent<Collider>().enabled = true;
        canBePickedUp = true;
    }

    public override void OnPickup()
    {
        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        GetComponent<Collider>().enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        transform.position = new Vector3(transform.position.x, spawnPosition.position.y, transform.position.z);
        rBody.MovePosition(Vector3.Lerp(transform.position, player ? player.transform.position : spawnPosition.position, Time.deltaTime * nutSpeed));
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 60, 60), "NUT"))
        {
            OnDrop();
        }
    }
}
