using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    public enum KeyType
    {
        Not,
        Crab,
        Towel,
        Shovel,
        Energy,
        Coke,
        Lax,
        Coin,
        Glass,
        Player,
        // Apendix,
        // PowerCore,
        // Fuse,
    }

    public KeyType type;

    //public bool isGrabbable;
}
