using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEnemy : Enemy
{
    Color myColor;

    private TextMesh tm;
    protected override void Awake()
    {
        base.Awake();
        tm = this.transform.GetChild(0).GetComponent<TextMesh>();
        myColor = tm.color;
    }

    protected void SetColor(Color c)
    {
        tm.color = c;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.transform.GetChild(0).GetComponent<TextMesh>().color = myColor;
    }
}
