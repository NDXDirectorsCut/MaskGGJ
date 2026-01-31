using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAction : MonoBehaviour
{
    EntityBehaviour entity;
    public float speed = 5;
    public float startAccel = 25;
    public float startDecel = 25;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInChildren<EntityBehaviour>();
    }

    void Move(float force, float hor, float deltaTime)
    {
        float acceleration;
        float currentSpeed = entity.body.linearVelocity.x;
        if(currentSpeed*hor>0)
            acceleration = (1-(Mathf.Abs(currentSpeed)/speed)) * force;
        else
            acceleration = force;

        entity.body.linearVelocity += Vector2.Perpendicular(-entity.normal) * hor * acceleration * deltaTime;
    }

    void Decelerate(float force, float deltaTime)
    {
        float deceleration = entity.body.linearVelocity.x/speed * force;
        entity.body.linearVelocity -= Vector2.Perpendicular(-entity.normal) * deceleration * deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(entity.GetState() == "Idle")
        {
            if(entity.grounded == true)
            {
                if(entity.inputs.horizontal!=0)
                {
                    Move(startAccel,entity.inputs.horizontal,Time.fixedDeltaTime);
                }
                else
                {
                    Decelerate(startDecel, Time.fixedDeltaTime);
                }
            }
            else
            {
                if(entity.inputs.horizontal!=0)
                {
                    Move(startAccel/2,entity.inputs.horizontal,Time.fixedDeltaTime);
                }
                else
                {
                    Decelerate(startDecel/10, Time.fixedDeltaTime);
                }
            }
        }
    }
}
