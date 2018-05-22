using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerTwo : MonoBehaviour {

    public Vector2 mapSize;

    public Transform tile;

    public RaycastHit hit;

    public TerrainEditor terrainEditor;

    [Range(0, 1)]
    public float tileSpacing;

    void Start()
    {
        terrainEditor = GameObject.Find("TerrainEditManager").GetComponent<TerrainEditor>();

        GenerateMap();

    }

    void GenerateMap() // genereert door speler gegeven aantal tiles op 0,0,0 (worldspace) met een spacing ertussen.
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                
                Vector3 tilePos = new Vector3(-mapSize.x + x*2,hit.point.y , -mapSize.y+ y*2);
                if (Physics.Raycast(new Vector3(tilePos.x, 100, tilePos.z), transform.TransformDirection(-Vector3.up), out hit, 200) && hit.transform.tag == "Floor")
                {
                    tilePos.y = hit.point.y+0.1f;

                    Transform newTile = Instantiate(tile, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
                    newTile.localScale = Vector3.one * (2 - tileSpacing);
                    newTile.GetComponent<Renderer>().material.color = new Color(1,0,0,0.25f);
                    newTile.gameObject.SetActive(false);
                    terrainEditor.redTiles.Add(newTile.gameObject);
                }              
                tilePos.y = 0;
            }
        }
    }
}
