using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EntityBehaviour entity;
    public GameObject target;
    public LayerMask checkLayers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entity = GetComponent<EntityBehaviour>();
    }

    void CrazyBehavior()
    {
        entity.inputs.horizontal = Random.Range(-1.0f,1.0f);//Input.GetAxisRaw("Horizontal");
        entity.inputs.vertical = Random.Range(-1.0f,1.0f);
        entity.inputs.jump = Random.value < 0.5f;
        entity.inputs.baseAttack =  Random.value < 0.5f;
        entity.inputs.special1 =  Random.value < 0.5f;
        entity.inputs.special2 =  Random.value < 0.5f;
        //entity.inputs.interact = Mathf.Clamp(Input.GetAxisRaw("Vertical"),0,1)!=0;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if(entity.masked == false)
        {
            CrazyBehavior();
        }
        else
        {
            if(target != null)
            {

            }
        }
    }
    
}
