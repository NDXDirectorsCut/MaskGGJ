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

    void Move(float hor, float deltaTime)
    {
        float acceleration;
        float currentSpeed = entity.body.velocity.x;
        if(currentSpeed*hor>0)
            acceleration = (1-(Mathf.Abs(entity.body.velocity.x)/speed)) * startAccel;
        else
            acceleration = startAccel;

        entity.body.velocity += new Vector2(hor*acceleration,0) * deltaTime;
    }

    void Decelerate(float deltaTime)
    {
        float deceleration = entity.body.velocity.x/speed * startDecel;
        entity.body.velocity -= new Vector2(deceleration,0) * deltaTime;
    }

    void AirMove(float deltaTime)
    {
        
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
                    Move(entity.inputs.horizontal,Time.fixedDeltaTime);
                }
                else
                {
                    Decelerate(Time.fixedDeltaTime);
                }
            }
            else
            {

            }
        }
    }
}
