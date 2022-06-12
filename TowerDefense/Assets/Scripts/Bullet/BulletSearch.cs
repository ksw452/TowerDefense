using System.Collections;
using System.Collections.Generic;
using UnityEngine;



enum EEnemyLayer
{ 
        enemy =6,
        hide = 10,
        fly =11,
        hidefly = 12


}

public class BulletSearch : Bullet
{

    int transition = 3; //전이 횟수
    float transitionRange = 10f; //전이 범위


    void PropertyReset(GameObject enemy)
    {
        if (enemy.layer != (int)EEnemyLayer.enemy)
        {
            enemy.layer = (int)EEnemyLayer.enemy;

            if (enemy.gameObject.activeSelf)
            {
                enemy.SendMessage("SetColor", Color.black);
            }
        }
    }

    //전이 방식
    //볼리베어 전기 평타패시브

    protected override void Attack(Collider[] enemys)
    {
        if (enemys == null)
        {
            return;
        }



        if (enemys.Length > 0)
        {

            List<Collider> enemyHitList = new List<Collider>(); //이미 때린 적 넣어줄 리스트


            enemys[0].gameObject.SendMessage("OnDamage", damage); // 해당 오브젝트에 메시지 보내기 (함수 호출)
            PropertyReset(enemys[0].gameObject);
            enemyHitList.Add(enemys[0]); //때렸으니깐 리스트에 담아줌


            Collider[]  enemysT = Rader(transitionRange); //전이할 오브젝트 찾기
            ObjectPool.Instance.Set(this.gameObject, (int)ekind, myFlag);
            if (enemysT == null)
            {

                return;


            }
            else //전이할 오브젝트가 있다면 그것이 이미 때린 오브젝트여도
            {
                int count = 0;
               

                for (int i = 0; i < enemysT.Length; i++) //더 때릴 오브젝트 찾기
                {

                    for (int j = 0; j < enemyHitList.Count; j++) //이미 때린 오브젝트 찾기
                    {
                      

                        if (enemyHitList[j] != enemysT[i]) //더 떄릴 오브젝트가 이미 떄린게 아니라면
                        {
                        

                            GameObject e =  ObjectPool.Instance.get((int)EFlagkind.EObjectFlag,(int)EObjectFlag.electric); //전기 이펙트 가져오기

                            Transform start = enemyHitList[enemyHitList.Count - 1].transform; //전기 시작위치
                            Transform end = enemysT[i].transform; //전기 도착위치

                            e.transform.position = start.position; 
                            e.transform.LookAt(end.position);

                            float distance = Vector3.Distance(start.position, end.position);

                            Vector3 scale = e.transform.localScale;
                            scale.z = distance;
                            e.transform.localScale = scale;


                            if (enemysT[i].gameObject.activeSelf)
                            {
                          
                               enemysT[i].gameObject.SendMessage("OnDamage", damage);
                                PropertyReset(enemysT[i].gameObject);
                            }
                                enemyHitList.Add(enemysT[i]); //end 오브젝트 때렸으니 추가

                            count++;

                            if (transition == count)
                            {
                                return;

                            }

                        }

                    }

                }
            
            }


        }
    }
}
