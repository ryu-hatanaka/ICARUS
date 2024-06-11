using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {
    //システムだけ、ここで作ってからプレイヤーで制御してみればいいかな。
    private GameObject _bullet;
    const float MOVE_SPEED = 5;
    bool _instance_obj;
    public enum ShootingType
    {
        NONE,
        RAPID_FIRE,//連射
        INJECTIVE,//単射
    }
    ShootingType _fire_type;
    public void SetBulletObject( GameObject bullet ) {
        _bullet = bullet;
    }
    public void SetInstanceObj( bool state ) {
        _instance_obj = state;
    }
    public bool IsObjState( ) {
        return _instance_obj;
    }
    void Start( ) {
        SetWeaponType( ShootingType.RAPID_FIRE );
        _instance_obj = false;
    }
    private void Update( ) {
        if ( _bullet != null ) {
            Move( );
        }
    }

    public void Move( ) {
        _bullet.transform.position += new Vector3 ( 0f, 0f, MOVE_SPEED * Time.deltaTime );
    }
    public void SetWeaponType( ShootingType kind ) {
        _fire_type = kind;
    }
    public bool IsCreateBullet( ) {
        return Input.GetMouseButton( 0 );
    }
    public ShootingType GetWeaponType( ) {
        return _fire_type;
    }
}
