using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using UnityChan;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
=======
using UnityEngine;
>>>>>>> 70039e1e2c5d9f4db011f012e3dc7b498c40ffdd

public class CreateObject : MonoBehaviour
{
    // Start is called before the first frame update
<<<<<<< HEAD
    private GameObject _machinegun;
    public GameObject _camera;
    void Start( ) {
        CameraControlller cam_player = _camera.GetComponent< CameraControlller >( );
        GameObject machinegun = Instantiate( _machinegun );//‰¼
    }
    
=======
    [ SerializeField ] GameObject _player;
    [ SerializeField ] GameObject _ground;
    void Start( ) {
        GameObject player = Instantiate( _player );
        GameObject ground = Instantiate( _ground );
        PlayerController p_contrl = GetComponent< PlayerController >( );

        p_contrl.SetPlayerObject( player );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
>>>>>>> 70039e1e2c5d9f4db011f012e3dc7b498c40ffdd
}
