using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionProcessor : MonoBehaviour {
    int _collision_count;
    void Start()
    {
        
    }
    
    //次にやること。
    //この二つの関数を使い、当たったという情報を行き来させる事。
    //セットの方は、弾に。ゲットの方は、タワーに。
    //行き来させることに成功したら、タワーの体力（今は一旦カウントで）を減らすコードを書く。
    //体力が0になったら、通知を行かせるか、タワーを消して０になったことをアピールする。
    //↑タワーを消した方が良さそうだけどね。
    public void SetBulletCollisionInformation( int count ) {
        _collision_count = count;
    }
    public int GetBulletCollisionInformation( ) {
        return _collision_count;
    }
}
