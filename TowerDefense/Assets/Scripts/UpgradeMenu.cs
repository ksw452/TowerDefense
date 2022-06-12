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
            upgradeText.text = "업그레이드 성공";
        }
        else
        {
            upgradeText.text = "돈이 부족해요";


        }
    
    }



    /// <summary>
    /// 메뉴 정보 세팅
    /// </summary>
    /// <param name="name">타워 이름</param>
    /// <param name="atk">공격력</param>
    /// <param name="speed">공속</param>
    /// <param name="range">사거리</param>
    /// <param name="cost">업글 비용</param>
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
