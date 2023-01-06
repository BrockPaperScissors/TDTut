using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public int standardDamage = 50;
    public float critChance = .2f;
    public float critDamageMultiplier = 1.5f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    private bool isCrit = false;
    public float critRoll {get; private set;}

    public void Seek (Transform _target) 
    {
        target = _target;
    }

  

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {   
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget ()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Destroy (effectIns, 5f); 

        if (explosionRadius > 0f)
        {
            Explode();
            Destroy (effectIns, 5f); 
        }else 
        {  
            Damage(target);
            Destroy(gameObject);
            Debug.Log("hit");
        }
       
        Destroy(effectIns);
        
    }

    void Explode ()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
                Destroy(gameObject);
            }
        }
    }

    void Damage (Transform enemy)
    {
        EnemyMovement e = enemy.GetComponent<EnemyMovement>();

        critRoll = Random.Range(0f, 1f);
        // Debug.Log("Dealing Damage");

        if (critChance >= critRoll)
        {
            isCrit = true;
        }
        

        if (e != null)
        {
            if (isCrit)
            {
                e.TakeDamage(standardDamage * critDamageMultiplier);
                Debug.Log("is crit");
            }
            else 
            {
                e.TakeDamage(standardDamage);
                Debug.Log("not crit");
            }
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
