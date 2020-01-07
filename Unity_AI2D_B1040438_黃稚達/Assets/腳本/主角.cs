﻿using UnityEngine;
using UnityEngine.Events;

public class 主角 : MonoBehaviour
{
    public int speed = 100;         //速度
    public float jump = 1000.0f;        //跳躍
    public string Name = "主角"; 
    public bool pass = false;                
    public bool isGround;           //重力
    

    public UnityEvent onEat;
    public AudioClip soundProp;

    private Rigidbody2D r2d;
    private AudioSource aud;

    [Header("血量"), Range(0, 200)]
    public float hp = 100;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Turn();
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);
        Jump();
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "鑽石")
        {
            aud.PlayOneShot(soundProp, 1.2f);
            Destroy(collision.gameObject);  
            onEat.Invoke();                 
        }
    }

    /// <summary>
    /// 走路
    /// </summary>
    private void Walk()
    {
        if (r2d.velocity.magnitude < 10)
            r2d.AddForce(new Vector2(speed * Input.GetAxisRaw("Horizontal"), 0));
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
        }
    }

    /// <summary>
    /// 轉彎
    /// </summary>
    private void Turn(int direction = 0)
    {
        transform.eulerAngles = new Vector3(0, direction, 0);
    }


    public void Damage(float damage)
    {
        hp -= damage;
    }
}