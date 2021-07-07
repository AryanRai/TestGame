using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
 public GameObject prefab;
 List<GameObject> enemylist;
//public GameObject x;
//public int y = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemylist = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f")) {
            //y = y + 1;
            enemylist.Add((GameObject) Instantiate(prefab));
            //Instantiate(prefab);
            //Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
        }
        
    }
}
