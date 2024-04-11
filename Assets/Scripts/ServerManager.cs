using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using TMPro;


public class ServerManager : MonoBehaviour
{
    [SerializeField] private string jsonURL;
    [SerializeField] private ItembuttonManager itemBtnManager;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private GameObject buttonContainerSearch;
    public TMP_InputField SearchText;
    private UIManager uiManager;

    [Serializable]
    public struct Items//struct de los elementos del json
    {
        [Serializable]
        public struct Item
        {
            public string Name;
            public string Description;
            public PlaneType Planetype;
            public category category;
            public styles styles;
            public string URLBundleModel;
            public string URLImageModel;
            public string URLArticulo;
        }

        public Item[] items;
    }

    public Items newItemsCollection = new Items();  
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        StartCoroutine(GetJsonData());//Empieza la corrutina de descarga del json
        //GameManager.instance.OnItemsMenu += createButtons;//suscripcion al evento
        uiManager.ItemsMenuMuebles += createButtonsMuebles;
        uiManager.ItemsMenuDeco += createButtonsDeco;
        uiManager.ItemsMenuElectro += createButtonsElectro;
       
        SearchText.onValueChanged.AddListener(delegate { createButtonsSearch(); });

    }

    // private void createButtons()//Creacion de botones a partir de los datos del json
    // {
    //     foreach (var item in newItemsCollection.items)
    //     {
    //         ItembuttonManager itembutton;
    //         itembutton = Instantiate(itemBtnManager, buttonContainer.transform);
    //         itembutton.name = item.Name;
    //         itembutton.ItemName = item.Name;
    //         itembutton.ItemDescription = item.Description;
    //         itembutton.PlaneType = item.Planetype;
    //         itembutton.Category = item.category;
    //         itembutton.Estilo = item.styles;
    //         itembutton.URLBundleModel = item.URLBundleModel;
    //         itembutton.URLArticulo = item.URLArticulo;
    //         StartCoroutine(GetBundleImg(item.URLImageModel, itembutton));
    //     }
    //     //GameManager.instance.OnItemsMenu -= createButtons;
    // }
    private void createButtonsN(int category)
    {
        //uiManager.ItemsMenuDeco += createButtonsElctro;
        foreach (var item in newItemsCollection.items)
        {
            if ((int)item.category == category)
            {
                ItembuttonManager itembutton;
                itembutton = Instantiate(itemBtnManager, buttonContainer.transform);
                itembutton.name = item.Name;
                itembutton.ItemName = item.Name;
                itembutton.ItemDescription = item.Description;
                itembutton.PlaneType = item.Planetype;
                itembutton.Category = item.category;
                itembutton.Estilo = item.styles;
                itembutton.URLBundleModel = item.URLBundleModel;
                itembutton.URLArticulo = item.URLArticulo;
                StartCoroutine(GetBundleImg(item.URLImageModel, itembutton));
            }
        }
    }
    private void createButtonsMuebles()
    {
        createButtonsN(1);
    }

    private void createButtonsDeco()
    {
        createButtonsN(2);
    }

    private void createButtonsElectro()
    {
        createButtonsN(0);
    }

    private void createButtonsSearch()
    {
        delArticuloSearch();
        string searchTextLower = SearchText.text.ToLower();
        foreach (var item in newItemsCollection.items)
        {
            
            string itemNameLower = item.Name.ToLower();
            Debug.Log("Item:" + itemNameLower);
            Debug.Log("Search: " + searchTextLower);
            if (itemNameLower.Contains(searchTextLower))
            {
                Debug.Log($"Art�culo similar encontrado: {item.Name}");
                
                ItembuttonManager itembutton;
                itembutton = Instantiate(itemBtnManager, buttonContainerSearch.transform);
                itembutton.name = item.Name;
                itembutton.ItemName = item.Name;
                itembutton.ItemDescription = item.Description;
                itembutton.PlaneType = item.Planetype;
                itembutton.Category = item.category;
                itembutton.Estilo = item.styles;
                itembutton.URLBundleModel = item.URLBundleModel;
                itembutton.URLArticulo = item.URLArticulo;
                StartCoroutine(GetBundleImg(item.URLImageModel, itembutton));
            }
        }
        
    }

    public void delArticulo()
    {
        if (buttonContainer != null)
        {
            foreach (Transform articulo in buttonContainer.transform)
            {
                Destroy(articulo.gameObject);
            }
        }
    }

    public void delArticuloSearch()
    {
        if (buttonContainerSearch != null)
        {
            foreach (Transform articulo in buttonContainerSearch.transform)
            {
                Destroy(articulo.gameObject);
            }
        }
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
