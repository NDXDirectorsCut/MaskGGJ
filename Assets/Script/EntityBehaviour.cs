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

    public int health = 5;
    
    [Space(10)]
    public EntityInputs inputs;
    //public List<MonoBehaviour> actionList = new List<MonoBehaviour>();
    public bool grounded;
    [System.NonSerialized]
    public Vector2 normal;
    //[System.NonSerialized]
    public float groundCheckDistance = 0.15f;
    [System.NonSerialized]
    public Vector2 faceDirection = Vector2.right;
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
        SetState("Idle");
        grounded = false;

        if(GetComponentInChildren<Collider2D>())
        {
            Collider2D col = GetComponentInChildren<Collider2D>();
            Vector2 lowestPoint = col.ClosestPoint(body.position - Vector2.up*10);
            RaycastHit2D hit = Physics2D.Raycast(lowestPoint + Vector2.up*0.1f,-Vector2.up,groundCheckDistance,collisionLayers);
            if(hit.collider != null)
            {
                Debug.DrawRay(lowestPoint + Vector2.up*0.1f,Vector2.up * -groundCheckDistance, Color.green);
                grounded = true;
                normal = hit.normal;
            }
            else
            {
                Debug.DrawRay(lowestPoint + Vector2.up*0.1f,Vector2.up * -groundCheckDistance, Color.red);
                normal = Vector2.up;
            }
        }

        if(Mathf.Abs(body.linearVelocity.x)>0.1f)
        {
            faceDirection = new Vector2(body.linearVelocity.x,0).normalized;
        }
        Debug.DrawRay(transform.position,faceDirection,Color.blue);
    }
}
