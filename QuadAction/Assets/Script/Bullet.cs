using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    public double damage;
    public bool isMelee;
    public bool isRock;
    Player player;
    Weapon weapon;
    GameManager manager;
    public SphereCollider findEnemy;
    SphereCollider findObject;
    RaycastHit hit;
    BulletCheck bulletcheck;
    public List<GameObject> FoundObjects;
    public GameObject enemy;
    public string TagName;
    public float shortDis;
    Vector3 SpotEnemy;
    bool followEnemy=false;
    Transform shortEnemy;
    //Vector3 distanceEnemy;
    //public float findEnemy = Vector3.

    //public Transform target;
    public NavMeshAgent navi;

    void Awake()
    {
        //nav = GetComponent<NavMeshAgent>();
        navi = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(followEnemy == true)
        {
            navi.SetDestination(shortEnemy.position);
            followEnemy = false;
            Debug.Log("follow");
            
        }
            
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isRock && collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
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
            //SpotEnemy = enemy.gameObject.transform.position;
            int i = 0;
        }
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
        if(other.gameObject.tag == "Enemy")
        {
            //Vector3.Distance()
           
        }
    }
}
