using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public TextAsset musicAsset;
    private Music music;

    // Start is called before the first frame update
    void Start()
    {
        music = new Music(musicAsset.text);
        
        foreach (Tuple<float, Note> m in music.NoteIterator(0)) {
            Debug.Log("Note: " + m.Item1 + " Time: " + m.Item2);
        }
    }
}
