using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorAnimation : MonoBehaviour
{
    private Animator anim;
    private Charactor player;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Charactor>();
    }
    
    void Update()
    {
        ChooseAnimation(player);
    }
    
    private void ChooseAnimation(Charactor p)
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsMove", false);
        anim.SetBool("IsAttack", false);

        switch (p.State)
        {
            case CharactorState.Idle:
                anim.SetBool("IsIdle", true);
                break;
            case CharactorState.Move:
                anim.SetBool("IsMove", true);
                break;
            case CharactorState.Attack:
                anim.SetBool("IsAttack", true);
                break;
        }
    }
}
