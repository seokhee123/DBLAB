using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Qskill : MonoBehaviour
{
    public GameObject[] grenade;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if(other.tag == "Enemy")
        {
            StartCoroutine(Boom());
        }
    }

    IEnumerator Boom()
    {
        for (int i = 0; i < 4; i++)
        {
            grenade[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
