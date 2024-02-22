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
    public struct Items//struct de los elementos del json
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
        StartCoroutine(GetJsonData());//Empieza la corrutina de descarga del json
        GameManager.instance.OnItemsMenu += createButtons;//suscripcion al evento
    }

    private void createButtons()//Creacion de botones a partir de los datos del json
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

        if (S_request.result == UnityWebRequest.Result.Success)//si se encontro resultado
        {
            newItemsCollection = JsonUtility.FromJson<Items>(S_request.downloadHandler.text);//introduce los datos del json en el struct
            Debug.Log(S_request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error..."); //si no da error
        }
    }

    //Corrutina para la descarga de la imagen del articulo
    IEnumerator GetBundleImg(string urlImage, ItembuttonManager button)
    {
        UnityWebRequest S_request = UnityWebRequest.Get(urlImage);
        S_request.downloadHandler = new DownloadHandlerTexture();
        yield return S_request.SendWebRequest();

        if (S_request.result == UnityWebRequest.Result.Success)//si se encontro resultado
        {
           button.URLImgModel.texture = ((DownloadHandlerTexture)S_request.downloadHandler).texture;//Guarda la imagen en su correspondiente boton.
        }
        else
        {
            Debug.Log("Error...");
        }
    }
}
