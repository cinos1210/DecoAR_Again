using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<Articulo> articulos = new List<Articulo>();
    [SerializeField] private GameObject ButtonContainer;
    [SerializeField] private ItembuttonManager itemButtonManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnItemsMenu += CreateButtons;
    }

    private void CreateButtons()
    {
        foreach (var articulo in articulos)
        {
            ItembuttonManager itemButton;
            itemButton = Instantiate(itemButtonManager, ButtonContainer.transform);
            itemButton.ItemName = articulo.ArtName;
            itemButton.ItemDescription = articulo.ArtDescription;
            itemButton.ItemImage = articulo.ArtImage;
            itemButton.Item3DObject = articulo.Art3DModel;
            itemButton.name = articulo.ArtName;
        }

        GameManager.instance.OnItemsMenu -= CreateButtons;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
