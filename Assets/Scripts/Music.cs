using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music
{
    // instrument -> (time, note)
    private List<List<Note>> notes;
    
    public int CountInstruments { get { return notes.Count; } }
    
    /// <summary>
    ///   Create music from stings. Music string should be in the following format
    ///   <Speed>
    ///   <Note> <Time>
    ///   <Note> <Time>
    ///   ...
    ///   where speed is a float factor to multiply each time by,
    ///   note is one of U,D,L,R,
    ///   and Time is the time the note should be played.
    ///   
    ///   The file should be sorted by Time.
    /// </summary>
    public Music(IEnumerable<string> instrumentNotes, float initialDelay) {
        notes = new List<List<Note>>();
        
        // put id for each note.
        int id = 0;

        // read each instrument
        foreach (string str in instrumentNotes) {
            notes.Add(new List<Note>());

            StringReader input = new StringReader(str);
            string line;
            
            // read speed at the top
            float speed = 1;
            line = input.ReadLine();
            if (line == null || !Single.TryParse(line, out speed)) {
                Debug.LogError(
                    "Invalid speed in music: " + line + "\n"
                    + "Speed set to " + speed
                );
            }
           
            while ((line = input.ReadLine()) != null) {
                string[] split = line.Split(null);

                if (!(split.Length == 2)) {
                    Debug.LogError("Invalid line in music: " + line);
                    continue;
                }

                Note.Dir note = Note.Dir.UP;
                float time;

                if (!Single.TryParse(split[1], out time)) {
                    Debug.LogError("Invalid time in music: " + line);
                    continue;
                }

                switch (split[0]) {
                    case "U": note = Note.Dir.UP; break;
                    case "D": note = Note.Dir.DOWN; break;
                    case "L": note = Note.Dir.LEFT; break;
                    case "R": note = Note.Dir.RIGHT; break;
                    default: Debug.LogError("Invalid note in music: " + line); continue;
                }

                notes[notes.Count - 1].Add(new Note(id, notes.Count - 1, note, initialDelay + time * speed));
                ++id;
            }
        }
    }

    public IEnumerable<Note> NoteIteratable(int instrument) {
        return notes[instrument];
    }
}

public class Note {
    public enum Dir { UP, DOWN, LEFT, RIGHT }
    public int id { get; private set; }
    public int instrument { get; private set; }
    public Dir dir { get; private set; }
    public float time { get; private set; }
    
    public Note(int id, int instrument, Dir dir, float time) {
        this.id = id;
        this.instrument = instrument;
        this.dir = dir;
        this.time = time;
    }

    public override int GetHashCode() {
        return id;
    }

    public override bool Equals(object other) {
        if (other is Note) {
            Note o = (Note)other;
            return id == o.id;
        }
        return false;
    }
    
    public override string ToString() {
        return "{ id: " + id + ", instrument: " + instrument + ", dir: " + dir + ", time: " + time + " }";
    }
}
