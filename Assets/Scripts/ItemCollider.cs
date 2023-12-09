using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

abstract public class ItemCollider : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    [SerializeField] protected GameObject victim;
    private int pizza;

    //public enum Key typeLock;
    //public Key type;

    //[SerializeField] private List<KeyType> fixingTypes;
    [SerializeField] private KeyType keyNeeded;

    //public List<FixableObjectCheck> fixableObjectChecks = new List<FixableObjectCheck>();

    abstract protected void Sprung(GameObject collision);
    protected bool sprung = false;

    //private void OnCollisionEnter(Collision other) {
    // private void OnTriggerEnter(Collider other){
    private void OnTriggerStay(Collider other){

        //Grabbable object collision
        if (other.transform.gameObject.tag == "Grab"){
            if (keyNeeded == other.transform.gameObject.GetComponent<Key>().type) {
                if (other.transform.parent == null){
                    //print("its on the ground");
                    if (sprung == false){
                        sprung = true;
                        Sprung(other.transform.gameObject);
                    }
                }
                // Sprung(other.transform.gameObject);
                
                // print("kill me wooooooooo");    
                //wall.tag = "Grab";
                //other.transform.gameObject.tag = "Untagged";
            }
        }

        
        //Player Collision
        if (other.transform.gameObject.tag == "Player"){
            if (keyNeeded == KeyType.Player) {
                if (sprung == false){
                    sprung = true;
                    Sprung(other.transform.gameObject);
                }
                
                // print("kill me wooooooooo");    
                //create sepreate function that gets called for collision cause there are double or also i dont fucking care

            }
        }




        
        // Debu
        // Debug.Log(other.transform.gameObject.tag);
        // if (other.transform.gameObject.tag == "Grab"){
        //     if (keyNeeded == other.transform.gameObject.GetComponent<Key>().type) {
        //         if (other.transform.parent == null){
        //             print("ITS ON THE GROUND");
        //             wall.tag = "Grab";
        //             other.transform.gameObject.tag = "Untagged";
        //         }
        //     }
        // }

    }

    // private void OnTriggerEnter(Collider other){
        

    //     //print("sadf");
    //     if (other.transform.gameObject.tag == "Grab"){
    //         //it works
    //         if (keyNeeded == other.transform.gameObject.GetComponent<Key>().type) {
    //             // if (other.transform.SetParent(null){
    //             // if (other.transform.parent == null){
    //             //     print("ITS ON THE GROUND");
    //             // }
    //             print("god shot on");    
    //             //wall.tag = "Grab";

    //             // other.transform.gameObject.tag = "Untagged";

    //         }

    //         //Create collision with player thing that way i can check that kind of collision


    //         print("colliosion");    
    //         pizza += 1;
    //         print(pizza);
    //         if (pizza > 7){
                
    //             Destroy(wall);
    //         }
    //     }

    //     if (other.transform.gameObject.tag == "Player"){
    //         if (keyNeeded == KeyType.Player) {
    //             print("kill me wooooooooo");    
    //             //create sepreate function that gets called for collision cause there are double or also i dont fucking care

    //         }
    //     }



    // } 
}
