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
    
    // Start is called before the first frame update
    void Start(){
        cur_health = max_health;
        
        
    }

    // Update is called once per frame
    void Update(){
        if ((Vector2)this.transform.position != player_position){
            Move();
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
}
