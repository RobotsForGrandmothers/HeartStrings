using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Spawner : MonoBehaviour{
    
    //rate per second that spawner will spawn a monster
    public float Spawn_rate = 1;
    public GameObject preFab;
    //public Vector2 spawner_position;
    
    private float next_spawn = 0;
    private Array monster_colours = Enum.GetValues(typeof(Monster.Colour));
    private System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start(){
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update(){
        if (Time.time > next_spawn){
            Monster.Colour curent_colour = (Monster.Colour)monster_colours.GetValue(random.Next(monster_colours.Length));
            SpawnMonster(curent_colour);
            
            next_spawn += Spawn_rate; 
        }
    }

    void SpawnMonster(Monster.Colour colour_enum){
        GameObject obj = Instantiate(preFab);
        Monster monster = obj.GetComponent<Monster>();
        
        monster.colour = colour_enum;
        //monster.monster_position = spawner_position + new Vector2(1,1); //Eventually make this a semi random thing 

    }
}
