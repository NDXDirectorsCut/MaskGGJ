using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAction : MonoBehaviour
{
    EntityBehaviour entity;
    float stunTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInChildren<EntityBehaviour>();
    }

    public IEnumerator Stun()
    {
        if(entity.SetState("Stunned"))
        {
            entity.stateLock = true;
            yield return new WaitForSeconds(stunTime);
            entity.stateLock = false;
            entity.SetState("Idle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
