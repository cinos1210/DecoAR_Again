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
    private Sprite itemImage;
    private GameObject item3DObject;
    private ARPlacerManager interactionManager;
    private string urlBundleModel;
    private RawImage urlImgModel;

    public string ItemName { set => itemName = value; }
        
    public string ItemDescription { set => itemDescription= value; }
    public Sprite ItemImage { set => itemImage = value; }
    public GameObject Item3DObject {  set => item3DObject = value; }

    public string URLBundleModel { set => urlBundleModel = value; }

    public RawImage URLImgModel { get => urlImgModel; set => urlImgModel = value; }


    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = itemName;
        //transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
        urlImgModel = transform.GetChild(1).GetComponent<RawImage>();
        transform.GetChild(2).GetComponent<TMP_Text>().text = itemDescription;

        var button = GetComponent<Button>();

        button.onClick.AddListener(GameManager.instance.ARPosition);
        button.onClick.AddListener(Place3DModel);

        interactionManager = FindObjectOfType<ARPlacerManager>();
        
    }

    private void Place3DModel()
    {
        //interactionManager.Art3DModel = Instantiate(item3DObject);
        StartCoroutine(DownloadAssetBundle(urlBundleModel));

    }

    IEnumerator DownloadAssetBundle(string URL_AB)
    {
        UnityWebRequest serverRequest = UnityWebRequestAssetBundle.GetAssetBundle(URL_AB);
        yield return serverRequest.SendWebRequest();

        if (serverRequest.result == UnityWebRequest.Result.Success)
        {
            AssetBundle model3D = DownloadHandlerAssetBundle.GetContent(serverRequest);

            if(model3D != null )
            {
                interactionManager.Art3DModel = Instantiate(model3D.LoadAsset(model3D.GetAllAssetNames()[0]) as GameObject);
            }
            else
            {
                Debug.Log("AB no valido...");
            }
        }
        else
        {
            Debug.Log("Error en la corrutina de descarga...");
        }
    }
    
}
