using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float _speed;
    public int _health;
    public float _curShotDelay;
    public int _enemyScore;

    public Sprite[] _spArr;
    Sprite[] _enemyArr;

    const string RESOURCES_PREFABS_PATH = "Prefabs";
    const string RESOURCES_SPRITES_PATH = "Sprites";
    const float ENEMY_FIRE_DELAY = 1.0f;
    const int ENEMY_FIRE_FORCE = 4;

    public GameObject _bulletA;
    public GameObject _bulletB;
    public GameObject _itemCoin;
    public GameObject _itemPower;
    public GameObject _itemBomb;
    public GameObject _gObjPlayer;
    public ObjectManager _objMgr;

    SpriteRenderer _spr;
    Rigidbody2D _rigid;
    Rigidbody2D _rigidL;
    Rigidbody2D _rigidR;

    void Awake()
    {
        _spr = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();

        _enemyArr = Resources.LoadAll<Sprite>(RESOURCES_SPRITES_PATH + "/Enemies");
        _bulletA  = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyBulletA");
        _bulletB  = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyBulletB");

        _itemCoin  = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/ItemCoin");
        _itemPower = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/ItemPower");
        _itemBomb  = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/ItemBomb");

        InitEnemy();
    }

    void OnEnable()
    {
        if (gameObject.name.StartsWith(POOLING_OBJECT.EnemyS.ToString()))
            _health = 3;
        else if (gameObject.name.StartsWith(POOLING_OBJECT.EnemyM.ToString()))
            _health = 15;
        else if (gameObject.name.StartsWith(POOLING_OBJECT.EnemyL.ToString()))
            _health = 50;
    }

    void Update()
    {
        Fire();
        Reload();
    }


    void InitEnemy()
    {
        if (gameObject.name.StartsWith(POOLING_OBJECT.EnemyS.ToString()))
        {
            _speed = 3;
            _health = 3;
            _enemyScore = 50;
            _spArr = new Sprite[] { _enemyArr[4], _enemyArr[5] };
        }
        else if (gameObject.name.StartsWith(POOLING_OBJECT.EnemyM.ToString()))
        {
            _speed = 5;
            _health = 15;
            _enemyScore = 200;
            _spArr = new Sprite[] { _enemyArr[6], _enemyArr[7] };
        }
        else if (gameObject.name.StartsWith(POOLING_OBJECT.EnemyL.ToString()))
        {
            _speed = 1;
            _health = 50;
            _enemyScore = 500;
            _spArr = new Sprite[] { _enemyArr[8], _enemyArr[9] };
        }

        // Debug.Log($"InitEnemy {gameObject.name} : {_speed}");
    }
    public void OnHit(int damage)
    {
        if (_health <= 0)
            return;

        _health -= damage;
        _spr.sprite = _spArr[1];
        Invoke("ReturnSprite", 0.1f);
        // Debug.Log($"damage={damage}, _health={_health}");

        if (_health <= 0)
        {
            Player player = _gObjPlayer.GetComponent<Player>();
            player._score += _enemyScore;

            // #Random Ratio Item Drop
            int ran = Random.Range(0, 10);
            if (ran < 3)
            {
                // Debug.Log("No Item");
            }
            else if (ran < 6)
            {
                // Instantiate(_itemCoin, transform.position, _itemCoin.transform.rotation);
                GameObject item = _objMgr.MakeObject(POOLING_OBJECT.ItemCoin);
                item.transform.position = transform.position;
            }
            else if (ran < 8)
            {
                // Instantiate(_itemPower, transform.position, _itemPower.transform.rotation);
                GameObject item = _objMgr.MakeObject(POOLING_OBJECT.ItemPower);
                item.transform.position = transform.position;
            }
            else if (ran < 10)
            {
                // Instantiate(_itemBomb, transform.position, _itemBomb.transform.rotation);
                GameObject item = _objMgr.MakeObject(POOLING_OBJECT.ItemBomb);
                item.transform.position = transform.position;
            }

            // Destroy(gameObject);
            gameObject.SetActive(false);
            // transform.rotation = Quaternion.identity;
        }
            
    }

    void ReturnSprite()
    {
        _spr.sprite = _spArr[0];
    }

    void Fire()
    {   
        if (_curShotDelay < ENEMY_FIRE_DELAY)
            return;

        if (!_gObjPlayer.activeSelf)
            return;


        if (gameObject.name.StartsWith("EnemyS"))
        {
            // GameObject bullet = Instantiate(_bulletA, transform.position, transform.rotation);
            // bullet.name = "EnemyBulletA";
            GameObject bullet = _objMgr.MakeObject(POOLING_OBJECT.EnemyBulletA);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            _rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 vec3 = _gObjPlayer.transform.position - transform.position;
            _rigid.AddForce(vec3.normalized * (ENEMY_FIRE_FORCE - 1), ForceMode2D.Impulse);
        }
        if (gameObject.name.StartsWith("EnemyL"))
        {
            // GameObject bulletR = Instantiate(_bulletB, transform.position + (Vector3.right * 0.3f), transform.rotation);
            // GameObject bulletL = Instantiate(_bulletB, transform.position + (Vector3.left * 0.3f), transform.rotation);

            // bulletR.name = "EnemyBulletB";
            // bulletL.name = "EnemyBulletB";

            GameObject bulletR = _objMgr.MakeObject(POOLING_OBJECT.EnemyBulletB);
            bulletR.transform.position = transform.position + (Vector3.right * 0.3f);
            bulletR.transform.rotation = transform.rotation;

            GameObject bulletL = _objMgr.MakeObject(POOLING_OBJECT.EnemyBulletB);
            bulletL.transform.position = transform.position + (Vector3.left * 0.3f);
            bulletL.transform.rotation = transform.rotation;

            _rigidR = bulletR.GetComponent<Rigidbody2D>();
            _rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 vecR3 = _gObjPlayer.transform.position - ( transform.position + (Vector3.right * 0.3f) );
            Vector3 vecL3 = _gObjPlayer.transform.position - ( transform.position + (Vector3.left * 0.3f) );

            _rigidR.AddForce(vecR3.normalized * ENEMY_FIRE_FORCE, ForceMode2D.Impulse);
            _rigidL.AddForce(vecL3.normalized * ENEMY_FIRE_FORCE, ForceMode2D.Impulse);
        }

        _curShotDelay = 0;
    }

    void Reload()
    {
        _curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
            // transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet._damage);
            // Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }   
    }

}
