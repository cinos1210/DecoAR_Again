using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DataKeeper : MonoBehaviour
{
    
    public Articulo articulo;

    public TMP_Text m_TextX;
    public TMP_Text m_TextY;
    public TMP_Text m_TextZ;
    // Start is called before the first
    // frame update
    void Start()
    {
        m_TextX.text = articulo.ArtDimension.x.ToString() + " cm";
        m_TextY.text = articulo.ArtDimension.y.ToString() + " cm";
        m_TextZ.text = articulo.ArtDimension.z.ToString() + " cm";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
