using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTower :Tower
{

    protected override void Awake()
    {
        myFlag = (int)ETowerFlag.DebugTower;
        bulletKind = EObjectFlag.bulletFly;

        base.Awake();
    }

    protected override IEnumerator Attack()
    {
        StartCoroutine(base.Attack());
        yield return null;
        Transform bullet = GetBullet().transform;
        bullet.position = this.transform.position;
        bullet.rotation = this.transform.rotation;




    }
}
