using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskBehavior : Interactable
{
    public EntityStats modifiedStats;
    bool modified = false;
    public GameObject attachedObject;
    EntityBehaviour attachedEntity;
    public LayerMask layers;
    public float yOffset;
    [Header("Sway")]
    public float swaySpeed;
    public float swayDistance;
    Transform sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = transform.GetChild(0);
    }

    void SwayAnim(float speed, float distance)
    {
        float sin = Mathf.Sin(Time.time * speed) * distance;
        sprite.position = transform.position + Vector3.up * sin;
    }

    public override void Interact(GameObject interactor)
    {
        if(canInteract)
            attachedObject = interactor;
    }

    void ModifyStats(EntityStats destination)
    {
        destination.health = modifiedStats.health;
        destination.maxStamina = modifiedStats.maxStamina;
        destination.activeStamina = modifiedStats.activeStamina;
        destination.baseSpeed = modifiedStats.baseSpeed;
        destination.accel = modifiedStats.accel;
        destination.decel = modifiedStats.decel;
        destination.sprintSpeed = modifiedStats.sprintSpeed;
        destination.baseJump = modifiedStats.baseJump;
        destination.addJump = modifiedStats.addJump;
        destination.baseDamage = modifiedStats.baseDamage;
        destination.attackCooldown = modifiedStats.attackCooldown;
        destination.knockback = modifiedStats.knockback;
    }

    // Update is called once per frame
    void Update()
    {
        if(attachedObject == null)
        {
            transform.parent = null;
            RaycastHit2D hit = Physics2D.Raycast(transform.position,-Vector2.up,100,layers);
            if(hit.collider!=null)
            {
                Vector2 objectPos = hit.point + Vector2.up * yOffset;
                Vector3 moveDir = new Vector3(objectPos.x, objectPos.y, 0) - transform.position;
                transform.position += moveDir.normalized * moveDir.magnitude * swaySpeed * Time.deltaTime;
                SwayAnim(swaySpeed,swayDistance);
                canInteract = true;
            }
        }
        else
        {
            sprite.position = transform.position;
            transform.parent = attachedObject.transform; 
            canInteract = false;
            if(attachedEntity == null)
            {
                attachedEntity = attachedObject.transform.root.GetComponentInChildren<EntityBehaviour>();
                modified = false;
            }
            else
            {
                if(modified == false)
                {
                    modified = true;
                    ModifyStats(attachedEntity.stats);
                    attachedEntity.masked = true;
                    attachedObject = attachedEntity.maskAttach;
                    transform.parent = attachedObject.transform;
                    transform.position = attachedObject.transform.position;
                }
                if(attachedEntity.stats.health == 0)
                {
                    attachedObject = null;
                    attachedEntity.stats = new EntityStats();
                    attachedEntity.masked = false;
                    attachedEntity = null;
                }
            }
        }
    }
}
