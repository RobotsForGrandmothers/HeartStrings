using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Spawner : MonoBehaviour{
    // need to listen to track to know which monster to spawn
    public Track track;
      
    public Monster monsterRed;
    public Monster monsterBlue;
    public Monster monsterGreen;
    public int spawnRedThreshold = 5;
    public int spawnBlueThreshold = 5;
    public int spawnGreenThreshold = 5;
    private int spawnRed = 0;
    private int spawnBlue = 0;
    private int spawnGreen = 0;
    
    //rate per second that spawner will spawn a monster
    private int cameraHeight = 10;

    private System.Random random;
    
    // Start is called before the first frame update
    void Start(){
        //Debug.Log(transform.position);
        random = new System.Random((int)transform.position.z);
        
        track.EnterWindow += NoteWindowEnter;
        track.PlayNote += NoteWindowExit;
        track.MissNote += NoteWindowExit;
    }

    // Update is called once per frame
    void Update() {
        if (spawnRed >= spawnRedThreshold) {
            SpawnMonster(monsterRed);
            spawnRed -= spawnRedThreshold;
        }
        if (spawnBlue >= spawnBlueThreshold) {
            SpawnMonster(monsterBlue);
            spawnBlue -= spawnBlueThreshold;
        }
        if (spawnGreen >= spawnGreenThreshold) {
            SpawnMonster(monsterGreen);
            spawnGreen -= spawnGreenThreshold;
        }
    }

    void SpawnMonster(Monster mon) {
        GameObject obj = Instantiate(mon.gameObject);
        Monster monster = obj.GetComponent<Monster>();
        float y_coord = random.Next(cameraHeight) - cameraHeight/2;
        float x_coord = transform.position.x;
        monster.transform.position = new Vector2(x_coord,y_coord);
    }
    
    void NoteWindowEnter(object obj, NoteEvent evt) {
        switch (evt.note.instrument) {
            case 0: ++spawnRed; break;
            case 1: ++spawnBlue; break;
            case 2: ++spawnGreen; break;
        }
    }
    
    void NoteWindowExit(object obj, NoteEvent evt) {
        switch (evt.note.instrument) {
            case 0: --spawnRed; break;
            case 1: --spawnBlue; break;
            case 2: --spawnGreen; break;
        }
    
    }
}
