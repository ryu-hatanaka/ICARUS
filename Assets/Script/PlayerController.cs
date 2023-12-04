using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int PLAYER_SPEED = 2;   //�v���C���[�{���̑��x
    const int BOOST_SPEED = 10;   //�u�[�X�g���̑��x
    const float BOOST_TIME = 3.5f;     //�R.�T�b�ԃu�[�X�g�^�C��
    const int BOOST_COOL_TIME = 5;//5�b�Ԃ̃N�[���^�C��

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
    void Start( ) { //������
        _rb_player = GetComponent< Rigidbody >( );
        _speed = PLAYER_SPEED * Time.deltaTime;
        _boost_time = 0;

    }

    void Update( ) {
        if ( _player != null ) {
            SpeedBoost( );
            Move( );
            JumpControl( );
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
        if ( Input.GetButtonDown( "Jump" ) ) {
            Jump( );
        }
    }
    
    void Jump( ) {
        _rb_player.AddForce( Vector3.up * 5f, ForceMode.Impulse );
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

    

    public GameObject GetPlayerObject( ) {
        return _player;
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
