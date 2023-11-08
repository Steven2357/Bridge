using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPosition : MonoBehaviour
{
    float relativex = 0;
    float relativey = 0;
    public int row;
    public int column;
    public int numberOfRow;
    public int numberOfColumn;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent == null){
            relativex = 0;
            relativey = 0;
        } else{
            relativex = transform.parent.position.x;
            relativey = transform.parent.position.y;
        }
        transform.position = new Vector3(relativex, relativey) + new Vector3((-320f-320f/column+640f*numberOfColumn/column)/100f, (180f+180f/row-360f*numberOfRow/row)/100f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
