using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NoteObject : MonoBehaviour {
    public Track track;
    
    private string text {
        get { return GetComponent<Text>().text; }
        set { GetComponent<Text>().text = value; }
    }
    private Note _note;
    public Note note {
        get { return _note; }
        set {
            _note = value;
            switch (_note.dir) {
                case Note.Dir.UP: text = char.ConvertFromUtf32(0x2191); break;
                case Note.Dir.DOWN: text = char.ConvertFromUtf32(0x2193); break;
                case Note.Dir.LEFT: text = char.ConvertFromUtf32(0x2190); break;
                case Note.Dir.RIGHT: text = char.ConvertFromUtf32(0x2192); break;
            }
        }
    }
    
    void Update() {
        Rect parent = this.transform.parent.GetComponent<RectTransform>().rect;
        RectTransform rect = this.GetComponent<RectTransform>();
        
        Vector3 pos = this.transform.localPosition;
        pos.x = parent.width / 2 + parent.width * (Time.time - _note.time - track.timeBuffer) / track.windowWidth;
        this.transform.localPosition = pos;
    }
}
