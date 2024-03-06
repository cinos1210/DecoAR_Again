using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class ItembuttonManager : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private PlaneType planeType;
    private category category;
    private Sprite itemImage;
    private GameObject item3DObject;
    private ARPlacerManager interactionManager;
    private bool downloadScreen = false;
    private UIManager uiManager;
    private ARPointer pointer;

    //Datos URL
    private string urlBundleModel;
    private RawImage urlImgModel;

    //setters and getters
    public string ItemName { set => itemName = value; }
    public string ItemDescription { set => itemDescription= value; }
    public PlaneType PlaneType { set => planeType = value; }
    public category Category { set => category = value; }
    public Sprite ItemImage { set => itemImage = value; }
    public GameObject Item3DObject {  set => item3DObject = value; }
    public string URLBundleModel { set => urlBundleModel = value; }
    public RawImage URLImgModel { get => urlImgModel; set => urlImgModel = value; }

    public bool DownloadScreen { get => downloadScreen; }


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

    }

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
