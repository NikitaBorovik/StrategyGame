using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBorder : MonoBehaviour
{
    [SerializeField]
    private Sprite normal;
    [SerializeField]
    private Sprite red;
    [SerializeField]
    private SpriteRenderer renderer;
    
    public void IndcateCanBuild(bool value)
    {
        if (value)
        {
            renderer.sprite = normal;
        }
        else
        {
            renderer.sprite = red;
        }
    }
}
