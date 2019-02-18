using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum Colour{
        RED,BLUE,GREEN
    }
    public Colour colour;
    public int max_health = 10;
    public float speed = 0.1f;
    public Vector2 monster_position;
    
    private int cur_health;
    private Vector2 player_position = new Vector2(0,0);
    private Transform health_bar;
    
    // Start is called before the first frame update
    void Start(){
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
        health_bar.localScale += new Vector3(hp_decr_factor,0,0);
        if (cur_health <= 0){
            Destroy(this.gameObject);
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
