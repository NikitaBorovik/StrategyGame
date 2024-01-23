using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.WorldGrid
{
    public class Cell 
    {
        private int x;
        private int y;
        private Dictionary<DamageAttribute,int> attributes;

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.attributes = new Dictionary<DamageAttribute, int>();
            attributes.Add(DamageAttribute.piercing, 0);
            attributes.Add(DamageAttribute.bludgeoning, 0);
            attributes.Add(DamageAttribute.magic, 0);
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Dictionary<DamageAttribute, int> Attributes { get => attributes;}
    }

    public enum DamageAttribute
    {
        piercing,
        bludgeoning,
        magic,
        fortified,
    }
}

