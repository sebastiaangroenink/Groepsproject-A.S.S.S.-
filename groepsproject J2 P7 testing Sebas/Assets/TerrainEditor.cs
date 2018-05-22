
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TerrainEditor : MonoBehaviour {

    public FadeToBlack fadeToBlack;

    public bool isEditting = false;
    public bool toggleOverlay = false;
    public bool gumBrush = false;

    public Slider brushSize;

    public GameObject[] brushTypes;
    public GameObject activeBrush;

    public Camera cam;

    public int brushID;

    [HideInInspector]
    public List<GameObject> redTiles = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> greenTiles = new List<GameObject>();

    private void Update()
    {
        if (isEditting && !fadeToBlack.grounded)
            Edit();
        else if (activeBrush != null)
            Destroy(activeBrush);

        if(activeBrush !=null)
        activeBrush.GetComponent<Transform>().localScale = new Vector3(brushSize.value, 2, brushSize.value);


    }

    void Edit()
    {
        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(cam.transform.position, ray.direction*100, Color.red);
            if (Physics.Raycast(ray, out hit))
            {
                if (activeBrush == null)
                {

                    GameObject brush = Instantiate(brushTypes[brushID], hit.transform.position, Quaternion.identity);
                    activeBrush = brush;
                }
                

                if (activeBrush != null)
                    activeBrush.transform.position = hit.point;
                     
                
            }
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Destroy(activeBrush.gameObject);
        }
    }





  public void ToggleEdit()
    {
        isEditting = isEditting ? isEditting = false : isEditting = true;
    }

    public void ToggleTool()
    {
        gumBrush = gumBrush ? gumBrush = false : gumBrush = true;
    }

    public void ToggleOverlay()
    {
        if (toggleOverlay)
        {
            foreach (GameObject gt in greenTiles)           
                gt.SetActive(false);
                
            
            foreach(GameObject rt in redTiles)          
                rt.SetActive(false);

            toggleOverlay = false;
        }
        else if (!toggleOverlay)
        {
            foreach (GameObject gt in greenTiles)
            {
                gt.SetActive(true);
            }

            foreach (GameObject rt in redTiles)
                rt.SetActive(true);

            toggleOverlay = true;
        }
            
    }

    public void UpdateBounds()
    {
        for(int i=0; i<redTiles.Count; i++)
        {
            redTiles[i].GetComponent<SetTileBoundaries>().UpdateBounds();
        }

        for (int i=0; i<greenTiles.Count; i++)
        {
            greenTiles[i].GetComponent<SetTileBoundaries>().UpdateBounds();
        }

    }
}