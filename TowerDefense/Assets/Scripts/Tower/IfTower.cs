using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfTower : Tower
{
   
    protected override void Awake()
    { 
        myFlag = (int)ETowerFlag.ifTower;
        bulletKind = EObjectFlag.bullet;
        base.Awake();
    }

    protected override IEnumerator Attack()
    {
        StartCoroutine(base.Attack());
        yield return null;
        Transform bullet = GetBullet().transform;
        bullet.position = this.transform.position;
        bullet.rotation = this.transform.rotation;

        yield return new WaitForSeconds(info.AtkSpeed * 0.5f);
        bullet = GetBullet().transform;
        bullet.position = this.transform.position;
        bullet.rotation = this.transform.rotation;
    }
}
