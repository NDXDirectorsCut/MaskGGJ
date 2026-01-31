using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackAction : MonoBehaviour
{
    EntityBehaviour entity;
    bool canAttack = true;
    public float offset;
    public float hitRadius;
    public LayerMask hitLayers;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInChildren<EntityBehaviour>();
    }

    IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(entity.stats.attackCooldown);
        canAttack = true;
    }

    IEnumerator DrawCircle(Vector2 pos, float length)
    {
        float time = 0;
        while(time<length)
        {
            Debug.DrawRay(pos, Vector2.up*hitRadius,Color.yellow);
            Debug.DrawRay(pos,-Vector2.up*hitRadius,Color.yellow);
            Debug.DrawRay(pos, Vector2.right*hitRadius,Color.yellow);
            Debug.DrawRay(pos,-Vector2.right*hitRadius,Color.yellow);
            time+= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    void Hit(EntityBehaviour hitEntity)
    {
        Rigidbody2D hitBody = hitEntity.transform.root.GetComponentInChildren<Rigidbody2D>();
        Vector2 knockDir = entity.faceDirection + new Vector2(0,0.5f);
        hitBody.linearVelocity = knockDir.normalized * entity.stats.knockback;
        StunAction stun = hitEntity.GetComponent<StunAction>();
        if(stun!=null)
        {
            StartCoroutine(stun.Stun());
        }
        hitEntity.stats.health -= entity.stats.baseDamage;
    }

    IEnumerator Jab()
    {
        if(canAttack == true)
        {
            StartCoroutine(Cooldown());
            Collider2D[] colliders = Physics2D.OverlapCircleAll(entity.body.position
                +entity.faceDirection.normalized*offset,
                hitRadius,
                hitLayers);
            StartCoroutine(DrawCircle(entity.body.position+entity.faceDirection.normalized*offset,2));
            foreach(Collider2D entityCol in colliders)
            {
                if(entityCol.transform.root != transform.root)
                {
                    EntityBehaviour hitEntity = entityCol.transform.root.GetComponentInChildren<EntityBehaviour>();
                    if(hitEntity!=null)
                    {
                        Hit(hitEntity);
                    }
                }
            }
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

        if(entity.GetState() == "Idle")
        {
            if(entity.inputs.baseAttack)
            {
                StartCoroutine(Jab());
            }
        }
    }
}
