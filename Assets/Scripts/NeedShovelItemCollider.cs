using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedShovelItemCollider : ItemCollider
{
    // Start is called before the first frame update
    protected override void Sprung(GameObject collision){
        //victim.state = 0;
        //victim.GetComponent<StayHoleCollider>().state = 1;
        victim.GetComponent<StayHoleCollider>().IncreaseState(1);

        //maybe make crab ungrappable idk
    }
}
