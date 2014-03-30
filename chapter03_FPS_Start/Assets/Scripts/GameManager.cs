﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;
    int m_score = 0;
    static int m_hiscore = 0;
    int m_ammo = 100;
    Player m_player;
    GUIText txt_ammo;
    GUIText txt_hiscore;
    GUIText txt_life;
    GUIText txt_score;


	// Use this for initialization
	void Start () {
        Instance = this;
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        txt_ammo = this.transform.FindChild("txt_ammo").GetComponent<GUIText>();
        txt_hiscore = this.transform.FindChild("txt_hiscore").GetComponent<GUIText>();
        txt_life = this.transform.FindChild("txt_life").GetComponent<GUIText>();
        txt_score = this.transform.FindChild("txt_score").GetComponent<GUIText>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetScore(int score)
    {
        m_score += score;
        if (m_score > m_hiscore)
            m_hiscore = m_score;
        txt_score.text = "Score " + m_score;
        txt_hiscore.text = "High Score " + m_hiscore;
    }

    public void SetAmmo(int ammo)
    {
        m_ammo -= ammo;
        if (m_ammo <= 0)
            m_ammo = 100 - m_ammo;

        txt_ammo.text = m_ammo.ToString() + "/100";
        
    }

    public void SetLife(int life)
    {
        txt_life.text = life.ToString();
    }

    void OnGUI()
    {
        if (m_player.m_life <= 0)
        {
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 40;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Game Over");
            GUI.skin.label.fontSize = 20;
            if (GUI.Button(new Rect(Screen.width * 0.5f - 150, Screen.height * 0.75f, 300, 40), "Try Again"))
            {
                Application.LoadLevel(Application.loadedLevelName);
            }
        }
    }

}
