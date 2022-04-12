using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject meshObj;
    public GameObject effectObj;
    public Rigidbody rigid;
    public float sec;
    public bool isExplode;
    private void Start()
    {
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(sec);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        
        meshObj.SetActive(false);
        effectObj.SetActive(true);

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 8, Vector3.up, 0f, LayerMask.GetMask("Enemy"));
        foreach (RaycastHit hitObj in rayHits) 
        {
            hitObj.transform.GetComponent<enemy>().HitByGrenade(transform.position);
        }
        Destroy(gameObject, 5);
    }
}
