using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qskill : MonoBehaviour
{
    public List<GameObject> FoundObjects;
    public string TagName;
    public float shortDis;
    public GameObject enemy;
    bool followEnemy = false;
    Transform shortEnemy;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));
        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position); // ù��°�� �������� ����ֱ� 

        enemy = FoundObjects[1]; // ù��°�� Ÿ���� �� �� �ι�° ����� ������ �̵� 

        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis) // ������ ���� �������� �Ÿ� ���
                {
                    enemy = found;
                    shortDis = Distance;

                    //transform.position = enemy;
                }
            }
            followEnemy = true;
            shortEnemy = enemy.gameObject.transform;
            //float enemyfind = Vector3.Distance(first_bullet,enemy);
            //SpotEnemy = enemy.gameObject.transform.position;   */
    }
    void OnTriggerEnter(Collider other)
    {
        //findEnemy = findObject.GetComponent<SphereCollider>();
        // find enemy �ݶ��̴� üũ
        /*if(player.PlayerSkill[0] != 0 &&  manager.enemyCntA+manager.enemyCntB+manager.enemyCntC+manager.enemyCntD >=2)
        {
            //int myindex =   
        }
        if (!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }*/
        //findObject = findEnemy.GetComponent<SphereCollider>();
        /*if (player.playerSkill[0] == true && manager.enemyCntAll >= 2 && other.gameObject.tag == "Enemy")
        {
            Debug.Log("�� Ȯ��");
        }*/
    }
}
