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
    private float polarAngle = 45.0f;
    private float azimuthalAngle = 45.0f;
    private float minPolarAngle = 5.0f;
    private float maxPolarAngle = 75.0f;

    public float sensitivity = 5.0f;  // マウス感度
    public float minYAngle = -80.0f;  // カメラの最小角度
    public float maxYAngle = 80.0f;   // カメラの最大角度

    private float rotationX = 0.0f;

    Vector3 offset = new Vector3( 0f, 0f, 2f );

    void Start( ) {
        _perspective_change = false;
        this.transform.Rotate( Vector3.zero );
    }

    // Update is called once per frame
    void Update( ) {
        //カメラの視点切り替え
        if ( Input.GetKey( KeyCode.Alpha1 ) ) {
            _perspective_change = !_perspective_change;
        }
        ChangePerspective( );
    }
    //各視点のプログラム
    void ChangePerspective( ) {
        if ( _perspective_change ) {
            FirstPersonPerspective( );
            FirstPersonCameraMove( );
        } else {
            ThirdPersonPerspective( );
            ThirdPersonCameraMove( );
        }
    }
    void ThirdPersonPerspective( ) {
        if ( _player != null ) {
            this.transform.position = _player.transform.position;
        }
    }

    void FirstPersonPerspective( ) {
        this.transform.position = _player.transform.position;
    }
    
    void ThirdPersonCameraMove( ) {
        float mouse_x = Input.GetAxis( "Mouse X" );
        updateAngle( mouse_x, Input.GetAxis( "Mouse Y" ) );
        _player.transform.Rotate( Vector3.up * mouse_x * sensitivity);
        var lookAtPos = _player.transform.position + offset;
        updatePosition(lookAtPos);
        transform.LookAt(lookAtPos);
    }
    //カメラの傾きなど
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
            lookAtPos.x + distance * Mathf.Sin( dp ) * Mathf.Cos( da ),
            lookAtPos.y + distance * Mathf.Cos( dp ),
            lookAtPos.z + distance * Mathf.Sin( dp ) * Mathf.Sin( da ) );
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
    public void SetPlayerObject( GameObject obj ) {
        _player = obj;
    }
}
