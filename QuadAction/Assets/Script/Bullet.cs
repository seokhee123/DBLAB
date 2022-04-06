using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnCollisionEnter(Collision collision)
    {
        if (!isRock && collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //findEnemy = findObject.GetComponent<SphereCollider>();
        // find enemy 콜라이더 체크

        if (!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject, 0);
        }
        //findObject = findEnemy.GetComponent<SphereCollider>();
        /*if (player.playerSkill[0] == true && manager.enemyCntAll >= 2 && other.gameObject.tag == "Enemy")
        {
            Debug.Log("적 확인");
        }*/
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("3쿠션");
            
        }
    }
}
