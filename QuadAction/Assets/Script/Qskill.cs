using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Qskill : MonoBehaviour
{
    /*public List<GameObject> FoundObjects;
    public string TagName;
    public float shortDis;
    public GameObject enemy;
    bool followEnemy = false;
    Transform shortEnemy;
    public NavMeshAgent nav;


    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(followEnemy == true)
        {
            nav.SetDestination(shortEnemy.position);
            followEnemy = false;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
    }
    void OnTriggerEnter(Collider other)
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
        //SpotEnemy = enemy.gameObject.transform.position;   
    }*/

    public GameObject boom1;
    public GameObject boom2;
    public GameObject boom3;
    public GameObject boom4;
    public Transform boomPos;
    public Transform boomPos1;
    public Transform boomPos2;
    public Transform boomPos3;
    public Transform boomPos4;

    //Player player;
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            
            StartCoroutine("Boom");
            //Destroy(player.qskill,3f);
        }
        //StopCoroutine("Fire");

    }

    IEnumerator Boom()
    {
        GameObject intantBoom1 = Instantiate(boom1, boomPos.position, boomPos.rotation);
        Rigidbody Boom1Rigid = intantBoom1.GetComponent<Rigidbody>();
        Boom1Rigid.velocity = boomPos.right * 50;
        Destroy(intantBoom1, 100f);

        GameObject intantBoom2 = Instantiate(boom2, boomPos.position, boomPos.rotation);
        Rigidbody Boom2Rigid = intantBoom2.GetComponent<Rigidbody>();
        Boom2Rigid.velocity = boomPos.right * -50;
        Destroy(intantBoom2, 100f);

        GameObject intantBoom3 = Instantiate(boom3, boomPos.position, boomPos.rotation);
        Rigidbody Boom3Rigid = intantBoom3.GetComponent<Rigidbody>();
        Boom3Rigid.velocity = boomPos.forward * 50;
        Destroy(intantBoom3, 100f);

        GameObject intantBoom4 = Instantiate(boom4, boomPos.position, boomPos.rotation);
        Rigidbody Boom4Rigid = intantBoom4.GetComponent<Rigidbody>();
        Boom4Rigid.velocity = boomPos.forward * -50;
        Destroy(intantBoom4, 100f);

        GameObject intantBoom5 = Instantiate(boom1, boomPos.position, boomPos.rotation);
        Rigidbody Boom5Rigid = intantBoom5.GetComponent<Rigidbody>();
        Boom5Rigid.velocity = boomPos.forward * 50 + boomPos.right * -50;

        GameObject intantBoom6 = Instantiate(boom1, boomPos.position, boomPos.rotation);
        Rigidbody Boom6Rigid = intantBoom6.GetComponent<Rigidbody>();
        Boom6Rigid.velocity = boomPos.forward * 50 + boomPos.right * 50;

        GameObject intantBoom7 = Instantiate(boom1, boomPos.position, boomPos.rotation);
        Rigidbody Boom7Rigid = intantBoom7.GetComponent<Rigidbody>();
        Boom7Rigid.velocity = boomPos.forward * -50 + boomPos.right * -50;

        GameObject intantBoom8 = Instantiate(boom1, boomPos.position, boomPos.rotation);
        Rigidbody Boom8Rigid = intantBoom8.GetComponent<Rigidbody>();
        Boom8Rigid.velocity = boomPos.forward * -50 + boomPos.right * 50;

        yield return null;
    }
}
