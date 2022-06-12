using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFlagkind
{
    EObjectFlag,
    ETowerFlag,
    EnemyFlag
}
public enum EObjectFlag
{
    bullet,
   bulletHide,
   bulletFly,
   bulletSearch,
   electric
  
  
}

public enum ETowerFlag
{
   
    variableTower,
    ifTower,
    HDetectTower,
    DebugTower,
    SearchTower

}

public enum EnemyFlag
{
    
    bugE,
    grammarE,
    redLineE,
    errorE,
    warningE
}


[System.Serializable] //중요
public class cpyObject
{
    public List<Queue<GameObject>> queList = new List<Queue<GameObject>>();
    public GameObject[] cpyObj;
    public int[] initCount;
}



public class ObjectPool : MonoBehaviour
{
    public List<cpyObject> cpyList;


    public static ObjectPool Instance; //싱글톤 패턴
    //public int[] initCount;
    public List<Queue<GameObject>> queListObject = new List<Queue<GameObject>>();
    public List<Queue<GameObject>> queListEnemy = new List<Queue<GameObject>>();
    public List<Queue<GameObject>> queListTower = new List<Queue<GameObject>>();

    //public GameObject[] cpyObject;










    private void init(List<Queue<GameObject>> list, int count, GameObject gb, int flag)
    {

        if (GameObject.Find(gb.name) == null)
        {

           



            for (int i = 0; i < count; i++)
            {

                GameObject tempGB = GameObject.Instantiate(gb, this.transform);
                tempGB.gameObject.SetActive(false);
                list[flag].Enqueue(tempGB);
            }
        }

    }

    // get<T> (T flag) 매개변수를 제네릭 T 사용으로 모든 변수를 받음 
    // var a; 모든 값을 var로 변수 선언 후 값을ㄴ받으면 알아서 변환 a = (int)b 일경우 var a 가 int a 로 형변환
    
    // object a; 모든 값을 박싱하여 object로 받음 즉,object는 형과 값을 따로 저장가능 int, 3 이렇게 저장후 언박싱 (int)a 시 형과 값을 합쳐서 다시 int값으로 보냄
  /// <summary>
  /// 
  /// </summary>
  /// <param name="kind"> 플래그의 종류</param>
  /// <param name="index">해당하는 플래그의 인덱스 값</param>
  /// <returns></returns>
    public GameObject get(int kind, int index)
    {
        // int index = (int)(object)flag; 오브젝트 형으로 변환하여 flag 값 박싱 후 int 로 다시 언박싱 object는 모든 변수를 그대로 받는게 아닌 박싱함 즉, 형 값과 value 값을 따로 박싱후 object로 저장
       
        GameObject tempGB;
        if (cpyList[kind].queList[index].Count > 0)
        {

            tempGB = cpyList[kind].queList[index].Dequeue();

            tempGB.transform.SetParent(null);
            tempGB.SetActive(true);




        }
        else
        {
            tempGB = GameObject.Instantiate(cpyList[kind].cpyObj[index], this.transform);

        }
        return tempGB;

    }
    /// <summary>
    /// 총알 전용
    /// </summary>
    /// <param name="index">총알 종류</param>
    /// <returns>해당총알</returns>
    public GameObject get(int index,LayerMask lm,int damage)
    {
        // int index = (int)(object)flag; 오브젝트 형으로 변환하여 flag 값 박싱 후 int 로 다시 언박싱 object는 모든 변수를 그대로 받는게 아닌 박싱함 즉, 형 값과 value 값을 따로 박싱후 object로 저장
        int kind = (int)EFlagkind.EObjectFlag;
        GameObject tempGB;
        if (cpyList[kind].queList[index].Count > 0)
        {

            tempGB = cpyList[kind].queList[index].Dequeue();

            tempGB.transform.SetParent(null);
            tempGB.SetActive(true);




        }
        else
        {
            tempGB = GameObject.Instantiate(cpyList[kind].cpyObj[index], this.transform);

        }

        //SendMessage는 오브젝트가 false일때 쓸 수없다


        // 이렇게 안할 경우 다음 오브젝트를 instantiate 가 아닌 있던 것을 불러올 때 lm이 이미 첫번 째 때 sendmessage로 넣어줬으므로 삭제가 아닌SET(active false) 후 다시 true 되는 것이므로 lm은 그대로 들어있음
        // 그래서 적이 너무 가까이 있을 경우 탐지 후 바로 불렛이  die(false) 되는데 이 타이밍에 이 함수는 타워오브젝트에서 실행되므로 타워오브젝트는 false 아니므로 sendmessage가 호출 된다. 근데 sendmessage가 불러올 오브젝트가 불렛 오브젝트인데 false가 된 상태, false일때는 안되므로 오류가 나온다. 
        if (tempGB.activeSelf != false)
        {
            tempGB.SendMessage("SetLm", lm);
        }
        tempGB.SendMessage("AddTowerDamage",damage);
        return tempGB;

    }


    public GameObject get(int kind, int index, Vector3 pos, Vector3 rot)
    {
        GameObject tempGB;



        if (cpyList[kind].queList[index].Count > 0)
        {

            tempGB = cpyList[kind].queList[index].Dequeue();

            tempGB.transform.SetParent(null);
            tempGB.SetActive(true);

        }
        else
        {
            tempGB = GameObject.Instantiate(cpyList[kind].cpyObj[index], this.transform);


        }
        tempGB.transform.position = pos;
        tempGB.transform.eulerAngles = rot;

   
        return tempGB;

    }


    //switch (flag)
    //{

    //    case EObjectFlag.missile:
    //        if (missiles.Count > 0)
    //        {
    //            Missile tempMissile = missiles.Dequeue();
    //            tempMissile.transform.SetParent(null);
    //            tempMissile.gameObject.SetActive(true);
    //            return tempMissile;
    //        }
    //        else
    //        {

    //            Missile tempMissile = GameObject.Instantiate(missile, this.transform);
    //            tempMissile.transform.SetParent(null);
    //            return tempMissile;
    //        }
    //        break;

    //    case EObjectFlag.asteroid:
    //        if (missiles.Count > 0)
    //        {
    //            Asteroid tempAsteroid = asteroids.Dequeue();
    //            tempAsteroid.transform.SetParent(null);
    //            tempAsteroid.gameObject.SetActive(true);
    //            return tempAsteroid;
    //        }
    //        else
    //        {

    //            Missile tempMissile = GameObject.Instantiate(missile, this.transform);
    //            tempMissile.transform.SetParent(null);
    //            return tempMissile;
    //        }
    //        break;
    //    default:
    //        break;

    //}




    //}

    public void Set(GameObject gb, int kind, int index)
    {

        gb.gameObject.SetActive(false);
        gb.transform.SetParent(this.transform);
        cpyList[kind].queList[index].Enqueue(gb);

    }






    void Awake()
    {
        Instance = this;

        for (int i = 0; i < cpyList.Count; i++)
        {
            for (int j = 0; j < cpyList[i].cpyObj.Length; j++)
            {
                cpyList[i].queList.Add(new Queue<GameObject>());
                init(cpyList[i].queList, cpyList[i].initCount[j], cpyList[i].cpyObj[j], j);
            }
        }

      
    }

}
