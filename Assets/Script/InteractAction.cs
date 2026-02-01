using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractAction : MonoBehaviour
{
    EntityBehaviour entity;
    public float interactRange;
    public float cooldown = 0.5f;
    bool canInteract = true;
    public LayerMask layers;

    // Start is called before the first frame update
    void Start()   
    {
        entity = GetComponentInChildren<EntityBehaviour>();
    }

    IEnumerator Cooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(cooldown);
        canInteract = true;
    }

    Interactable GetInteractable()
    {
        float distance = interactRange+1;
        Interactable found = null;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,interactRange,layers);
        foreach(Collider2D interCol in hits)
        {
            if(interCol.gameObject.GetComponent<Interactable>() != null)
            {
                if(Vector3.Distance(transform.position,interCol.transform.position)<distance)
                {
                    found = interCol.gameObject.GetComponent<Interactable>();
                }
            }
        }
        return found;
    }

    // Update is called once per frame
    void Update()
    {
        if(entity.inputs.interact)
        {
            Interactable interactObj = GetInteractable();
            if(interactObj != null && canInteract == true)
            {
                StartCoroutine(Cooldown());
                interactObj.Interact(gameObject);
            }
        }
    }
}
