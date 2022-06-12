using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeMenu : MonoBehaviour
{

    [SerializeField]
    private Text tName;

    [SerializeField]
    private Text atk;

    [SerializeField]
    private Text atkSpeed;

    [SerializeField]
    private Text atkRange;

    [SerializeField]
    private Text cost;

    [SerializeField]
    private Text upgradeText;


    private Tower targetTower;



    public void TowerUpgrade()
    {

        if (GameManager.Instance.Money >= targetTower.info.Cost)
        {

            targetTower.info.Upgrade();
            Setinfo(targetTower);
            upgradeText.text = "���׷��̵� ����";
        }
        else
        {
            upgradeText.text = "���� �����ؿ�";


        }
    
    }



    /// <summary>
    /// �޴� ���� ����
    /// </summary>
    /// <param name="name">Ÿ�� �̸�</param>
    /// <param name="atk">���ݷ�</param>
    /// <param name="speed">����</param>
    /// <param name="range">��Ÿ�</param>
    /// <param name="cost">���� ���</param>
    public void Setinfo(Tower tower)
    {

        
        this.gameObject.SetActive(true);
        this.tName.text = tower.name;
        this.atk.text = tower.info.Now.atk.ToString() + " -> " + (tower.info.Now.atk+ tower.info.Add.atk).ToString();
        this.atkSpeed.text = tower.info.Now.atkSpeed.ToString() + " -> " + (tower.info.Now.atkSpeed + tower.info.Add.atkSpeed).ToString();
        this.atkRange.text = tower.info.Now.atkRange.ToString() + " -> " + (tower.info.Now.atkRange + tower.info.Add.atkRange).ToString();
        this.cost.text = tower.info.Now.cost.ToString();
        targetTower = tower;
        upgradeText.text = "";

    }
}
