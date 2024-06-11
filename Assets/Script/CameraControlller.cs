using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
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
    private float maxPolarAngle = 180.0f;//カメラY軸 +方向

    public float sensitivity = 5.0f;  // マウス感度
    public float minYAngle = -90.0f;  // カメラの最小角度
    public float maxYAngle = 90.0f;   // カメラの最大角度

    private float rotationX = 0.0f;


    void Start( ) {
        _perspective_change = false;
        if ( transform.parent != null ) {
            _player = transform.parent.gameObject;
        }
=======
using UnityEngine;

public class CameraControlller : MonoBehaviour
{
    [ SerializeField ] Camera _camera;
    private GameObject _player;
    bool _perspective_change;

    public float mouseSensitivity = 100f; // マウス感度
    float xRotation = 0f;

    Vector3 offset = new Vector3( 0f, 2f, -5f );

    void Start( ) {
        _perspective_change = false;
        PlayerController p_control = GetComponent< PlayerController >( );
        _player = p_control.GetPlayerObject( );
>>>>>>> 70039e1e2c5d9f4db011f012e3dc7b498c40ffdd
    }

    // Update is called once per frame
    void Update( ) {
<<<<<<< HEAD
        //カメラの視点切り替え　デバッグ用
        if ( Input.GetKey( KeyCode.Alpha1 ) ) {
            _perspective_change = !_perspective_change;
        }
        if ( _player != null ) {
            this.transform.position = _player.transform.position;
            ChangePerspective( );
        }
        
    }
    //各視点のプログラム
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
=======
        if ( Input.GetKey( KeyCode.Alpha1 ) ) {
            _perspective_change = !_perspective_change;
        }
        ChangePerspective( );
        MoveTheMouse( );
    }

    void ChangePerspective( ) {
        if ( _perspective_change ) {
            FirstPersonPerspective( );
        } else {
            ThirdPersonPerspective( );
        }
    }
    void ThirdPersonPerspective( ) {
        if ( _player != null ) {
            _camera.transform.position = _player.transform.position + offset;
        }
    }

    void FirstPersonPerspective( ) {
        _camera.transform.position = _player.transform.position;
    }

    void MoveTheMouse( ) {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _player.transform.Rotate( Vector3.up * mouseX );

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 上下の回転角度を制限
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
>>>>>>> 70039e1e2c5d9f4db011f012e3dc7b498c40ffdd
    }
}
