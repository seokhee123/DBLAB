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
    public GameObject boom1;
    public GameObject boom2;
    public GameObject boom3;
    public Transform boomPos1;
    public Transform boomPos2;
    public Transform boomPos3;

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
        //yield return new WaitForSeconds(1f) ;
        //boomPos1.position = new Vector3;
        /*GameObject intantBoom = Instantiate(boom, boomPos1.position, boomPos1.rotation);
        Rigidbody BoomRigid = intantBoom.GetComponent<Rigidbody>();
        BoomRigid.velocity = boomPos1.forward * - 50;*/
        //BoomRigid.transform
        //Destroy(boom, 3f);
        
        //boomPos.position = new Vector3(0, 0, 2);
        GameObject intantBoom1 = Instantiate(boom1, boomPos2.position, boomPos2.rotation);
        Rigidbody Boom1Rigid = intantBoom1.GetComponent<Rigidbody>();
        Boom1Rigid.velocity = boomPos2.right * 50;
        Destroy(intantBoom1, 5f);

        /*GameObject intantBoom3 = Instantiate(boom3, boomPos3.position, boomPos2.rotation);
        Rigidbody Boom3Rigid = intantBoom1.GetComponent<Rigidbody>();
        Boom1Rigid.velocity = boomPos3.right * 50 + boomPos3.forward * 50;
        Destroy(boom1, 3f);*/

        //boomPos.position = new Vector3(0, 0, -2);
        GameObject intantBoom2 = Instantiate(boom2, boomPos3.position, boomPos3.rotation);
        Rigidbody Boom2Rigid = intantBoom2.GetComponent<Rigidbody>();
        Boom2Rigid.velocity = boomPos3.right * -50;
        Destroy(intantBoom2, 5f);

        yield return null;
    }
}
