using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {
    [ SerializeField ] GameObject _bullet;
    private GameObject _bullet_obj;
    private GameObject _hand;
    private GameObject _camera;

    public GameObject _rapid_obj;//����
    public GameObject _injective_obj;//��

    const float MOVE_SPEED = 50;
    const float SHOOTING_TIME = 0.1f;
    const float INJECTIVE_TIME = 0.5f;
    float _time;
    bool _instance_obj;
    bool _is_clicked;
    bool _is_direction;

    //kari
    public float shotSpeed;
    public int shotCount = 50;
    private int _bullet_count = 0;
    //
    private float _long_press_time = 0;
    private float RELOAD_TIME = 180;
    public enum ShootingType {
        NONE,
        RAPID_FIRE,//�A��
        INJECTIVE,//�P��
    }
    ShootingType _fire_type;
    public void SetInstanceObj( bool state ) {
        _instance_obj = state;
    }
    public bool IsObjState( ) {
        return _instance_obj;
    }
    void Start( ) {
        SetWeaponType( ShootingType.RAPID_FIRE );
        _instance_obj = false;
        _hand = GameObject.Find( "Hand Pos" );
        _camera = GameObject.Find( "Main Camera" );
    }
    private void Update( ) {
        ShootingModeType( );
        ShootingModeChengeSystem( );
        if ( _bullet_obj != null && IsObjState( ) ) {
            Move( );
        }
    }
    void OnCollisionEnter( Collision collision ) {
        if ( collision.gameObject.CompareTag( "enemy" ) ) {//��
            Destroy( _bullet_obj );
        }
        if ( collision.gameObject.CompareTag( "Tower" ) ) {
            Destroy( _bullet_obj );
        }
    }
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
        _is_direction = false;
        if ( IsCreateBullet( ) && !_is_clicked ) {
            _is_clicked = true;
            _is_direction = true;
            if ( ShootingTime( ) >= SHOOTING_TIME && _bullet_count != shotCount ) {
                _instance_obj = true;
                _bullet_count++;
                _bullet_obj = Instantiate( _bullet,
                    _hand.transform.position,
                        Quaternion.Euler( _hand.transform.eulerAngles.x,
                        _hand.transform.eulerAngles.y, _hand.transform.eulerAngles.z ) );
                    _is_clicked = false;
                    _time = 0;
            }
            Destroy( _bullet_obj, 3.0f );
        }
        if ( _bullet_count >= shotCount ) {
            if ( Input.GetKey( KeyCode.R ) ) {
                _long_press_time++;
            }
            if ( Input.GetKeyUp( KeyCode.R ) ) {
                _long_press_time = 0;
            }
            if ( _long_press_time == RELOAD_TIME ) {
                _bullet_count = 0;
                _long_press_time = 0;
                Debug.Log( "�����[�h����" );
            }
        }
    }
    private void InjectiveSystem( ) {
        _is_clicked = false;
        _is_direction = false;
        if ( IsCreateBullet( ) && !_is_clicked ) {
            _is_clicked = true;
            _is_direction = true;
            if ( ShootingTime( ) >= INJECTIVE_TIME ) {
                _bullet_obj = Instantiate( _bullet, 
                    _hand.transform.position,
                    Quaternion.Euler( _hand.transform.eulerAngles.x,
                        _hand.transform.eulerAngles.y, 0 ) );
                _is_clicked = false;
                _time = 0;
                Destroy( _bullet_obj, 3.0f );
            }
        }
    }
    void ShootingModeChengeSystem( ) {
        //�ꉞ�o�������A�{���̓A�C�e���������ă^�C�v��ς��銴��
        if ( ShootingModeChangeRange( _rapid_obj ) < 1.5f ) {
            SetWeaponType( ShootingType.RAPID_FIRE );
        }
        if ( ShootingModeChangeRange( _injective_obj ) < 1.5f ) {
            SetWeaponType( ShootingType.INJECTIVE );
        }
    }

    float ShootingModeChangeRange( GameObject obj ) {
        GameObject player = GameObject.Find( "Player" );
        Vector3 diff = player.transform.position - obj.transform.position;
        float mag = diff.magnitude;
        return mag;
    }

    Vector3 _bullet_direction;
    public void Move( ) {
        //�Ō�̈�����܂��t���Ă��邯�Ǒ��͑��v�ɂȂ����B�͂�
        //�����ł����B��肪����������B�����Ă���Ԃ̓J�����̌�������邱�Ƃ���������
        //���̈ʒu���������ƂɂȂ�B�܂蓮�������Ƃ��Ă��������邱�Ƃ��Ȃ��B
        //�܂苁�߂�����̂́A�����Ă��鎞���J�����̌��������A���˂�����̒e���e���Ȃ��悤��
        //���Ȃ��Ƃ����Ȃ��B
        Rigidbody rb_bullet = _bullet_obj.GetComponent< Rigidbody >( );
        if ( !_is_direction ) {
            _hand.transform.forward = _camera.transform.forward;
        } else {
            _bullet_direction = _hand.transform.forward;
        }
        rb_bullet.velocity = _bullet_direction * MOVE_SPEED;
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
