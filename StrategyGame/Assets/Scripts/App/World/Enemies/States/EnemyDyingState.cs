using App;
using App.World.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDyingState : IState
{
    private Enemy parent;
    public EnemyDyingState(Enemy parent)
    {
        this.parent = parent;
    }
    public void Enter()
    {
        SetEnterAnimationParameters();
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }

    private void SetEnterAnimationParameters()
    {
        parent.Animator.SetBool("IsMoving", false);
        parent.Animator.SetBool("IsAttacking", false);
        parent.Animator.SetBool("IsDying", true);
    }
}
