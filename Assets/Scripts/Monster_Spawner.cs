using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Spawner : MonoBehaviour{
    
    //rate per second that spawner will spawn a monster
    public float Spawn_rate = 1;

    private GameObject[] monsters;
    
    private float next_spawn = 0;
    private System.Random random;

    // Start is called before the first frame update
    void Start(){
        //Debug.Log(transform.position);
        random = new System.Random((int)transform.position.z);
        monsters = Resources.LoadAll<GameObject>("Prefabs/Monsters");
    }

    // Update is called once per frame
    void Update(){
        if (Time.time > next_spawn){
            SpawnMonster();
            next_spawn += Spawn_rate; 
        }
    }

    void SpawnMonster(){
        GameObject obj = Instantiate(monsters[random.Next(monsters.Length)]);
        Monster monster = obj.GetComponent<Monster>();
        float y_coord = random.Next(10) - 5.0f;
        float x_coord = transform.position.x;
        monster.transform.position = new Vector2(x_coord,y_coord);
    }
}
