﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Direction/Face
// Health
// Spawn Wave
// Change direction
// Instrument "SPACE"

public class PlayerController : MonoBehaviour
{
    // False = Left, True = Right
    private bool direction;
    private int healthPoints;

    // Have the sprites for the bard - the bard starts off facing right because direction is true
    private Sprite bardLeft;
    private Sprite bardRight;
    private SpriteRenderer bardRenderer;

    // Assign the wave object
    public GameObject wave;

    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 100;
        direction = true;

        bardRenderer = gameObject.GetComponent<SpriteRenderer>();
        bardLeft = Resources.Load<Sprite>("Sprites/BardLeft");
        bardRight = Resources.Load<Sprite>("Sprites/BardRight");
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
        {
            // Go to end game screen
            UIController.ShowEndGameScreen();
        }

        // Left and right arrow change the direction the player is facing
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction)
            {
                // Change sprite to face left
                bardRenderer.sprite = bardLeft;
                Debug.Log("Facing left");
                direction = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!direction)
            {
                // Change sprite to face right 
                bardRenderer.sprite = bardRight;
                Debug.Log("Facing right");
                direction = true;
            }
        }

        // Space changes the instrument into the next one
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

        // TEMP BUTTON PRESS - assume callback for spawn wave later
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnWave();
        }
    }

    void ChangeInstrument()
    {

    }

    void SpawnWave()
    {
        GameObject projectile = Instantiate(wave, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Wave>().SetDirection(direction);
        projectile.GetComponent<Wave>().SetColor(1);
    }

    // The player is taking damage :C 
    void TakeDamage (int damage)
    {
        healthPoints -= damage;
    }

}
