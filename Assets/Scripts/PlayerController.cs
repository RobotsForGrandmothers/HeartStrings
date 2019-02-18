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
	// track player is playing
	public Track track;

    // False = Left, True = Right
    private bool direction;
    private int healthPoints;
	private int instrument;

    // Animations
    private Animator animator;
    private List<RuntimeAnimatorController> animatorControllers;
    private RuntimeAnimatorController redBardController;
    private RuntimeAnimatorController blueBardController;
    private RuntimeAnimatorController greenBardController;


    // controls to be set
    public KeyCode playUp = KeyCode.UpArrow;
	public KeyCode playDown = KeyCode.DownArrow;
	public KeyCode playLeft = KeyCode.LeftArrow;
	public KeyCode playRight = KeyCode.RightArrow;
	public KeyCode instrumentNext = KeyCode.S;
	public KeyCode instrumentPrev = KeyCode.W;
	public KeyCode faceLeft = KeyCode.A;
	public KeyCode faceRight = KeyCode.D;

    // Assign the wave object
    public GameObject wave;

    // Start is called before the first frame update
    void Start()
    {
        instrument = 0;
        healthPoints = 100;
        direction = true;

        animator = gameObject.GetComponent<Animator>();
        animatorControllers = new List<RuntimeAnimatorController>();

        redBardController = Resources.Load<RuntimeAnimatorController>("Animations/Red Bard") as RuntimeAnimatorController;
        blueBardController = Resources.Load<RuntimeAnimatorController>("Animations/Blue Bard") as RuntimeAnimatorController;
        greenBardController = Resources.Load<RuntimeAnimatorController>("Animations/Green Bard") as RuntimeAnimatorController;
        animatorControllers.Add(redBardController);
        animatorControllers.Add(blueBardController);
        animatorControllers.Add(greenBardController);

        animator.runtimeAnimatorController = animatorControllers[instrument];
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
        if (Input.GetKeyDown(faceLeft))
        {
            if (direction)
            {
                // Change sprite to face left
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                Debug.Log("Facing left");
                direction = false;
            }
        }

        if (Input.GetKeyDown(faceRight))
        {
            if (!direction)
            {
                // Change sprite to face right 
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                Debug.Log("Facing right");
                direction = true;
            }
        }

        // Space changes the instrument into the next one
        if (Input.GetKeyDown(instrumentNext))
        {
			++instrument;
			if (instrument >= track.CountInstruments) instrument = 0;
            animator.runtimeAnimatorController = animatorControllers[instrument];
            Debug.Log("Switched to instrument " + instrument);
        }

		if (Input.GetKeyDown(instrumentPrev))
		{
			--instrument;
			if (instrument < 0) instrument = track.CountInstruments -1;
            animator.runtimeAnimatorController = animatorControllers[instrument];
            Debug.Log("Switched to instrument " + instrument);
		}

		// try to play the note
        if (Input.GetKeyDown(playUp))
        {
            if (track.TryPlayNote(instrument, Note.Dir.UP)) {
				SpawnWave();
			}
        }
        if (Input.GetKeyDown(playDown))
        {
            if (track.TryPlayNote(instrument, Note.Dir.DOWN)) {
				SpawnWave();
			}
        }
        if (Input.GetKeyDown(playLeft))
        {
            if (track.TryPlayNote(instrument, Note.Dir.LEFT)) {
				SpawnWave();
			}
        }
        if (Input.GetKeyDown(playRight))
        {
            if (track.TryPlayNote(instrument, Note.Dir.RIGHT)) {
				SpawnWave();
			}
        }
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
