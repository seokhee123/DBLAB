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
        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position); // 첫번째를 기준으로 잡아주기 

        enemy = FoundObjects[1]; // 첫번째는 타격자 그 후 두번째 가까운 적에게 이동 

        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
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
        // find enemy 콜라이더 체크
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
            Debug.Log("적 확인");
        }*/
    }
}
