using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    EntityBehaviour entity;
    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInChildren<EntityBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        entity.inputs.horizontal = Input.GetAxisRaw("Horizontal");
        entity.inputs.vertical = Input.GetAxisRaw("Vertical");
        entity.inputs.jump = Input.GetButton("Jump");
        entity.inputs.baseAttack = Input.GetButton("Fire1");
        entity.inputs.special1 = Input.GetButton("Fire2");
        entity.inputs.special2 = Input.GetButton("Fire3");
        entity.inputs.interact = Mathf.Clamp(Input.GetAxisRaw("Vertical"),0,1)!=0;
    }
}
