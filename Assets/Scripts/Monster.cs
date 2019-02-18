using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum Colour{
        RED, BLUE, GREEN
    }
    public Colour colour;
    public int max_health = 10;
    public float speed = 0.1f;
    public Vector2 monster_position;
    
    private int cur_health;
    private Vector2 player_position = new Vector2(0,0);
    private Transform health_bar;

    private Animator animator;
    private List<RuntimeAnimatorController> normalMonsterAnimationControllers;
    private List<RuntimeAnimatorController> happyMonsterAnimationControllers;

    private RuntimeAnimatorController redNormalMonster;
    private RuntimeAnimatorController blueNormalMonster;
    private RuntimeAnimatorController greenNormalMonster;

    private RuntimeAnimatorController redHappyMonster;
    private RuntimeAnimatorController blueHappyMonster;
    private RuntimeAnimatorController greenHappyMonster;


    // Start is called before the first frame update
    void Start(){

        animator = gameObject.GetComponent<Animator>();
        normalMonsterAnimationControllers = new List<RuntimeAnimatorController>();
        happyMonsterAnimationControllers = new List<RuntimeAnimatorController>();

        redNormalMonster = Resources.Load<RuntimeAnimatorController>("Animations/Floaty Cyclops Cyclops") as RuntimeAnimatorController;
        blueNormalMonster = Resources.Load<RuntimeAnimatorController>("Animations/Normal Squiggly") as RuntimeAnimatorController;
        greenNormalMonster = Resources.Load<RuntimeAnimatorController>("Animations/Shuffle Snail") as RuntimeAnimatorController;
        normalMonsterAnimationControllers.Add(redNormalMonster);
        normalMonsterAnimationControllers.Add(blueNormalMonster);
        normalMonsterAnimationControllers.Add(greenNormalMonster);

        redHappyMonster = Resources.Load<RuntimeAnimatorController>("Animations/Happy Cyclops") as RuntimeAnimatorController;
        blueHappyMonster = Resources.Load<RuntimeAnimatorController>("Animations/Happy Squiggly") as RuntimeAnimatorController;
        greenHappyMonster = Resources.Load<RuntimeAnimatorController>("Animations/Happy Snail") as RuntimeAnimatorController;
        happyMonsterAnimationControllers.Add(redHappyMonster);
        happyMonsterAnimationControllers.Add(blueHappyMonster);
        happyMonsterAnimationControllers.Add(greenHappyMonster);

        animator.runtimeAnimatorController = normalMonsterAnimationControllers[(int) colour];

        cur_health = max_health;
        health_bar = this.transform.GetChild(0);
        //childTransform.localScale += new Vector3(-0.05f,0,0);
    }

    // Update is called once per frame
    void Update(){
        if ((Vector2)this.transform.position != player_position){
            Move();
            //(Vector2)this.transform.GetChild(0).localScale += new Vector2(0.5f,0);
        }
        else{
            //do something with player damage
            //maybe leave the screen
            Destroy(this.gameObject);
        }
    }

    void Move(){
        this.transform.position = Vector2.MoveTowards((Vector2)this.transform.position, player_position, speed);
    }
    
    Colour colourConverter(int int_colour){
        if (int_colour == 0){
            return Colour.RED;
        }
        else if (int_colour == 1){
            return Colour.BLUE;
        }
        return Colour.GREEN;
    }
    
    void takeDamage(int damage){
        cur_health -= damage;
        float hp_decr_factor = -0.1f * damage / max_health;
        Vector3 temp_hp_scale = health_bar.localScale + new Vector3(hp_decr_factor,0,0);
        if (temp_hp_scale.x >= 0){
            //Debug.Log("decrease health");
            health_bar.localScale = temp_hp_scale;
        }
            
        //health_bar.localScale += new Vector3(hp_decr_factor,0,0);
        if (cur_health <= 0){
            animator.runtimeAnimatorController = happyMonsterAnimationControllers[(int)colour];
            Destroy(this.gameObject, 1.0f);
        }
    }
    
    void stopMovement(){
        //stop the monster from moving
    }
    
    void OnTriggerEnter2D(Collider2D wave){
        //Debug.Log("we have a collision");
        if(wave.gameObject.CompareTag("Wave")){
            //Debug.Log("we have hit a 'Wave'");
            GameObject obj = wave.gameObject;//.GetComponent<Wave>();
            Wave comp = obj.GetComponent<Wave>();
            int wave_colour = comp.GetColor();
            if (colourConverter(wave_colour) == this.colour){
                takeDamage(comp.GetDamage());
                stopMovement();
            }
            //Debug.Log(typeof(comp));
            //int Damage = 2;
            //takeDamage(wave.gameObject.);
            
            //Destroy(this.gameObject);
        }
    }
}
