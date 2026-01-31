using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : MonoBehaviour
{
    EntityBehaviour entity;
    bool canJump = true;
    public float jumpForce = 10;
    public float holdForce = 5;
    public float holdTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInChildren<EntityBehaviour>();
    }

    IEnumerator Jump(float jumpForce, float holdForce)
    {
        canJump = false;
        entity.body.linearVelocity += entity.normal*jumpForce;
        entity.body.position += Vector2.up * entity.groundCheckDistance/2;
        entity.grounded = false;
        float held = 0;
        float addedForce = 0;
        while(entity.inputs.jump && held<holdTime && entity.GetState() == "Idle" && entity.grounded == false)
        {
            
            entity.body.linearVelocity += new Vector2(0,holdForce) * Time.fixedDeltaTime;
            addedForce += holdForce * Time.fixedDeltaTime;
            held += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        while(entity.grounded == false)
        {
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.25f);
        canJump = true;
        yield return null;
    }

    void FixedUpdate()
    {
        if(entity.GetState() == "Idle" && entity.grounded == true )
        {
            if(entity.inputs.jump && canJump == true)
            {
                StartCoroutine(Jump(entity.stats.baseJump, entity.stats.addJump));
            }
        }
    }
}
