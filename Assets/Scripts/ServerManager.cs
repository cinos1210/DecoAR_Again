// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using UnityEngine.Networking;
// using TMPro;



// public class ServerManager : MonoBehaviour
// {
//     [SerializeField] private string jsonURL;
//     [SerializeField] private ItembuttonManager itemBtnManager;
//     [SerializeField] private GameObject buttonContainer;
//     [SerializeField] private GameObject buttonContainerSearch;
//     //[SerializeField] private GameObject buttonContainerRecommendation;
//     public TMP_InputField SearchText;
//     private UIManager uiManager;

//     [Serializable]
//     public struct Items//struct de los elementos del json
//     {
//         [Serializable]
//         public struct Item
//         {
//             public string Name;
//             public string Description;
//             public PlaneType Planetype;
//             public category category;
//             public styles styles;
//             public string URLBundleModel;
//             public string URLImageModel;
//             public string URLArticulo;
//         }

//         public Item[] items;
//     }

//     public Items newItemsCollection = new Items();  
//     //rootItems misArticulos;
//     // Start is called before the first frame update
//     void Start()
//     {
//         uiManager = FindAnyObjectByType<UIManager>();
//         StartCoroutine(GetJsonData());//Empieza la corrutina de descarga del json
//         GameManager.instance.OnItemsMenu += createButtons;//suscripcion al evento
//         uiManager.ItemsMenuMuebles += createButtonsMuebles;
//         uiManager.ItemsMenuDeco += createButtonsDeco;
//         uiManager.ItemsMenuElectro += createButtonsElectro;
       
//         SearchText.onValueChanged.AddListener(delegate { createButtonsSearch(); });

//     }

//     private void createButtons()//Creacion de botones a partir de los datos del json
//     {
//         foreach (var item in newItemsCollection.items)
//         {
//             ItembuttonManager itembutton;
//             itembutton = Instantiate(itemBtnManager, buttonContainer.transform);
//             itembutton.name = item.Name;
//             itembutton.ItemName = item.Name;
//             itembutton.ItemDescription = item.Description;
//             itembutton.PlaneType = item.Planetype;
//             itembutton.Category = item.category;
//             itembutton.Estilo = item.styles;
//             itembutton.URLBundleModel = item.URLBundleModel;
//             itembutton.URLArticulo = item.URLArticulo;
//             StartCoroutine(GetBundleImg(item.URLImageModel, itembutton));
//         }
//         //GameManager.instance.OnItemsMenu -= createButtons;
//     }
//     private void createButtonsN(int category)
//     {
//         //uiManager.ItemsMenuDeco += createButtonsElctro;
//         foreach (var item in newItemsCollection.items)
//         {
//             if ((int)item.category == category)
//             {
//                 ItembuttonManager itembutton;
//                 itembutton = Instantiate(itemBtnManager, buttonContainer.transform);
//                 itembutton.name = item.Name;
//                 itembutton.ItemName = item.Name;
//                 itembutton.ItemDescription = item.Description;
//                 itembutton.PlaneType = item.Planetype;
//                 itembutton.Category = item.category;
//                 itembutton.Estilo = item.styles;
//                 itembutton.URLBundleModel = item.URLBundleModel;
//                 itembutton.URLArticulo = item.URLArticulo;
//                 StartCoroutine(GetBundleImg(item.URLImageModel, itembutton));
//             }
//         }
//     }
//     private void createButtonsMuebles()
//     {
//         createButtonsN(1);
//     }

//     private void createButtonsDeco()
//     {
//         createButtonsN(2);
//     }

//     private void createButtonsElectro()
//     {
//         createButtonsN(0);
//     }

//     private void createButtonsSearch()
//     {
//         delArticuloSearch();
//         string searchTextLower = SearchText.text.ToLower();
//         foreach (var item in newItemsCollection.items)
//         {
            
//             string itemNameLower = item.Name.ToLower();
//             Debug.Log("Item:" + itemNameLower);
//             Debug.Log("Search: " + searchTextLower);
//             if (itemNameLower.Contains(searchTextLower))
//             {
//                 Debug.Log($"Art�culo similar encontrado: {item.Name}");
                
//                 ItembuttonManager itembutton;
//                 itembutton = Instantiate(itemBtnManager, buttonContainerSearch.transform);
//                 itembutton.name = item.Name;
//                 itembutton.ItemName = item.Name;
//                 itembutton.ItemDescription = item.Description;
//                 itembutton.PlaneType = item.Planetype;
//                 itembutton.Category = item.category;
//                 itembutton.Estilo = item.styles;
//                 itembutton.URLBundleModel = item.URLBundleModel;
//                 itembutton.URLArticulo = item.URLArticulo;
//                 StartCoroutine(GetBundleImg(item.URLImageModel, itembutton));
//             }
//         }
        
//     }

//     public void delArticulo()
//     {
//         if (buttonContainer != null)
//         {
//             foreach (Transform articulo in buttonContainer.transform)
//             {
//                 Destroy(articulo.gameObject);
//             }
//         }
//     }

//     public void delArticuloSearch()
//     {
//         if (buttonContainerSearch != null)
//         {
//             foreach (Transform articulo in buttonContainerSearch.transform)
//             {
//                 Destroy(articulo.gameObject);
//             }
//         }
//     }
    
//     IEnumerator GetJsonData()
//     {
//         string serverURL = "http://regional-sociology.gl.at.ply.gg:24198";
//         UnityWebRequest request = UnityWebRequest.Get(serverURL + "/jsondata");

//         yield return request.SendWebRequest();

//         if (request.result == UnityWebRequest.Result.Success)
//         {
//             newItemsCollection = JsonUtility.FromJson<Items>("{\"items\":" + request.downloadHandler.text + "}");
//             Debug.Log(request.downloadHandler.text);
//         }
//         else
//         {
//             Debug.LogError("Failed to fetch JSON data: " + request.error);
//         }
//     }



//     //Corrutina para la descarga de la imagen del articulo
//     IEnumerator GetBundleImg(string urlImage, ItembuttonManager button)
//     {
//         UnityWebRequest S_request = UnityWebRequest.Get(urlImage);
//         S_request.downloadHandler = new DownloadHandlerTexture();
//         yield return S_request.SendWebRequest();

//         if (S_request.result == UnityWebRequest.Result.Success)//si se encontro resultado
//         {
//            button.URLImgModel.texture = ((DownloadHandlerTexture)S_request.downloadHandler).texture;//Guarda la imagen en su correspondiente boton.
//         }
//         else
//         {
//             Debug.Log("Error...");
//         }
//     }
// }
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    [SerializeField] private ItembuttonManager itemBtnManager;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private GameObject buttonContainerSearch;
    //[SerializeField] private GameObject buttonContainerRecommendation;
    public TMP_InputField SearchText;
    public Button searchButton;
    private UIManager uiManager;

    [Serializable]
    public struct Items
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

    public Items SearchItemsCollection = new Items();   

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        StartCoroutine(GetJsonData());
        GameManager.instance.OnItemsMenu += createButtons;
        uiManager.ItemsMenuMuebles += createButtonsMuebles;
        uiManager.ItemsMenuDeco += createButtonsDeco;
        uiManager.ItemsMenuElectro += createButtonsElectro;

        searchButton.onClick.AddListener(createButtonsSearch);

        //SearchText.onValueChanged.AddListener(delegate { createButtonsSearch(); });
    }

    private void createButtons()
    {
        foreach (var item in SearchItemsCollection.items)
        {
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

    private void createButtonsN(int category)
    {
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

    private IEnumerator SearchItems(string searchTerm)
    {
        string searchURL = "http://regional-sociology.gl.at.ply.gg:24198/search?term=" + UnityWebRequest.EscapeURL(searchTerm);
        UnityWebRequest request = UnityWebRequest.Get(searchURL);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            SearchItemsCollection = JsonUtility.FromJson<Items>("{\"items\":" + request.downloadHandler.text + "}");
            createButtons();
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Fallo al encontrar el objeto: " + request.error);
        }
    }


    private void createButtonsSearch()
    {
        delArticuloSearch();
        string searchTextLower = SearchText.text.ToLower();
        StartCoroutine(SearchItems(searchTextLower));
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

    IEnumerator GetJsonData()
    {
        string serverURL = "http://regional-sociology.gl.at.ply.gg:24198";
        UnityWebRequest request = UnityWebRequest.Get(serverURL + "/jsondata");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            newItemsCollection = JsonUtility.FromJson<Items>("{\"items\":" + request.downloadHandler.text + "}");
        }
        else
        {
            Debug.LogError("Failed to fetch JSON data: " + request.error);
        }
    }

    IEnumerator GetBundleImg(string urlImage, ItembuttonManager button)
    {
        UnityWebRequest request = UnityWebRequest.Get(urlImage);
        request.downloadHandler = new DownloadHandlerTexture();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            button.URLImgModel.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
        else
        {
            Debug.LogError("Failed to download image: " + request.error);
        }
    }
}

