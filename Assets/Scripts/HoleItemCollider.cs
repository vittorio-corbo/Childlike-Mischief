using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class HoleItemCollider : ItemCollider
{
    private int state = 0;

    [Space(10)]
    [Header("Sand Removed")]
    [SerializeField] public GameObject sandA;
    [SerializeField] public GameObject sandB;
    [SerializeField] public GameObject sandC;

    [SerializeField] public GameObject guyA;
    [SerializeField] public GameObject guyB;

    


    protected override void OnTriggerStay(Collider other){
        //CHECK IF ITEM ON FLOOR


        if (other.transform.gameObject.tag == "Grab"){
            // if (other.transform.gameObject.GetComponent<Key>().type == Key.KeyType.Shovel){
            if (other.transform.gameObject.GetComponent<Key>().type == keyNeeded){ //shovel
                state = 1;
                other.transform.tag = "Untagged";//shovel is gone now

                //DEACTIVATE WHAT NEEDS TO BE DEACTIVATED
                    //TODOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                //transition()

                //deactivate 
                //destroy sand
                Destroy(sandA);
                Destroy(guyA);
                guyB.SetActive(true);

                
            }

            if (state == 1){
                if (other.transform.gameObject.GetComponent<Key>().type == Key.KeyType.Energy){
                    //go to state 3
                    state = 3;

                    //transition()

                    Destroy(other);//destroy drink

                    //destroy sand
                    Destroy(sandB);
                    Destroy(sandC);
                    Destroy(guyB);

                } else if (other.transform.gameObject.GetComponent<Key>().type == Key.KeyType.Coke){
                    //go to state 2
                    state = 2;

                    //transition()

                    Destroy(other);//destroy drink

                    //destroy sand
                    Destroy(sandB);
                    Destroy(guyB);

                } else if (other.transform.gameObject.GetComponent<Key>().type == Key.KeyType.Lax){
                    //do nothing (state = 1)


                    Destroy(other);
                    Destroy(guyB);
                }

            }

            //
            
            //print("did this happen");
            // if (keyNeeded == other.transform.gameObject.GetComponent<Key>().type) {
            //     sprung = true;
            // //     if (other.transform.parent == null){
            // //         //print("its on the ground");
            // //         if (sprung == false){
            // //             //print("broooooooooooooooooo");
            // //             sprung = true;
            // //             Sprung(other.transform.gameObject);
            // //         }
            // //     }
            // //     // Sprung(other.transform.gameObject);
                
            // //     // print("kill me wooooooooo");    
            // //     //wall.tag = "Grab";
            // //     //other.transform.gameObject.tag = "Untagged";
            // }else{
            //     sprung = false;
            // }
        }
        //print(sprung);
    }

    public int getState(){
        return state;
    }



    protected override void Sprung(GameObject collision){
        victim.tag = "Grab";

        //maybe make crab ungrappable idk
    }

    // public bool getSprung(){
    //     return sprung;
    // }

}
