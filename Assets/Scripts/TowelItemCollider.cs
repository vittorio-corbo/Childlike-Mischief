using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class TowelItemCollider : ItemCollider
{
    //deactivate the towel
    protected override void Sprung(GameObject collision){
        print("i have been sprung");
        print("i have been sprung");
        print("i have been sprung");
        print("i have been sprung");
        print("i have been sprung");
        victim.tag = "Grab";
    }


}
