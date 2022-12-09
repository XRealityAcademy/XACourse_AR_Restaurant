using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObject : MonoBehaviour
{

    public GameObject gameObjectToInstantiate;

    private GameObject spawnedObject;
    private ARRaycastManager _aRRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update

    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition (out Vector2 touchPosition)
    {
        if(Input.touchCount >0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
