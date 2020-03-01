using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Speed at which the level plane is falling in units per second
    public float fallingSpeed = 5f;
    // How much time the level should take in seconds
    public float levelTimer = 360f;
    // Length of a segment in Unity units
    public float segmentSize = 2f;
    // How many segments ahead should be rendered
    public int visibleSegmentsAhead = 3;
    // Transform of the level plane
    public Transform level;
    // Start/End prefabs
    public GameObject startPrefab;
    public GameObject endPrefab;
    // Segment prefabs
    public List<GameObject> segmentPrefabs;
    GameObject[] segments;

    public bool active = true;

    // The number of segments of the level will be calculated at the start of the level, and will be progressively toggled on/off
    int numberOfSegments;
    bool hasEnded = false;

    void CalculateNumberOfSegments()
    {
        numberOfSegments = Mathf.RoundToInt(fallingSpeed * levelTimer / segmentSize);
    }

    void GenerateSegments()
    {
        CalculateNumberOfSegments();
        segments = new GameObject[numberOfSegments];

        segments[0] = Instantiate(startPrefab, new Vector3(0, -segmentSize, 0), Quaternion.identity, transform);
        
        for(int i = 1; i < numberOfSegments - 1; i++)
        {
            segments[i] = Instantiate(segmentPrefabs[Random.Range(0, segmentPrefabs.Count)], new Vector3(0, -segmentSize * (i + 1), 0), Quaternion.identity, transform);
        }

        segments[numberOfSegments - 1] = Instantiate(endPrefab, new Vector3(0, -(numberOfSegments) * segmentSize, 0), Quaternion.identity, transform);
    }

    void Start()
    {
        GenerateSegments();
    }

    void Update()
    {
        if(active)
        {
            level.GetComponent<Rigidbody>().MovePosition(level.position - level.transform.up * fallingSpeed * Time.deltaTime);
            levelTimer -= Time.deltaTime;
        }

        if(!hasEnded && levelTimer <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        active = false;
        hasEnded = true;
        FindObjectOfType<LevelEnd>().endLevel();
    }
}
