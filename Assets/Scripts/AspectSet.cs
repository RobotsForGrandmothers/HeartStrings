using UnityEngine;

public class AspectSet : MonoBehaviour {
	public Vector2 aspect = new Vector2(16.0f, 9.0f);
	public float size = 5;
	public bool lockWidth = false;

	// Use this for initialization
	// http://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html
	void Awake () {
		// set the desired aspect ratio
		float targetaspect = aspect.x / aspect.y;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		
		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		// set size depending on if width or height is locked
		if (lockWidth) {
			camera.orthographicSize = size / windowaspect;
		} else {
			camera.orthographicSize = size;
		}

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f) {  
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			
			camera.rect = rect;
		}
		// add pillarbox
		else {
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}
}
