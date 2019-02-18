using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private int damage = 1;

    void Awake()
    {
        Destroy(gameObject, 2.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime*5 * Vector2.right);

        Vector3 scale = transform.localScale;
        float newScale = scale.x + Time.deltaTime * 3;
        scale.Set(newScale, newScale, newScale);
        transform.localScale = scale;

        transform.localScale.Set(0, 0, 0);
    }

    public void SetDirection(bool direction) {
        if (direction)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 180));
        }
    }

    public void SetColor(int color)
    {
        switch (color)
        {
            case 2:
                Debug.Log("2");
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.3f, 0.9f);
                break;
            case 1:
                Debug.Log("1");
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0.8f, 0.43f, 0.9f);
                break;
            case 0:
                Debug.Log("0");
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0.60f, 1f, 0.9f);
                break;
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public int GetDamage()
    {
        return damage;
    }
}
