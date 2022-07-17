using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float _speed;
    public int _health;
    public Sprite[] _spArr;
    Sprite[] ememiesArr;

    const string RESOURCES_SPRITES_PATH = "Sprites";

    Rigidbody2D _rigid;
    SpriteRenderer _spr;

    void Awake()
    {
        _spr = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();

        ememiesArr = Resources.LoadAll<Sprite>(RESOURCES_SPRITES_PATH + "/Enemies");
    }

    void Start()
    {
        InitEnemy();

        _rigid.velocity = Vector2.down * _speed;
    }

    void InitEnemy()
    {
        if (gameObject.name == "EnemyS")
        {
            _speed = 3;
            _health = 3;
            _spArr = new Sprite[] { ememiesArr[4], ememiesArr[5] };
        }
        else if (gameObject.name == "EnemyM")
        {
            _speed = 10;
            _health = 15;
            _spArr = new Sprite[] { ememiesArr[6], ememiesArr[7] };
        }
        else if (gameObject.name == "EnemyL")
        {
            _speed = 1;
            _health = 50;
            _spArr = new Sprite[] { ememiesArr[8], ememiesArr[9] };
        }
    }
    void OnHit(int damage)
    {
        _health -= damage;
        _spr.sprite = _spArr[1];
        Invoke("ReturnSprite", 0.1f);
        // Debug.Log($"damage={damage}, _health={_health}");

        if (_health <= 0)
            Destroy(gameObject);
    }

    void ReturnSprite()
    {
        _spr.sprite = _spArr[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet._damage);
            Destroy(collision.gameObject);
        }   
    }

}
