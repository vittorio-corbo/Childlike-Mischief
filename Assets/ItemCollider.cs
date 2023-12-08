using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class ItemCollider : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    [SerializeField] GameObject wall;
    private int pizza;

    //public enum Key typeLock;
    //public Key type;

    //[SerializeField] private List<KeyType> fixingTypes;
    [SerializeField] private KeyType keyNeeded;

    //public List<FixableObjectCheck> fixableObjectChecks = new List<FixableObjectCheck>();



    //private void OnCollisionEnter(Collision other) {
    private void OnTriggerEnter(Collider other){
        //print("sadf");
        if (other.transform.gameObject.tag == "Grab"){
            //it works
            if (keyNeeded == other.transform.gameObject.GetComponent<Key>().type) {
                print("god shot on");    

            }

            print("colliosion");    
            pizza += 1;
            print(pizza);
            if (pizza > 7){
                
                Destroy(wall);
            }
        }

    } 
}
