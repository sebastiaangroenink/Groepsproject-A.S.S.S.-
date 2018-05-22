using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushTool : MonoBehaviour
{

    private bool isGumTool;

    private TerrainEditor terrainEditor;

    private void Awake()
    {
        terrainEditor = GameObject.Find("TerrainEditManager").GetComponent<TerrainEditor>();

        if (terrainEditor.gumBrush)
            isGumTool = true;

        if (!terrainEditor.gumBrush)
            isGumTool = false;
    }
    private void Update()
    {
        if (terrainEditor.gumBrush)
            isGumTool = true;

        if (!terrainEditor.gumBrush)
            isGumTool = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGumTool)
        {
            transform.GetComponent<Renderer>().material.color = new Color(0,1,0,0.25f);

            if (other.tag == "TerrainCell")
            {
                other.GetComponent<Renderer>().material.color = new Color(0,1,0,0.25f);

                if (terrainEditor.redTiles.Contains(other.gameObject))
                {
                    terrainEditor.redTiles.Remove(other.gameObject);
                    terrainEditor.greenTiles.Add(other.gameObject);
                }
            }
        }
        else if (isGumTool)
        {
            transform.GetComponent<Renderer>().material.color = new Color(1,0,0,0.25f);

            if (other.tag == "TerrainCell")
            {
                other.GetComponent<Renderer>().material.color = new Color(1,0,0,0.25f);

                if (terrainEditor.greenTiles.Contains(other.gameObject))
                {
                    terrainEditor.greenTiles.Remove(other.gameObject);
                    terrainEditor.redTiles.Add(other.gameObject);
                }
            }
        }
    }
}
