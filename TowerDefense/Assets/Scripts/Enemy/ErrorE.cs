using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorE : ColorEnemy
{
    protected override void Awake()
    {
        base.Awake();
        myFlag = (int)EnemyFlag.errorE;
       

    }
}
