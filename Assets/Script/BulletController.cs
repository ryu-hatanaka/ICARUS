using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    int _hit_count;
    bool _hit;
    float _hit_timer;
    float HIT_COOLDOWN = 1.0f;

    void Start()
   {
        
    }

    // Update is called once per frame
    void Update( ) {
        ProjectileCollisionProcessor pcp = GetComponent<ProjectileCollisionProcessor>( );
        pcp.SetBulletCollisionInformation( _hit_count );
        if ( _hit ) {
            _hit_timer += Time.deltaTime;
            if ( _hit_timer >= HIT_COOLDOWN ) {
                _hit = false;
                _hit_timer = 0.0f;
            }
        }
    }
    void OnCollisionEnter( Collision collision ) {
        if ( collision.gameObject.CompareTag( "Tower" ) ) {
            if ( !_hit ) {
                _hit_count += 1;
                _hit = true;
            } 
        }
    }
}
