using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCoin : MonoBehaviour
{
    [SerializeField] GameObject coin2;
    [SerializeField] GameObject coin3;
    private int coinNum = 1;
    public bool AddCoin(){
        coinNum += 1;
        if (coinNum == 2){
            //acitvate it coin2
            coin2.SetActive(true);
            return true;
        }
        if (coinNum == 3){
            //acitvate it coin3
            coin3.SetActive(true);
            return true;
        }
        return false;
    }

    public int getCoinNum(){
        return coinNum;
    }
}
