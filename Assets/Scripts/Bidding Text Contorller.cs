using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BiddingTextContorller : MonoBehaviour
{
    TextCreator textCreator;
    public GameObject obj;
    int noc = 2;
    List<string> bid = new List<string>() {"", "", "", ""};
    [HideInInspector] public GameObject clickedObj;
    [HideInInspector] public bool clicked;
    [HideInInspector] public bool isBidding = true;
    public GameObject objCardContorller;

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
                bid[0] = clickedObj.GetComponent<TextMeshPro>().text;
                textCreator.CreateText(obj, bid[0], 0, 0, 1, noc);
                if(bid[2]=="P"&&bid[3]=="P"&&bid[0]=="P")EndBidding();
            }

            //west
            if (isBidding){
                bid[1] = "P";
                textCreator.CreateText(obj, bid[1], 0, 0, 2, noc);
                if(bid[1]=="P"&&bid[3]=="P"&&bid[0]=="P")EndBidding();
            }
            
            //north(an expection : All Pass)
            if (isBidding){
                bid[2] = "P";
                textCreator.CreateText(obj, bid[2], 0, 0, 3, noc);
                if(bid[1]=="P"&&bid[2]=="P"&&bid[0]=="P"&&noc!=2)EndBidding();
            }
            
            //east
            if (isBidding){
                bid[3] = "P";
                textCreator.CreateText(obj, bid[3], 0, 0, 4, noc);
                if(bid[2]=="P"&&bid[3]=="P"&&bid[1]=="P")EndBidding();
            }
            

            noc++;
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
            bid = new List<string>() {"", "", "", ""};
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
}
