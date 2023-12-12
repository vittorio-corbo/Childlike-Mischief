using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class GuardItemCollider : ItemCollider
{
    //deactivate the towel
    protected override void Sprung(GameObject collision){
        print("I was sprunkkkkkk");
        //victim.tag = "Grab";
        Destroy(victim);

        //maybe make crab ungrappable idk
    }


}
