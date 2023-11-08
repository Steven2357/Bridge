using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextCreator : MonoBehaviour
{
    public void CreateText(GameObject obj, string str, float x, float y, int nor, int noc){
        GameObject Text1 = Instantiate(obj, new Vector3(x, y), Quaternion.identity);
        TextMeshPro text0 = Text1.GetComponent<TextMeshPro>();
        TextPosition tp = Text1.GetComponent<TextPosition>();

        text0.text = str;
        tp.numberOfRow = nor;
        tp.numberOfColumn = noc;
    }
}
