using UnityEngine;
using System.Collections;

[AddComponentMenu ("MyGame/Enemy")]

public class Enemy : MonoBehaviour {
    public float m_speed = 1;
    //旋转速度
    public float m_life = 10;
    public AudioClip m_shootClip;
    protected AudioSource m_audio;
    public Transform m_explosionFX;
    public int m_point = 10;

    protected float m_rotspeed = 30;
    //变向间隔时间，计时器
    protected float m_timer = 1.5f;
    protected Transform m_transform;


	// Use this for initialization
	void Start () {
        m_transform = this.transform;
        m_audio = this.audio;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMove();
	
	}
    protected virtual void UpdateMove()
    {
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_timer = 3;
        //每3秒转向一次
            m_rotspeed = -m_rotspeed;
        }
        m_transform.Rotate(Vector3.up, m_rotspeed * Time.deltaTime, Space.World);

        m_transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("PlayerRocket") == 0)
        {
            //获得对方碰撞体Rocket脚本组件
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null)
            {
                m_life -= rocket.m_power;
                if (m_life <= 0)
                {
                    GameManager.Instance.AddScore(m_point);
                    Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }
     //撞到玩家自己也毁坏
        else if (other.tag.CompareTo("Player") == 0)
        {
            m_life = 0;
            Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.tag.CompareTo("bound") == 0)
        {
            m_life = 0;
            Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

        
}
