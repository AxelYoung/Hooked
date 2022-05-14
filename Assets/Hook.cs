using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Transform curPoint;
    public bool hitWall;

    // If something enters collider
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If collision tag is hookpoint
        if(collision.tag == "Hookpoint")
        {
            // Set curpoint to object that entered collision
            curPoint = collision.transform;
        }
        // If collision tag is wall
        if (collision.tag == "Wall")
        {
            // Hit wall set true
            hitWall = true;
        }
    }
}
