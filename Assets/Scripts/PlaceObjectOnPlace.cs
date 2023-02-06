using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlaceObjectOnPlace : MonoBehaviour
{
    [SerializeField] private Button scanningCompleteButton;
    private ARRaycastManager raycastManager;
    private Pose placementPose;

    private GameObject drinkPrefab;
    public Camera aRCamera;

    private GameObject currentlyPlaced;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        scanningCompleteButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
    }

    private void UpdatePlacementPose()
    {
        print("Update");
        var screenCenter = aRCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.AllTypes);
        
        if (hits.Count <= 0) return;
        if(scanningCompleteButton != null)
            scanningCompleteButton.gameObject.SetActive(true);
        
        print(hits.Count);
        placementPose = hits[0].pose;
        var cameraForward = aRCamera.transform.forward;
        var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

        placementPose.rotation = Quaternion.LookRotation(cameraBearing);
    }

    private void PlaceObject()
    {
        if (currentlyPlaced != null || drinkPrefab == null) return;
        currentlyPlaced = Instantiate(drinkPrefab, placementPose.position, placementPose.rotation);
        currentlyPlaced.transform.localScale = drinkPrefab.transform.localScale;
        print(currentlyPlaced.gameObject.name);
        print(currentlyPlaced.transform.position);
    }

    public void UpdateModel(GameObject model)
    {
        Destroy(currentlyPlaced);
        currentlyPlaced = null;
        drinkPrefab = model;
        PlaceObject();
    }
}
