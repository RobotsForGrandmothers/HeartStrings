using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class NoteObject : MonoBehaviour {
    public TrackRenderer trackRenderer;
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
        Vector3 pos = this.transform.localPosition;
        pos.x = trackRenderer.XForDeltaTime(Time.time - _note.time);
        this.transform.localPosition = pos;
    }
}
