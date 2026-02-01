using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorBehavior : Interactable
{
    public Transform aPoint;
    public Transform bPoint;

    public bool onB = false;
    public float speed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    IEnumerator Move(Vector3 targetPos)
    {
        float distance = 100;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 3);
        while (distance>0.1f)
        {
            Vector3 moveDir = targetPos - transform.position;
            distance = moveDir.magnitude;
            foreach(Collider2D hit in hits)
            {
                //hit.transform.root.position += moveDir.normalized * speed * Time.fixedDeltaTime;
            }
            transform.position += moveDir.normalized * speed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        onB = !onB;
    }

    public override void Interact(GameObject interactor)
    {
        Vector3 targetPos = onB ? aPoint.position : bPoint.position;
        StartCoroutine(Move(targetPos));
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
