using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using DG.Tweening;
using System;
using Unity.XR.CoreUtils;

public class UIManager : MonoBehaviour
{
    public event Action ItemsMenuMuebles;
    public event Action ItemsMenuDeco;
    public event Action ItemsMenuElectro;
    

    ServerManager ServerManager;

    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject itemsMenuCanvas;
    [SerializeField] private GameObject ARPositionCanvas;
    [SerializeField] private GameObject LoginMenuCanvas;
    [SerializeField] private GameObject ARcamara;
    [SerializeField] private GameObject LoginCamara;

    
    //[SerializeField] private GameObject RulerToolCanvas;
    private ARPointer pointer;
    void Start()
    {
        //Suscription a los eventos
        GameManager.instance.OnMainMenu += ActivateMainMenuN;
        GameManager.instance.OnItemsMenu += ActivateItemsMenuN;
        GameManager.instance.OnARPosition += ActivateARPosition;
        GameManager.instance.OnLoginMenu += ActivateLoginMenu;
        //GameManager.instance.OnRulerMenu += ActivateARRulerTool;

        loadScreenOFF();

        pointer = FindObjectOfType<ARPointer>();

        ServerManager = FindAnyObjectByType<ServerManager>();

        ARcamara.SetActive(false);
    }

    //Ocultacion de los menus dependiendo del evento llamado
    
    private void ActivateMainMenuN()
    {
        ARcamara.SetActive(true);
        LoginCamara.SetActive(false);
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);

        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(0).transform.DOMoveX(180, 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        LoginMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        //LoginMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    }

    private void ActivateItemsMenuN()
    {
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        itemsMenuCanvas.transform.GetChild(0).transform.DOMoveX(180, 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        LoginMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        //LoginMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

    }

    private void ActivateARPosition()
    {
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        //mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);
        //mainMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        //itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

        // itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        // itemsMenuCanvas.transform.GetChild(0).transform.DOMoveX(180, 0.3f);
        // itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        // itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);

        //RulerToolCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        LoginMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        LoginMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        //LoginMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    }

    private void ActivateLoginMenu()
    {
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(0).transform.DOMoveX(180, 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        LoginMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0.3f);
        LoginMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);
        LoginMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), 0.3f);
        LoginMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1,1,1), 0.3f);
        LoginMenuCanvas.transform.GetChild(4).transform.DOScale(new Vector3(1,1,1), 0.3f);
        LoginMenuCanvas.transform.GetChild(5).transform.DOScale(new Vector3(1,1,1), 0.3f);
        //LoginMenuCanvas.transform.GetChild(6).transform.DOScale(new Vector3(1,1,1), 0.3f);
    }


    //////Funciones para ocultar el boton de posicionamiento//////  
    public void LockPosition()
    {
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    }

    public void UnlockPosition()
    {
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
    //////////////////////////////
    
    //////////////Funciones para GIF de carga del modelo///////////////
    public void loadScreenON()
    {
        ARPositionCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);   
    }

    public void loadScreenOFF()
    {
        ARPositionCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);
    }
    //////////////////////////////
    ///

    public void showItemsMenuMuebles()
    {
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ServerManager.delArticulo();
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        ItemsMenuMuebles?.Invoke();
    }

    public void showItemsMenudeco()
    {
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ServerManager.delArticulo();
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        ItemsMenuDeco?.Invoke();
    }

    public void showItemsMenuElectro()
    {
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ServerManager.delArticulo();
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        ItemsMenuElectro?.Invoke();
    }

    public void showItemMenuSearch()
    {
        itemsMenuCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(0).transform.DOMoveX(180, 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        
    }

    


}
