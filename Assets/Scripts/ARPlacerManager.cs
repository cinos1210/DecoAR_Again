using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



public class ARPlacerManager : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> Hits = new List<ARRaycastHit>();
    private PlaneType planoDeseado;

    private UIManager uiManager;
    private ARFilteredPlanes Filters;

    [SerializeField] private Image ScanImg;
    [SerializeField] private Color ScanImgtrue;
    [SerializeField] private Color ScanImgFalse;

    


    private GameObject arPointer;
    private GameObject art3DModel;
    private GameObject ArtSelected;
    private bool isInitialPosition;
    private bool isOverUI;
    private Vector2 initialTouchPos;
    private bool isOver3DModel;

    public PlaneType PlanoDeseado { set => planoDeseado = value; }
    public GameObject Art3DModel
    {
        set
        {
            art3DModel = value;
            art3DModel.transform.position = arPointer.transform.position;
            art3DModel.transform.parent = arPointer.transform;
            isInitialPosition = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        arPointer = transform.GetChild(0).gameObject;
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        GameManager.instance.OnMainMenu += setItemPosition;
        uiManager = FindObjectOfType<UIManager>();
        Filters = FindObjectOfType<ARFilteredPlanes>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(planoDeseado);
        if (isInitialPosition) {
            Vector2 midlePointScreen = new Vector2(Screen.width/2, Screen.height/2);
            arRaycastManager.Raycast(midlePointScreen, Hits, TrackableType.Planes);

            if (Hits.Count>0)
            {
                transform.position = Hits[0].pose.position;
                transform.rotation = Hits[0].pose.rotation;
                arPointer.SetActive(true);
                isInitialPosition = false;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touchUno = Input.GetTouch(0);
            if (touchUno.phase == TouchPhase.Began)
            {
                var touchPosition = touchUno.position;
                isOverUI = isTapOverUI(touchPosition);
                isOver3DModel = isTapOver3DModel(touchPosition);
                
            }

            if (touchUno.phase == TouchPhase.Moved)
            {
                if (arRaycastManager.Raycast(touchUno.position, Hits,TrackableType.Planes))
                {
                    Pose hitPose = Hits[0].pose;
                    if (!isOverUI && isOver3DModel)
                    {
                        transform.position = hitPose.position;
                    }
                }
            }

            if (Input.touchCount == 2)
            {
                Touch touchDos = Input.GetTouch(1);

                if (touchUno.phase == TouchPhase.Began || touchDos.phase == TouchPhase.Began)
                {
                    initialTouchPos = touchDos.position - touchUno.position;
                }

                if (touchUno.phase == TouchPhase.Moved || touchDos.phase == TouchPhase.Moved)
                {
                    Vector2 currentTouchPos = touchDos.position - touchUno.position;
                    float angle = Vector2.SignedAngle(initialTouchPos, currentTouchPos);
                    art3DModel.transform.rotation = Quaternion.Euler(0,art3DModel.transform.eulerAngles.y- angle, 0);
                    initialTouchPos = currentTouchPos;
                }
            }

            if (isOver3DModel && art3DModel == null && !isOverUI)
            {
                
                GameManager.instance.ARPosition();
                art3DModel = ArtSelected;
                ArtSelected = null;
                arPointer.SetActive(true);
                transform.position = art3DModel.transform.position;
                art3DModel.transform.parent = arPointer.transform;
                
            }
        }
        Debug.Log("Plano Deseado:" + planoDeseado);

        if ((int)planoDeseado == 0 && Filters.IsHorizontal)
        {
            uiManager.UnlockPosition();
            Debug.Log("Plano Deseado:" + planoDeseado);
        }
        else
        {
            uiManager.LockPosition();
            Debug.Log("Plano Deseado:" + planoDeseado);

        }

        if ((int)planoDeseado == 1 && Filters.IsVertical)
        {
            uiManager.UnlockPosition();
        }
        else
        {
            uiManager.LockPosition();
        }

    }

    private bool isTapOver3DModel(Vector2 touchPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit3Dmodel))
        {
            if (hit3Dmodel.collider.CompareTag("Item"))
            {
                ArtSelected = hit3Dmodel.transform.gameObject;
                return true;
                
            }
            
        }
        return false;
    }

    private bool isTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touchPosition.x, touchPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }

    private void setItemPosition()
    {
        if (art3DModel != null)
        {
            art3DModel.transform.parent = null;
            arPointer.SetActive(false);
            art3DModel = null;
        }
        
    }

    public void Delete()
    {
        Destroy(art3DModel);
        arPointer.SetActive(false);
        GameManager.instance.MainMenu();
    }

}
