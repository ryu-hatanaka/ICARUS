using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update( ) {
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
    }
}
