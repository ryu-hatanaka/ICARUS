using System.Collections;
using System.Collections.Generic;

using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraControlller : MonoBehaviour
{
    private GameObject _player;
    bool _perspective_change;
    private float distance = 4.0f;
    private float polarAngle = 90.0f;
    private float azimuthalAngle = 90.0f;
    private float minPolarAngle = 5.0f;
    private float maxPolarAngle = 180.0f;//�J����Y�� +����

    public float sensitivity = 5.0f;  // �}�E�X���x
    public float minYAngle = -90.0f;  // �J�����̍ŏ��p�x
    public float maxYAngle = 90.0f;   // �J�����̍ő�p�x

    private float rotationX = 0.0f;


    void Start( ) {
        _perspective_change = false;
        if ( transform.parent != null ) {
            _player = transform.parent.gameObject;
        }

    }

    // Update is called once per frame
    void Update( ) {

        //�J�����̎��_�؂�ւ��@�f�o�b�O�p
        if ( Input.GetKey( KeyCode.Alpha1 ) ) {
            _perspective_change = !_perspective_change;
        }
        if ( _player != null ) {
            this.transform.position = _player.transform.position;
            ChangePerspective( );
        }
        
    }
    //�e���_�̃v���O����
    void ChangePerspective( ) {
        if ( _perspective_change ) {
            FirstPersonCameraMove( );
        } else {
            ThirdPersonCameraMove( );
        }
    }
    
    void ThirdPersonCameraMove( ) {
        float mouse_x = Input.GetAxis( "Mouse X" );
        updateAngle( mouse_x, Input.GetAxis( "Mouse Y" ) );
        _player.transform.Rotate( Vector3.up * mouse_x * sensitivity );
        //var lookAtPos = _player.transform.position + offset;
        var lookAtPos = _player.transform.position;
        updatePosition( lookAtPos );
        transform.LookAt( lookAtPos );
    }
    //�J�����̌X���Ȃ�
    void updateAngle(float x, float y)
    {
        x = azimuthalAngle - x * sensitivity;
        azimuthalAngle = Mathf.Repeat( x, 360 );

        y = polarAngle + y * sensitivity;
        polarAngle = Mathf.Clamp( y, minPolarAngle, maxPolarAngle );
    }
    void updatePosition( Vector3 lookAtPos ) {
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + -distance * Mathf.Sin( dp ) * Mathf.Cos( da ),
            lookAtPos.y + distance * Mathf.Cos( dp ),
            lookAtPos.z + -distance * Mathf.Sin( dp ) * Mathf.Sin( da ) );
    }
    void FirstPersonCameraMove( ) {
        float mouseX = Input.GetAxis( "Mouse X" );
        float mouseY = Input.GetAxis( "Mouse Y" );
        _player.transform.Rotate( Vector3.up * mouseX * sensitivity );
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle );
        transform.rotation = Quaternion.Euler( rotationX, _player.transform.eulerAngles.y, 0.0f );
        transform.position = _player.transform.position;

    }
}
