using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PlaneType
{
    horizontal = 0,
    vertical = 1
}

[CreateAssetMenu]
public class Articulo : ScriptableObject
{
    public string ArtName;
    public Sprite ArtImage;
    public PlaneType Type;
    public string ArtDescription;
    public GameObject Art3DModel;
}
