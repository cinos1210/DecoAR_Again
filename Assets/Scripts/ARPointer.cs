using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPointer : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject pointer;
    private List<ARRaycastHit> hits = new List<ARRaycastHit> ();
    private ARFilteredPlanes filteredPlanes;

    private ARPlacerManager placerManager;
    

    public GameObject Pointer { get => pointer; }
    // Start is called before the first frame update
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        pointer = transform.GetChild(0).gameObject;
        filteredPlanes = FindObjectOfType<ARFilteredPlanes>();
        placerManager = FindAnyObjectByType<ARPlacerManager>();
        //pointer.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        
        var ray = new Vector2 (Screen.width/2, Screen.height/2);
        if (raycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;

            if (filteredPlanes.IsHorizontal)
            {
                transform.position = hitPose.position;
                transform.rotation = hitPose.rotation;
            }else if (filteredPlanes.IsVertical && placerManager.Art3DModel == null)
            {
                transform.position = hitPose.position;
                transform.rotation = hitPose.rotation;
                Quaternion currentRotation = transform.rotation;
        
                transform.rotation = currentRotation; 
            }

            // transform.position = hitPose.position;
            // transform.rotation = hitPose.rotation;
        }
    }

}
