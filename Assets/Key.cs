using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    public enum KeyType
    {
        Not,
        Battery,
        Motor,
        Antenna,
        Gear,
        Rock,
        SidePannel,
        Liver,
        BellyBottom,
        Apendix,
        PowerCore,
        Fuse,
    }

    public KeyType type;

    //public bool isGrabbable;
}
