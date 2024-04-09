using App.World.WorldGrid;
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
        private int g = 10000000;
        private int h = 0;
        private List<AffectingAttributeCounts> attributes;
        private Cell parentCell;

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.attributes = new List<AffectingAttributeCounts>();
            attributes.Add(new AffectingAttributeCounts(DamageAttribute.piercing, 0));
            attributes.Add(new AffectingAttributeCounts(DamageAttribute.bludgeoning, 0));
            attributes.Add(new AffectingAttributeCounts(DamageAttribute.magic, 0));
            attributes.Add(new AffectingAttributeCounts(DamageAttribute.fortified, 0));
        }
        public void ResetValues()
        {
            parentCell = null;
            G = 10000000;
            h = 0;
        }
        public List<AffectingAttributeCounts> Attributes { get => attributes;}
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
                comp = G.CompareTo(other.G);

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

public class AffectingAttributeCounts
{
    public DamageAttribute attribute;
    public int count;

    public AffectingAttributeCounts(DamageAttribute attribute, int count)
    {
        this.attribute = attribute;
        this.count = count;
    }
}
