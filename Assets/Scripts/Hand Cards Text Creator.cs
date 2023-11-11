using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
using UnityEngine.TextCore.Text;
using System.Linq;

public class HandCardsTextCreator : MonoBehaviour
{
    [HideInInspector]public GameObject text, TextS, TextW, TextN, TextE;

    string cs, ds, hs, ss;

    CardContorller cardContorller;
    List<List<string>> hands;

    // Start is called before the first frame update
    void Start()
    {
        cardContorller = FindObjectOfType<CardContorller>();
        hands = cardContorller.hands;

        #region S
        TextS = Instantiate(text, new Vector3(0,0,0), quaternion.identity);
        TextMeshPro text0 = TextS.GetComponent<TextMeshPro>();
        cs = "";
        ds = "";
        hs = "";
        ss = "";

        foreach (string str in hands[0]){
            if (str.Contains("Club")){
                cs += str[5];
            }else if (str.Contains("Diamond")){
                ds += str[8];
            }else if (str.Contains("Heart")){
                hs += str[6];
            }else{
                ss += str[6];
            }
        }

        //Count and display HCP
        List<char> cards =  (cs + ds + hs + ss).ToList();
        int HCP = 0;
        foreach (char card in cards){
            if (card == 'A')
                HCP+=4;
            else if (card == 'K')
                HCP+=3;
            else if (card == 'Q')
                HCP+=2;
            else if (card == 'J')
                HCP+=1;
        }
        GameObject.FindWithTag("tHCPText").GetComponent<TextMeshPro>().text = "HCP : " + HCP.ToString();

        text0.text = $"S : {ss}\nH : {hs}\nD : {ds}\nC : {cs}\n";
        TextS.transform.position = new Vector3(-3.1f, -3.6f);
        #endregion

        #region W
        TextW = Instantiate(text, new Vector3(0,0,0), quaternion.identity);
        TextMeshPro text1 = TextW.GetComponent<TextMeshPro>();
        cs = "";
        ds = "";
        hs = "";
        ss = "";

        foreach (string str in hands[1]){
            if (str.Contains("Club")){
                cs += str[5];
            }else if (str.Contains("Diamond")){
                ds += str[8];
            }else if (str.Contains("Heart")){
                hs += str[6];
            }else{
                ss += str[6];
            }
        }

        text1.text = $"S : {ss}\nH : {hs}\nD : {ds}\nC : {cs}\n";
        TextW.transform.position = new Vector3(-9.5f, 0f);
        #endregion

        #region N
        TextN = Instantiate(text, new Vector3(0,0,0), quaternion.identity);
        TextMeshPro text2 = TextN.GetComponent<TextMeshPro>();
        cs = "";
        ds = "";
        hs = "";
        ss = "";

        foreach (string str in hands[2]){
            if (str.Contains("Club")){
                cs += str[5];
            }else if (str.Contains("Diamond")){
                ds += str[8];
            }else if (str.Contains("Heart")){
                hs += str[6];
            }else{
                ss += str[6];
            }
        }

        text2.text = $"S : {ss}\nH : {hs}\nD : {ds}\nC : {cs}\n";
        TextN.transform.position = new Vector3(-3.1f, 3.6f);
        #endregion

        #region E
        TextE = Instantiate(text, new Vector3(0,0,0), quaternion.identity);
        TextMeshPro text3 = TextE.GetComponent<TextMeshPro>();
        cs = "";
        ds = "";
        hs = "";
        ss = "";

        foreach (string str in hands[3]){
            if (str.Contains("Club")){
                cs += str[5];
            }else if (str.Contains("Diamond")){
                ds += str[8];
            }else if (str.Contains("Heart")){
                hs += str[6];
            }else{
                ss += str[6];
            }
        }

        text3.text = $"S : {ss}\nH : {hs}\nD : {ds}\nC : {cs}\n";
        TextE.transform.position = new Vector3(3.3f, 0f);
        #endregion
        TextW.GetComponent<MeshRenderer>().enabled = false;
        TextN.GetComponent<MeshRenderer>().enabled = false;
        TextE.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
