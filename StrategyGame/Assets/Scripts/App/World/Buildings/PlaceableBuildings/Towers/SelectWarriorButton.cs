using App.World.Buildings.Towers.TowerSoldiers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace App.World.Buildings.PlaceableBuildings.Towers
{
    public class SelectWarriorButton : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            print(spriteRenderer);
        }
        [SerializeField]
        private Soldier soldier;
        private void OnMouseDown()
        {
            AddSoldierToTower();
        }
        private void OnMouseEnter()
        {
            if (spriteRenderer == null)
            {
                print("No renderer");
                return;
            }    
            spriteRenderer.color = new Color(245f / 255f, 245f / 255f, 245f / 255f, 1f);
            print(spriteRenderer.color);
        }
        private void OnMouseExit()
        {
            if (spriteRenderer == null)
                return;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        private void OnMouseDrag()
        {
            if (spriteRenderer == null)
                return;
            spriteRenderer.color = new Color(220f / 255f, 220f / 255f, 220f / 255f, 1f);
        }
        private void OnMouseUp()
        {
            if (spriteRenderer == null)
                return;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        private void AddSoldierToTower()
        {

            Tower parentTower = GetComponentInParent<Tower>();
            if(parentTower.SoldierPlaces.Count <= parentTower.SoldiersNumber || parentTower.PlayerMoney.Money < parentTower.SoldierPrice)
            {
                parentTower.AudioSource.PlayOneShot(parentTower.AudioSource.clip);
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
