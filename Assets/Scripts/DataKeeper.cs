using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class DataKeeper : MonoBehaviour
{
    public Articulo articulo;

    public TMP_Text m_TextX;
    public TMP_Text m_TextY;
    public TMP_Text m_TextZ;
    public TMP_Text m_Description;
    public Canvas dataCanvas;
    public Button show_button;
    public GameObject DataUI;

    private bool visible; 
    // Start is called before the first
    // frame update
    void Start()
    {
        m_TextX.text = articulo.ArtDimension.x.ToString() + " cm";
        m_TextY.text = articulo.ArtDimension.y.ToString() + " cm";
        m_TextZ.text = articulo.ArtDimension.z.ToString() + " cm";
        m_Description.text = articulo.ArtDescription;

        dataCanvas.worldCamera = Camera.main;

        show_button.onClick.AddListener(ShowData);
        
        visible = false;

        transform.GetChild(0).transform.DOScale(new Vector3(0,0,0),0.5f);
        transform.GetChild(1).transform.DOScale(new Vector3(0,0,0),0.5f);
        transform.GetChild(2).transform.DOScale(new Vector3(0,0,0),0.5f);
        transform.GetChild(3).GetChild(0).GetChild(1).transform.DOScale(new Vector3(0,0,0),0.5f);

    }
    private void ShowData()
    {
        visible = !visible;
        if (visible)
        {
            transform.GetChild(0).transform.DOScale(new Vector3(1,1,1),0.5f);
            transform.GetChild(1).transform.DOScale(new Vector3(1,1,1),0.5f);
            transform.GetChild(2).transform.DOScale(new Vector3(1,1,1),0.5f);
            transform.GetChild(3).GetChild(0).GetChild(1).transform.DOScale(new Vector3(1,1,1),0.5f);
        }else if(!visible)
        {
            transform.GetChild(0).transform.DOScale(new Vector3(0,0,0),0.5f);
            transform.GetChild(1).transform.DOScale(new Vector3(0,0,0),0.5f);
            transform.GetChild(2).transform.DOScale(new Vector3(0,0,0),0.5f);
            transform.GetChild(3).GetChild(0).GetChild(1).transform.DOScale(new Vector3(0,0,0),0.5f);
        }
    }
    void LateUpdate() 
    {
        Debug.Log(visible);
        DataUI.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    public void Execute()
    {
        Application.OpenURL(articulo.ArtURL);
    }
}
