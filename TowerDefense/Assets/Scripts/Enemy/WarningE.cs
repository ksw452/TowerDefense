using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningE : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        myFlag = (int)EnemyFlag.warningE;

    }
}
