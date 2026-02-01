using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTrigger : MonoBehaviour
{
    EnemySpawner eSpawner;
    CameraSystem camSystem;
    public List<GameObject> enemyList = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.root.gameObject.GetComponentInChildren<CameraSystem>())
        {
            camSystem = col.transform.root.gameObject.GetComponentInChildren<CameraSystem>();
            camSystem.cameraState = CameraState.Fixed;
            camSystem.fixedTarget = transform;
            eSpawner.activate = true;
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
        if(enemyList.Contains(enemy))
            enemyList.Remove(enemy);
        if(enemyList.Count < 1)
        {
            eSpawner.activate = false;
            if(camSystem!=null)
            {
                camSystem.cameraState = CameraState.Follow;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eSpawner = GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
