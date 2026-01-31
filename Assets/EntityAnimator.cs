using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    EntityBehaviour entity;
    SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entity = transform.root.GetComponentInChildren<EntityBehaviour>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.flipX = entity.faceDirection.x<0;   
    }
}
