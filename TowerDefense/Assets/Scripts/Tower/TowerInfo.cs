

public class TowerInfo
{
    private AtkInfo baseInfo;
    private AtkInfo now;
    private AtkInfo add;


    public AtkInfo Now
    {
        get {
            return now;
        }
    
    }

    public AtkInfo Add
    {
        get
        {
            return add;
        }

    }

    public int Atk
    {

        get {

            return now.atk;
        }
    
    }
    public int Cost
    {

        get
        {

            return now.cost;
        }

    }

    public float AtkRange
    {

        get
        {

            return now.atkRange;
        }

    }

    public float AtkSpeed
    {

        get
        {

            return now.atkSpeed;
        }

    }


    public TowerInfo(AtkInfo now, AtkInfo add)
   {
        baseInfo = now;
        this.now = now;
        this.add = add;


    }

    public void Upgrade()
    {
        now.atk += add.atk;
        now.atkRange += add.atkRange;
        now.atkSpeed += add.atkSpeed;
        GameManager.Instance.Money -= now.cost;
        now.cost += add.cost;

    }

    public void nowReset()
    {

        now = baseInfo;

    
    }
}
