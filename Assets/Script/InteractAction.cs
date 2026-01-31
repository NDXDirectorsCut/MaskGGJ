using UnityEngine;

public class InteractAction : MonoBehaviour
{
    EntityBehaviour entity;
    public float interactRange;
    public LayerMask layers;

    // Start is called before the first frame update
    void Start()   
    {
        entity = GetComponentInChildren<EntityBehaviour>();
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
            if(interactObj != null);
            {
                interactObj.Interact(gameObject);
            }
        }
    }
}
