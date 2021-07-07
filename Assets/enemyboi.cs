using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyboi : MonoBehaviour
{
    //public Transform playerObj;

    
    private Transform playerObj;
    
    
    protected NavMeshAgent enemyMesh;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectsWithTag("theplayer")[0].transform;
        enemyMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyMesh.SetDestination(playerObj.position);
    }
}
