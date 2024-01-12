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
    private SpriteRenderer cellRenderer;
    
    public void IndcateCanBuild(bool value)
    {
        if (value)
        {
            cellRenderer.sprite = normal;
        }
        else
        {
            cellRenderer.sprite = red;
        }
    }
}
