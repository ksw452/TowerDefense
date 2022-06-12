using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLineE : ColorEnemy
{
    protected override void Awake()
    {
        base.Awake();
        myFlag = (int)EnemyFlag.redLineE;

    }
}
