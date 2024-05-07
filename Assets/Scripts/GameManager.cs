using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //Eventos
    public event Action OnLoginMenu;
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;
    public event Action OnRulerMenu;

    public static GameManager instance;
    
    //Singleton
    private void Awake()
    {
        if (instance != null && instance != this)   
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        //MainMenu();
        LoginMenu();
    }

    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        Debug.Log("Main Menu Activated");
    }

    public void ItemMenu()
    {
        OnItemsMenu?.Invoke();
        Debug.Log("Item Menu Activated");
    }

    public void ARPosition()
    {
        OnARPosition?.Invoke();
        Debug.Log("AR Position Activated");
    }

    public void ARRuler()
    {
        OnRulerMenu?.Invoke();
        Debug.Log("AR Ruler Activated");
    }

    public void LoginMenu()
    {
        OnLoginMenu?.Invoke();
        Debug.Log("Login Menu Activated");
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
