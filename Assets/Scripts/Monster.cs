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
        Debug.Log(this.transform.GetChild(0));
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
    
    void takeDamage(int damage){
        cur_health -= damage;
        float hp_decr_factor = -0.1f * damage / max_health;
        health_bar.localScale += new Vector3(hp_decr_factor,0,0);
        if (cur_health <= 0){
            Destroy(this.gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D wave){
        if(wave.gameObject.name == "test_wave"){
            //int Damage = 2;
            takeDamage(10);
            
            //Destroy(this.gameObject);
        }
    }
}
