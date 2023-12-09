using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class SandCastleItemCollider : ItemCollider
{
    void Start()
    {
        //on 
        victim.SetActive(false);
    }

    //deactivate the towel
    protected override void Sprung(GameObject collision){


        //REACTIVATE SHOVEL 
            //TO DO
            victim.SetActive(true);

        //Destroy self
            //TO DO
            Destroy(gameObject);
            // Destroy(this);

        //AND MAKE IT GRABBABLE (Should not be done)
        //victim.tag = "Grab";
    }


}
