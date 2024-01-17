using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.WorldGrid
{
    public class Cell 
    {
        private int x;
        private int y;
        private Dictionary<CellAttachedAttribute,int> attributes;

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.attributes = new Dictionary<CellAttachedAttribute, int>();
            attributes.Add(CellAttachedAttribute.piercing, 0);
            attributes.Add(CellAttachedAttribute.bludgeoning, 0);
            attributes.Add(CellAttachedAttribute.magic, 0);
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Dictionary<CellAttachedAttribute, int> Attributes { get => attributes;}
    }

    public enum CellAttachedAttribute
    {
        piercing,
        bludgeoning,
        magic,
    }
}

