using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Blue : Monster
{
    protected override void Start()
    {
        base.Start();
        color = (int)Colors.Blue;
    }

    protected override void Move()
    {
        Vector2 step = Vector2.MoveTowards((Vector2)this.transform.position, Vector2.zero, Time.deltaTime * speed);
        this.transform.position = step;
    }
}
