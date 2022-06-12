using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : PoolOb
{
    public float lifeTime = 0.2f;

    private WaitForSeconds wfs;
    private void Awake()
    {
        ekind = EFlagkind.EObjectFlag;
        wfs = new WaitForSeconds(lifeTime);
        myFlag = (int)EObjectFlag.electric;
    }

    private void OnEnable()
    {
        StartCoroutine(Die());

    }

    IEnumerator Die()
    {


        yield return wfs;
        ObjectPool.Instance.Set(this.gameObject, (int)ekind, myFlag);

    }
}
