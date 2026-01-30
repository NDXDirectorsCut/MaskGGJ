using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityInputs
{
    public float horizontal;
    public float vertical;
    public bool jump;
    public bool baseAttack;
    public bool special1;
    public bool special2;
    public bool interact;
}

public class EntityBehaviour : MonoBehaviour
{
    [System.NonSerialized]
    public Rigidbody2D body;
    [SerializeField]
    private string entityState = "Null";
    public bool stateLock = false;
    [Space(10)]
    public EntityInputs inputs;
    public List<MonoBehaviour> actionList = new List<MonoBehaviour>();
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

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        grounded = false;
        if(GetComponentInChildren<Collider2D>())
        {
            Collider2D col = GetComponentInChildren<Collider2D>();
            Vector2 lowestPoint = col.ClosestPoint((Vector2)transform.position - Vector2.up*10);
            RaycastHit2D hit = Physics2D.Raycast(lowestPoint + Vector2.up*0.1f,-Vector2.up,0.125f,collisionLayers);
            if(hit.collider != null)
            {
                Debug.DrawRay(lowestPoint + Vector2.up*0.1f,Vector2.up * -0.125f, Color.green);
                grounded = true;
            }
            else
            {
                Debug.DrawRay(lowestPoint + Vector2.up*0.1f,Vector2.up * -0.125f, Color.red);
            }
        }
    }
}
