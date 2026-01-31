using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : MonoBehaviour
{
    EntityBehaviour entity;
    public float jumpForce = 10;
    public float holdForce = 5;
    public float holdTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInChildren<EntityBehaviour>();
    }

    IEnumerator Jump()
    {
        entity.body.linearVelocity += new Vector2(0,jumpForce);
        entity.grounded = false;
        float startTime = Time.time;
        float held = 0;
        float addedForce = 0;
        yield return new WaitForFixedUpdate();
        while(entity.inputs.jump && held<holdTime && entity.GetState() == "Idle" && entity.grounded == false)
        {
            entity.body.linearVelocity += new Vector2(0,holdForce) * Time.fixedDeltaTime;
            addedForce += holdForce * Time.fixedDeltaTime;
            held = Time.time - startTime;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log(addedForce);
        yield return null;
    }

    void FixedUpdate()
    {
        if(entity.GetState() == "Idle" && entity.grounded == true )
        {
            if(entity.inputs.jump)
            {
                StopAllCoroutines();
                StartCoroutine(Jump());
            }
        }
    }
}
