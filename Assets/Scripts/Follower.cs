using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float _curShotDelay;

    const int FOLLOWER_FIRE_FORCE = 10;
    const float FOLLOWER_FIRE_DELAY = 0.2f;

    public Rigidbody2D _rigid;
    public ObjectManager _objMgr;

    public Vector3 _followPos;
    public int _followDelay;

    public GameObject _player;
    public Transform _parent;
    public Queue<Vector3> _parentPos;

    void Awake()
    {
        _objMgr = GameObject.FindGameObjectWithTag("ObjectManager").GetComponent<ObjectManager>();
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _followDelay = 30;
        _parentPos = new Queue<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        Watch();
        Follow();
        Fire();
        Reload();
    }

    void Watch()
    {
        // Queue = FIFO(First Input First Out)

        // #Input
        if (!_parentPos.Contains(_parent.position))
            _parentPos.Enqueue(_parent.position);

        // #Output
        if (_parentPos.Count > _followDelay)
            _followPos = _parentPos.Dequeue();
        else if (_parentPos.Count < _followDelay)
            _followPos = _parent.position;
    }

    void Follow()
    {
        transform.position = _followPos;
    }

    void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            if (_curShotDelay >= FOLLOWER_FIRE_DELAY)
            {  
                GameObject bullet = _objMgr.MakeObject(POOLING_OBJECT.FollowerBullet);
                bullet.transform.position = transform.position + (Vector3.up * 0.1f);
                bullet.transform.rotation = transform.rotation;

                _rigid = bullet.GetComponent<Rigidbody2D>();
                _rigid.AddForce(Vector2.up * FOLLOWER_FIRE_FORCE, ForceMode2D.Impulse);

                _curShotDelay = 0;
            }
        }
    }

   
    void Reload()
    {
        _curShotDelay += Time.deltaTime;
    }
}
