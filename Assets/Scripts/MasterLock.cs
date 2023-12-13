using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class MasterLock : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }


    //maybes
    //[SerializeField] private List<KeyType> fixingTypes;
    //public List<FixableObjectCheck> fixableObjectChecks = new List<FixableObjectCheck>();

    //in og
    [SerializeField] public GameObject glassesLock;
    [SerializeField] private GameObject holeLock;
    [SerializeField] private GameObject towelLock;
    [SerializeField] private GameObject aloeLock;

    void Update(){
        // print( (ItemColliderStay glassesLock).getSprung());
        
        // print(holeLock.GetComponent<ItemColliderStay>().getSprung());


        //WHAT I WANT TO USE
        bool atrue = glassesLock.GetComponent<ItemColliderStay>().getSprung();
        bool btrue = holeLock.GetComponent<HoleItemCollider>().getState() == 3;
        bool ctrue = towelLock.GetComponent<ItemColliderStay>().getSprung();
        bool dtrue = aloeLock.GetComponent<ItemColliderStay>().getSprung();
        if (atrue && btrue && ctrue && dtrue){
            print("hooooooly shittt");
        }
        print("start");
        print(glassesLock.GetComponent<ItemColliderStay>().getSprung());
        print(holeLock.GetComponent<HoleItemCollider>().getState() == 3);
        print(towelLock.GetComponent<ItemColliderStay>().getSprung());
        print(aloeLock.GetComponent<ItemColliderStay>().getSprung());
        print("end");
        print("");

    }
    // [SerializeField] private KeyType keyNeeded;
    // abstract protected void Sprung(GameObject collision);
    // protected bool sprung = false;

    //private void OnCollisionEnter(Collision other) {
    // private void OnTriggerEnter(Collider other){
    // protected override void OnTriggerStay(Collider other){
    //     print(other);

    //     //Grabbable object collision
    //     // print("i have something?");
    //     //if (other.transform.gameObject.tag == "Grab"){
        
    //     if (other.transform.gameObject.tag == "Grab"){
    //         //
            
    //         //print("did this happen");
    //         if (keyNeeded == other.transform.gameObject.GetComponent<Key>().type) {
    //             sprung = true;
    //         //     if (other.transform.parent == null){
    //         //         //print("its on the ground");
    //         //         if (sprung == false){
    //         //             //print("broooooooooooooooooo");
    //         //             sprung = true;
    //         //             Sprung(other.transform.gameObject);
    //         //         }
    //         //     }
    //         //     // Sprung(other.transform.gameObject);
                
    //         //     // print("kill me wooooooooo");    
    //         //     //wall.tag = "Grab";
    //         //     //other.transform.gameObject.tag = "Untagged";
    //         }else{
    //             sprung = false;
    //         }
    //     }
    //     //print(sprung);
    // }

    // protected override void Sprung(GameObject collision){
    //     victim.tag = "Grab";

    //     //maybe make crab ungrappable idk
    // }

}
