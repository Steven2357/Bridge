using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BiddingTextContorller : MonoBehaviour
{
    TextCreator textCreator;
    public GameObject obj;
    int noc = 2;
    [HideInInspector] public GameObject clickedObj;
    [HideInInspector] public bool clicked;
    [HideInInspector] public bool isBidding = true;
    public GameObject objCardContorller;

    //Bidding 
    string S = "P";
    string W = "P";
    string N = "P";
    string E = "P";
    List<string> Bidsort = new List<string> {"1C", "1D", "1H", "1S", "1N", "2C", "2D", "2H", "2S", "2N", "3C", "3D", "3H", "3S", "3N", "4C", "4D", "4H", "4S", "4N", "5C", "5D", "5H", "5S", "5N", "6C", "6D", "6H", "6S", "6N", "7C", "7D", "7H", "7S", "7N"};
    List<string> BidsortAll = new List<string> {"P", "X", "XX", "1C", "1D", "1H", "1S", "1N", "2C", "2D", "2H", "2S", "2N", "3C", "3D", "3H", "3S", "3N", "4C", "4D", "4H", "4S", "4N", "5C", "5D", "5H", "5S", "5N", "6C", "6D", "6H", "6S", "6N", "7C", "7D", "7H", "7S", "7N"};
    // Start is called before the first frame update
    void Start()
    {
        textCreator = FindObjectOfType<TextCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked){
            if (isBidding){
                string bid = clickedObj.GetComponent<TextMeshPro>().text;
                if (Bidsort.Contains(bid)){
                    if (CanBid(bid)){
                        BidS(bid);
                        noc++;
                    }
                }else if (bid == "P"){
                    BidS(bid);
                    noc++;
                }else if (bid == "X" && CanX()){
                    BidS(bid);
                    noc++;
                }else if (bid == "XX" && CanXX()){
                    BidS(bid);
                    noc++;
                }
            }
            clicked = false;
        }
    }

    void EndBidding(){
        isBidding = false;
        var hctc = FindObjectOfType<HandCardsTextCreator>();
        hctc.TextW.GetComponent<MeshRenderer>().enabled = true;
        hctc.TextN.GetComponent<MeshRenderer>().enabled = true;
        hctc.TextE.GetComponent<MeshRenderer>().enabled = true;
    }

    public void NewHand(){
        if (! isBidding){
            isBidding = true;
            noc = 2;
            S = "P";
            W = "P";
            N = "P";
            E = "P";
            Destroy(GameObject.FindGameObjectWithTag("tCardContorller"));
            Destroy(GameObject.FindGameObjectWithTag("tHandCardTextContorller"));
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("tBiddingText")){
                Destroy(obj);
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("tCardText")){
                Destroy(obj);
            }
            var cardContorller = Instantiate(objCardContorller);
            cardContorller.GetComponent<CardContorller>().enabled = true;
        }
    }

    void BidS(string bid){
        S = bid;
        textCreator.CreateText(obj, S, 0, 0, 1, noc);
        if(N=="P"&&E=="P"&&S=="P"){
            EndBidding();
            return;
        }

        //west
        W = "P";
        textCreator.CreateText(obj, W, 0, 0, 2, noc);
        if(W=="P"&&E=="P"&&S=="P"){
            EndBidding(); 
            return;
        }
                
        //north(an expection : All Pass)
        N = "P";
        textCreator.CreateText(obj, N, 0, 0, 3, noc);
        if(W=="P"&&N=="P"&&S=="P"&&noc!=2){
            EndBidding(); 
            return;
        }

        //east
        E = "P";
        textCreator.CreateText(obj, E, 0, 0, 4, noc);
        if(N=="P"&&E=="P"&&W=="P"){
            EndBidding();
            return;
        }
    }

    bool CanBid(string bid){
        return BidsortAll.IndexOf(bid) > new List<int>() {BidsortAll.IndexOf(W), BidsortAll.IndexOf(N), BidsortAll.IndexOf(E)}.Max();
    }

    bool CanX(){
        if (Bidsort.Contains(E))
            return true;
        else if (E == "X" || E == "XX")
            return false;
        else if (N != "P")
            return false;
        else if (Bidsort.Contains(W))
            return true;
        else
            return false;
    }

    bool CanXX(){
        if (E == "X")
            return true;
        else if (Bidsort.Contains(E) || E == "XX")
            return false;
        else if (N != "P")
            return false;
        else if (W == "X")
            return true;
        else
            return false;
    }
}
