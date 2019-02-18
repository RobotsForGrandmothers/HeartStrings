using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Green : Monster
{
    public AnimationCurve dashes;

    protected override void Start()
    {
        base.Start();
        color = (int)Colors.Green;
    }

    protected override void Move()
    {
        if (dashes.Evaluate((Time.time - creationTime) % 1.0f) < 0.5f)
        {
           Vector2 step = Vector2.MoveTowards((Vector2)this.transform.position, Vector2.zero, Time.deltaTime * speed);
           this.transform.position = step;
        }
    }
}
