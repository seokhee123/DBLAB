using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eskill : MonoBehaviour
{
    public GameObject[] BulletRPoint;
    public GameObject[] BulletLPoint;
    public GameObject[] Bullet;
    public bool isShoot;
    
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

            // ������ �Ѿ�
            GameObject RBulletPrefab = Instantiate(Bullet[rand3], BulletRPoint[rand1].transform.position, BulletRPoint[rand1].transform.rotation);
            Rigidbody RbulletRigid = RBulletPrefab.GetComponent<Rigidbody>();
            RbulletRigid.AddForce(BulletRPoint[rand1].transform.forward * 50, ForceMode.VelocityChange);

            // ���� �Ѿ�
            GameObject LBulletPrefab = Instantiate(Bullet[rand3], BulletLPoint[rand2].transform.position, BulletLPoint[rand2].transform.rotation);
            Rigidbody LbulletRigid = LBulletPrefab.GetComponent<Rigidbody>();
            LbulletRigid.AddForce(BulletLPoint[rand2].transform.forward * 50, ForceMode.VelocityChange);

            StartCoroutine(CoolDown());
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        isShoot = true;
    }
}
