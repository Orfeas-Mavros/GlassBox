using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkShapeObject : MonoBehaviour
{
    public int[] architecture;

    public double maxSizeX;
    public double maxSizeY;
    public int maxWidth;
    public int maxDepth;

    public GameObject nodePrefab;
    public GameObject weightPrefab;
    private GameObject[][] nodes;
    private GameObject[][] weights;
    public Sprite threeDots;

    public bool active;

    private int activeLayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        nodes = new GameObject[architecture.Length][];

        int count = 0;
        for (int L = 0; L < architecture.Length; L++)
        {
            for (int i = 0; i < architecture[i]; i++)
            {
                nodes[L][i] = Instantiate(nodePrefab);
                
                if (L != 0)
                {
                    for (int j = 0; j < architecture[L - 1]; j++)
                    {
                        weights[L - 1][count] = Instantiate(weightPrefab);
                        count++;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {

        }
    }
}
