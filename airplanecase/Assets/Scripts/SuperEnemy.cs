﻿using UnityEngine;
using System.Collections;

[AddComponentMenu ("MyGame/SuperEnemy")]
public class SuperEnemy : Enemy {

    public Transform m_rocket;

    //子弹间隔
    protected float m_fireTimer = 1;
    protected Transform m_player;

    //Awake继承于MonoBehaviour,游戏体实例化时执行一次，先于Start()
    void Awake()
    {
        //获取主角的游戏体实体
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            m_player = obj.transform;
        }
    }

    
    protected override void UpdateMove()
    {
        m_fireTimer -= Time.deltaTime;
        if (m_fireTimer <= 0)
        {
            m_fireTimer = 1;
            if (m_player != null)
            {
                Vector3 relativePos = m_transform.position - m_player.position;
                m_audio.PlayOneShot(m_shootClip);
                //子弹初始化时朝向主角
                Instantiate(m_rocket, m_transform.position, Quaternion.LookRotation(relativePos));
            }
        }

        
        m_transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }
}
