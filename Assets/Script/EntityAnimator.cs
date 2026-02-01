using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    EntityBehaviour entity;
    Animator animator;
    SpriteRenderer sprite;
    string state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entity = transform.root.GetComponentInChildren<EntityBehaviour>();
        sprite = GetComponent<SpriteRenderer>();
        animator = transform.root.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float flip = entity.faceDirection.x<0 ? -1 : 1;
        transform.localScale = new Vector3(flip,1,1);
        if(entity.GetState()!=state)
        {
            state = entity.GetState();
            animator.CrossFadeInFixedTime(state,0.125f);
        }
        if(entity.grounded == true)
            animator.SetFloat("velocity",Mathf.Abs(entity.body.linearVelocity.x));
        if(entity.grounded == false)
            animator.SetFloat("velocity",entity.body.linearVelocity.y);
        animator.SetFloat("grounded",entity.grounded ? 10 : -10);
        //sprite.flipX = entity.faceDirection.x<0;   
    }
}
