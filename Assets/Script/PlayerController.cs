using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    const int PLAYER_SPEED = 10;   //プレイヤー本来の速度
    const int BOOST_SPEED = 50;   //ブースト時の速度
    const float BOOST_TIME = 3.5f;     //３.５秒間ブーストタイム
    const int BOOST_COOL_TIME = 5;//5秒間のクールタイム
    const float JUMP_POWER = 50f; //ジャンプの高さ
    const float PREPARATION_TIME = 1.5f;
    const float CHARGE_BOOST_MAX_TIME = 3.5f;
    const float CHARGE_COOL_TIME = 10f;

private GameObject _player;

    private Rigidbody _rb_player;
    public LayerMask _ground_layer;

    bool _boost_change;
    bool _boost_form_change;
    bool _sense_button;
    bool _jumping;

    float _preparation_time;
    float _speed;
    float _boost_time;
    float _boost_cool_time;
    
    void Start( ) { //初期化
        _player = GameObject.Find( "Player" );
        _rb_player = _player.GetComponent< Rigidbody >( );
        _speed = PLAYER_SPEED * Time.deltaTime;
        _boost_time = 0;
        this.transform.Rotate( new Vector3( 0f, 0f, 10f ) );
=======
    const int PLAYER_SPEED = 2;   //プレイヤー本来の速度
    const int BOOST_SPEED = 10;   //ブースト時の速度
    const float BOOST_TIME = 3.5f;     //３.５秒間ブーストタイム
    const int BOOST_COOL_TIME = 5;//5秒間のクールタイム

    private GameObject _player;
    private GameObject _ground;

    public Rigidbody _rb_player;
    public LayerMask _ground_layer;

    bool _grounded;
    bool _boost_change;
    bool _boost_form_change;
    bool _sense_button;

    float _speed;
    float _boost_time;
    float _boost_cool_time;
    public void SetPlayerObject( GameObject obj ) {
        _player = obj;
    }

    public void SetGrondObject( GameObject obj ) {
        _ground = obj;
    }
    void Start( ) { //初期化
        _rb_player = GetComponent< Rigidbody >( );
        _speed = PLAYER_SPEED * Time.deltaTime;
        _boost_time = 0;

>>>>>>> 70039e1e2c5d9f4db011f012e3dc7b498c40ffdd
    }

    void Update( ) {
        if ( _player != null ) {
            SpeedBoost( );
            Move( );
            JumpControl( );
<<<<<<< HEAD
            ChargeBoost( );
        }
=======
        }
        
>>>>>>> 70039e1e2c5d9f4db011f012e3dc7b498c40ffdd
    }

    void Move( ) {
        _sense_button = false;
        Vector3 vec = new Vector3( );
        if ( Input.GetKey( KeyCode.W ) ) {
            vec.z += _speed;
            _sense_button = true;
        }
        if ( Input.GetKey( KeyCode.S ) ) {
            vec.z += -_speed;
            _sense_button = true;
        }
        if ( Input.GetKey( KeyCode.A ) ) {
            vec.x += -_speed;
            _sense_button = true;
        }
        if ( Input.GetKey( KeyCode.D ) ) {
            vec.x += _speed;
            _sense_button = true;
        }
        _player.transform.Translate( vec );
        
    }

    void JumpControl( ) {
<<<<<<< HEAD
        if ( Input.GetButtonDown( "Jump" ) && _jumping ) {
            Jump( );
        }
    }
    private void OnCollisionEnter( Collision collision ) {
        if( collision.gameObject.CompareTag( "Ground" ) ) {
            _jumping = true;
        }
    }
    private void OnCollisionExit( Collision collision ) {
        if ( collision.gameObject.CompareTag( "Ground" ) ) {
            _jumping = false;
        }
    }

    void Jump( ) {
        _rb_player.AddForce( Vector3.up * JUMP_POWER, ForceMode.Impulse );
=======
        if ( Input.GetButtonDown( "Jump" ) ) {
            Jump( );
        }
    }
    
    void Jump( ) {
        _rb_player.AddForce( Vector3.up * 5f, ForceMode.Impulse );
>>>>>>> 70039e1e2c5d9f4db011f012e3dc7b498c40ffdd
    }

    void SpeedBoost( ) {
        if ( IsButtonShift( ) ) {
             _boost_change = true;
        } //ブースト開始
        if ( _boost_change ) {
            if ( !IsBoostControl( ) ) { //３秒以下なら速度をあげる
                _speed = BOOST_SPEED * Time.deltaTime;
                if ( !_sense_button ) { //何もボタンを押されてない時はWKeyを押されてる判定
                    _player.transform.Translate( new Vector3( 0, 0, _speed ) );
                }
            } else { //３秒たったら速度を戻し、ブーストを終了させる。そしてクールタイムが始まる
                _speed = PLAYER_SPEED * Time.deltaTime;
                _boost_change = false;
                _boost_form_change = true;
                
           }
        }
        if ( _boost_form_change ) {
            if ( IsBoostCoolControl( ) ){ //クールタイムの５秒たったら、ブーストを再度可能にする。そしてクールタイムも元に戻す
                _boost_time = 0;
                _boost_cool_time = 0;
                _boost_form_change = false;
            }
        }
    }
<<<<<<< HEAD
    float _charge_boost_time;
    float _charge_cool_time;
    bool _charge_form_change;
    void ChargeBoost( ) {
        //改善する箇所↓
        //発動時間が最大3.5秒　←途中でキャンセルすることが可能の為、最大という書き方をしている
        //上で書いているように途中でキャンセル出来るようにする
        //一回押しただけで作動するような仕様もあったからそれを作成
        if ( Input.GetKey( KeyCode.W ) && Input.GetKey( KeyCode.LeftControl ) ) {
            _preparation_time += Time.deltaTime;
            if ( _preparation_time > PREPARATION_TIME ) {
                _charge_boost_time += Time.deltaTime ;
                if ( _charge_boost_time < CHARGE_BOOST_MAX_TIME ) {
                    Debug.Log( "スピードUP" );
                    _speed = BOOST_SPEED * 2 * Time.deltaTime;
                } else {
                    Debug.Log( "ブースト終了" );
                    _speed = PLAYER_SPEED * Time.deltaTime;
                    _charge_form_change = true;
                }
            }
        }
        if ( _charge_form_change ) {
            _charge_cool_time += Time.deltaTime;
            if ( _charge_cool_time > CHARGE_COOL_TIME ) {
                Debug.Log( "ブースト開始" );
                _charge_boost_time = 0;
                _preparation_time = 0;
                _charge_cool_time = 0;
                _charge_form_change = false;
            }
        }
=======

    

    public GameObject GetPlayerObject( ) {
        return _player;
>>>>>>> 70039e1e2c5d9f4db011f012e3dc7b498c40ffdd
    }

    bool IsButtonShift( ) {
        return Input.GetKey( KeyCode.LeftShift );
    }

    bool IsBoostControl( ) {
        return GetBoostTime( ) > BOOST_TIME;
    }
    float GetBoostTime( ) {
        _boost_time += Time.deltaTime;
        return _boost_time;
    }
    bool IsBoostCoolControl( ) {
        return GetBoostCoolTime( ) > BOOST_COOL_TIME;
    }
    float GetBoostCoolTime( ) {
        _boost_cool_time += Time.deltaTime;
        return _boost_cool_time;
    }
}
