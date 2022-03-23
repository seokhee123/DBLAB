using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public enum Type { A, B, C, D };
    public Type enemytype;
    public int maxHealth;
    public int curHealth;
    public int score;
    public int enemyexp;
    public GameManager manager;
    public GameObject dieG;
    public Transform target;
    public BoxCollider meleeArea;
    public GameObject bullet;
    public GameObject[] coins;
    public bool isChase;
    public bool isAttack;
    public bool isDead;
    public bool isDamage;

    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public MeshRenderer[] mat;
    public NavMeshAgent nav;
    public Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        if (enemytype != Type.D)
        {
            Invoke("ChaseStart", 2);
        }
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }
    private void Update()
    {
        if (nav.enabled && enemytype != Type.D) { 
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
      
    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }

    }
    private void Targeting()
    {
        if (!isDead && enemytype != Type.D)
        {
            float targetRadius = 0f;
            float targetRange = 0;

            switch (enemytype)
            {
                case Type.A:
                    targetRadius = 1.5f;
                    targetRange = 3f;
                    break;
                case Type.B:
                    targetRadius = 1f;
                    targetRange = 12f;
                    break;
                case Type.C:
                    targetRadius = 0.5f;
                    targetRange = 25f;
                    break;
            }
            RaycastHit[] rayHits =
                Physics.SphereCastAll(transform.position,
                                      targetRadius, transform.forward,
                                      targetRange, LayerMask.GetMask("Player"));
            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
      
    }
    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true; 
        anim.SetBool("isAttack", true);

        switch (enemytype)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);
                break;
            case Type.C:
                yield return new WaitForSeconds(0.5f);
                GameObject instatntBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instatntBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 20;

                yield return new WaitForSeconds(2f);
                break;
        }
       
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }
    private void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();    
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isDamage)
        {
            if (other.tag == "Melee")
            {
                isDamage = true;
                Weapon weapon = other.GetComponent<Weapon>();
                curHealth -= weapon.damage;
                Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamage(reactVec, false));
            }

            else if (other.tag == "Bullet")
            {
                isDamage = true;
                Bullet bullet = other.GetComponent<Bullet>();
                Vector3 reactVec = transform.position - other.transform.position;
                curHealth -= bullet.damage;
                Destroy(other.gameObject);
                StartCoroutine(OnDamage(reactVec, false));
            }
        }
    }
    public void HitByGrenade(Vector3 explosionPos)
    {
        curHealth -= 80;
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec, true));
    }
    IEnumerator OnDamage(Vector3 reacVec, bool isGrenade)
    {   
        foreach (MeshRenderer mesh in mat) 
            mesh.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            foreach (MeshRenderer mesh in mat)
                mesh.material.color = Color.white;
            isDamage = false;
        }
        else if (curHealth <= 0)
        {
            this.gameObject.layer = 14;
            if (enemytype == Type.D)
            {
                manager.isBoss = false;
            }
            Player player = target.GetComponent<Player>();
            if (player.equipWeapon.name.Equals("Weapon SubMachineGun") && player.skills[2]&& enemytype != Type.D)
            {
                dieG.SetActive(true);
            }
            foreach (MeshRenderer mesh in mat)
                mesh.material.color= Color.gray;
            isDead = true;
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");
            player.score += score;
            player.exp += enemyexp;
            int ranCoin = Random.Range(0, 3);
            Instantiate(coins[ranCoin], transform.position, Quaternion.identity);


            switch(enemytype)
            {
                case Type.A:
                    manager.enemyCntA--;
                    break;
                case Type.B:
                    manager.enemyCntB--;
                    break;
                case Type.C:
                    manager.enemyCntC--;
                    break;
                case Type.D:
                    manager.enemyCntD--;
                    break;
            }
            if (isGrenade)
            {
                reacVec = reacVec.normalized;
                reacVec += Vector3.up*3;
                rigid.freezeRotation = false;
                rigid.AddForce(reacVec * 5, ForceMode.Impulse);
                rigid.AddTorque(reacVec * 15, ForceMode.Impulse);
            }
            else
            {
                reacVec = reacVec.normalized;
                reacVec += Vector3.up;
                rigid.AddForce(reacVec * 5, ForceMode.Impulse);
            }
                Destroy(gameObject, 3f);
        }   
    }
}
