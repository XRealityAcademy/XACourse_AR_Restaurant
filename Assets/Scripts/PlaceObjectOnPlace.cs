using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlaceObjectOnPlace : MonoBehaviour
{

    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid;

    public GameObject positionIndicator;
    public GameObject prefabtoPlace;
    public Camera aRCamera;


    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();

        if(placementPoseIsValid && Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began)
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

        placementPoseIsValid = hits.Count > 0;

        if(placementPoseIsValid)
        {
            //The placementPosition = the first ray hit the plane
            placementPose = hits[0].pose;
            var cameraForward = aRCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            positionIndicator.SetActive(true);
            positionIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            positionIndicator.SetActive(false);
        }

    }

    private void PlaceObject()
    {
        Instantiate(prefabtoPlace, placementPose.position, placementPose.rotation);
    }



}
