﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class directorScr : MonoBehaviour
{

    GameObject msgtxt;
    GameObject clickedObject;
    GameObject target;
    float start_time, diff_time, diff_distance;
    Vector2 start_positon;
    int hit, miss = 0;
    bool first_click = false;
    void Start()
    {
        msgtxt = GameObject.Find("MsgTxt");
        msgtxt.GetComponent<Text>().text = "click to start";
        target = GameObject.Find("Target");
    }

    void Update()
    {
        // 終了条件
        if (Input.GetKey(KeyCode.Escape)) Quit();



        //　データを集める処理
        if (Input.GetMouseButtonDown(0) && first_click)
        {
            // マウスのポジションを得る
            Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // SpriteのCollider2dとの接触を検知する
            // 指定した位置にオブジェクトがあれば、オブジェクトを取り出す
            // なければnullを返す
            Collider2D collision2d = Physics2D.OverlapPoint(clickPoint);
            if (collision2d)
            {
                diff_time = Time.time - start_time;
                start_time = Time.time;

                if (miss == 0 && hit == 0)
                {
                    start_positon = clickPoint;
                }

                // クリックされた GameObject clickedObjectを取得
                clickedObject = collision2d.transform.gameObject;
                diff_distance = Vector2.Distance(target.transform.position, start_positon);
                Debug.Log("Distance" + diff_distance);

                start_positon = target.transform.position;
                target.transform.position = new Vector2(Random.Range(-7.0f, 7.0f), Random.Range(-4.0f, 4.0f));

                float scale = Random.Range(-2.0f, 2.0f);
                target.transform.localScale = new Vector2(scale, scale);
                hit++;
            }
            else
            {
                // その下にオブジェクトがない
                miss++;
            }

            string res = hit.ToString() + "hit" + " " + miss.ToString() + "miss" + " " + diff_time.ToString() + "s";
            msgtxt.GetComponent<Text>().text = res;

            string data = hit.ToString() + "," + miss.ToString() + "," + diff_time.ToString() + "," + diff_distance.ToString();
            WritePointingData(data);

        }
        // 1回目の処理
        if (Input.GetMouseButtonDown(0) && !first_click) First();
    }

    void WritePointingData(string txt)
    {
        using (StreamWriter stream_writer = new StreamWriter("./PointingLog.txt", true))
        {
            stream_writer.WriteLine(txt);
            stream_writer.Close();
        }
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

    void First()
    {
        // マウスのポジションを得る
        Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        start_positon = clickPoint;
        target.transform.position = new Vector2(Random.Range(-7.0f, 7.0f), Random.Range(-4.0f, 4.0f));
        start_time = Time.time;
        first_click = true;
    }
}
