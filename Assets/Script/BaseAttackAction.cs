using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackAction : MonoBehaviour
{
    EntityBehaviour entity;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInChildren<EntityBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(entity.GetState() == "Idle")
        {
            if(entity.inputs.baseAttack)
            {
                
            }
        }
    }
}
