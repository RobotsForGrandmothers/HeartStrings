using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum Colors { Red, Blue, Green };

    public int max_health;
    public float speed;
    public Vector2 monster_position;
    public float wait_time = 2;
    
    private int cur_health;
    private Vector2 player_position = new Vector2(0,0);
    private Transform health_bar;
    private bool should_move = true;
    private float time_of_stop = 0;

    private Animator animator;
    public RuntimeAnimatorController normalAnimationController;
    public RuntimeAnimatorController deathAnimationController;
    
    protected float creationTime;
    protected int color;

    // Start is called before the first frame update
    protected virtual void Start(){
        creationTime = Time.time;
        animator = gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = normalAnimationController;
        
        cur_health = max_health;
        health_bar = this.transform.GetChild(0);

        if (transform.position.x < 0)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
    }

    // Update is called once per frame
    void Update(){
        if (Vector2.Distance(transform.position, Vector2.zero) > 1){
            if (Time.time > time_of_stop + wait_time){
                Move();
            }
        }
        else{
            //maybe leave the screen
            Destroy(this.gameObject);
        }
    }

    virtual protected void Move(){}
    
    void takeDamage(int damage){
        cur_health -= damage;
        float hp_decr_factor = -0.1f * damage / max_health;
        Vector3 temp_hp_scale = health_bar.localScale + new Vector3(hp_decr_factor,0,0);
        if (temp_hp_scale.x >= 0){
            health_bar.localScale = temp_hp_scale;
        }
            
        if (cur_health <= 0){
            animator.runtimeAnimatorController = deathAnimationController;
            Destroy(this.gameObject);
        }
    }
    
    void stopMovement(){
        should_move = false;
        time_of_stop = Time.time;
        //stop the monster from moving
    }
    
    void OnTriggerEnter2D(Collider2D wave){
        if(wave.gameObject.CompareTag("Wave")){
            Wave obj = wave.gameObject.GetComponent<Wave>();
            if (obj.GetColor() == color){
                takeDamage(obj.GetDamage());
                stopMovement();
            }
            //Debug.Log(typeof(comp));
            //int Damage = 2;
            //takeDamage(wave.gameObject.);
            
            //Destroy(this.gameObject);
        }
    }
}
