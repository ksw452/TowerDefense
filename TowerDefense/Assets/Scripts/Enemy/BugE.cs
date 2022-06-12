using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugE : Enemy
{

    
    protected override void Awake()
    {
        base.Awake();
        myFlag = (int)EnemyFlag.bugE;

    }
}
