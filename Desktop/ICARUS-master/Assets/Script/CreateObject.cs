using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class CreateObject : MonoBehaviour
{
    // Start is called before the first frame update
    [ SerializeField ] GameObject _player;
    [ SerializeField ] GameObject _ground;
    [ SerializeField ] GameObject _machinegun;
    [ SerializeField ] GameObject _bullet;
    public GameObject _camera;
    void Start( ) {
        GameObject player = Instantiate( _player );
        GameObject ground = Instantiate( _ground );
        PlayerController p_contrl = player.GetComponent< PlayerController >( );
        CameraControlller cam_player = _camera.GetComponent< CameraControlller >( );
        ShootingSystem s_system = _bullet.GetComponent< ShootingSystem >( );
        GameObject machinegun = Instantiate( _machinegun );//‰¼
        p_contrl.SetPlayerObject( player );
        s_system.SetPlyaerObject( player );
        cam_player.SetPlayerObject( player );
    }
    
}
