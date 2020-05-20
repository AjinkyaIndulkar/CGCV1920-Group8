using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helper : MonoBehaviour
{
    // Start is called before the first frame update

    
    public Color dijkstra_to_color(int dijkstra)
    {
        Color col = new Color();
        var PI = Mathf.PI;
        float f = ((float)dijkstra)/10.0f;

        float d = 1 / (f/2);

        col.r = Mathf.Cos(f + (PI * 2 / 3) * 0) * 8 * d;
        col.b = Mathf.Cos(f + (PI * 2 / 3) * 1) * 8 * d;
        col.g = Mathf.Cos(f + (PI * 2 / 3) * 2) * 8 * d;

        return col;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
