using App.World.WorldGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttributeResistance 
{
    [SerializeField]
    public DamageAttribute attribute;

    [Range(0f,1f)]
    [SerializeField]
    public float resistance;
}
