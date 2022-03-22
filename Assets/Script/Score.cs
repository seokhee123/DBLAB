using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class Score : MonoBehaviour
{
    public void WriteFile(string path, Dictionary<int, string> scoreTable, int score, string date)
    {
        
        StreamWriter sw = new StreamWriter(path);
        foreach(int key in scoreTable.Keys)
        {
            sw.WriteLine(key + "," + scoreTable[key]);
        }
        sw.Flush();
        sw.Close();
    }

    public void ReadFile(string path, Dictionary<int, string> scoreTable)
    {
        StreamReader sr = new StreamReader(path);
        string[] scoredata = new string[10];
        int i = 0;

        while (!sr.EndOfStream)
        {
           scoredata[i++] = sr.ReadLine();
        }
        sr.Close();
        for (int a=0; a<i; a++)
        {
            string[] split = scoredata[a].Split(new string[] { "," }, System.StringSplitOptions.None);
            scoreTable.Add (Int32.Parse(split[0]), split[1]);
        }
        foreach (KeyValuePair<int, string> dic in scoreTable)
        {
            Debug.Log(dic.Key + "Á¡ " +dic.Value);
        }
    }

    Player player;
    GameManager gameManager;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {

    }
}
