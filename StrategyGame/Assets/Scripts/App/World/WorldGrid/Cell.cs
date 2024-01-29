using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.WorldGrid
{
    public class Cell : IComparable<Cell>
    {
        private int x;
        private int y;
        private int g = int.MaxValue;
        private int h = 0;
        private Dictionary<DamageAttribute,int> attributes;
        private Cell parentCell;

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.attributes = new Dictionary<DamageAttribute, int>();
            attributes.Add(DamageAttribute.piercing, 0);
            attributes.Add(DamageAttribute.bludgeoning, 0);
            attributes.Add(DamageAttribute.magic, 0);
        }
        public Dictionary<DamageAttribute, int> Attributes { get => attributes;}
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int G { get => g; set => g = value; }
        public int H { get => h; set => h = value; }
        public int F { get => h + g;}
        public Cell ParentCell { get => parentCell; set => parentCell = value; }

        public int CompareTo(Cell other)
        {
            int comp = F.CompareTo(other.F);
            if (comp == 0)
                comp = H.CompareTo(other.H);

            return comp;
        }
    }

    public enum DamageAttribute
    {
        piercing,
        bludgeoning,
        magic,
        fortified,
    }
}

