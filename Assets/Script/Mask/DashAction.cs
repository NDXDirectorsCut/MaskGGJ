using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class DashAction : EntityBehaviour
{
    EntityBehaviour entity;
    bool canDash = true;
    float baseDash = 30;
    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInChildren<EntityBehaviour>();
    }

    IEnumerator Dash(float dashForce)
    {
        canDash = false;
        Vector2 vectorDirection = new Vector2(Mathf.Round(entity.inputs.horizontal), Mathf.Clamp(entity.inputs.vertical,0,1) ).normalized;
        entity.body.linearVelocity += vectorDirection * dashForce;
        yield return new WaitForSeconds(1f);
        canDash = true;
        yield return null;
    }

    void Update()
    {
        if (entity.GetState() == "Idle")
        {
            if (entity.inputs.special1 && canDash == true)
            {
                StartCoroutine(Dash(baseDash));
            }
        }
    }
}