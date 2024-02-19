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
        List<ARRaycastHit> hits = new List<ARRaycastHit>();//lista de resultado del raycast
        Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        if (raycastManager.Raycast(ScreenCenter, hits, TrackableType.PlaneWithinPolygon))// si el raycast golpea un plano
        {
            foreach (var hit in hits)
            {
                TrackableId planeID = hit.trackableId;
                ARPlane planehit = planeManager.GetPlane(planeID);//obtiene el plano del plane manager
                

                if (planehit.alignment == PlaneAlignment.HorizontalUp)//si el alineamiento es horizontal
                {
                    isHorizontal = true;
                    isVertical = false;
                }
                else if (planehit.alignment == PlaneAlignment.Vertical)//si el alineamiento es vertical
                {
                    isHorizontal = false;
                    isVertical = true;
                }
            }
        }
    }

    // Update is called once per frame



}
