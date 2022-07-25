using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool _isTouchTop;
    public bool _isTouchBottom;
    public bool _isTouchLeft;
    public bool _isTouchRight;
    public bool _isHit;
    public bool _isBombTime;

    public int _life;
    public int _score;

    public GameObject _bulletA;
    public GameObject _bulletB;
    public GameObject _bombEffect;
    public GameManager _gMgr;
    public ObjectManager _objMgr;
    public GameObject[] _followersArr;

    public float _speed;
    public int _power;
    public int _maxPower;
    public int _bomb;
    public int _maxBomb;
    public float _curShotDelay;

    Animator _ani;
    Rigidbody2D _rigid;
    Rigidbody2D _rigidL;
    Rigidbody2D _rigidR;

    public const int PLAYER_BASIC_LIFE = 3;
    public const int PLAYER_BASIC_POWER = 1;
    public const float PLAYER_BASIC_SPEED = 3.0f;

    const int PLAYER_FIRE_FORCE = 10;
    const float PLAYER_FIRE_DELAY = 0.2f;

    const int PLAYER_MAX_POWER = 6;
    public const int PLAYER_MAX_BOMB = 3;

    const string RESOURCES_PREFABS_PATH = "Prefabs";

    void Awake()
    {
        _ani = GetComponent<Animator>();
        _bombEffect = GameObject.FindGameObjectWithTag("Effect").transform.Find("BombEffect").gameObject;
        // _gMgr = GameObject.FindObjectOfType<GameManager>();
        _gMgr   = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _objMgr = GameObject.FindGameObjectWithTag("ObjectManager").GetComponent<ObjectManager>();
    }
    void Start()
    {
        _speed = PLAYER_BASIC_SPEED;
        _power = PLAYER_BASIC_POWER;        
        _life  = PLAYER_BASIC_LIFE;

        _maxPower = PLAYER_MAX_POWER;
        _maxBomb = PLAYER_MAX_BOMB;

        _bulletA = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/PlayerBulletA");
        _bulletB = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/PlayerBulletB");
    }

    // Update is called once per frame
    void Update()
    {   
        MovePlayer();
        Fire();
        Bomb();
        Reload();
    }

    void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // BorderLine 에 닿을 경우 체크
        if ((_isTouchRight && h == 1.0f) || (_isTouchLeft && h == -1.0f))
            h = 0;

        if ((_isTouchTop && v == 1.0f) || (_isTouchBottom && v == -1.0f))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * _speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        //if ( h == 0.0f || h == 1.0f || h == -1.0f)
        //_ani.SetInteger("input", int.Parse(h.ToString()));
        _ani.SetInteger("input", (int) h);
    }

    void Fire()
    {
        if ( Input.GetButton("Fire1") )
        {
            if (_curShotDelay >= PLAYER_FIRE_DELAY)
            {

                switch (_power)
                {
                    case 1:
                        // GameObject bullet = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f), transform.rotation);
                        // bullet.name = "PlayerBulletA";
                        GameObject bullet = _objMgr.MakeObject(POOLING_OBJECT.PlayerBulletA);
                        bullet.transform.position = transform.position + (Vector3.up * 0.1f);
                        bullet.transform.rotation = transform.rotation;

                        _rigid = bullet.GetComponent<Rigidbody2D>();
                        _rigid.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);
                        break;
                    case 2:
                        // GameObject bulletL = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f) + (Vector3.left * 0.1f), transform.rotation);
                        // bulletL.name = "PlayerBulletA";

                        GameObject bulletL = _objMgr.MakeObject(POOLING_OBJECT.PlayerBulletA);
                        bulletL.transform.position = transform.position + (Vector3.up * 0.1f) + (Vector3.left * 0.1f);
                        bulletL.transform.rotation = transform.rotation;

                        _rigidL = bulletL.GetComponent<Rigidbody2D>();
                        _rigidL.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);

                        // GameObject bulletR = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f) + (Vector3.right * 0.1f), transform.rotation);
                        // bulletR.name = "PlayerBulletA";

                        GameObject bulletR = _objMgr.MakeObject(POOLING_OBJECT.PlayerBulletA);
                        bulletR.transform.position = transform.position + (Vector3.up * 0.1f) + (Vector3.right * 0.1f);
                        bulletR.transform.rotation = transform.rotation;

                        _rigidR = bulletR.GetComponent<Rigidbody2D>();
                        _rigidR.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);
                        break;
                    default:
                        // GameObject bulletCC = Instantiate(_bulletB, transform.position + (Vector3.up * 0.1f), transform.rotation);
                        // bulletCC.name = "PlayerBulletB";
                        GameObject bulletCC = _objMgr.MakeObject(POOLING_OBJECT.PlayerBulletB);
                        bulletCC.transform.position = transform.position + (Vector3.up * 0.1f);
                        bulletCC.transform.rotation = transform.rotation;

                        _rigid = bulletCC.GetComponent<Rigidbody2D>();
                        _rigid.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);

                        // GameObject bulletLL = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f) + (Vector3.left * 0.3f), transform.rotation);
                        // bulletLL.name = "PlayerBulletA";
                        GameObject bulletLL = _objMgr.MakeObject(POOLING_OBJECT.PlayerBulletA);
                        bulletLL.transform.position = transform.position + (Vector3.up * 0.1f) + (Vector3.left * 0.3f);
                        bulletLL.transform.rotation = transform.rotation;

                        _rigidL = bulletLL.GetComponent<Rigidbody2D>();
                        _rigidL.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);

                        // GameObject bulletRR = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f) + (Vector3.right * 0.3f), transform.rotation);
                        // bulletRR.name = "PlayerBulletA";

                        GameObject bulletRR = _objMgr.MakeObject(POOLING_OBJECT.PlayerBulletA);
                        bulletRR.transform.position = transform.position + (Vector3.up * 0.1f) + (Vector3.right * 0.3f);
                        bulletRR.transform.rotation = transform.rotation;

                        _rigidR = bulletRR.GetComponent<Rigidbody2D>();
                        _rigidR.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);
                        break;
                }

                _curShotDelay = 0;
            }
        }
    }

    void Bomb()
    {
        if (!Input.GetButton("Fire2"))
            return;

        if (_isBombTime)
            return;

        if (_bomb == 0)
            return;

        _bomb--;
        _isBombTime = true;
        _gMgr.UpdateImgBomb(_bomb);

        // #1. Effect visible
        _bombEffect.SetActive(true);
        Invoke("OffBoomEffect", 4.0f);

        // #2. Remove Enemy
        /*
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gObj in enemies)
        {
            Enemy en = gObj.GetComponent<Enemy>();
            en.OnHit(1000);
        }
        */

        GameObject[] enemiesL = _objMgr.GetPool(POOLING_OBJECT.EnemyL);
        foreach (GameObject gObj in enemiesL)
        {
            if (gObj.activeSelf)
            {
                Enemy en = gObj.GetComponent<Enemy>();
                en.OnHit(1000);
            }
        }
        GameObject[] enemiesM = _objMgr.GetPool(POOLING_OBJECT.EnemyM);
        foreach (GameObject gObj in enemiesM)
        {
            if (gObj.activeSelf)
            {
                Enemy en = gObj.GetComponent<Enemy>();
                en.OnHit(1000);
            }
        }

        GameObject[] enemiesS = _objMgr.GetPool(POOLING_OBJECT.EnemyS);
        foreach (GameObject gObj in enemiesS)
        {
            if (gObj.activeSelf)
            {
                Enemy en = gObj.GetComponent<Enemy>();
                en.OnHit(1000);
            }
        }

        // #3. Remove Bullet
        /*
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject gObj in bullets)
        {
            //Destroy(gObj);
            gObj.SetActive(false);
        }
        */

        GameObject[] bulletsA = _objMgr.GetPool(POOLING_OBJECT.EnemyBulletA);
        foreach (GameObject gObj in bulletsA)
        {
            if (gObj.activeSelf)
            {
                //Destroy(gObj);
                gObj.SetActive(false);
            }   
        }

        GameObject[] bulletsB = _objMgr.GetPool(POOLING_OBJECT.EnemyBulletB);
        foreach (GameObject gObj in bulletsB)
        {
            if (gObj.activeSelf)
            {
                //Destroy(gObj);
                gObj.SetActive(false);
            }
        }
    }

    void Reload()
    {
        _curShotDelay += Time.deltaTime;
    }

    void OffBoomEffect()
    {
        _bombEffect.SetActive(false);
        _isBombTime = false;
    }

    void AddFollower()
    {
        if (_power == 4)
            _followersArr[0].SetActive(true);
        else if (_power == 5)
            _followersArr[1].SetActive(true);
        else if (_power == 6)
            _followersArr[2].SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Border" )
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    _isTouchTop = true;
                    break;
                case "Bottom":
                    _isTouchBottom = true;
                    break;
                case "Left":
                    _isTouchLeft = true;
                    break;
                case "Right":
                    _isTouchRight = true;
                    break;
            }   
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {

            if (_isHit)
                return;

            _isHit = true;
            _life--;
            _gMgr.UpdateImgLife(_life);
            if (_life == 0)
            {
                _gMgr.GameOver();
            }
            else
            {
                _gMgr.ReSpawnPlayer();
            }
            gameObject.SetActive(false);
            // Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item._type)
            {
                case ITEM_TYPE.COIN:
                    _score += 1000;
                    break;
                case ITEM_TYPE.POWER:
                    if (_power == PLAYER_MAX_POWER)
                        _score += 500;
                    else
                    {
                        _power++;
                        AddFollower();
                    }   
                    break;
                case ITEM_TYPE.BOMB:
                    if (_bomb == PLAYER_MAX_BOMB)
                        _score += 500;
                    else
                    {
                        _bomb++;
                        _gMgr.UpdateImgBomb(_bomb);
                    }
                        
                    break;
            }

            // Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    _isTouchTop = false;
                    break;
                case "Bottom":
                    _isTouchBottom = false;
                    break;
                case "Left":
                    _isTouchLeft = false;
                    break;
                case "Right":
                    _isTouchRight = false;
                    break;
            }
        }
    }
}
