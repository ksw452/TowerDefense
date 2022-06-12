using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTower : Tower /*, Iattack*/ // c# java는 일반 클래스가 여러 일반 클래스 다중상속 불가능 인터페이스 다중상속은 가능   c++은 하나의 일반클래스가 일반클래스 여러개 다중상속 가능
{





    protected override void Awake()
    {
                myFlag = (int)ETowerFlag.variableTower;
        bulletKind = EObjectFlag.bullet;
        base.Awake();

        cost = 10;
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
