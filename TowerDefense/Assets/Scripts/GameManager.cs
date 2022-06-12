using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public static GameManager Instance
    {
        get {
            return instance;    
            }
    
    }

    [SerializeField]
    Text moneyText;

    [SerializeField]
    GameObject  gameover;

    [SerializeField]
    Transform buttons;
    [SerializeField]
    Text hpText;
    [SerializeField]
    Toggle doubleSpeed;

    [SerializeField]
    public UpgradeMenu upgradeMenu;

    public List<GameObject> enemyList = new List<GameObject>();


    public void SetSpeed()
    {

        if (doubleSpeed.isOn)
        {
            Time.timeScale = 2.0f;

        }
        else {

            Time.timeScale = 1.0f;
        }
    
    }
    public class ButtonData
    {
        public Button bt;
        public int cost;
        public ButtonData(Button bt, int cost)
        {
            this.bt = bt;
            this.cost = cost;
        }


    
    }


    public List<ButtonData> buttonArr = new List<ButtonData>();
    
    public int money = 0;

    public int Money
    {
        get { return money; }
        set {

            money = value;
            moneyText.text = money.ToString();

            for (int i = 0; i < buttonArr.Count; i++)
            {
                if (buttonArr[i].cost > money)
                {
                    buttonArr[i].bt.interactable = false;
                }
                else {
                    buttonArr[i].bt.interactable = true;
                }
                
                }
        
        }
    }

    private int hp;
    public int Hp
    {
        get {
            return hp;
        
        }
        set {

            hp= value;
            hpText.text = hp.ToString();

            if (hp <= 0)
            {
                GameOver();
            
            }
        }
    }
    public bool isBatch = false;
    public int spawnCount;
    public int enemyCount;

    void GameOver()
    {

        gameover.SetActive(true);
        Time.timeScale = 0;
    
    }

    public void ReStart()
    {

        GameObject gs = GameObject.Find("ObjectPool");

        for (int i = 0; i < gs.transform.childCount; i++)
        {
            if (gs.transform.GetChild(i).GetComponent<Enemy>() != null)
            {

                StopCoroutine(gs.transform.GetChild(i).GetComponent<Enemy>().coss);
            }
            }


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     public void Title()
    { SceneManager.LoadScene("Title"); }
    IEnumerator Spawn(Wave w)
    {
        spawnCount=0;
        enemyCount = 0;
        for (int i = 0; i < w.enemys.Count; i++)
        {
            spawnCount += w.enemys[i].enemyCount;
        }

            for (int i = 0; i < w.enemys.Count; i++)
        {
            for (int c = 0; c< w.enemys[i].enemyCount; c++)
            {
                enemyList.Add(ObjectPool.Instance.get((int)EFlagkind.EnemyFlag,(int)w.enemys[i].flag));
                yield return new WaitForSeconds(w.enemys[i].times);
            }
        }
        while (spawnCount != enemyCount)
        {
            yield return null;
        }
      


    
    }

    void WaveReset(Wave w)
    {
        for (int i = 0; i < w.enemys.Count; i++)
        {
            w.enemys.Remove(w.enemys[0]);
        }    
    
    }


    void Clear()
    {
        gameover.transform.GetChild(0).GetComponent<Text>().text = "Clear";
        gameover.SetActive(true);
        


    }
    IEnumerator Wave1()
    {
        Wave wave = new Wave();
        wave.enemys.Add(new WaveInfo(EnemyFlag.redLineE, 2, 1.0f));
        wave.enemys.Add(new WaveInfo(EnemyFlag.errorE, 2, 1.0f));
        wave.enemys.Add(new WaveInfo(EnemyFlag.bugE, 2, 1.0f));
        wave.enemys.Add(new WaveInfo(EnemyFlag.grammarE, 2, 1.0f));
        yield return StartCoroutine(Spawn(wave));

        WaveReset(wave);

        wave.enemys.Add(new WaveInfo(EnemyFlag.grammarE, 2, 1.0f));
        wave.enemys.Add(new WaveInfo(EnemyFlag.bugE, 2, 1.0f));
        wave.enemys.Add(new WaveInfo(EnemyFlag.redLineE, 2, 1.0f));
        wave.enemys.Add(new WaveInfo(EnemyFlag.errorE, 2, 1.0f));
        yield return StartCoroutine(Spawn(wave));

        Clear();

    }

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        money = 200;
        Hp = 200;
        moneyText.text = money.ToString();


        for (int i = 0; i < buttons.childCount; i++)
        {
           
           buttonArr.Add(new ButtonData(buttons.GetChild(i).GetComponent<Button>(),int.Parse(buttons.GetChild(i).name)));
        }
    }


    public void StartGame()
    {
        GameObject.Find("Ways").GetComponent<WayPoint>().WayStart();

        StartCoroutine(Wave1());
        
    }
  

  
}
