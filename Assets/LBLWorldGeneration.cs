using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBLWorldGeneration : MonoBehaviour
{
    // Amount of screens prerendered
    public int prerender;
    // A list of prefabs
    public Prefabs[] prefabs;
    // Camera
    private Transform cam;
    // Screen y value in units
    public int screenHeight;
    // Spawn area correction
    public int spawnAreaCorrection;
    // Current screen
    private int currentScreen = 0;

    // On start
    void Start()
    {
        // Camera is found via Main Camera tag and set to cam
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // If cam is not in rerendered territory
        if (cam.position.y >= transform.position.y - screenHeight * prerender)
        {
            // Spawn in objects for screen
            SpawnIn();
        }

        transform.position = new Vector3(0, screenHeight * currentScreen);
    }

    // Spawn in
    public void SpawnIn()
    {
        float spawnAreaY = screenHeight - spawnAreaCorrection;
        // For all prefabs in list
        for (int i = 0; i < prefabs.Length; i++)
        {
            // Get amount in current screen based on min and max found in prefab
            int amountInScreen = Random.Range(prefabs[i].minAmount, prefabs[i].maxAmount);
            float distBetweenObjects = 0;
            if (amountInScreen != 0)
            {
                if(amountInScreen > 1)
                {
                    // Distance between gameobjects in area
                    distBetweenObjects = spawnAreaY / (amountInScreen - 1);
                }
                // For every needed gameobject spawned in for each prefab
                for (int b = 0; b < amountInScreen; b++)
                {
                    // Set transform position
                    transform.position = new Vector3(0, (screenHeight * currentScreen) - (spawnAreaY / 2) + (distBetweenObjects * b));
                    // Spawn in prefab based on current i prefab
                    GameObject currentPrefab = Instantiate(prefabs[i].prefab);
                    // Create position within range of xrange and within range of 20 y units
                    Vector2 curPos = new Vector2(Random.Range(-prefabs[i].xRange, prefabs[i].xRange), Random.Range(-prefabs[i].yVar, prefabs[i].yVar) + transform.position.y);
                    // Keep locations short by prefabs amount x2
                    if (prefabs[i].locations.Capacity >= prefabs[i].maxAmount * 3)
                    {
                        prefabs[i].locations.RemoveAt(0);
                    }
                    // Add current position to list of positions
                    prefabs[i].locations.Add(curPos);
                    // Set position to random pos
                    currentPrefab.transform.position = curPos;
                    // Get a random float so that squared objects still exists within size constraints
                    float sizeSquared = Random.Range(prefabs[i].minSize, prefabs[i].maxSize);
                    // Set scale based on sizesquared
                    currentPrefab.transform.localScale = new Vector3(sizeSquared, sizeSquared);
                }
            }
        }
        if(currentScreen == 0)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = prefabs[0].locations[0];
        }
        currentScreen += 1;
    }
}

//See in editor
[System.Serializable]
public class Prefabs
{
    // Object spawned
    public GameObject prefab;
    // Minimum amount per screen
    public int minAmount;
    // Maximum amount per screen
    public int maxAmount;
    // Range of position in x axis
    public float xRange;
    // Minimum size 
    public float minSize;
    // Maximum size
    public float maxSize;
    // List of gameobject positions
    public List<Vector2> locations = new List<Vector2>();
    // Distance in between prefabs
    public float distancing;
    // Variation in y position
    public float yVar;
}
