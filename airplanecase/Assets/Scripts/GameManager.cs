﻿using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/GameManager")]

public class GameManager : MonoBehaviour {

    //通过GameManager.Instance能取得GameManager的实例，只有一个
    public static GameManager Instance;
    //得分
    public int m_score = 0;
    //纪录
    public static int m_hiscore = 0;
    //玩家
    protected Player m_player;
    //BGM
    public AudioClip m_musicClip;
    //声音源
    protected AudioSource m_Audio;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {

        m_Audio = this.audio;
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            m_player = obj.GetComponent<Player>();
        }
        	
	}

	
	// Update is called once per frame
	void Update () {
        //循环播放BGM
        if (!m_Audio.isPlaying)
        {
            m_Audio.clip = m_musicClip;
            m_Audio.Play();
        }

        //暂停游戏
        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
        }

	}

    void OnGUI()
    {
        //暂停游戏时，可选择继续或退出
        if(Time .timeScale == 0 )
        {
            if(GUI.Button (new Rect (Screen.width * 0.5f - 50,Screen .height * 0.4f,100,30),"游戏继续"))
            {
                Time.timeScale = 1;
            }
            if(GUI.Button (new Rect (Screen.width * 0.5f - 50,Screen.height * 0.6f,100,30),"退出游戏"))
            {
                Application .Quit ();
            }
        }

        int life = 0;
        if(m_player != null )
        {
            life = (int)m_player .m_life ;
        }
        else  //Player==null,则玩家已阵亡 
        {
            GUI.skin.label .fontSize = 50;
            GUI.skin.label .alignment = TextAnchor.LowerCenter ;
            GUI.Label (new Rect (0,Screen .height * 0.2f,Screen .width ,60),"游戏失败");
            GUI .skin .label .fontSize = 20;
            //显示按钮
            if(GUI .Button (new Rect (Screen .width * 0.5f -50, Screen .height * 0.5f,100,30),"再试一次"))
            {
                //读取当前关卡
                Application .LoadLevel (Application .loadedLevelName );
            }
        }

        GUI.skin.label .fontSize = 15;
        //显示主角生命
        GUI .Label (new Rect (5,5,100,30),"护盾"+life );
        //显示最高分
        GUI .skin.label.alignment = TextAnchor .LowerCenter ;
        GUI.Label (new Rect (0,5,Screen .width ,30),"纪录"+m_hiscore );

        //显示当前得分
        GUI.Label (new Rect (0,25,Screen .width ,30),"得分"+m_score );
    }
    public void AddScore(int point)
    {
        m_score += point;
        //更新最高分

        if (m_hiscore < m_score)
            m_hiscore = m_score;
    }



}
