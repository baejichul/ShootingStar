using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.gameObject.tag);
        if ( collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
    }
}
