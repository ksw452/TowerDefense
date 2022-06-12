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


[System.Serializable] //�߿�
public class cpyObject
{
    public List<Queue<GameObject>> queList = new List<Queue<GameObject>>();
    public GameObject[] cpyObj;
    public int[] initCount;
}



public class ObjectPool : MonoBehaviour
{
    public List<cpyObject> cpyList;


    public static ObjectPool Instance; //�̱��� ����
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

    // get<T> (T flag) �Ű������� ���׸� T ������� ��� ������ ���� 
    // var a; ��� ���� var�� ���� ���� �� ������������ �˾Ƽ� ��ȯ a = (int)b �ϰ�� var a �� int a �� ����ȯ
    
    // object a; ��� ���� �ڽ��Ͽ� object�� ���� ��,object�� ���� ���� ���� ���尡�� int, 3 �̷��� ������ ��ڽ� (int)a �� ���� ���� ���ļ� �ٽ� int������ ����
  /// <summary>
  /// 
  /// </summary>
  /// <param name="kind"> �÷����� ����</param>
  /// <param name="index">�ش��ϴ� �÷����� �ε��� ��</param>
  /// <returns></returns>
    public GameObject get(int kind, int index)
    {
        // int index = (int)(object)flag; ������Ʈ ������ ��ȯ�Ͽ� flag �� �ڽ� �� int �� �ٽ� ��ڽ� object�� ��� ������ �״�� �޴°� �ƴ� �ڽ��� ��, �� ���� value ���� ���� �ڽ��� object�� ����
       
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
    /// �Ѿ� ����
    /// </summary>
    /// <param name="index">�Ѿ� ����</param>
    /// <returns>�ش��Ѿ�</returns>
    public GameObject get(int index,LayerMask lm,int damage)
    {
        // int index = (int)(object)flag; ������Ʈ ������ ��ȯ�Ͽ� flag �� �ڽ� �� int �� �ٽ� ��ڽ� object�� ��� ������ �״�� �޴°� �ƴ� �ڽ��� ��, �� ���� value ���� ���� �ڽ��� object�� ����
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

        //SendMessage�� ������Ʈ�� false�϶� �� ������


        // �̷��� ���� ��� ���� ������Ʈ�� instantiate �� �ƴ� �ִ� ���� �ҷ��� �� lm�� �̹� ù�� ° �� sendmessage�� �־������Ƿ� ������ �ƴ�SET(active false) �� �ٽ� true �Ǵ� ���̹Ƿ� lm�� �״�� �������
        // �׷��� ���� �ʹ� ������ ���� ��� Ž�� �� �ٷ� �ҷ���  die(false) �Ǵµ� �� Ÿ�ֿ̹� �� �Լ��� Ÿ��������Ʈ���� ����ǹǷ� Ÿ��������Ʈ�� false �ƴϹǷ� sendmessage�� ȣ�� �ȴ�. �ٵ� sendmessage�� �ҷ��� ������Ʈ�� �ҷ� ������Ʈ�ε� false�� �� ����, false�϶��� �ȵǹǷ� ������ ���´�. 
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
