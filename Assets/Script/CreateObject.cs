using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class CreateObject : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _machinegun;
    public GameObject _camera;
    void Start( ) {
        CameraControlller cam_player = _camera.GetComponent< CameraControlller >( );
        GameObject machinegun = Instantiate( _machinegun );//‰¼
    }
    
}
