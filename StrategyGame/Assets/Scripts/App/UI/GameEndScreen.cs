using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndScreen : MonoBehaviour
{

    [SerializeField]
    private GameObject toAppear;
    [SerializeField]
    private GameObject pauser;
    
    public void EndGame()
    {
        pauser.SetActive(false);
        toAppear.GetComponent<Animator>().SetBool("isIdle", false);
        toAppear.GetComponent<Animator>().SetBool("isVisible", true);
    }
}
