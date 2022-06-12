
using UnityEngine;

public class WaveInfo
{
    public WaveInfo(EnemyFlag f, int c, float t)
    {
        flag = f;
        enemyCount = c;
        times = t;

         
    }

        public EnemyFlag flag;

        public int enemyCount;

        public float times;
    
}
