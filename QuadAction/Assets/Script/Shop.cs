using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform UIGroup;
    public Animator anim;
    public Button[] btn;
    public Player player;
    public GameObject melee;
    public GameObject range;

    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;
    public string[] TalkData;
    public Text[] leveltxt;
    public GameManager manager;

    public bool isSkill;
    Player enterPlayer;

    public void Awake()
    {
        if (isSkill)
        {
            btn[0].interactable = false;
            btn[1].interactable = false;
            btn[2].interactable = false;
        }
    }
    private void LateUpdate()
    {
        if (isSkill == false)
        {
            leveltxt[0].text = player.healthLevel + " / 20";
            leveltxt[1].text = player.rangeLevel + " / 20";
            leveltxt[2].text = player.meleeLevel + " / 20";
        }
        else if (isSkill == true)
        {
            if (!player.skills[0])
                leveltxt[0].text = player.kill[0] + " / 10";
            else
                leveltxt[0].text = "획득 완료!";

            if (!player.skills[1])
                leveltxt[1].text = player.kill[1] + " / 10";
            else
                leveltxt[1].text = "획득 완료!";
            if (!player.skills[2])
                leveltxt[2].text = player.kill[2] + " / 10";
            else
                leveltxt[2].text = "획득 완료!";
            if (player.kill[0] == 10 && !player.skills[0]) btn[0].interactable = true;
            if (player.kill[1] == 10 && !player.skills[1]) btn[1].interactable = true;
            if (player.kill[2] == 10 && !player.skills[2]) btn[2].interactable = true;
        }
    }
    public void Enter(Player player)
    {
        //talkText.text = TalkData[0];
        enterPlayer = player;
        UIGroup.anchoredPosition = Vector3.zero;
    }
    public void Exit()
    {
        anim.SetTrigger("doHello");
        UIGroup.anchoredPosition = Vector3.down * 1000;
    }

    public void SkillUp(int n)
    {
        if (n == 0)
        {
            player.skills[0] = true;
            player.skillpt--;
            UIGroup.gameObject.SetActive(false);
            btn[0].interactable = false;
            if(player.skillpt == 0) player.isSkill = false;

        }
        else if (n == 1)
        {
            btn[1].interactable = false;
            player.skills[1] = true;
            player.skillpt--;
            UIGroup.gameObject.SetActive(false);
            btn[1].interactable = false;
            if(player.skillpt == 0) player.isSkill = false;
        }
        else if (n == 2) 
        {
            btn[2].interactable = false;
            player.skills[2] = true;
            player.skillpt--;
            UIGroup.gameObject.SetActive(false);
            btn[2].interactable = false;
            if(player.skillpt == 0) player.isSkill = false;
        }
        else if (n==3)
        {

        }
    }
    public void StatUp(int n)
    {
        if (n == 0)
        {
            player.maxHealth += 10;
            player.healthLevel++;
            if (player.healthLevel == 20) btn[0].interactable = false;
            UIGroup.gameObject.SetActive(false);
            player.isLevel = false;
        }
        else if (n == 1)
        {
            Bullet bullet = range.GetComponent<Bullet>();
            bullet.damage += 10;
            player.rangeLevel++;
            if (player.rangeLevel == 20) btn[1].interactable = false;
            UIGroup.gameObject.SetActive(false);
            player.isLevel = false;
        }
        else if (n == 2)
        {
            Weapon weapon = melee.GetComponent<Weapon>();
            weapon.damage += 10;
            player.meleeLevel++;
            if (player.meleeLevel == 20) btn[2].interactable = false;
            UIGroup.gameObject.SetActive(false);
            player.isLevel = false;
        }
        player.health = player.maxHealth;
    }
    public void Buy(int index)
    {
        int price = itemPrice[index];
        if (price > enterPlayer.coin)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk()); 
            return;
        }
        enterPlayer.coin -= price;
        if (isSkill)
        {
            btn[index].gameObject.SetActive(false);
            if (index <= 2)
            {
                btn[index + 3].gameObject.SetActive(true);
                Vector3 ranVec = Vector3.right * Random.Range(-3, 3)
                                + Vector3.forward * Random.Range(-3, 3);
                Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);
            }
        }
        else
        {
            Vector3 ranVec = Vector3.right * Random.Range(-3, 3)
                               + Vector3.forward * Random.Range(-3, 3);
            Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);
        }
        
    }
    public void ActiveSkills(int index)
    {
        player.skills[index] = true;
    }
    IEnumerator Talk()
    {
        //talkText.text = TalkData[1];
        yield return new WaitForSeconds(2f);
    }
}
