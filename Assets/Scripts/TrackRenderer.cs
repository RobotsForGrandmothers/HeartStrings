using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Track))]
public class TrackRenderer : MonoBehaviour {
    Dictionary<Note, NoteObject> notes = new Dictionary<Note, NoteObject>();
    public NoteObject notePrefab;
    

    void Start() {
        GetComponent<Track>().EnterWindow += AddNote;
        GetComponent<Track>().PlayNote += PlayNote;
        GetComponent<Track>().MissNote += MissNote;
    }
    
    void Update() {
        
    }
    
    void AddNote(object obj, NoteEvent evt) {
        Debug.Log("Added note " + evt);
        Rect rect = GetComponent<RectTransform>().rect;
        Vector2 localPos = -Vector2.up * (-rect.height / 2 + (evt.note.instrument + 0.5f) * rect.height / GetComponent<Track>().CountInstruments);
        notes[evt.note] = Instantiate(notePrefab, this.transform).GetComponent<NoteObject>();
        notes[evt.note].transform.localPosition = localPos;
        notes[evt.note].track = GetComponent<Track>();
        notes[evt.note].note = evt.note;
    }
    
    void PlayNote(object obj, NoteEvent evt) {
        Debug.Log("Played note " + evt);
        Destroy(notes[evt.note].gameObject);
        notes.Remove(evt.note);
    }
    
    void MissNote(object obj, NoteEvent evt) {
        Debug.Log("Missed note " + evt);
        Destroy(notes[evt.note].gameObject);
        notes.Remove(evt.note);
    }
}
