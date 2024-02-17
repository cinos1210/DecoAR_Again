using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARFilteredPlanes : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private List<ARPlane> arPlanes;
    private bool isHorizontal;
    private bool isVertical;
    ARRaycastManager raycastManager;

    public bool IsHorizontal { get => isHorizontal; }
    public bool IsVertical { get => isVertical; }


    private void Start()
    {
        planeManager = FindAnyObjectByType<ARPlaneManager>();
        raycastManager = FindAnyObjectByType<ARRaycastManager>();
    }

    private void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        if (raycastManager.Raycast(ScreenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (var hit in hits)
            {
                TrackableId planeID = hit.trackableId;
                ARPlane planehit = planeManager.GetPlane(planeID);
                

                if (planehit.alignment == PlaneAlignment.HorizontalUp)
                {
                    isHorizontal = true;
                    isVertical = false;
                    Debug.Log("Plane aligment : " + planehit.alignment);
                    
                    
                }
                else if (planehit.alignment == PlaneAlignment.Vertical)
                {
                    isHorizontal = false;
                    isVertical = true;
                    Debug.Log("Plane aligment : " + planehit.alignment);
                }
            }
        }
    }

    // Update is called once per frame



}
