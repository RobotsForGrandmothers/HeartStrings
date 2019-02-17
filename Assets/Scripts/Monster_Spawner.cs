﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Spawner : MonoBehaviour{
    
    //rate per second that spawner will spawn a monster
    public float Spawn_rate = 1;
    public GameObject preFab;
    public float camera_height = 10;
    //public Vector2 spawner_position;
    
    private float next_spawn = 0;
    private Array monster_colours = Enum.GetValues(typeof(Monster.Colour));
    //private Array y_spawn_coords = Enumerable.Range(-5,5).ToArray();
    private System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start(){
        //Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update(){
        if (Time.time > next_spawn){
            
            SpawnMonster();
            
            next_spawn += Spawn_rate; 
        }
    }

    void SpawnMonster(){
        GameObject obj = Instantiate(preFab);
        Monster monster = obj.GetComponent<Monster>();
        //float randFloat = (float)random.NextDouble();
        //Debug.Log(randFloat);
        float y_coord = camera_height*((float)random.NextDouble() - 0.5f);//camera_height*randFloat;//(float)random.NextDouble();//(int)y_spawn_coords.GetValue(random.Next(y_spawn_coords.Length));
        Debug.Log(y_coord);
        
        monster.colour = (Monster.Colour)monster_colours.GetValue(random.Next(monster_colours.Length));
        monster.transform.position = (Vector2)this.transform.position + new Vector2(-2,y_coord);
        //monster.monster_position = spawner_position + new Vector2(1,1); //Eventually make this a semi random thing 

    }
}
