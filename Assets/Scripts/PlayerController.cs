using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private int _instrument = 0;
	private int instrument {
        get { return _instrument; }
        set {
            _instrument = value;
            OnInstrumentSwitch(_instrument);
        }
    }
    private Transform health_bar;
    public int max_health = 10;
    private int combo = 0;
    

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
    
    public event EventHandler<InstrumentEvent> InstrumentSwitch;

    // Start is called before the first frame update
    void Start()
    {
        instrument = 0;
        healthPoints = 10;
        direction = true;
        health_bar = this.transform.GetChild(0);

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
            SceneManager.LoadScene("Game Over");
        }

        // Left and right arrow change the direction the player is facing
        if (Input.GetKeyDown(faceLeft))
        {
            if (direction)
            {
                // Change sprite to face left
                gameObject.transform.localScale = new Vector3(3, 3, 1);
                direction = false;
            }
        }

        if (Input.GetKeyDown(faceRight))
        {
            if (!direction)
            {
                // Change sprite to face right 
                gameObject.transform.localScale = new Vector3(-3, 3, 1);
                direction = true;
            }
        }

        // Space changes the instrument into the next one
        if (Input.GetKeyDown(instrumentNext))
        {
            instrument = (instrument + 1) % track.CountInstruments;
            animator.runtimeAnimatorController = animatorControllers[instrument];
        }

		if (Input.GetKeyDown(instrumentPrev))
		{
            if (instrument == 0) instrument = track.CountInstruments - 1;
            else instrument = instrument - 1;
            animator.runtimeAnimatorController = animatorControllers[instrument];
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
        // DEVELOPER CHEAT KEY
        if (Input.GetKeyDown("t"))
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        combo += 1;
        GameObject projectile = Instantiate(wave, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Wave>().SetDirection(direction);
        projectile.GetComponent<Wave>().SetColor(instrument, combo);
        projectile.GetComponent<Wave>().SetDamage(combo);
    }

    // The player is taking damage :C 
    void TakeDamage (int damage)
    {
        healthPoints -= damage;
        float hp_decr_factor = -0.1f * damage / max_health;
        Vector3 temp_hp_scale = health_bar.localScale + new Vector3(hp_decr_factor,0,0);
        if (temp_hp_scale.x >= 0){
            health_bar.localScale = temp_hp_scale;
        }
        else if (temp_hp_scale.x < 0){
            health_bar.localScale = new Vector3(0,0,0);
        }
   }

    void OnInstrumentSwitch(int instrument) {
        EventHandler<InstrumentEvent> handler = InstrumentSwitch;
        if (handler != null) handler(this, new InstrumentEvent(instrument));
        this.combo = 0;
        Debug.Log("Player switched instrument to " + instrument);
    }

    void OnTriggerEnter2D(Collider2D monster){
        if (monster.gameObject.CompareTag("Monster")){
            Monster obj = monster.gameObject.GetComponent<Monster>();
            TakeDamage(obj.playerDamage);
        }
    
    } 
}

public class InstrumentEvent : EventArgs {
    public int instrument;
    
    public InstrumentEvent(int instrument) {
        this.instrument = instrument;
    }
}
