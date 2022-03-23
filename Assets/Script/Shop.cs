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

    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;
    public string[] TalkData;
    public Text talkText;
    public GameManager manager;

    public bool isSkill;
    Player enterPlayer;
    
    public void Enter(Player player)
    {
        talkText.text = TalkData[0];
        enterPlayer = player;
        UIGroup.anchoredPosition = Vector3.zero;
    }
    public void Exit()
    {
        anim.SetTrigger("doHello");
        UIGroup.anchoredPosition = Vector3.down * 1000;
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
        talkText.text = TalkData[1];
        yield return new WaitForSeconds(2f);
    }
}