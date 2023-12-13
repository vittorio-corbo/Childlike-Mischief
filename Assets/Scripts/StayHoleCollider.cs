using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Key;

public class StayHoleCollider : ItemColliderStay
{
    [SerializeField] GameObject heldGlasses;
    public int state = 0;
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
            victim.transform.SetParent(null);   
            // victim.transform.GetChild(0).SetParent(null);

        //Destroy self
            //TO DO
            Destroy(heldGlasses);
            Destroy(gameObject);
            // Destroy(this);

        //AND MAKE IT GRABBABLE (Should not be done)
        //victim.tag = "Grab";
    }

    public void IncreaseState(int increase){
        state += increase;

    }


}
