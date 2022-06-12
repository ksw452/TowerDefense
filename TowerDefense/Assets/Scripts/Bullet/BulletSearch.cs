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

    int transition = 3; //���� Ƚ��
    float transitionRange = 10f; //���� ����


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

    //���� ���
    //�������� ���� ��Ÿ�нú�

    protected override void Attack(Collider[] enemys)
    {
        if (enemys == null)
        {
            return;
        }



        if (enemys.Length > 0)
        {

            List<Collider> enemyHitList = new List<Collider>(); //�̹� ���� �� �־��� ����Ʈ


            enemys[0].gameObject.SendMessage("OnDamage", damage); // �ش� ������Ʈ�� �޽��� ������ (�Լ� ȣ��)
            PropertyReset(enemys[0].gameObject);
            enemyHitList.Add(enemys[0]); //�������ϱ� ����Ʈ�� �����


            Collider[]  enemysT = Rader(transitionRange); //������ ������Ʈ ã��
            ObjectPool.Instance.Set(this.gameObject, (int)ekind, myFlag);
            if (enemysT == null)
            {

                return;


            }
            else //������ ������Ʈ�� �ִٸ� �װ��� �̹� ���� ������Ʈ����
            {
                int count = 0;
               

                for (int i = 0; i < enemysT.Length; i++) //�� ���� ������Ʈ ã��
                {

                    for (int j = 0; j < enemyHitList.Count; j++) //�̹� ���� ������Ʈ ã��
                    {
                      

                        if (enemyHitList[j] != enemysT[i]) //�� ���� ������Ʈ�� �̹� ������ �ƴ϶��
                        {
                        

                            GameObject e =  ObjectPool.Instance.get((int)EFlagkind.EObjectFlag,(int)EObjectFlag.electric); //���� ����Ʈ ��������

                            Transform start = enemyHitList[enemyHitList.Count - 1].transform; //���� ������ġ
                            Transform end = enemysT[i].transform; //���� ������ġ

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
                                enemyHitList.Add(enemysT[i]); //end ������Ʈ �������� �߰�

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
