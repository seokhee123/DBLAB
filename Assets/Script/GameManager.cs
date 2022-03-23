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
    public Player player;
    public Boss boss;
    public int stage;
    public float playTime;
    public bool isBattle;
    public int enemyCntA;
    public int enemyCntB;
    public int enemyCntC;
    public int enemyCntD;

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

    Dictionary<string, int> scoredata = new Dictionary<string, int>();

    private void Awake()

    {
        player.ReadFile("score.txt", scoredata);
        enemyList = new List<int>();
        /*maxScoreTxt.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore"));
        if (PlayerPrefs.HasKey("MaxScore"))
            PlayerPrefs.SetInt("MaxScore", 0);
        */
    }

    public void GameStart()
    {
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

        int maxScore = PlayerPrefs.GetInt("MaxScore");
        if (player.score > maxScore)
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
        /*
        if (stage % 5 == 0)
        {
            enemyCntD++;
            GameObject instantEnemy = Instantiate(enemies[3], enemyZones[0].position, enemyZones[0].rotation);
            enemy enemy = instantEnemy.GetComponent<enemy>();
            enemy.target = player.transform;
            enemy.manager = this;
            enemy.tag = "Enemy";
            boss = instantEnemy.GetComponent<Boss>();
        }
        else
        {
            for (int index = 0; index < stage; index++)
            {
                int ran = Random.Range(0, 3);
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
                int ranZone = Random.Range(0, 4);
                GameObject instantEnemy = Instantiate(enemies[enemyList[0]], enemyZones[ranZone].position, enemyZones[ranZone].rotation);
                enemy enemy = instantEnemy.GetComponent<enemy>();
                enemy.target = player.transform;
                enemy.manager = this;
                enemy.tag = "enemy";
                enemyList.RemoveAt(0);
                yield return new WaitForSeconds(4f);
            }
        }
        
        while (enemyCntA + enemyCntB + enemyCntC + enemyCntD >0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        boss = null;
        StageEnd();
        */
    }
    private void Update()
    {
        if (isBattle)
            playTime += Time.deltaTime;
        scorecnt = (int)(player.score / 3000);
    }
    private void LateUpdate()
    {
        scoreTxt.text = string.Format("{0:n0}", player.score);
        stageTxt.text = "STAGE " + stage;

        int hour = (int)(playTime / 3600);
        int min = (int)((playTime-hour*3600) / 60);
        int sec = (int)(playTime % 60);

        playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);

        playerHealthTxt.text = player.health + " / " + player.maxHealth;
        playerCoinTxt.text = string.Format("{0:n0}", player.coin);
        if (player.equipWeapon == null)
        {
            playerAmmoTxt.text = "- / - ";
        }
        else if (player.equipWeapon.type == Weapon.Type.Melee)
        {
            playerAmmoTxt.text = "- / ¡Ä";
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

        if (player.skills[0]) sikillImg[0].gameObject.SetActive(true);
        if (player.skills[1]) sikillImg[1].gameObject.SetActive(true);
        if (player.skills[2]) sikillImg[2].gameObject.SetActive(true);

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
