using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POOLING_OBJECT
{
    EnemyL
    , EnemyM
    , EnemyS
    , ItemCoin
    , ItemPower
    , ItemBomb
    , PlayerBulletA
    , PlayerBulletB
    , EnemyBulletA
    , EnemyBulletB
}

public class ObjectManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject _enemyLPrefab;
    [SerializeField] GameObject _enemyMPrefab;
    [SerializeField] GameObject _enemySPrefab;
    [SerializeField] GameObject _itemCoinPrefab;
    [SerializeField] GameObject _itemPowerPrefab;
    [SerializeField] GameObject _itemBombPrefab;
    [SerializeField] GameObject _bulletPlayerAPrefab;
    [SerializeField] GameObject _bulletPlayerBPrefab;
    [SerializeField] GameObject _bulletEnemyAPrefab;
    [SerializeField] GameObject _bulletEnemyBPrefab;

    [Header("GameObject")]
    GameObject[] _enemyL;
    GameObject[] _enemyM;
    GameObject[] _enemyS;
    GameObject[] _itemCoin;
    GameObject[] _itemPower;
    GameObject[] _itemBomb;
    GameObject[] _bulletPlayerA;
    GameObject[] _bulletPlayerB;
    GameObject[] _bulletEnemyA;
    GameObject[] _bulletEnemyB;

    GameObject[] _targetPool;

    void Awake()
    {
        _enemyL = new GameObject[10];
        _enemyM = new GameObject[10];
        _enemyS = new GameObject[20];
        _itemCoin  = new GameObject[20];
        _itemPower = new GameObject[10];
        _itemBomb  = new GameObject[10];
        _bulletPlayerA = new GameObject[100];
        _bulletPlayerB = new GameObject[100];
        _bulletEnemyA = new GameObject[100];
        _bulletEnemyB = new GameObject[100];

        Generate();
    }

    void Generate()
    {   
        for(int i = 0; i< _enemyL.Length; i++)
        {
            _enemyL[i] = Instantiate(_enemyLPrefab);
            _enemyL[i].name = POOLING_OBJECT.EnemyL.ToString();
            _enemyL[i].SetActive(false);
        }
            
        for (int i = 0; i < _enemyM.Length; i++)
        {
            _enemyM[i] = Instantiate(_enemyMPrefab);
            _enemyM[i].name = POOLING_OBJECT.EnemyM.ToString();
            _enemyM[i].SetActive(false);
        }
            
        for (int i = 0; i < _enemyS.Length; i++)
        {
            _enemyS[i] = Instantiate(_enemySPrefab);
            _enemyS[i].name = POOLING_OBJECT.EnemyS.ToString();
            _enemyS[i].SetActive(false);
        }
            
        for (int i = 0; i < _itemCoin.Length; i++)
        {
            _itemCoin[i] = Instantiate(_itemCoinPrefab);
            _itemCoin[i].name = POOLING_OBJECT.ItemCoin.ToString();
            _itemCoin[i].SetActive(false);
        }
            
        for (int i = 0; i < _itemPower.Length; i++)
        {
            _itemPower[i] = Instantiate(_itemPowerPrefab);
            _itemPower[i].name = POOLING_OBJECT.ItemPower.ToString();
            _itemPower[i].SetActive(false);
        }
            
        for (int i = 0; i < _itemBomb.Length; i++)
        {
            _itemBomb[i] = Instantiate(_itemBombPrefab);
            _itemBomb[i].name = POOLING_OBJECT.ItemBomb.ToString();
            _itemBomb[i].SetActive(false);
        }
            
        for (int i = 0; i < _bulletPlayerA.Length; i++)
        {
            _bulletPlayerA[i] = Instantiate(_bulletPlayerAPrefab);
            _bulletPlayerA[i].name = POOLING_OBJECT.PlayerBulletA.ToString();
            _bulletPlayerA[i].SetActive(false);
        }
            
        for (int i = 0; i < _bulletPlayerB.Length; i++)
        {
            _bulletPlayerB[i] = Instantiate(_bulletPlayerBPrefab);
            _bulletPlayerB[i].name = POOLING_OBJECT.PlayerBulletB.ToString();
            _bulletPlayerB[i].SetActive(false);
        }
            
        for (int i = 0; i < _bulletEnemyA.Length; i++)
        {
            _bulletEnemyA[i] = Instantiate(_bulletEnemyAPrefab);
            _bulletEnemyA[i].name = POOLING_OBJECT.EnemyBulletA.ToString();
            _bulletEnemyA[i].SetActive(false);
        }
            
        for (int i = 0; i < _bulletEnemyB.Length; i++)
        {
            _bulletEnemyB[i] = Instantiate(_bulletEnemyBPrefab);
            _bulletEnemyB[i].name = POOLING_OBJECT.EnemyBulletB.ToString();
            _bulletEnemyB[i].SetActive(false);
        }   
    }

    public GameObject MakeObject(POOLING_OBJECT type)
    {
        switch (type)
        {
            case POOLING_OBJECT.EnemyL:
                _targetPool = _enemyL;
                break;
            case POOLING_OBJECT.EnemyM:
                _targetPool = _enemyM;
                break;
            case POOLING_OBJECT.EnemyS:
                _targetPool = _enemyS;
                break;
            case POOLING_OBJECT.ItemCoin:
                _targetPool = _itemCoin;
                break;
            case POOLING_OBJECT.ItemPower:
                _targetPool = _itemPower;
                break;
            case POOLING_OBJECT.ItemBomb:
                _targetPool = _itemBomb;
                break;
            case POOLING_OBJECT.PlayerBulletA:
                _targetPool = _bulletPlayerA;
                break;
            case POOLING_OBJECT.PlayerBulletB:
                _targetPool = _bulletPlayerB;
                break;
            case POOLING_OBJECT.EnemyBulletA:
                _targetPool = _bulletEnemyA;
                break;
            case POOLING_OBJECT.EnemyBulletB:
                _targetPool = _bulletEnemyB;
                break;
        }

        for (int i = 0; i< _targetPool.Length; i++)
        {
            if (!_targetPool[i].activeSelf)
            {
                _targetPool[i].SetActive(true);
                return _targetPool[i];
            }
        }

        return null;
    }

    public GameObject[] GetPool(POOLING_OBJECT type)
    {
        switch (type)
        {
            case POOLING_OBJECT.EnemyL:
                _targetPool = _enemyL;
                break;
            case POOLING_OBJECT.EnemyM:
                _targetPool = _enemyM;
                break;
            case POOLING_OBJECT.EnemyS:
                _targetPool = _enemyS;
                break;
            case POOLING_OBJECT.ItemCoin:
                _targetPool = _itemCoin;
                break;
            case POOLING_OBJECT.ItemPower:
                _targetPool = _itemPower;
                break;
            case POOLING_OBJECT.ItemBomb:
                _targetPool = _itemBomb;
                break;
            case POOLING_OBJECT.PlayerBulletA:
                _targetPool = _bulletPlayerA;
                break;
            case POOLING_OBJECT.PlayerBulletB:
                _targetPool = _bulletPlayerB;
                break;
            case POOLING_OBJECT.EnemyBulletA:
                _targetPool = _bulletEnemyA;
                break;
            case POOLING_OBJECT.EnemyBulletB:
                _targetPool = _bulletEnemyB;
                break;
        }

        return _targetPool;
    }
}
