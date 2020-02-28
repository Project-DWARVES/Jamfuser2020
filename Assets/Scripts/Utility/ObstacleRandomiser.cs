using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleLocation
{
    public Transform transform;
    [HideInInspector] public GameObject obstacle;
    [HideInInspector] public bool populated;
}

public class ObstacleRandomiser : MonoBehaviour
{
    // Min/Max rotation of objects
    [Tooltip("Minimum/Maximum rotation of this object when spawned")] public Vector2 initialRotation = new Vector2Int(0, 0);

    // Min/Max number of obstacles to spawn
    [Tooltip("Minimum/Maximum number of obstacles to spawn")] public Vector2Int obstaclesAmount = new Vector2Int(0, 10);
    // List of transforms where to place objects
    public List<ObstacleLocation> obstacleLocations;
    // List of obstacles to place
    public List<GameObject> obstacles;

    void Start()
    {
        // Mark all positions as not populated
        foreach(ObstacleLocation o in obstacleLocations)
            o.populated = false;

        transform.Rotate(0, Random.Range(-1, 2) * Random.Range(initialRotation.x, initialRotation.y), 0);

        Populate();
    }

    // Instantiate obstacle in location
    void PlaceObstacle(ObstacleLocation location, GameObject obstacle)
    {
        location.obstacle = Instantiate(obstacle, location.transform.position, location.transform.rotation, location.transform);
        location.populated = true;
    }

    // Place random obstacles
    public void Populate()
    {
        if(obstacleLocations.Count > 0 && obstacles.Count > 0)
        {
            // Get a random number of obstacles to place
            int amount = Random.Range(obstaclesAmount.x, obstaclesAmount.y + 1);
            // Make sure there are enough obstacles
            amount = Mathf.Min(amount, obstacleLocations.Count);

            for(int i = 0; i < amount; i++)
            {
                bool placedObstacle = false;

                while(!placedObstacle)
                {
                    int obstacle = Random.Range(0, obstacleLocations.Count);

                    if(!obstacleLocations[obstacle].populated)
                    {
                        PlaceObstacle(obstacleLocations[obstacle], obstacles[Random.Range(0, obstacles.Count)]);
                        placedObstacle = true;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("No obstacle locations/obstacles have been set for " + name);
        }
    }
}
