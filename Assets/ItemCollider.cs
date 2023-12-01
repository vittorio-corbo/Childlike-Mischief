using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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



    //private void OnCollisionEnter(Collision other) {
    private void OnTriggerEnter(Collider other){
        //print("sadf");
        if (other.transform.gameObject.tag == "Grab"){
            print("colliosion");    
            pizza += 1;
            print(pizza);
            if (pizza > 7){
                
                Destroy(wall);
            }
        }

    } 
}
