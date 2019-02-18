using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class NoteObject : MonoBehaviour {
    public Track track;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    
    public bool fade {
        set {
            Color color = image.color;
            color.a = value ? 0.5f : 1f;
            image.color = color;
        }
    }
        
    private Image image {
        get { return GetComponent<Image>(); }
    }
    private Sprite sprite {
        get { return image.sprite; }
        set { image.sprite = value; }
    }
    private Note _note;
    public Note note {
        get { return _note; }
        set {
            _note = value;
            switch (_note.dir) {
                case Note.Dir.UP: sprite = spriteUp; break;
                case Note.Dir.DOWN: sprite = spriteDown; break;
                case Note.Dir.LEFT: sprite = spriteLeft; break;
                case Note.Dir.RIGHT: sprite = spriteRight; break;
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
