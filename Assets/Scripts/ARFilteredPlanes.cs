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
    private ARPlacerManager placerManager;

    public bool IsHorizontal { get => isHorizontal; }
    public bool IsVertical { get => isVertical; }


    private void Start()
    {
        planeManager = FindAnyObjectByType<ARPlaneManager>();
        raycastManager = FindAnyObjectByType<ARRaycastManager>();
        placerManager = FindAnyObjectByType<ARPlacerManager>();
    }

    private void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();//lista de resultado del raycast
        Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        //Vector2 artPosicion = new Vector2(placerManager.Art3DModel.transform.position.x, placerManager.Art3DModel.transform.position.z);
        if (raycastManager.Raycast(ScreenCenter, hits, TrackableType.PlaneWithinPolygon))// si el raycast golpea un plano
        {
            foreach (var hit in hits)
            {
                TrackableId planeID = hit.trackableId;
                ARPlane planehit = planeManager.GetPlane(planeID);//obtiene el plano del plane manager
                

                if (planehit.alignment == PlaneAlignment.HorizontalUp)//si el alineamiento es horizontal
                {
                    Debug.Log("filterd aligment:" + planehit.alignment);
                    isHorizontal = true;
                    isVertical = false;
                }
                else if (planehit.alignment == PlaneAlignment.Vertical)//si el alineamiento es vertical
                {
                    Debug.Log("filterd aligment:" + planehit.alignment);
                    isHorizontal = false;
                    isVertical = true;
                }
            }
        }
    }

    // Update is called once per frame



}
