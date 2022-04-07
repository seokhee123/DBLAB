using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public GameObject itemShop;
    public GameObject weaponShop;
    public GameObject startZone;
    public GameObject range;

    public Player player;
    public Boss boss;
    public int stage;
    public float playTime;
    public bool isBattle;
    public int enemyCntA;
    public int enemyCntB;
    public int enemyCntC;
    public int enemyCntD;
    public int enemyCntAll;
    public Image[] cooltxt;
    public Text[] skillcooltxt;

    public Transform[] enemyZones;
    public GameObject[] enemies;
    public List<int> enemyList;

    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject overPanel;
    public GameObject scorePanel;
    public Text maxScoreTxt;
    public Text scoreTxt;
    public Text stageTxt;
    public Text playTimeTxt;
    public Text playerHealthTxt;
    public Text playerAmmoTxt;
    public Text playerCoinTxt;
    public Text[] RankTxt;
    public Text[] DateTxt;
    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;
    public Image weaponRImg;
    public Image[] sikillImg;
    public Text enemyATxt;
    public Text enemyBTxt;
    public Text enemyCTxt;
    public RectTransform bossHealthgroup;
    public RectTransform bossHealthbar;
    public Text curScoreText;
    public Text bestText;
    public bool isBoss;
    public int bosscnt;
    public int scorecnt;
    public bool[] isCool;
    public float[] CoolTime;
    public GameObject[] equip;

    Dictionary<string, int> scoredata = new Dictionary<string, int>();

    public void Awake()
    {
        player.ReadFile("score.txt", scoredata);
        enemyList = new List<int>();
    }

    public void GameStart()
    {
        Bullet bullet = range.GetComponent<Bullet>();
        bullet.damage = 10;
        menuCam.SetActive(false);
        gameCam.SetActive(true);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        player.gameObject.SetActive(true);
    }

    public void ScoreBoard()
    {
        var Ranking = scoredata.OrderByDescending(x => x.Value);
        int i = 0;
        foreach (var dic in Ranking)
        {
            RankTxt[i].text = Convert.ToString(dic.Value);
            DateTxt[i].text = dic.Key;
            i++;
        }

        scorePanel.SetActive(true);
    }
    public void ScoreOff()
    {
        scorePanel.SetActive(false);
    }
    public void GameOver()
    {
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
        curScoreText.text = scoreTxt.text;

        var Ranking = scoredata.OrderBy(x => x.Value);
        int max = Ranking.Last().Value;
        if (player.score > max)
        {
            
            bestText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("MaxScore", player.score);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void StageStart()
    {
        startZone.SetActive(false);
        foreach (Transform zone in enemyZones)
        {
            zone.gameObject.SetActive(true);    
        }

        isBattle = true;
        StartCoroutine(InBattle());
    }
    public void StageEnd()
    {
        player.transform.position = Vector3.up * 0.8f;
        startZone.SetActive(true);
        foreach (Transform zone in enemyZones)
        {
            zone.gameObject.SetActive(false);
        }
        isBattle =false;
    }
    IEnumerator InBattle()
    {
        int count=0;
        bosscnt = 0;

        while (player.health > 0)
        {
            if (scorecnt - bosscnt > 0 && isBoss == false)
            {
                isBoss = true;
                bosscnt++;
                enemyCntD++;
                GameObject instantEnemy = Instantiate(enemies[3], enemyZones[0].position, enemyZones[0].rotation);
                enemy enemy = instantEnemy.GetComponent<enemy>();
                enemy.target = player.transform;
                enemy.manager = this;
                enemy.tag = "Enemy";
                boss = instantEnemy.GetComponent<Boss>();
            }

            if (playTime >=0 && playTime < 180)
            {
                count = 10;
            }
            else if (playTime >= 180 && playTime < 360)
            {
                count = 15;
            }
            else if (playTime >= 360 && playTime < 540)
            {
                count = 20;
            }
            else if (playTime >= 540 && playTime < 720)
            {
                count = 25;
            }
            else if (playTime >= 720)
            {
                count = 40;
            }
            for (int index = 0; index < count; index++)
            {
                int ran = UnityEngine.Random.Range(0, 3);
                enemyList.Add(ran);
                switch (ran)
                {
                    case 0:
                        enemyCntA++;
                        break;
                    case 1:
                        enemyCntB++;
                        break;
                    case 2:
                        enemyCntC++;
                        break;
                }
            }
            while (enemyList.Count > 0)
            {
                int ranZone = UnityEngine.Random.Range(0, 4);
                GameObject instantEnemy = Instantiate(enemies[enemyList[0]], enemyZones[ranZone].position, enemyZones[ranZone].rotation);
                enemy enemy = instantEnemy.GetComponent<enemy>();
                enemy.target = player.transform;
                enemy.manager = this;
                enemy.tag = "Enemy";
                enemyList.RemoveAt(0);
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(30f);
        }
    }

    void Update()
    {
        if (isBattle)
            playTime += Time.deltaTime;
        scorecnt = (int)(player.score / 3000);

        enemyCntAll = enemyCntA + enemyCntA + enemyCntA + enemyCntA;
    }
    
    public void CoolDown() {
        if (isCool[0]) {
            if (CoolTime[0] <= 0)
            {
                skillcooltxt[0].gameObject.SetActive(false);
                sikillImg[0].color = Color.white;
                CoolTime[0] = 5;
                isCool[0] = false;
            }
            else
            {
                skillcooltxt[0].text = Convert.ToString((int)CoolTime[0]);
                CoolTime[0] -= Time.deltaTime;
            }
        }
        if (isCool[1] == true) {
            bool dob = true;
            if (CoolTime[1] <= 0)
            {
                skillcooltxt[1].gameObject.SetActive(false);
                sikillImg[1].color = Color.white;
                CoolTime[1] = 30;
                isCool[1] = false;
            }
            else
            {
                CoolTime[1] -= Time.deltaTime;
                if (dob && CoolTime[1] <= 25)
                {
                    dob = false;
                    skillcooltxt[1].gameObject.SetActive(true);
                    player.EskillGroup.SetActive(false);
                }
                skillcooltxt[1].text = Convert.ToString((int)CoolTime[1]);
            }
        }
        if (isCool[2] == true) {
            if (CoolTime[2] <= 0)
            {
                skillcooltxt[2].gameObject.SetActive(false);
                sikillImg[2].color = Color.white;
                CoolTime[2] = 5;
                isCool[2] = false;
            }
            else
            {
                skillcooltxt[2].text = Convert.ToString((int)CoolTime[2]);
                CoolTime[2] -= Time.deltaTime;
            }
        }
        if (isCool[3] == true) {
            if (CoolTime[3] <= 0)
            {
                skillcooltxt[3].gameObject.SetActive(false);
                sikillImg[3].color = Color.white;
                CoolTime[3] = 5;
                isCool[3] = false;
                player.hasGrenades = 4;
                player.grenades[0].SetActive(true);
                player.grenades[1].SetActive(true);
                player.grenades[2].SetActive(true);
                player.grenades[3].SetActive(true);
            }
            else
            {
                skillcooltxt[3].text = Convert.ToString((int)CoolTime[3]);
                CoolTime[3] -= Time.deltaTime;
            }
        }
    }

    private void LateUpdate()
    {
        CoolDown();
        if (player.isLevel == true||player.isSkill == true) Time.timeScale = 0;
        else if (player.isLevel == false && player.isSkill == false) Time.timeScale = 1;
        scoreTxt.text = string.Format("{0:n0}", player.score);
        stageTxt.text = "STAGE " + stage;

        int hour = (int)(playTime / 3600);
        int min = (int)((playTime-hour*3600) / 60);
        int sec = (int)(playTime % 60);

        playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);

        playerHealthTxt.text = player.health + " / " + player.maxHealth;
        playerCoinTxt.text = (int)player.exp + " / " + (int)player.maxExp;
        //string.Format("{0:n0}", player.coin);
        if (player.equipWeapon == null)
        {
            playerAmmoTxt.text = "- / - ";
        }
        else if (player.equipWeapon.type == Weapon.Type.Melee)
        {
            playerAmmoTxt.text = "- / ∞";
        }
        else
        {
            playerAmmoTxt.text = player.equipWeapon.curAmmo + " / " + player.ammo;
        }
        weapon1Img.color = new Color(1, 1, 1, player.hasWeapons[0] ? 1 : 0);
        weapon2Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
        weapon3Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
        weaponRImg.color = new Color(1, 1, 1, player.hasGrenades>0 ? 1 : 0);
        
        enemyATxt.text = enemyCntA.ToString();
        enemyBTxt.text = enemyCntB.ToString();
        enemyCTxt.text = enemyCntC.ToString();

        // if (player.skills[0]) sikillImg[0].gameObject.SetActive(true);
        // if (player.skills[1]) sikillImg[1].gameObject.SetActive(true);
        // if (player.skills[2]) sikillImg[2].gameObject.SetActive(true);
        // if (player.skills[3]) sikillImg[3].gameObject.SetActive(true);

        if (boss != null)
        {
            bossHealthgroup.anchoredPosition = Vector3.down * 30;
            if (boss.curHealth >= 0)
                bossHealthbar.localScale = new Vector3(((float)boss.curHealth / boss.maxHealth), 1, 1);
        }
        else
        {
            bossHealthgroup.anchoredPosition = Vector3.down * 2000;
        }
    }
}
