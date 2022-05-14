using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Minimum time for laser cycle
    public float minTime;
    // Maximum time for laser cycle
    public float maxTime;
    // Laser object
    private GameObject laser;
    // Laser cycle time
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        // Time is random between minimum time and maximum time
        time = Random.Range(minTime, maxTime);
        // Laser is first child gameobject
        laser = transform.GetChild(0).gameObject;
        // Start laser cycle
        StartCoroutine(LaserCycle());
    }

    // Laser cycle
    public IEnumerator LaserCycle()
    {
        // Set laser active to opposite of current active state
        laser.SetActive(!laser.activeSelf);
        // Wait for time
        yield return new WaitForSeconds(time);
        // Restart laser cycle
        StartCoroutine(LaserCycle());
    }

    // On trigger enter
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If tag of collision is hookpoint
        if (collision.tag == "Hookpoint" || collision.tag == "Laser")
        {
            // If collision above gameobject
            if (collision.transform.position.y > gameObject.transform.position.y)
            {
                // Shift gameobject down
                gameObject.transform.position = new Vector2(0, gameObject.transform.position.y - 1);
            }
            // Else
            else
            {
                // Shift gameobject up
                gameObject.transform.position = new Vector2(0, gameObject.transform.position.y + 1);
            }
        }
    }
}
