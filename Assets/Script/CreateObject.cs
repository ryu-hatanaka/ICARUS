using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    // Start is called before the first frame update
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
}
