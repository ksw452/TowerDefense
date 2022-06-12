using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{

    private static TowerManager instance;

    public static TowerManager Instance
    {
        get { return instance; }

        set { }


    }
    //람다식은 실제 실행되기 전에는 참조형태로 가지고있는데,

    //for문을 돌리면서 같은 변수인 i를 계속 줬기 때문에 마지막 값으로 통일된 것.

    //이를 closure problem이라고 부른다.

    private void Awake()
    {
        instance = this;
        
        for (int i = 0; i < transform.childCount; i++)
        {

            int index = i;
            //람다식은 참조 형식이므로 버튼에서는 실행이 되었을 때 참조값(i주소의 값)이 들어가므로 실행 되었을 때는 i가 for문마다 새로 만들어지는 것이 아니므로 i값은 2이다. 그래서 모든 버튼의 i값은 2가 될 것이다.
            //그러므로 int index로 새로 만들어 주소값을 다 다르게 한 뒤 실행 되었을 때 for문에서 같은 주소에 값이 계속 변하는게 아닌 다른 주소로 값이 들어가므로 값이 다를수 있도록 한다.
        
            transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => {GetTower(index); });

            


        }

        
    }


    public void GetTower(int index)
    {
     
        if (GameManager.Instance.Money >= GameManager.Instance.buttonArr[index].cost && GameManager.Instance.isBatch == false)
        {
            ObjectPool.Instance.get((int)EFlagkind.ETowerFlag, index);

            GameManager.Instance.isBatch = true;
        }

    }
    //public void VariableTower()
    //{
    //    if (GameManager.Instance.Money >= GameManager.Instance.buttonArr[0].cost && GameManager.Instance.isBatch ==false)
    //    { 
    //    ObjectPool.Instance.get((int)EFlagkind.ETowerFlag,(int)ETowerFlag.variableTower);

    //        GameManager.Instance.isBatch = true;
    //    }
    //}

    //public void IfTower()
    //{
    //    if (GameManager.Instance.Money >= GameManager.Instance.buttonArr[1].cost && GameManager.Instance.isBatch == false)
    //    {
    //        ObjectPool.Instance.get((int)EFlagkind.ETowerFlag,(int)ETowerFlag.ifTower);
           
    //        GameManager.Instance.isBatch = true;
    //    }
    //}

}
