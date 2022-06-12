using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarE : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        myFlag = (int)EnemyFlag.grammarE;
    }
}
