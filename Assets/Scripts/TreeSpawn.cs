using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{

    public GameObject treeObject;

    public GameObject treesInWorldObject;

    public int treeAmount;

    List<GameObject> treesList = new List<GameObject>();

    GameObject[] treesArray;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= treeAmount; i++){
            treesList.Add(Instantiate<GameObject>(treeObject));
            treesArray = treesList.ToArray();
            treesArray[i].transform.position = new Vector3(Random.Range(-840, 780), 0, Random.Range(-625, 590));
            treesArray[i].transform.parent = treesInWorldObject.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
