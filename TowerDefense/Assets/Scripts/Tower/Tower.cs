using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public interface Iattack
//{

//    public void Attack();


//}


enum state
{ 
    batchMode,
    idle,
    attack



}




public class Tower : PoolOb
{

    state myState = state.batchMode;


    public TowerInfo info;

    [SerializeField]
    public string name;

    [SerializeField]
    protected AtkInfo now;
    [SerializeField]
    protected AtkInfo add;

    [SerializeField]
    protected int cost; //설치 비용
    [SerializeField]
    protected EObjectFlag bulletKind;

    
    
    public int Cost
    {
        set { }
        get { return cost; }
    }

    [SerializeField]
    protected LayerMask lm;
    [SerializeField]
    Transform target;
    [SerializeField]
    LayerMask lm2;

    Collider spCollider;
    WaitForSeconds wfs;

    private TextMesh text;

    protected GameObject GetBullet()
    { 
    
        return ObjectPool.Instance.get((int)bulletKind, lm,now.atk);

    }



    protected virtual void Awake()
    {
   
        info = new TowerInfo(now, add);
        ekind = EFlagkind.ETowerFlag;
        spCollider = GetComponent<Collider>();
        wfs = new WaitForSeconds(info.AtkSpeed);

        text = this.transform.GetChild(0).GetComponent<TextMesh>();
        cost = GameManager.Instance.buttonArr[myFlag].cost;
    }






    private void OnEnable()
    {
        spCollider.enabled = false;
        myState = state.batchMode;
        info.nowReset();
        StartCoroutine(Fsm());
    }


    private void OnMouseDown()
    {

        if (myState != state.batchMode)
        {

            GameManager.Instance.upgradeMenu.Setinfo(this);
            Debug.Log("배치한 타워 들어옴");
        }
    }

    void FindEnemy(Collider[] enemys)
    {

        if (enemys == null)
        {
            return;
        }
        if (target == null)
        {



            if (enemys.Length > 0)
            {
                target = enemys[0].transform;

            }



        }
        else
        {



            if (enemys.Length > 0)
            {

                for (int i = 0; i < enemys.Length; i++)
                {

                    if (target == enemys[i].transform)
                    {
                       
                        break;
                    }
                }
                target = enemys[0].transform;
            }
            else
            {
                target = null;
            }
        }

    }

    

    Collider[] Rader()
    {
        Collider[] enemys = Physics.OverlapSphere(transform.position, info.AtkRange, lm);

        if (enemys.Length == 0)
        {
            myState = state.idle;
            return null;

        }
        else
        {
            myState = state.attack;
            return enemys;

        }
      
    }

    IEnumerator Batch()
    {
        while (true)
        {
            //Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Pos.y = 0;
            //this.transform.position = Pos;

            //if (Input.GetMouseButtonDown(0))
            //{
            //    spCollider.enabled = true;
            //    myState = state.idle;
            //    break;
            //}

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //RaycastHit hit;


            //if (Physics.Raycast(ray, out hit, 15f))
            //{
            //    Vector3 tempPos = hit.point;
            //    tempPos.y = 0;

            //    this.transform.position = tempPos;
            //    if (hit.transform.gameObject.layer == 7)
            //    {

            //        text.color = Color.black;
            //        if (Input.GetMouseButtonDown(0))
            //        {
            //            spCollider.enabled = true;
            //            myState = state.idle;
            //            break;
            //        }

            //    }
            //    else { 

            //    text.color = Color.red;}

            //}


            RaycastHit[] hits = Physics.RaycastAll(ray, 15f);

            if (hits.Length == 1)
            {
                text.color = Color.black;
                this.transform.position = hits[0].point;
                if (Input.GetMouseButtonDown(0))
                {
                    spCollider.enabled = true;
                    myState = state.idle;
                    GameManager.Instance.isBatch = false;

                    switch (myFlag)
                    {
                       
                        case(int) ETowerFlag.variableTower:
                            GameManager.Instance.Money -= GameManager.Instance.buttonArr[0].cost;
                            break;
                        case (int)ETowerFlag.ifTower:
                            GameManager.Instance.Money -= GameManager.Instance.buttonArr[1].cost;
                            break;
                        case (int)ETowerFlag.HDetectTower:
                            GameManager.Instance.Money -= GameManager.Instance.buttonArr[2].cost;
                            break;
                        case (int)ETowerFlag.DebugTower:
                            GameManager.Instance.Money -= GameManager.Instance.buttonArr[3].cost;
                            break;
                        case (int)ETowerFlag.SearchTower:
                            GameManager.Instance.Money -= GameManager.Instance.buttonArr[4].cost;
                            break;
                        default:
                            break;
                    }

                    break;

                }

           
            }
            else
            {
                text.color = Color.red;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.gameObject.layer == 7)
                    {
                        this.transform.position = hits[i].point;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                ObjectPool.Instance.Set(this.gameObject,(int)ekind, myFlag);
                GameManager.Instance.isBatch = false;
            }


            yield return null;
        }
    }


        IEnumerator Fsm()
        {

            while (true)
            {
               

                switch (myState)
                {
                    case state.batchMode:
                        yield return StartCoroutine(Batch());    
                        break;
                    case state.idle:
                        Rader();
                        break;
                    case state.attack:
                      
                        FindEnemy(Rader());
                    
                    yield return StartCoroutine(Attack());
                        break;
                    default:
                        break;
                }




                yield return wfs;
            }

        }


    protected virtual IEnumerator Attack()
    {
        if (target != null)
        {

            this.transform.LookAt(target.position);
        }
        yield return null;
    }

        //void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(this.transform.position, atkRange);


        //}
    

}
