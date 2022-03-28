using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range};
    public Type type;
    public double damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;
    
    public GameObject chargeShot;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;
    public Player player;

    GameObject b;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing"); // 코루틴 시작
        }
        else if (type == Type.Range)
        {
            if (curAmmo > 0)
            {
                if (player.equipWeaponIndex == 1 && player.toggle && curAmmo >= 10)
                {
                    curAmmo -= 10;
                    StartCoroutine("Shot");
                    return;
                }
                else if (player.equipWeaponIndex == 1 && player.toggle && curAmmo < 10)
                {
                    return;
                }
                curAmmo--;
                StartCoroutine("Shot");
            }
        }
    }

    IEnumerator Swing() // 코루틴
    {
        yield return new WaitForSeconds(0.1f); // 1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        b = bullet;
        if (player.equipWeapon.name.Equals("Weapon HandGun") && player.toggle == true)
        {
            b = chargeShot;
        }
        GameObject intantBullet = Instantiate(b, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;
        yield return null;

        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
        yield return null;

    }
}
