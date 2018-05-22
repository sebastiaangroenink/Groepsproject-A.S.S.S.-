using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileBoundaries : MonoBehaviour
{

    public TerrainEditor terrainEditor;

    public RaycastHit hit;

    public bool one, two, three, four;

    private void Awake()
    {
        terrainEditor = GameObject.Find("TerrainEditManager").GetComponent<TerrainEditor>();

        UpdateBounds();
    }
    public void UpdateBounds()
    {

        {
            if (terrainEditor.redTiles.Contains(transform.gameObject))
            {

                if (Physics.Raycast(transform.position, Vector3.forward * 2, out hit))
                {
                    if (terrainEditor.greenTiles.Contains(hit.transform.gameObject))
                    {
                        transform.GetComponent<Renderer>().material.color = new Color(1.0F, 0.92f, 0.016f, 0.5f);
                    }
                    else
                    {
                        one = true;
                    }
                }
                if (Physics.Raycast(transform.position, Vector3.back * 2, out hit))
                {
                    if (terrainEditor.greenTiles.Contains(hit.transform.gameObject))
                    {
                        transform.GetComponent<Renderer>().material.color = new Color(1.0F, 0.92f, 0.016f, 0.5f);
                    }
                    else
                    {
                        two = true;
                    }
                }
                if (Physics.Raycast(transform.position, Vector3.left * 2, out hit))
                {
                    if (terrainEditor.greenTiles.Contains(hit.transform.gameObject))
                    {
                        transform.GetComponent<Renderer>().material.color = new Color(1.0F, 0.92f, 0.016f, 0.5f);
                    }
                    else
                    {
                        three = true;
                    }
                }
                if (Physics.Raycast(transform.position, Vector3.right * 2, out hit))
                {
                    if (terrainEditor.greenTiles.Contains(hit.transform.gameObject))
                    {
                        transform.GetComponent<Renderer>().material.color = new Color(1.0F,0.92f,0.016f,0.5f);
                    }
                    else
                    {
                        four = true;
                    }
                }

                if (one && two && three && four)
                {
                    transform.GetComponent<Renderer>().material.color = new Color(1,0,0,0.25f);
                    one = false;
                    two = false;
                    three = false;
                    four = false;
                }
            }
        }
    }
}
