using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTower : Tower /*, Iattack*/ // c# java�� �Ϲ� Ŭ������ ���� �Ϲ� Ŭ���� ���߻�� �Ұ��� �������̽� ���߻���� ����   c++�� �ϳ��� �Ϲ�Ŭ������ �Ϲ�Ŭ���� ������ ���߻�� ����
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
