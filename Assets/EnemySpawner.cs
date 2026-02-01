using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool activate;
    public List<GameObject> enemySpawns = new List<GameObject>();
    public int enemies = 5;
    public float spawnRate = 1;
    public float radius = 2f;

    public IEnumerator Spawn()
    {
        activate = false;
        int enemyCount = 0;
        while(enemyCount<enemies)
        {
            int id = Random.Range(0, enemySpawns.Count-1);
            Vector2 pos = Random.insideUnitCircle * Random.Range(0, radius);
            Vector3 spawnPosition = transform.position + new Vector3(pos.x, pos.y, 0);
            GameObject curEnemy = Instantiate(enemySpawns[id], spawnPosition, Quaternion.identity);
            enemyCount++;
            yield return new WaitForSeconds(1 / spawnRate);
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activate == true)
        {
            StartCoroutine(Spawn());
        }
    }
}
