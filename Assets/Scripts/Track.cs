using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {
    public TextAsset[] musicTexts;
    public AudioSource audio;
    private Music music;
    public int CountInstruments { get { return music.CountInstruments; } }
    
    public float windowWidth = 10;
    public float timeBuffer = 0.5f;
    public float initialDelay = 5f;
    
    private List<IEnumerator<Note>> noteIterators;
    public event EventHandler<NoteEvent> EnterWindow;
    public event EventHandler<NoteEvent> PlayNote;
    public event EventHandler<NoteEvent> MissNote;
    
    List<Queue<Note>> windowNotes = new List<Queue<Note>>();
    
    // Start is called before the first frame update
    void Start() {
        string[] musicStrings = new string[musicTexts.Length];
        for (int i = 0; i < musicTexts.Length; ++i) {
            musicStrings[i] = musicTexts[i].text;
        }
        music = new Music(musicStrings, initialDelay);
        
        noteIterators = new List<IEnumerator<Note>>();
        for (int i = 0; i < music.CountInstruments; ++i) {
            var it = music.NoteIteratable(i).GetEnumerator();
            it.MoveNext();
            noteIterators.Add(it);
            windowNotes.Add(new Queue<Note>());
        }
        
        audio.Play();
    }
    
    void Update() {
        for (int i = 0; i < noteIterators.Count; ++i) {
            var it = noteIterators[i];
            Note note = it.Current;
          
            while (note != null && (Time.time - note.time + windowWidth > 0)) {
                OnEnterWindow(note);
                it.MoveNext();
                note = it.Current;
            }
            
            var q = windowNotes[i];
            
            while (q.Count > 0 && q.Peek().time - audio.time < -timeBuffer) {
                OnMissNote(q.Dequeue());
            }
        }
    }
    
    void OnEnterWindow(Note note) {
        windowNotes[note.instrument].Enqueue(note);
        EventHandler<NoteEvent> handler = EnterWindow;
        //Debug.Log("Note entered window: " + note);
        if (handler != null) handler(this, new NoteEvent(note));
    }
    
    void OnMissNote(Note note) {
        EventHandler<NoteEvent> handler = MissNote;
        //Debug.Log("Missed note: " + note);
        if (handler != null) handler(this, new NoteEvent(note));
    }
    
    void OnPlayNote(Note note) {
        EventHandler<NoteEvent> handler = PlayNote;
        //Debug.Log("Played note: " + note);
        if (handler != null) handler(this, new NoteEvent(note));
    }
    
    public bool TryPlayNote(int instrument, Note.Dir dir) {
		if (windowNotes[instrument].Count == 0) return false;

        Note latest = windowNotes[instrument].Peek();

        if (Mathf.Abs(latest.time - audio.time) <= timeBuffer) {
            windowNotes[instrument].Dequeue();

            if (latest.dir == dir) {
                OnPlayNote(latest);
                return true;
            } else {
                OnMissNote(latest);
                return false;
            }
        } else {
            return false;
        }
    }
}

public class NoteEvent : EventArgs {
    public Note note;
      
    public NoteEvent(Note note) {
        this.note = note;
    }
    
    public override string ToString() {
        return note.ToString();
    }
}
