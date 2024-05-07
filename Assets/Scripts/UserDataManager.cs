using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    private string UserID;

    public string userid{set => UserID = value;}

    private string Articulo;
    public static UserDataManager instance;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(UserID);
    }
}
