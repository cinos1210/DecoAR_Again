using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class TapeManager : MonoBehaviour
{
    ARPointer pointer;
    ARRaycastManager raycastManager;
    public GameObject[] tapePoints;
    public GameObject tapePrefab;
    public TMP_Text display_text;
    public float distanceBetweenPoints = 0f;
    public LineRenderer line;

    int currentTapePoint = 0;
    bool placementEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        pointer = GetComponent<ARPointer>();
        raycastManager = FindObjectOfType<ARRaycastManager>();
        //pointer.Pointer.SetActive(true);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> Hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.height/2,Screen.width/2), Hits, TrackableType.PlaneWithinPolygon);
        

        if (Hits.Count < 0)
        {
            Debug.Log("hitcount:" + Hits.Count);
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (currentTapePoint < 2)
                {
                    placePoint(Hits[0].pose.position, currentTapePoint);
                }
                placementEnabled = false;
            }
        }
        else if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            placementEnabled = true;
        }

        if (currentTapePoint > 1)
        {
            line.enabled = true;
            line.SetPosition(0, tapePoints[0].transform.position);
            line.SetPosition(0, tapePoints[1].transform.position);
        }

        distanceBetweenPoints = Vector3.Distance(tapePoints[0].transform.position, tapePoints[1].transform.position);

        display_text.text = distanceBetweenPoints.ToString();
    }

    void AddTapePoint(GameObject newPoint)
    {
        GameObject[] newTapePoints = new GameObject[tapePoints.Length + 1];

        for (int i = 0; i < tapePoints.Length; i++)
        {
            newTapePoints[i] = tapePoints[i];
        }

        newTapePoints[newTapePoints.Length - 1] = newPoint;
        tapePoints = newTapePoints;
    }

    public void placePoint(Vector3 pointPosition, int pointIndex)
    {
        tapePoints[pointIndex].SetActive(true);
        tapePoints[pointIndex].transform.position = pointPosition;

        currentTapePoint = currentTapePoint + 1;
        Debug.Log("placePoint");
    }
}
