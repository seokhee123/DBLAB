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

    public GameObject boom;
    public Transform boomPos;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            StartCoroutine("Boom");
        }
        //StopCoroutine("Fire");

    }

    IEnumerator Boom()
    {
        GameObject intantBoom = Instantiate(boom, boomPos.position, boomPos.rotation);
        Rigidbody BoomRigid = intantBoom.GetComponent<Rigidbody>();
        BoomRigid.velocity = boomPos.forward * -50;
        Destroy(gameObject, 3f);

        yield return null;
    }
}
