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
    //���ٽ��� ���� ����Ǳ� ������ �������·� �������ִµ�,

    //for���� �����鼭 ���� ������ i�� ��� ��� ������ ������ ������ ���ϵ� ��.

    //�̸� closure problem�̶�� �θ���.

    private void Awake()
    {
        instance = this;
        
        for (int i = 0; i < transform.childCount; i++)
        {

            int index = i;
            //���ٽ��� ���� �����̹Ƿ� ��ư������ ������ �Ǿ��� �� ������(i�ּ��� ��)�� ���Ƿ� ���� �Ǿ��� ���� i�� for������ ���� ��������� ���� �ƴϹǷ� i���� 2�̴�. �׷��� ��� ��ư�� i���� 2�� �� ���̴�.
            //�׷��Ƿ� int index�� ���� ����� �ּҰ��� �� �ٸ��� �� �� ���� �Ǿ��� �� for������ ���� �ּҿ� ���� ��� ���ϴ°� �ƴ� �ٸ� �ּҷ� ���� ���Ƿ� ���� �ٸ��� �ֵ��� �Ѵ�.
        
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
