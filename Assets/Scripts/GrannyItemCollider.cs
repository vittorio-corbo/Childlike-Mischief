using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class GrannyItemCollider : ItemCollider
{
    protected override void OnTriggerStay(Collider other){
        //print(other);

        //Grabbable object collision
        // print("i have something?");
        //if (other.transform.gameObject.tag == "Grab"){
        
        if (other.transform.gameObject.tag == "Sand Bag"){
            //
            
            //print("did this happen");
            if (Key.KeyType.Coke == other.transform.gameObject.GetComponent<Key>().type || Key.KeyType.Energy == other.transform.gameObject.GetComponent<Key>().type) {
                if (sprung == false){
                    sprung = true;
                    //Destroy(other);
                    Sprung(other.transform.GameObject());
                }

            }
        }
        //print(sprung);
    }

    protected override void Sprung(GameObject collision){
        // victim.tag = "Grab";
        //RUN BLINK

        Destroy(collision);
        Destroy(victim);

        //victim.tag = "Grab";

        //maybe make crab ungrappable idk
    }

    public bool getSprung(){
        return sprung;
    }

}
