using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHand : MonoBehaviour
{
    BiddingTextContorller biddingTextContorller;
    CardContorller cardContorller;
    void Start(){
        biddingTextContorller = FindObjectOfType<BiddingTextContorller>();
        cardContorller = FindObjectOfType<CardContorller>();
    }

    void OnMouseDown(){
        biddingTextContorller.NewHand();
    }
}
