using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int _damage;

    void Start()
    {
        // Debug.Log($"gameObject.name = {gameObject.name}");
        if (gameObject.name == POOLING_OBJECT.PlayerBulletA.ToString())
            _damage = 1;
        else if (gameObject.name == POOLING_OBJECT.PlayerBulletB.ToString())
            _damage = 3;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.gameObject.tag);
        if ( collision.gameObject.tag == "BorderBullet")
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
