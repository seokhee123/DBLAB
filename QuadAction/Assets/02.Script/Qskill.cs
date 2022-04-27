using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Qskill : Bullet
{
    public GameObject[] expolde;
    public GameObject gre;

    void OnTriggerEnter(Collider other)
    {
        if (isRocket && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Wall"))
        {
            StartCoroutine(Explode());
        }
        /*else if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        */
    }

    IEnumerator Explode()
    {

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            Instantiate(gre, expolde[i].transform.position, transform.rotation);
            //expolde[i].SetActive(true);
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
