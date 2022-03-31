using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : Bullet
{
    public Transform target;
    NavMeshAgent nav;
     //Start is called before the first frame update
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        nav.SetDestination(target.position);
    }
}
