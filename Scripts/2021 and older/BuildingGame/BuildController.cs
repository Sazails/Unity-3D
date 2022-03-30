using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    #region Stuff
    [SerializeField]
    protected GameObject[] buildablesPrefabs;
    protected GameObject selectedBuilding;
    protected int selectedBuildingNum = 0;

    [SerializeField]
    protected Camera cam;

    protected GameObject[] buildableSpotAreas;

    protected float colliderScaleY;
    protected float colliderPositionY;
    protected float spawnObjectScaleY;
    #endregion
    private void Start()
    {
        buildableSpotAreas = GameObject.FindGameObjectsWithTag("BuildableSpot");
        Debug.Log("BuildableAreas: " + buildableSpotAreas.Length);

        foreach (GameObject buildSpot in buildableSpotAreas)
        {
            colliderScaleY = buildSpot.transform.lossyScale.y / 2;
            colliderPositionY = buildSpot.transform.position.y;
            colliderPositionY += colliderScaleY;
        }
    }

    private void Update()
    {
        #region Building Selection
        // Scroll through buildings in array
        var scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel > 0f) // Scroll up
        {
            if (selectedBuildingNum >= buildablesPrefabs.Length - 1)
                selectedBuildingNum = 0;
            else
                selectedBuildingNum++;
        }

        if (scrollWheel < 0f) // Scroll down
        {
            if (selectedBuildingNum <= 0)
                selectedBuildingNum = buildablesPrefabs.Length - 1;
            else
                selectedBuildingNum--;
        }
        #endregion
        #region Building Selection Indicator
        for (int i = 0; i < buildablesPrefabs.Length; i++)
        {
            //Set All buildings inactive
            buildablesPrefabs[i].SetActive(false);
        }
        //Test to ensure that the desired index does not exceed the array length
        if (selectedBuildingNum < buildablesPrefabs.Length)
        {
            //Set the desired building to be active
            buildablesPrefabs[selectedBuildingNum].SetActive(true);
        }

        selectedBuilding = buildablesPrefabs[selectedBuildingNum];
        #endregion
        #region Building Ray
        // Place building ray
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (hit.collider.gameObject.tag == "BuildableSpot")
                {
                    spawnObjectScaleY = selectedBuilding.transform.lossyScale.y / 2;

                    spawnObjectScaleY += colliderPositionY;

                    Instantiate(selectedBuilding, new Vector3(hit.collider.gameObject.transform.position.x, spawnObjectScaleY, hit.collider.gameObject.transform.position.z), Quaternion.identity);
                    Destroy(hit.collider);
                }
            }

        }
        #endregion
    }

}
