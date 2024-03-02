using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class StateMachine
    {
        public IState State { get; private set; }


        public void Init(IState startingState)
        {
            State = startingState;
            startingState.Enter();
        }

        public void ChangeState(IState newState)
        {
            State.Exit();

            State = newState;
            State.Enter();
        }
    }
}

