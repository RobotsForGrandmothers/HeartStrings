using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class NoteObject : MonoBehaviour {
    public Track track;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    
    private Sprite image {
        get { return GetComponent<Image>().sprite; }
        set { GetComponent<Image>().sprite = value; }
    }
    private Note _note;
    public Note note {
        get { return _note; }
        set {
            _note = value;
            switch (_note.dir) {
                case Note.Dir.UP: image = spriteUp; break;
                case Note.Dir.DOWN: image = spriteDown; break;
                case Note.Dir.LEFT: image = spriteLeft; break;
                case Note.Dir.RIGHT: image = spriteRight; break;
            }
        }
    }
    
    void Update() {
        Rect parent = this.transform.parent.GetComponent<RectTransform>().rect;
        RectTransform rect = this.GetComponent<RectTransform>();
        
        Vector3 pos = this.transform.localPosition;
        pos.x = parent.width / 2 + parent.width * (Time.time - _note.time) / track.windowWidth;
        this.transform.localPosition = pos;
    }
}
