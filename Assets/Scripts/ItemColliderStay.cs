using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class ItemColliderStay : ItemCollider
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }


    //maybes
    //[SerializeField] private List<KeyType> fixingTypes;
    //public List<FixableObjectCheck> fixableObjectChecks = new List<FixableObjectCheck>();

    //in og
    // [SerializeField] protected GameObject victim;
    // [SerializeField] private KeyType keyNeeded;
    // abstract protected void Sprung(GameObject collision);
    // protected bool sprung = false;

    //private void OnCollisionEnter(Collision other) {
    // private void OnTriggerEnter(Collider other){
    protected override void OnTriggerStay(Collider other){
        //print(other);

        //Grabbable object collision
        // print("i have something?");
        //if (other.transform.gameObject.tag == "Grab"){
        
        if (other.transform.gameObject.tag == "Grab"){
            //
            
            //print("did this happen");
            if (keyNeeded == other.transform.gameObject.GetComponent<Key>().type) {
                sprung = true;
            //     if (other.transform.parent == null){
            //         //print("its on the ground");
            //         if (sprung == false){
            //             //print("broooooooooooooooooo");
            //             sprung = true;
            //             Sprung(other.transform.gameObject);
            //         }
            //     }
            //     // Sprung(other.transform.gameObject);
                
            //     // print("kill me wooooooooo");    
            //     //wall.tag = "Grab";
            //     //other.transform.gameObject.tag = "Untagged";
            }else{
                sprung = false;
            }
        }
        //print(sprung);
    }

    protected override void Sprung(GameObject collision){
        victim.tag = "Grab";

        //maybe make crab ungrappable idk
    }

    public bool getSprung(){
        return sprung;
    }

}
