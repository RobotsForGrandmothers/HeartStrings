using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private int damage = 1;
    public AnimationCurve scaleCurve;
    public Gradient gradientCurve;
    float creationTime;

    void Awake()
    {
        creationTime = Time.time;
        Destroy(gameObject, 2.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * 5);
        transform.localScale = Vector3.one * scaleCurve.Evaluate(Time.time - creationTime);
        gameObject.GetComponent<SpriteRenderer>().color = gradientCurve.Evaluate((Time.time - creationTime)/1.8f);
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
        Color newColor;
        switch (color)
        {
            case 0:  newColor  = Color.red;   break;
            case 1:  newColor  = Color.green; break;
            case 2:  newColor  = Color.blue;  break;
            default: newColor  = Color.white; break;
        }

        GradientColorKey[] colorKeys = gradientCurve.colorKeys;
        colorKeys[0].color = newColor;
        colorKeys[1].color = newColor;

        gradientCurve.SetKeys(colorKeys, gradientCurve.alphaKeys);
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
