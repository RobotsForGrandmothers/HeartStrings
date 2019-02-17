using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music
{
    // instrument -> (time, note)
    private Dictionary<int, List<Tuple<float, Note>>> notes;
    
    /// <summary>
    ///   Create music from stream. Music stream should be in the following format
    ///   <Instrument> <Note> <Time>
    ///   ...
    ///   where Note is one of U,D,L,R, and Time is the time the note should be played.
    ///   the file should be sorted by Time. Instrument should be an integer.
    /// </summary>
    public Music(string str) {
        StringReader input = new StringReader(str);
        
        notes = new Dictionary<int, List<Tuple<float, Note>>>();
        
        string line;
        while ((line = input.ReadLine()) != null) {
            string[] split = line.Split(null);
            
            if (!(split.Length == 3)) Debug.LogError("Invalid line in music: " + line);
            
            int instrument;
            Note note = Note.UP;
            float time;
            
            if (!Int32.TryParse(split[0], out instrument)) Debug.LogError("Invalid instrument in music: " + line);
            if (!Single.TryParse(split[2], out time)) Debug.LogError("Invalid time in music: " + line);
            
            switch (split[1]) {
                case "U": note = Note.UP; break;
                case "D": note = Note.DOWN; break;
                case "L": note = Note.LEFT; break;
                case "R": note = Note.RIGHT; break;
                default: Debug.LogError("Invalid note in music: " + line); break;
            }
            
            if (!notes.ContainsKey(instrument)) {
                notes[instrument] = new List<Tuple<float, Note>>();
            }
            
            notes[instrument].Add(new Tuple<float,Note>(time, note));
        }
    }

    public IEnumerable<Tuple<float, Note>> NoteIterator(int instrument) {
        if (!notes.ContainsKey(instrument)) throw new ArgumentOutOfRangeException();
        
        return notes[instrument];
    }
}

public enum Note { UP, DOWN, LEFT, RIGHT }
