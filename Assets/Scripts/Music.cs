using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music
{
    // instrument -> (time, note)
    private List<List<Tuple<float, Note>>> notes;
    
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
    public Music(IEnumerable<string> instrumentNotes) {
        notes = new List<List<Tuple<float, Note>>>();

        // read each instrument
        foreach (string str in instrumentNotes) {
            notes.Add(new List<Tuple<float, Note>>());

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

                Note note = Note.UP;
                float time;

                if (!Single.TryParse(split[1], out time)) {
                    Debug.LogError("Invalid time in music: " + line);
                    continue;
                }

                switch (split[0]) {
                    case "U": note = Note.UP; break;
                    case "D": note = Note.DOWN; break;
                    case "L": note = Note.LEFT; break;
                    case "R": note = Note.RIGHT; break;
                    default: Debug.LogError("Invalid note in music: " + line); continue;
                }

                notes[notes.Count - 1].Add(new Tuple<float,Note>(time * speed, note));
            }
        }
    }

    public IEnumerable<Tuple<float, Note>> NoteIterator(int instrument) {
        return notes[instrument];
    }
}

public enum Note { UP, DOWN, LEFT, RIGHT }
