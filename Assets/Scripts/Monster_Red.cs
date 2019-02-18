using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Red : Monster
{
    public AnimationCurve wobble;

    protected override void Start()
    {
        base.Start();
        color = (int)Colors.Red;
    }

    protected override void Move()
    {
        Vector2 step = Vector2.MoveTowards((Vector2)this.transform.position, Vector2.zero, Time.deltaTime * speed);
        step = step + Vector2.up * wobble.Evaluate((Time.time - creationTime)%0.5f);
        this.transform.position = step;
    }
}
