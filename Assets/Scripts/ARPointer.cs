using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPointer : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject pointer;
    private List<ARRaycastHit> hits = new List<ARRaycastHit> ();

    public GameObject Pointer { get => pointer; }
    // Start is called before the first frame update
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        pointer = transform.GetChild(0).gameObject;
        //pointer.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        
        var ray = new Vector2 (Screen.width/2, Screen.height/2);
        if (raycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;

            transform.position = hitPose.position;
            transform.rotation = hitPose.rotation;
        }
    }

}
