using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionProcessor : MonoBehaviour {
    int _collision_count;
    void Start()
    {
        
    }
    
    //���ɂ�邱�ƁB
    //���̓�̊֐����g���A���������Ƃ��������s���������鎖�B
    //�Z�b�g�̕��́A�e�ɁB�Q�b�g�̕��́A�^���[�ɁB
    //�s���������邱�Ƃɐ���������A�^���[�̗̑́i���͈�U�J�E���g�Łj�����炷�R�[�h�������B
    //�̗͂�0�ɂȂ�����A�ʒm���s�����邩�A�^���[�������ĂO�ɂȂ������Ƃ��A�s�[������B
    //���^���[�������������ǂ����������ǂˁB
    public void SetBulletCollisionInformation( int count ) {
        _collision_count = count;
    }
    public int GetBulletCollisionInformation( ) {
        return _collision_count;
    }
}
