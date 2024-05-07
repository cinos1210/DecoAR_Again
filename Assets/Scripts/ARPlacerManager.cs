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
    private ARPointer pointer;
    

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
            //art3DModel.transform.position = arPointer.transform.position;
            art3DModel.transform.position = pointer.transform.position;
            //art3DModel.transform.parent = arPointer.transform;
            art3DModel.transform.parent = pointer.transform;
            isInitialPosition = true;
            planoDeseado = art3DModel.GetComponent<DataKeeper>().articulo.Type;
        }
        get => art3DModel;
    }
    // Start is called before the first frame update
    void Start()
    {
        arPointer = transform.GetChild(0).gameObject;
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        GameManager.instance.OnMainMenu += setItemPosition;//Suscripcion al evento OnMainMenu
        uiManager = FindObjectOfType<UIManager>();
        Filters = FindObjectOfType<ARFilteredPlanes>();//filtro de planos
        pointer = FindObjectOfType<ARPointer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(planoDeseado);
        //////////////Posicionamiento del modelo/////////////////
        if (isInitialPosition) {
            Vector2 midlePointScreen = new Vector2(Screen.width/2, Screen.height/2);
            arRaycastManager.Raycast(midlePointScreen, Hits, TrackableType.Planes);

            if (Hits.Count>0)
            {
                transform.position = Hits[0].pose.position;
                transform.rotation = Hits[0].pose.rotation;
                //arPointer.SetActive(true);
                //pointer.Pointer.SetActive(true);
                isInitialPosition = false;
            }

        }
        //////////////////////////////////////
        
        //////////////////Movimiento del Modelo////////////////////
        if (Input.touchCount > 0)
        {
            Touch touchUno = Input.GetTouch(0);
            if (touchUno.phase == TouchPhase.Began)//cuando comienza el touch
            {
                var touchPosition = touchUno.position;
                isOverUI = isTapOverUI(touchPosition);
                isOver3DModel = isTapOver3DModel(touchPosition);
                
            }

            if (touchUno.phase == TouchPhase.Moved)//Cuando se mueve
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
            //////////////////////////////////////
            
            ////////////////Rotacion de modelo//////////////////////
            if (Input.touchCount == 2)
            {
                Touch touchDos = Input.GetTouch(1);

                if (touchUno.phase == TouchPhase.Began || touchDos.phase == TouchPhase.Began)//Ambos touch inician
                {
                    initialTouchPos = touchDos.position - touchUno.position;
                }

                if (touchUno.phase == TouchPhase.Moved || touchDos.phase == TouchPhase.Moved)//Ambos touch se mueven
                {
                    Vector2 currentTouchPos = touchDos.position - touchUno.position;//nueva posicion de los touch
                    float angle = Vector2.SignedAngle(initialTouchPos, currentTouchPos);//angulo entre las posiciones de los touch
                    art3DModel.transform.rotation = Quaternion.Euler(0,art3DModel.transform.eulerAngles.y- angle, 0);//aplicacion de la rotacion al modelo 3D
                    initialTouchPos = currentTouchPos;//Actualizacion de la posicion
                }
            }
            /////////////////////////////////////////////////
            
            ///////////////////////Seleccion del modelo/////////////////////////
            if (isOver3DModel && art3DModel == null && !isOverUI)
            {
                
                GameManager.instance.ARPosition();
                art3DModel = ArtSelected;
                ArtSelected = null;
                //arPointer.SetActive(true);
                pointer.Pointer.SetActive(true);
                //transform.position = art3DModel.transform.position;
                pointer.transform.position = art3DModel.transform.position;
                //art3DModel.transform.parent = arPointer.transform;
                art3DModel.transform.parent = pointer.transform;


            }
            /////////////////////////////////////////////////
        }

        if (art3DModel != null)
        {
            //si el plano es horizontal al igual que el plano deseado desbloquea el boton para posicionarlo 
            if ((int)planoDeseado == 0 && Filters.IsHorizontal)
            {
                uiManager.UnlockPosition();
            }
            //si el plano es vertical al igual que el plano deseado desbloquea el boton para posicionarlo 
            else if ((int)planoDeseado == 1 && Filters.IsVertical)//
            {
                uiManager.UnlockPosition();
                //pointer.transform.position = art3DModel.transform.position;
            }
            else//Bloquea el boton para posicionar el modelo
            {
                uiManager.LockPosition();
            }
        }
        
        
    }

    private bool isTapOver3DModel(Vector2 touchPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit3Dmodel))//si se a tocado el modelo
        {
            if (hit3Dmodel.collider.CompareTag("Item"))//si se toco un modelo con el tag "item"
            {
                ArtSelected = hit3Dmodel.transform.gameObject;
                planoDeseado = ArtSelected.GetComponent<DataKeeper>().articulo.Type;
                return true;
                
            }
            
        }
        return false;
    }

    private bool isTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);//Datos del evento de la interfaz
        eventData.position = new Vector2(touchPosition.x, touchPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);//Verifico si hay evento en la posicion del touch

        return results.Count > 0;
    }

    private void setItemPosition()
    {
        if (art3DModel != null)//Comprobacion que si se tenga un modelo asignado 
        {
            art3DModel.transform.parent = null;//parent vuelve a null y se fija el modelo
            //arPointer.SetActive(false);
            //pointer.Pointer.SetActive(false);
            art3DModel = null;//El modelo vuelve a estar vacio
        }
        
    }

    public void Delete()//borra el modelo
    {   
        Destroy(art3DModel);
        //arPointer.SetActive(false);
        //pointer.Pointer.SetActive(false);
        GameManager.instance.MainMenu();
    }

}
