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
    ///   <Note> <Time>
    ///   ...
    ///   where Note is one of U,D,L,R, and Time is the time the note should be played.
    ///   the file should be sorted by Time.
    /// </summary>
    public Music(IEnumerable<string> instrumentNotes) {
        notes = new List<List<Tuple<float, Note>>>();

        foreach (string str in instrumentNotes) {
            notes.Add(new List<Tuple<float, Note>>());

            StringReader input = new StringReader(str);

            string line;
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

                notes[notes.Count - 1].Add(new Tuple<float,Note>(time, note));
            }
        }
    }

    public IEnumerable<Tuple<float, Note>> NoteIterator(int instrument) {
        return notes[instrument];
    }
}

public enum Note { UP, DOWN, LEFT, RIGHT }
