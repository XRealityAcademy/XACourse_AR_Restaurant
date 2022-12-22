using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlaceObjectOnPlace : MonoBehaviour
{

    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool canPlace;

    public GameObject drinkPrefab;
    public Camera aRCamera;
    public float minDistance;

    private GameObject currentlyPlaced;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();

        float dist = Vector3.Distance(placementPose.position, aRCamera.transform.position);

        if(canPlace && dist > minDistance)
        {
            PlaceObject();
        }
    }

    private void UpdatePlacementPose()
    {
        //The center of the screen
        var screenCenter = aRCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.AllTypes);

        canPlace = hits.Count > 0;

        if (!canPlace) return;
        
        //The placementPosition = the first ray hit the plane
        placementPose = hits[0].pose;
        var cameraForward = aRCamera.transform.forward;
        var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

        placementPose.rotation = Quaternion.LookRotation(cameraBearing);
    }

    private void PlaceObject()
    {
        if (currentlyPlaced != null) return;
        // TODO: Find selected drink
        currentlyPlaced = Instantiate(drinkPrefab, placementPose.position, placementPose.rotation);
    }

    public void UpdateModel(GameObject model)
    {
        Destroy(currentlyPlaced);
        currentlyPlaced = null;
        drinkPrefab = model;
        PlaceObject();
    }
}
