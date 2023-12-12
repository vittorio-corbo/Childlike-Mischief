using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class TowerTeleportCollider : ItemCollider
{
    [SerializeField] GameObject player;

    void Start()
    {
        //on 
        //victim.SetActive(false);
    }

    //deactivate the towel
    protected override void Sprung(GameObject collision){
        // print("tried to teleport");
        // print("tried to teleport");
        // print("tried to teleport");
        // print("tried to teleport");
        // print("tried to teleport");
        // print(collision.transform.position);
        // print(victim.transform.position);   
        //collision.transform.position = victim.transform.position;
        // player.transform.position = victim.transform.position;
        CharacterController cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        player.transform.position = victim.transform.position;
        // player.transform.position += Vector3.up*10f;
        cc.enabled = true;
        // print(collision.transform.position);
        sprung = false;//can be resprung

        //REACTIVATE SHOVEL 
            //TO DO
            //victim.SetActive(true);

        //Destroy self
            //TO DO
            //Destroy(gameObject);
            // Destroy(this);

        //AND MAKE IT GRABBABLE (Should not be done)
        //victim.tag = "Grab";
    }


}
