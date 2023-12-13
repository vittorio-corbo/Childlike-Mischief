using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class StoreItemCollider : ItemCollider
{
    [SerializeField] GameObject storeclerk;
    //deactivate the towel
    protected override void Sprung(GameObject collision){
        print("i have been sprung");
        print("i have been sprung");
        print("i have been sprung");
        print("i have been sprung");
        print("i have been sprung");
        if (collision.GetComponent<GrabCoin>().GetCoinNum() == 3){
            //GetCoinNum
            print(collision.GetComponent<GrabCoin>().GetCoinNum());
            victim.tag = "Grab";
            Destroy(collision);
            Destroy(storeclerk);
        }else{
            sprung = false;
            print("not enough stuff");
        }
        
    }


}
