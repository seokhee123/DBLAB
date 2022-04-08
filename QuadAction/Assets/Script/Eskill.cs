using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eskill : MonoBehaviour
{
    public GameObject[] BulletRPoint;
    public GameObject[] BulletLPoint;
    public GameObject[] Bullet;
    public bool isShoot;

    public GameObject Minibullet;
    public GameObject[] MiniL;
    public GameObject[] MiniR;
    int i = 0;
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (isShoot)
        {
            int rand1 = Random.Range(0, 5);
            int rand2 = Random.Range(0, 5);
    
            int rand3 = Random.Range(0, 4);
            isShoot = false;

            // ¿À¸¥ÂÊ ÃÑ¾Ë
            GameObject RBulletPrefab = Instantiate(Bullet[rand3], BulletRPoint[rand1].transform.position, BulletRPoint[rand1].transform.rotation);
            Rigidbody RbulletRigid = RBulletPrefab.GetComponent<Rigidbody>();
            RbulletRigid.AddForce(BulletRPoint[rand1].transform.forward * 50, ForceMode.VelocityChange);

            // ¿ÞÂÊ ÃÑ¾Ë
            GameObject LBulletPrefab = Instantiate(Bullet[rand3], BulletLPoint[rand2].transform.position, BulletLPoint[rand2].transform.rotation);
            Rigidbody LbulletRigid = LBulletPrefab.GetComponent<Rigidbody>();
            LbulletRigid.AddForce(BulletLPoint[rand2].transform.forward * 50, ForceMode.VelocityChange);

            //¿ÞÂÊ
            //asdf;
            GameObject MiniLBullet = Instantiate(Minibullet, MiniL[i].transform.position, MiniL[i].transform.rotation);
            Rigidbody MiniLRigid = MiniLBullet.GetComponent<Rigidbody>();
            MiniLRigid.AddForce(MiniL[i].transform.forward * 50, ForceMode.VelocityChange);
            
            GameObject MiniRBullet = Instantiate(Minibullet, MiniR[i].transform.position, MiniR[i].transform.rotation);
            Rigidbody MiniRRigid = MiniRBullet.GetComponent<Rigidbody>();
            MiniRRigid.AddForce(MiniR[i].transform.forward * 50, ForceMode.VelocityChange);


            StartCoroutine(CoolDown());
            
        }
    }
    IEnumerator CoolDown()
    {
        i++;
        i %= 8;
        yield return new WaitForSeconds(0.1f);
        isShoot = true;
    }
}
