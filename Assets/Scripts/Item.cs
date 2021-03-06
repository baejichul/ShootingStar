using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_TYPE
{
    BOMB = 0
    , COIN
    , POWER
}

public class Item : MonoBehaviour
{
    public ITEM_TYPE _type;
    Rigidbody2D _rigid;

    const float ITEM_DOWN_SPEED = 3.0f;

    void Awake()
    {
        if ( gameObject.name.ToUpper().Contains( ITEM_TYPE.BOMB.ToString() ) )
            this._type = ITEM_TYPE.BOMB;
        else if (gameObject.name.ToUpper().Contains(ITEM_TYPE.COIN.ToString() ) )
            this._type = ITEM_TYPE.COIN;
        else if (gameObject.name.ToUpper().Contains( ITEM_TYPE.POWER.ToString() ) )
            this._type = ITEM_TYPE.POWER;

        // Debug.Log($"gameObject.name = {gameObject.name}");
        // Debug.Log($"i_type = {_type}");
    }

    void OnEnable()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.velocity = Vector2.down * ITEM_DOWN_SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
