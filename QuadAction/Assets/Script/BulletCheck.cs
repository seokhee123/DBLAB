using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCheck : MonoBehaviour
{
    public bool bulletCheck = false;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            bulletCheck = true;
        }
        else
        {
            bulletCheck = false;
        }
    }
}
