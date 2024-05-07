using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class ItembuttonManager : MonoBehaviour
{
    private string idItem;
    private string itemName;
    private string itemDescription;
    private PlaneType planeType;
    private category category;

    private styles estiloSsustancia;
    private Sprite itemImage;
    private GameObject item3DObject;
    private string articuloUrl;
    private ARPlacerManager interactionManager;
    private bool downloadScreen = false;
    private UIManager uiManager;
    private ARPointer pointer;

    //Datos URL
    private string urlBundleModel;
    private RawImage urlImgModel;

    //setters and getters
    public string IdItem {set => idItem = value; }  
    public string ItemName { set => itemName = value; }
    public string ItemDescription { set => itemDescription= value; }
    public PlaneType PlaneType { set => planeType = value; }
    public category Category { set => category = value; }
    public styles Estilo{ set => estiloSsustancia = value; }
    public Sprite ItemImage { set => itemImage = value; }
    public GameObject Item3DObject {  set => item3DObject = value; }
    public string URLBundleModel { set => urlBundleModel = value; }
    public RawImage URLImgModel { get => urlImgModel; set => urlImgModel = value; }

    public string URLArticulo { set => articuloUrl = value; }

    public bool DownloadScreen { get => downloadScreen; }

    string cat;
    string sty;

    // Start is called before the first frame update
    void Start()
    {
        pointer = FindAnyObjectByType<ARPointer>();
        uiManager = FindAnyObjectByType<UIManager>();
        //asignacion de valores para intanciarse un boton
        transform.GetChild(0).GetComponent<TMP_Text>().text = itemName;
        urlImgModel = transform.GetChild(1).GetComponent<RawImage>();
        transform.GetChild(2).GetComponent<TMP_Text>().text = itemDescription;

        var button = GetComponent<Button>();

        //Evento
        button.onClick.AddListener(GameManager.instance.ARPosition);
        button.onClick.AddListener(Place3DModel);

        interactionManager = FindObjectOfType<ARPlacerManager>();
        
    }

    private void Place3DModel()
    {
        pointer.Pointer.SetActive(true);
        uiManager.loadScreenON();
        StartCoroutine(DownloadAssetBundle(urlBundleModel));
        //interactionManager.PlanoDeseado = planeType;
        Debug.Log("planodeseado cambiado");

        if (category == 0) cat = "Electrodomesticos";
        if ((int)category == 1) cat = "muebles";
        if ((int)category == 2) cat = "Decorativos";

        if ((int)estiloSsustancia == 0) sty = "cocina";
        if ((int)estiloSsustancia == 1) sty = "sala";
        if ((int)estiloSsustancia == 2) sty = "jardin";
        if ((int)estiloSsustancia == 3) sty = "oficina";
        if ((int)estiloSsustancia == 4) sty = "Habitacion";

        Debug.Log(itemName + " " + itemDescription + " " + cat + " " + sty);

    }

    // private string Knnsearch()
    // {
        
        
        
    // }

    //Corutina de descarga de Asset bundle
    IEnumerator DownloadAssetBundle(string URL_AB)
    {
        UnityWebRequest serverRequest = UnityWebRequestAssetBundle.GetAssetBundle(URL_AB);
        yield return serverRequest.SendWebRequest();

        if (serverRequest.result == UnityWebRequest.Result.Success)
        {
            AssetBundle model3D = DownloadHandlerAssetBundle.GetContent(serverRequest);


            if (model3D != null)
            {
                interactionManager.Art3DModel = Instantiate(model3D.LoadAsset(model3D.GetAllAssetNames()[0]) as GameObject);
                model3D.Unload(false);
            }
            else
            {
                Debug.Log("AB no valido...");
            }

            uiManager.loadScreenOFF();
        }
        else
        {
            Debug.Log("Error en la corrutina de descarga...");
        }
    }
}
