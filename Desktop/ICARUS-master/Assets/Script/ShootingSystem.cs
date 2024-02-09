using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {
    //�V�X�e�������A�����ō���Ă���v���C���[�Ő��䂵�Ă݂�΂������ȁB
    
    [ SerializeField ] GameObject _bullet;
    private GameObject _bullet_obj;
    private GameObject _player;
    private Transform _fire_point;
    const float MOVE_SPEED = 50f;
    const float SHOOTING_TIME = 0.05f;
    const float INJECTIVE_TIME = 0.5f;
    float _time;
    bool _instance_obj;
    bool _is_clicked;
    public enum ShootingType {
        NONE,
        RAPID_FIRE,//�A��
        INJECTIVE,//�P��
    }
    ShootingType _fire_type;
    public void SetPlyaerObject( GameObject player ) {
        _player = player;
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
        if ( _player != null ) {
            ShootingModeType( );
        }
        if ( _bullet_obj != null && IsObjState( ) ) {
            Move( );
        }
    }
    //void OnCollisionEnter( Collision collision ) {
    //    if ( collision.gameObject.CompareTag( "enemy" ) ) {//��
    //        Destroy( _bullet_obj );
    //    }
    //}
    private void ShootingModeType( ) {
        switch ( GetWeaponType( ) ) {
            case ShootingType.RAPID_FIRE:
                RapidFireSystem( );
                break;
            case ShootingType.INJECTIVE:
                InjectiveSystem( );
                break;
        }
    }
    private void RapidFireSystem( ) {
        _is_clicked = false;
        if ( IsCreateBullet( ) && !_is_clicked ) {
            _is_clicked = true;
            if ( ShootingTime( ) >= SHOOTING_TIME ) {
                _instance_obj = true;
                _bullet_obj = Instantiate( _bullet,
                    _player.transform.position + new Vector3( 0f, 0f, 0.5f ),
                        _player.transform.rotation );
                    _is_clicked = false;
                    _time = 0;
            }
            Destroy( _bullet_obj, 3.0f );
        }
    }
    private void InjectiveSystem( ) {
        _is_clicked = false;
        if ( IsCreateBullet( ) && !_is_clicked ) {
            _is_clicked = true;
            if ( ShootingTime( ) >= INJECTIVE_TIME ) {
                _bullet_obj = Instantiate( _bullet, 
                    _player.transform.position + new Vector3( 0f, 0f, 0.5f ),
                    _player.transform.rotation );
                _is_clicked = false;
                _time = 0;
                Destroy( _bullet_obj, 3.0f );
            }
        }
    }

    public void Move( ) {
        //_bullet_obj.transform.position += new Vector3 ( 0f, 0f, MOVE_SPEED * Time.deltaTime );
        Rigidbody rb_bullet = _bullet.GetComponent< Rigidbody >( );
        rb_bullet.velocity = _player.transform.forward * -MOVE_SPEED;
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
    private float ShootingTime( ) {
        _time += Time.deltaTime;
        return _time;
    }
}
