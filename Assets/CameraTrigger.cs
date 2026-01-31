using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTrigger : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.root.gameObject.GetComponentInChildren<CameraSystem>())
        {
            CameraSystem camSystem = col.transform.root.gameObject.GetComponentInChildren<CameraSystem>();
            camSystem.cameraState = CameraState.Fixed;
            camSystem.fixedTarget = transform;
        }
        else if(col.transform.root.gameObject.GetComponentInChildren<EnemyAI>())
        {
            GameObject enemy = col.transform.root.gameObject.GetComponentInChildren<EnemyAI>().gameObject;
            enemyList.Add(enemy);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        GameObject enemy = col.transform.root.gameObject.GetComponentInChildren<EnemyAI>().gameObject;
        enemyList.Remove(enemy);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
