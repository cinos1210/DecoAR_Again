using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class ServerManager : MonoBehaviour
{
    [SerializeField] private string jsonURL;
    [SerializeField] private ItembuttonManager itemBtnManager;
    [SerializeField] private GameObject buttonContainer;

    [Serializable]
    public struct Items
    {
        [Serializable]
        public struct Item
        {
            public string Name;
            public string Description;
            public PlaneType Planetype;
            public string URLBundleModel;
            public string URLImageModel;
        }

        public Item[] items;
    }

    public Items newItemsCollection = new Items();  
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetJsonData());
        GameManager.instance.OnItemsMenu += createButtons;
    }

    private void createButtons()
    {
        foreach (var item in newItemsCollection.items)
        {
            ItembuttonManager itembutton;
            itembutton = Instantiate(itemBtnManager, buttonContainer.transform);
            itembutton.name = item.Name;
            itembutton.ItemName = item.Name;
            itembutton.ItemDescription = item.Description;
            itembutton.PlaneType = item.Planetype;
            itembutton.URLBundleModel = item.URLBundleModel;
            StartCoroutine(GetBundleImg(item.URLImageModel, itembutton));
        }
        GameManager.instance.OnItemsMenu -= createButtons;
    }

    //corutina para descargar el archivo json
    IEnumerator GetJsonData()
    {
        UnityWebRequest S_request = UnityWebRequest.Get(jsonURL);
        yield return S_request.SendWebRequest();

        if (S_request.result == UnityWebRequest.Result.Success)
        {
            newItemsCollection = JsonUtility.FromJson<Items>(S_request.downloadHandler.text);
            Debug.Log(S_request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error...");
        }
    }

    //Corrutina para la descarga de la imagen del articulo
    IEnumerator GetBundleImg(string urlImage, ItembuttonManager button)
    {
        UnityWebRequest S_request = UnityWebRequest.Get(urlImage);
        S_request.downloadHandler = new DownloadHandlerTexture();
        yield return S_request.SendWebRequest();

        if (S_request.result == UnityWebRequest.Result.Success)
        {
           button.URLImgModel.texture = ((DownloadHandlerTexture)S_request.downloadHandler).texture;
        }
        else
        {
            Debug.Log("Error...");
        }
    }
}
