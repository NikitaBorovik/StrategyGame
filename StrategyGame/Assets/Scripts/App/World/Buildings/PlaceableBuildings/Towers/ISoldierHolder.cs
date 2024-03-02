using App.World.Buildings.Towers.TowerSoldiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.PlaceableBuildings.Towers
{
    public interface ISoldierHolder
    {
        void AddSoldier(Soldier soldier);
    }
}

