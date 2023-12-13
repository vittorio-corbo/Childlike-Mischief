using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class GuardItemCollider : ItemCollider
{
    [SerializeField] GameObject lifeguard;
    //deactivate the towel
    protected override void Sprung(GameObject collision){
        print("I was sprunkkkkkk");
        //victim.tag = "Grab";

        //run blink thing

        Destroy(lifeguard);
        Destroy(collision);
        Destroy(victim);

        //maybe make crab ungrappable idk
    }


}
