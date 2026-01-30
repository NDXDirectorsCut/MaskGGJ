using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    [SerializeField]
    private string entityState = "Null";
    public bool stateLock = false;
    public bool grounded;
    public LayerMask collisionLayers;

    public bool SetState(string newState)
    {
        if(stateLock == false)
        {
            entityState = newState;
        }
        return stateLock;
    }

    public string GetState()
    {
        return entityState;
    }

    void Update()
    {
        grounded = false;
        if(GetComponentInChildren<Collider>())
        {
            Collider col = GetComponentInChildren<Collider>();
            Vector3 lowestPoint = col.ClosestPoint(transform.position - Vector3.up*10);
            RaycastHit hit; 
            if(Physics.Raycast(lowestPoint + Vector3.up*0.1f,-Vector3.up,out hit,0.25f,collisionLayers))
            {
                grounded = true;
            }
        }
    }
}
