using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public double damage;
    public bool isMelee;
    public bool isRock;
    public bool isRocket;

    void Start()
    {
        if (!isMelee || isRocket)
            Destroy(gameObject, 5);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isRocket && !isRock && other.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3);
        }
        else if (!isRocket && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

}
