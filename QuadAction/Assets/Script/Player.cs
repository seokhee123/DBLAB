using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public GameObject[] grenades;
    public int hasGrenades = 0;
    public GameObject grenadeObj;
    public Camera followCamera;
    public GameManager manager;
    public GameObject StatShop;
    public GameObject stat;
    public int ammo;
    public int coin;
    public double health;
    public int score;
    //public GameObject Qskill;
    Qskill qs;

    public int maxAmmo;
    public int maxCoin;
    public int maxHealth;
    public int maxHasGrenades;
    public double exp;
    public double maxExp;
    public int healthLevel, meleeLevel, rangeLevel;
    float hAxis;
    float vAxis;

    bool wDown;
    bool jDown;
    bool fDown;
    bool gDown;
    bool rDown;
    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool qDown;

    bool isJump;
    bool isDodge;
    bool isSwap;
    bool isReload;
    bool isFireReady= true;
    bool isBorder;
    bool isDamage;
    bool isShop;
    public bool isDead;
    public bool[] skills;
    public bool toggle;

    Vector3 moveVec;
    Vector3 dodgeVec;

    Rigidbody rigid;
    Animator anim;
    MeshRenderer[] meshs;

    GameObject nearObject;
    public Weapon equipWeapon;
    public int equipWeaponIndex = -1;
    float fireDelay;
    Dictionary<string, int> scoredata = new Dictionary<string, int>();

    public bool[] playerSkill;
    //private static Player instance;

    public Transform qskillPos;
    public GameObject qskill;

    void Awake()
    {
        ReadFile("score.txt", scoredata);
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        
        PlayerPrefs.SetInt("MaxScore", 0);
    }

    void Update()
    {
        if(!isDead)
        {
            GetInput();
            Move();
            Turn();
            Jump();
            Grenade();
            Attack();
            Reload();
            Dodge();
            Swap();
            Skills();
            Interation();
            LevelUp();
            //Power();
        }

    }

    void LevelUp()
    { 
        if (meleeLevel == 20 && rangeLevel == 20 && healthLevel == 20) return;
        if (exp >= maxExp)
        {
            Time.timeScale = 0;
            exp = exp - maxExp;
            maxExp *= 1.3;
            StatShop.SetActive(true);
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButton("Fire1");
        gDown = Input.GetButtonDown("Fire2");
        rDown = Input.GetButtonDown("Reload");
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
        qDown = Input.GetButtonDown("Qskill");
    }
    
    void Skills()
    {
        if (qDown)
        {
            StartCoroutine("Fire");
            
        }
        StopCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        GameObject intantQskill = Instantiate(qskill, qskillPos.position, qskillPos.rotation);
        Rigidbody qskill1Rigid = intantQskill.GetComponent<Rigidbody>();
        qskill1Rigid.velocity = qskillPos.forward * 50;
        
        yield return null;
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if(isDodge)
            moveVec = dodgeVec;
        if (isSwap || !isFireReady || isReload)
            moveVec = Vector3.zero;
        if (!isBorder)
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }
    void Turn()
    {
        // 키보드 회전
        transform.LookAt(transform.position + moveVec);

        // 마우스 회전(클릭 시)
        if (fDown || isDead)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0f;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }
    void Jump()
    {
        if(jDown && moveVec == Vector3.zero && !isJump && !isDodge && !isSwap || isDead)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }
    void Grenade()
    {
        if (hasGrenades == 0)
        {
            return;
        }

        if (gDown && !isReload && !isSwap || isDead)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 10;

                GameObject instantGrenade = Instantiate(grenadeObj, transform.position, transform.rotation);
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                rigidGrenade.AddForce(nextVec, ForceMode.Impulse);
                rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);
                hasGrenades--;
                grenades[hasGrenades].SetActive(false);
            }
        }
    }
    void Attack()
    {
        if (equipWeapon == null)
            return;
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(fDown && isFireReady && !isDodge && !isSwap && !isShop || isDead && !isJump)
        {
            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            fireDelay = 0;
        }
    }
    void Reload()
    {
        if (equipWeapon == null || equipWeapon.type == Weapon.Type.Melee || ammo <= 0 || equipWeapon.curAmmo == equipWeapon.maxAmmo || isShop )
            return ;

        if (rDown && !isJump && !isDodge && !isSwap && isFireReady || isDead)
        {
            isReload = true;
            anim.SetTrigger("doReload");
            Invoke("ReloadOut", 1.4f);
        }
    }
    void ReloadOut()
    {
        int reAmmo = equipWeapon.maxAmmo - equipWeapon.curAmmo;
        ammo -= reAmmo;
        equipWeapon.curAmmo = equipWeapon.maxAmmo;
        isReload = false;
    }
    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero &&!isJump && !isDodge && !isSwap && !isShop || isDead)
        {
            
            if (skills[0] == true && equipWeaponIndex == 0)
            {
                foreach (MeshRenderer mesh in meshs)
                {
                    mesh.material.color = Color.blue;
                }
                isDamage = true;
            }
            
            dodgeVec = moveVec;
            speed *= 2; 
            anim.SetTrigger("doDodge");
            isDodge = true;
            Invoke("DodgeOut", 0.4f);

        }
    }
    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
        if (skills[0] == true && equipWeaponIndex == 0)
        {
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }
            isDamage = false;
        }
    }
    void Swap()
    {
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;

        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;

        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;        
        if (sDown3) weaponIndex = 2;

        if((sDown1 || sDown2 || sDown3) && !isJump && !isDodge && !isShop)
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
            anim.SetTrigger("doSwap");
            for(int i = 0; i < weapons.Length; i++)
            {
                if (i == equipWeaponIndex)
                {
                    manager.sikillImg[i].color = Color.cyan;
                }
                else
                {
                    manager.sikillImg[i].color = Color.white;
                }
            }
            isSwap = true;
            Invoke("SwapOut", 0.4f);
        }
    }
    void SwapOut()
    {
        isSwap = false;
    }
    void Interation()
    {
        if(iDown && nearObject != null && !isJump && !isDodge)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;
                Destroy(nearObject);
            }
            else if (nearObject.tag == "Shop")
            {
                Shop shop = nearObject.GetComponent<Shop>();
                shop.Enter(this);
                isShop = true;
            }
        }
    }
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }
    private void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
        if (collision.gameObject.tag == "Die")
        {
            health =0;
            StartCoroutine(OnDamage(false));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;

                case Item.Type.Coin:
                    coin += item.value;
                    if (coin > maxCoin)
                    coin = maxCoin;
                    break;

                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;

                case Item.Type.Grenade:
                    if (hasGrenades > maxHasGrenades)
                        return;
                    grenades[hasGrenades].SetActive(true);
                    hasGrenades += item.value;
                    break;
            }
            Destroy(other.gameObject);
        }
        else if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;
                bool isBossAtk = other.name == "Boss Melee Area";
                StartCoroutine(OnDamage(isBossAtk));
            }
            if (other.GetComponent<Rigidbody>() != null)
                Destroy(other.gameObject);
        }
    }
    IEnumerator OnDamage(bool isBossAtk)
    {
        isDamage = true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        }
        if (isBossAtk)
            rigid.AddForce(transform.forward * -25, ForceMode.Impulse);

        if (health <= 0 && !isDead)
        {
            OnDie();
        }

        yield return new WaitForSeconds(1f);

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }

        if(isBossAtk) 
            rigid.velocity = Vector3.zero;
      
    }
    public void WriteFile(string path, Dictionary<string, int> scoreTable)
    {

        StreamWriter sw = new StreamWriter(path);
        foreach (KeyValuePair<string, int> dic in scoreTable)
        {
            sw.WriteLine(dic.Key + "," + dic.Value);
        }
        sw.Flush();
        sw.Close();
    }

    public void ReadFile(string path, Dictionary<string, int> scoreTable)
    {
        StreamReader sr = new StreamReader(path);
        string[] scored = new string[10];
        int i = 0;

        while (!sr.EndOfStream)
        {
            scored[i++] = sr.ReadLine();
        }
        sr.Close();
        for (int a = 0; a < i; a++)
        {
            string[] split = scored[a].Split(new string[] { "," }, System.StringSplitOptions.None);
            scoreTable.Add(split[0], Int32.Parse(split[1]));
        }
    }

    public void InputScore(Dictionary<string, int> scoreTable)
    {
        bool chk = false;
        String now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        List<int> list = scoredata.Values.ToList();
        if (list.Count < 5)
        {
            scoredata.Add(now, score);
        }
        else
        {
            list.Sort();
            list.Reverse();
            foreach (int i in list)
            {
                if (score > i)
                {
                    foreach (KeyValuePair<string, int> dic in scoreTable)
                    {
                        if (dic.Value == list[4])
                        {
                            scoredata.Remove(dic.Key);
                            scoredata.Add(now, score);
                            chk = true;
                            break;
                        }
                    }
                }
                if (chk) break;
            }
        }

    }

    void OnDie()
    {
        InputScore(scoredata);
        //ReadFile("score.txt", scoredata);
        WriteFile("score.txt", scoredata);
        anim.SetTrigger("doDie");
        isDead = true;
        this.gameObject.layer = 16;
        manager.GameOver();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon" || other.tag == "Shop")
            nearObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
        else if (other.tag == "Shop")
        {
            Shop shop = other.GetComponent<Shop>();
            shop.Exit();
            isShop = false;
            nearObject = null;
        }
    }
}
