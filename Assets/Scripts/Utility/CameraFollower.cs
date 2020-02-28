using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // Distance to keep between camera and players
    public float followDistance = 2f;
    // Obstacle detection range
    public float avoidanceRange = 2f;
    // Distance camera should move to avoid impact
    public float avoidanceDistance = 0.2f;
    // Speed at which camera should move to avoid impact
    public float avoidanceSpeed = 1f;
    // Player transforms to keep track of
    public List<Transform> playerTransforms;

    // Centre between all players
    Vector3 idealPosition;
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void CalculateIdealPosition()
    {
        idealPosition = Vector3.zero;

        for(int i = 0; i < playerTransforms.Count; i++)
        {
            idealPosition.x += playerTransforms[i].position.x;
            idealPosition.z += playerTransforms[i].position.z;
        }

        idealPosition.x /= (float)playerTransforms.Count;
        idealPosition.z /= (float)playerTransforms.Count;
    }

    // Returns how much the camera should zoom in(+) or out (-) in order to fit a transform in the view
    float DistanceToMove(Transform t)
    {
        Vector3 pos = t.position;
        Vector3 cameraPos = transform.position;
        float distanceToMove = 0;
        float angleBetween = Vector3.Angle(transform.forward, (pos - cameraPos).normalized);

        int i = 0;
        while(angleBetween > GetComponent<Camera>().fieldOfView / 2)
        {
            distanceToMove += 1f;
            cameraPos -= transform.forward;

            angleBetween = Vector3.Angle(transform.forward, (pos - cameraPos).normalized);
            i++;
            if(i == 1000)
            {
                Debug.LogError("Yikes");
                break;
            }
        }

        return distanceToMove;
    }

    float ZoomChange()
    {
        float zoom = 0f;

        foreach(Transform t in playerTransforms)
        {
            if(DistanceToMove(t) > zoom)
                zoom = DistanceToMove(t);
        }

        return zoom;
    }
    
    void FixedUpdate()
    {
        CalculateIdealPosition();

        idealPosition.y = playerTransforms[0].position.y + followDistance + ZoomChange();

        rigidbody.MovePosition(Vector3.Lerp(transform.position, idealPosition, Time.deltaTime * avoidanceSpeed));
    }

    void OnCollisionStay(Collision other)
    {
        rigidbody.AddForce(transform.up);
    }
}
