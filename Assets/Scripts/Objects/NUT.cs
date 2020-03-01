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
        GetComponent<MeshRenderer>().enabled = true;
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
        GetComponent<MeshRenderer>().enabled = false;
        transform.SetParent(player.transform);
        transform.position = player.transform.position - new Vector3(0, .5f, 0);
        GetComponent<Collider>().enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        transform.position = new Vector3(transform.position.x, spawnPosition.position.y, transform.position.z);
        rBody.MovePosition(Vector3.Lerp(transform.position, player ? player.transform.position - new Vector3(0, .5f, 0) : spawnPosition.position, Time.deltaTime * nutSpeed));
    }
}
