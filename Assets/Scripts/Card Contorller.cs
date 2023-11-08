using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class CardContorller : MonoBehaviour
{
    List<string> deck;
    List<string> originalDeck;
    [HideInInspector]public List<List<string>> hands;
    System.Random rd = new System.Random();
    public HandCardsTextCreator handCardsTextCreator;

    // Start is called before the first frame update
    void Start()
    {
        originalDeck = CreateDeck();
        deck = originalDeck;
        string originalDeckstr = String.Join(",", originalDeck);
        deck = WashCards(deck);
        hands = DealCards(deck);
        originalDeck = originalDeckstr.Split(",").ToList();
        
        //sort
        for (int i = 0; i < 4; i++){
            List<string> newHand = new List<string>() {};
            foreach (string card in originalDeck){
                if (hands[i].Contains(card)){
                    newHand.Add(card);
                }
            }
            hands[i] = newHand;
        }
        
        Instantiate(handCardsTextCreator, new Vector3(0,0,0), quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //create deck
    List<string> CreateDeck(){
        List<string> suits =  new List<string>(){"Club", "Diamond", "Heart", "Spade"};
        List<string> numbers =  new List<string>(){"A", "K", "Q", "J", "T", "9", "8", "7", "6", "5", "4", "3", "2"};
        List<string> deck = new List<string>();
        for (int i = 0; i < 4; i++){
            for  (int j = 0; j < 13; j++){
                deck.Add(suits[i] + " " + numbers[j]);
            }
        }
        return deck;
    }
    //deal deck

    List<string> WashCards(List<string> deck){
        List<string> newDeck = new List<string>();
        while (deck.Count != 0){
            int rdn = rd.Next(deck.Count);
            newDeck.Add(deck[rdn]);
            deck.RemoveAt(rdn);
        }
        return newDeck;
    }
    List<List<string>> DealCards(List<string> deck, int hands = 4){
        List<List<string>> handCards = new List<List<string>>();
        for (int i = 0; i < hands; i++){
            handCards.Add(deck.GetRange((deck.Count/hands)*i, deck.Count/hands));
        }
        return handCards;
    }
}
