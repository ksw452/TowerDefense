using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bullet : PoolOb
{
    //[SerializeField]
    ////private int myDamage = 1;
    public float speed =2f;
    protected int damage =1;
    public float lifeTime = 3f;

    private WaitForSeconds wfs;

    private LayerMask lm;


    public void SetLm(LayerMask lm)
    {


        this.lm = lm;
    
    }


    private void Awake()
    {
        ekind = EFlagkind.EObjectFlag;
        wfs = new WaitForSeconds(lifeTime);
        myFlag = (int)EObjectFlag.bullet;
    }

    private void OnEnable()
    {
        StartCoroutine(Die());
        StartCoroutine(FindEnemy());
    }


    IEnumerator Die()
    {


        yield return wfs;
        ObjectPool.Instance.Set(this.gameObject,(int)ekind, myFlag);

    }

    public void AddTowerDamage(int damage)
    {
        this.damage += damage;
    }

     protected virtual void Attack(Collider[] enemys)
    {

        if (enemys == null)
        {
            return;
        }



        if (enemys.Length > 0)
        {

            enemys[0].gameObject.SendMessage("OnDamage", damage); // 해당 오브젝트에 메시지 보내기 (함수 호출)

            ObjectPool.Instance.Set(this.gameObject, (int)ekind, myFlag);

        }
   

    }

    protected Collider[] Rader()
    {
        Collider[] enemys = Physics.OverlapSphere(transform.position, 0.5f, lm);

        if (enemys.Length == 0)
        {
          
            return null;

        }
        else
        {
      
            return enemys;

        }

    }

    protected Collider[] Rader(float range)
    {
        Collider[] enemys = Physics.OverlapSphere(transform.position, range, lm);

        if (enemys.Length == 0)
        {

            return null;

        }
        else
        {

            return enemys;

        }

    }
    IEnumerator FindEnemy()
    {

        while (true)
        {

            Attack(Rader());

            yield return null;
          
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*speed);
    }
}
