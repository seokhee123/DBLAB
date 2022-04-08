using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Qskill : Bullet
{
    public GameObject[] expolde;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(Explode());
        }
        else if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Explode()
    {
        for (int i = 0; i < 4; i++)
        {
            expolde[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
