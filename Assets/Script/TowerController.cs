using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    int _tower_hp = 10;
    ProjectileCollisionProcessor _pcp;

    void Start( ) {
        _pcp = GetComponent<ProjectileCollisionProcessor>( );
    }

    void Update( ) {
        _tower_hp += -_pcp.GetBulletCollisionInformation( );
        if ( _tower_hp < 0 ) {
            Debug.Log( "Á‚¦‚½" );
        }
        //Debug.Log( _tower_hp );
    }
}
