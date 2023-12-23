using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeuralNetworks;

public class NetworkShapeObject : MonoBehaviour
{
    public NeuralNet Network;

    public double maxSizeX;
    public double maxSizeY;
    public int maxWidth;
    public int maxDepth;

    public GameObject nodePrefab;
    public GameObject weightPrefab;
    
    public bool active;

    private int activeLayer = 0;


    void Start()
    {
        //nodes = new GameObject[Network.Architecture.Length][];

        int count = 0;
        for (int L = 0; L < Network.Architecture.Length; L++)
        {
            for (int i = 0; i < Network.Architecture[i]; i++)
            {
                //nodes[L][i] = Instantiate(nodePrefab);
                
                if (L != 0)
                {
                    for (int j = 0; j < Network.Architecture[L - 1]; j++)
                    {
                        //weights[L - 1][count] = Instantiate(weightPrefab);
                        count++;
                    }
                }
            }
        }
    }

    void Update()
    {
        if (active)
        {
            // To be tested during the Training Method Stage of the Project.
        }
    }
}
