using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public TextAsset[] musicTexts;
    private Music music;

    // Start is called before the first frame update
    void Start()
    {
        string[] musicStrings = new string[musicTexts.Length];
        for (int i = 0; i < musicTexts.Length; ++i) {
            musicStrings[i] = musicTexts[i].text;
        }
        music = new Music(musicStrings);
        
        for (int i = 0; i < music.CountInstruments; ++i) {
            foreach (Tuple<float, Note> m in music.NoteIterator(0)) {
                Debug.Log("Instrument: " + i + " Note: " + m.Item2 + " Time: " + m.Item1);
            }
        }
    }
}
