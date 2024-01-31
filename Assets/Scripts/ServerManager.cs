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
            itembutton.URLBundleModel = item.URLBundleModel;
            StartCoroutine(GetBundleImg(item.URLImageModel, itembutton));
        }
        GameManager.instance.OnItemsMenu -= createButtons;
    }


    IEnumerator GetJsonData()
    {
        UnityWebRequest S_request = UnityWebRequest.Get(jsonURL);
        yield return S_request.SendWebRequest();

        if (S_request.result == UnityWebRequest.Result.Success)
        {
            newItemsCollection = JsonUtility.FromJson<Items>(S_request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error no supo que hacer e3sta mierda");
        }
    }

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
            Debug.Log("Error no supo que hacer e3sta mierda");
        }
    }
}
