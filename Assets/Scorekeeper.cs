using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scorekeeper : MonoBehaviour
{
    // Score
    public int score;

    // If score increase by distance
    public bool distanceScore;
    // Units required to increase score
    public int unitsRequired;
    // UI Text
    public TextMeshProUGUI uiText;
    // Old position of camera
    private Vector2 oldPos;

    // On start
    void Start()
    {
        // Set old position to current position
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If distance between old position and current position is greater than units required and distance score is true
        if (Vector2.Distance(oldPos, transform.position) >= unitsRequired && distanceScore)
        {
            // Increase score by units required
            AddScore(unitsRequired);
            // Old position set to current position
            oldPos = transform.position;
        }
    }

    // Add score
    public void AddScore(int amount)
    {
        // Add amount to score
        score += amount;
        // Update score
        UpdateScore();
    }

    // Update score text
    public void UpdateScore()
    {
        // UI text set to score
        uiText.text = score.ToString();
    }
}
