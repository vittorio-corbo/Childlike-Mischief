using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class CoupleTowelItemCollider : ItemCollider
{
    [SerializeField] GameObject him;
    [SerializeField] GameObject her;
    //deactivate the towel
    protected override void Sprung(GameObject collision){
        victim.tag = "Grab";
        Destroy(him);
        Destroy(her);

        //maybe make crab ungrappable idk
    }


}
