using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    const int PLAYER_SPEED = 10;   //�v���C���[�{���̑��x
    const int BOOST_SPEED = 50;   //�u�[�X�g���̑��x
    const float BOOST_TIME = 3.5f;     //�R.�T�b�ԃu�[�X�g�^�C��
    const int BOOST_COOL_TIME = 5;//5�b�Ԃ̃N�[���^�C��
    const float JUMP_POWER = 50f; //�W�����v�̍���
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
    
    void Start( ) { //������
        _player = GameObject.Find( "Player" );
        _rb_player = _player.GetComponent< Rigidbody >( );
        _speed = PLAYER_SPEED * Time.deltaTime;
        _boost_time = 0;
        this.transform.Rotate( new Vector3( 0f, 0f, 10f ) );

    }

    void Update( ) {
        if ( _player != null ) {
            SpeedBoost( );
            Move( );
            JumpControl( );
            ChargeBoost( );
        }

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

    }

    void SpeedBoost( ) {
        if ( IsButtonShift( ) ) {
             _boost_change = true;
        } //�u�[�X�g�J�n
        if ( _boost_change ) {
            if ( !IsBoostControl( ) ) { //�R�b�ȉ��Ȃ瑬�x��������
                _speed = BOOST_SPEED * Time.deltaTime;
                if ( !_sense_button ) { //�����{�^����������ĂȂ�����WKey��������Ă锻��
                    _player.transform.Translate( new Vector3( 0, 0, _speed ) );
                }
            } else { //�R�b�������瑬�x��߂��A�u�[�X�g���I��������B�����ăN�[���^�C�����n�܂�
                _speed = PLAYER_SPEED * Time.deltaTime;
                _boost_change = false;
                _boost_form_change = true;
                
           }
        }
        if ( _boost_form_change ) {
            if ( IsBoostCoolControl( ) ){ //�N�[���^�C���̂T�b��������A�u�[�X�g���ēx�\�ɂ���B�����ăN�[���^�C�������ɖ߂�
                _boost_time = 0;
                _boost_cool_time = 0;
                _boost_form_change = false;
            }
        }
    }

    float _charge_boost_time;
    float _charge_cool_time;
    bool _charge_form_change;
    void ChargeBoost( ) {
        //���P����ӏ���
        //�������Ԃ��ő�3.5�b�@���r���ŃL�����Z�����邱�Ƃ��\�ׁ̈A�ő�Ƃ��������������Ă���
        //��ŏ����Ă���悤�ɓr���ŃL�����Z���o����悤�ɂ���
        //��񉟂��������ō쓮����悤�Ȏd�l�����������炻����쐬
        if ( Input.GetKey( KeyCode.W ) && Input.GetKey( KeyCode.LeftControl ) ) {
            _preparation_time += Time.deltaTime;
            if ( _preparation_time > PREPARATION_TIME ) {
                _charge_boost_time += Time.deltaTime ;
                if ( _charge_boost_time < CHARGE_BOOST_MAX_TIME ) {
                    Debug.Log( "�X�s�[�hUP" );
                    _speed = BOOST_SPEED * 2 * Time.deltaTime;
                } else {
                    Debug.Log( "�u�[�X�g�I��" );
                    _speed = PLAYER_SPEED * Time.deltaTime;
                    _charge_form_change = true;
                }
            }
        }
        if ( _charge_form_change ) {
            _charge_cool_time += Time.deltaTime;
            if ( _charge_cool_time > CHARGE_COOL_TIME ) {
                Debug.Log( "�u�[�X�g�J�n" );
                _charge_boost_time = 0;
                _preparation_time = 0;
                _charge_cool_time = 0;
                _charge_form_change = false;
            }
        }

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
