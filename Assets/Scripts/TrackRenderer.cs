using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Track))]
public class TrackRenderer : MonoBehaviour {
    Dictionary<Note, NoteObject> notes = new Dictionary<Note, NoteObject>();
    public NoteObject notePrefab;
    public GameObject playBoundsPrefab;
    
    public PlayerController player;
    private int currentInstrument = 0;
    
    public Track track { get { return GetComponent<Track>(); } }

    void Start() {
        track.EnterWindow += AddNote;
        track.PlayNote += PlayNote;
        track.MissNote += MissNote;
        
        player.InstrumentSwitch += InstrumentSwitch;
        
        GameObject startBound = Instantiate(playBoundsPrefab, this.transform);
        startBound.name = "Play Bound Start";
        GameObject endBound = Instantiate(playBoundsPrefab, this.transform);
        endBound.name = "End Bound Start";
        Vector2 pos = startBound.transform.localPosition;
        pos.x = XForDeltaTime(-track.timeBuffer);
        startBound.transform.localPosition = pos;
        pos = endBound.transform.localPosition;
        pos.x = XForDeltaTime(track.timeBuffer);
        endBound.transform.localPosition = pos;
    }
    
    void InstrumentSwitch(object obj, InstrumentEvent evt) {
        currentInstrument = evt.instrument;
        foreach (NoteObject noteObj in notes.Values) {
            noteObj.fade = noteObj.note.instrument != evt.instrument;
        }
    }
    
    void AddNote(object obj, NoteEvent evt) {
        //Debug.Log("Added note " + evt);
        Rect rect = GetComponent<RectTransform>().rect;
        Vector2 localPos = -Vector2.up * (-rect.height / 2 + (evt.note.instrument + 0.5f) * rect.height / track.CountInstruments);
        notes[evt.note] = Instantiate(notePrefab, this.transform).GetComponent<NoteObject>();
        notes[evt.note].transform.localPosition = localPos;
        notes[evt.note].trackRenderer = this;
        notes[evt.note].note = evt.note;
        notes[evt.note].fade = evt.note.instrument != currentInstrument;
    }
    
    void PlayNote(object obj, NoteEvent evt) {
        //Debug.Log("Played note " + evt);
        Destroy(notes[evt.note].gameObject);
        notes.Remove(evt.note);
    }
    
    void MissNote(object obj, NoteEvent evt) {
        //Debug.Log("Missed note " + evt);
        Destroy(notes[evt.note].gameObject);
        notes.Remove(evt.note);
    }
    
    /// <summary>
    ///   Time 0 is the right hand side. Negative time going left.
    /// </summary>
    public float XForDeltaTime(float time) {
        Rect rect = GetComponent<RectTransform>().rect;
        return rect.width / 2 + rect.width * (time - track.timeBuffer) / track.windowWidth;
    }
}
