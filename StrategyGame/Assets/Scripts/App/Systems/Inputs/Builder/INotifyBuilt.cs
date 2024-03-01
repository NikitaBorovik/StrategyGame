using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INotifyBuilt 
{
    public void Subscribe(Action action);

    public void Unsubscribe(Action action);
}
