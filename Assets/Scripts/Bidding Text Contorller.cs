using System;
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
    string S = "None";
    string W = "None";
    string N = "None";
    string E = "None";
    List<string> Bidsort = new List<string> {"1C", "1D", "1H", "1S", "1N", "2C", "2D", "2H", "2S", "2N", "3C", "3D", "3H", "3S", "3N", "4C", "4D", "4H", "4S", "4N", "5C", "5D", "5H", "5S", "5N", "6C", "6D", "6H", "6S", "6N", "7C", "7D", "7H", "7S", "7N"};
    List<string> BidsortAll = new List<string> {"P", "X", "XX", "1C", "1D", "1H", "1S", "1N", "2C", "2D", "2H", "2S", "2N", "3C", "3D", "3H", "3S", "3N", "4C", "4D", "4H", "4S", "4N", "5C", "5D", "5H", "5S", "5N", "6C", "6D", "6H", "6S", "6N", "7C", "7D", "7H", "7S", "7N"};
    GameObject NSVUL;
    GameObject EWVUL;
    GameObject NumberOfHand;
    List<int> NSVULs = new List<int>() {2, 4, 5, 7, 10, 12, 13, 15};
    List<int> EWVULs = new List<int>() {3, 4, 6, 7, 9, 10, 13, 16};

    // Start is called before the first frame update
    void Start()
    {
        textCreator = FindObjectOfType<TextCreator>();
        NumberOfHand = GameObject.FindWithTag("tNumberOfHand");
        NSVUL = GameObject.FindWithTag("tNSVUL");
        EWVUL = GameObject.FindWithTag("tEWVUL");
        CheckDealer();
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
                    }
                }else if (bid == "P"){
                    BidS(bid);
                }else if (bid == "X" && CanX()){
                    BidS(bid);
                }else if (bid == "XX" && CanXX()){
                    BidS(bid);
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
            S = "None";
            W = "None";
            N = "None";
            E = "None";
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

            NumberOfHand.GetComponent<TextMeshPro>().text = (Int32.Parse(NumberOfHand.GetComponent<TextMeshPro>().text)+1).ToString();

            //change VUL color
            if (NSVULs.Contains(Int32.Parse(NumberOfHand.GetComponent<TextMeshPro>().text)%16))
                NSVUL.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
            else{
                NSVUL.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
            }
            if (EWVULs.Contains(Int32.Parse(NumberOfHand.GetComponent<TextMeshPro>().text)%16))
                EWVUL.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
            else{
                EWVUL.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
            }

            CheckDealer();
        }
    }

    void BidS(string bid){
        S = bid;
        textCreator.CreateText(obj, S, 0, 0, 1, noc);
        if(N=="P"&&E=="P"&&S=="P"&&W!="None"){
            EndBidding();
            return;
        }

        //west
        W = BidW();
        textCreator.CreateText(obj, W, 0, 0, 2, noc);
        if(W=="P"&&E=="P"&&S=="P"&&N!="None"){
            EndBidding(); 
            return;
        }
                
        //north
        N = BidN();
        textCreator.CreateText(obj, N, 0, 0, 3, noc);
        if(W=="P"&&N=="P"&&S=="P"&&E!="None"){
            EndBidding(); 
            return;
        }

        //east
        E = BidE();
        textCreator.CreateText(obj, E, 0, 0, 4, noc);
        if(N=="P"&&E=="P"&&W=="P"&&S!="None"){
            EndBidding();
            return;
        }

        noc++;
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

    string BidW(){
        return "P";
    }

    string BidN(){
        return "P";
    }

    string BidE(){
        return "P";
    }

    void CheckDealer(){
        if (Int32.Parse(NumberOfHand.GetComponent<TextMeshPro>().text) % 4 == 0){
            W = BidW();
            textCreator.CreateText(obj, W, 0, 0, 2, noc);
            N = BidN();
            textCreator.CreateText(obj, N, 0, 0, 3, noc);
            E = BidE();
            textCreator.CreateText(obj, E, 0, 0, 4, noc);
            noc++;
        }else if (Int32.Parse(NumberOfHand.GetComponent<TextMeshPro>().text) % 4 == 1){
            N = BidN();
            textCreator.CreateText(obj, N, 0, 0, 3, noc);
            E = BidE();
            textCreator.CreateText(obj, E, 0, 0, 4, noc);
            noc++;
        }else if (Int32.Parse(NumberOfHand.GetComponent<TextMeshPro>().text) % 4 == 2){
            E = BidE();
            textCreator.CreateText(obj, E, 0, 0, 4, noc);
            noc++;
        }
    }
}