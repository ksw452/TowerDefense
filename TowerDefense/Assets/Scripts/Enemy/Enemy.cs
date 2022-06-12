using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolOb
{


    
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected int money;
    [SerializeField]
    protected int attack;
    [SerializeField]
    protected float speed;
    //[SerializeField]
    //protected bool isHide; // 은신
    //[SerializeField]
    //protected bool isFly; //  공중

    private int startHp;
    private int myLayer;

    Transform moveTarget;


    public Coroutine coss;
    int section = 1;
    public void OnDamage(int value)
    { 
    
        hp -= value;

        if (hp <= 0)
        {
            GameManager.Instance.Money += money;
            Die();
        }
    
    }


  


    void Die()
    {

        ObjectPool.Instance.Set(this.gameObject,(int)ekind, myFlag);
        GameManager.Instance.enemyCount += 1;
    }

    protected virtual void Awake()
    {
        
        myLayer = gameObject.layer;
        ekind = EFlagkind.EnemyFlag;
        startHp = hp;
    }

    protected virtual void OnEnable()
    {
        
        gameObject.layer = myLayer;
        hp = startHp;
        coss = StartCoroutine(Move());
            
        
    }
    IEnumerator Move()
    {
        section = 1;
        this.transform.position = WayPoint.Way[0].position;
        this.transform.LookAt(WayPoint.Way[1].position);
        float distance;
        float distance1 = Vector3.Distance(WayPoint.Way[section - 1].position, WayPoint.Way[section].position);
        bool distances = false;
        while (true)
        {


            yield return null;





           float distance2 = Vector3.Distance(this.transform.position, WayPoint.Way[section-1].position);
            distance = Vector3.Distance(this.transform.position, WayPoint.Way[section].position);
                if ((distance < 0.05|| distance1 < distance2) && distances == false)
                {

                    section++;
                    if (section == WayPoint.Way.Count)
                    {
                    Die();
                    GameManager.Instance.Hp -= attack;
                        break;
                    }
                    Debug.Log(section);
                    distances = true;
                    distance1 = Vector3.Distance(WayPoint.Way[section - 1].position, WayPoint.Way[section].position);
                    this.transform.LookAt(WayPoint.Way[section].position);


                }

            if (distance1 >= distance2)
            {

                distances = false;
                this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
           

        }

            
    }


}
