using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _1CText : MonoBehaviour
{
    BiddingTextContorller biddingTextContorller;

    void Start()
    {
        biddingTextContorller = FindObjectOfType<BiddingTextContorller>();
    }

    void OnMouseDown()
    {
        biddingTextContorller.clickedObj = gameObject;
        biddingTextContorller.clicked = true;
    }
}
