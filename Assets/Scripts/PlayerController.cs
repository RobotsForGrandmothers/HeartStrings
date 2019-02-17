using System.Collections;
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

    private Sprite bardLeft;
    private Sprite bardRight;
    private SpriteRenderer bardRenderer;

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
    }

    void ChangeInstrument()
    {

    }

    void SpawnWave()
    {
        // True spawns wave on right, otherwise on left
        if (direction)
        {

        } else
        {

        }
    }

    // The player is taking damage :C 
    void TakeDamage (int damage)
    {
        healthPoints -= damage;
    }

}
