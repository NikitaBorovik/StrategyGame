using App.World.Buildings.Towers.TowerSoldiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.PlaceableBuildings.Towers
{
    public class SelectWarriorButton : MonoBehaviour
    {
        [SerializeField]
        private Soldier soldier;
        private void OnMouseDown()
        {
            AddSoldierToTower();
        }
        private void AddSoldierToTower()
        {

            Tower parentTower = GetComponentInParent<Tower>();
            if(parentTower.SoldierPlaces.Count <= parentTower.SoldiersNumber)
            {
                return;
            }
            if (parentTower.MaxSoldiersNumber > parentTower.SoldiersNumber)
            {
                GameObject instantiatedSoldier = GameObject.Instantiate(soldier.gameObject);
                instantiatedSoldier.transform.parent = parentTower.gameObject.transform;
                instantiatedSoldier.transform.position = parentTower.SoldierPlaces[parentTower.SoldiersNumber].position;
                instantiatedSoldier.GetComponent<Soldier>().Init(parentTower.CurrentTarget);
                parentTower.AddSoldier(instantiatedSoldier.GetComponent<Soldier>());
                parentTower.SoldiersNumber++;
            }
            
        }
    }

}
