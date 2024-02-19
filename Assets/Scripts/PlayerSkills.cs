using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ZeusSkills
{
    Dash,
    Spear,
    Aoe
}

public enum KronosSkills
{
    JumpBoost,
    Strike,
    TimeSlow
}
public class PlayerSkills : MonoBehaviour
{
    public GameObject spearPrefab;
    [SerializeField] private GameObject rayCastPos;
    private Rigidbody rb;
    private Vector3 position;
    private float skillTime = 0f;
    [SerializeField] private float limitSkillTime = 1f;
    [SerializeField] private float kronosLimitSkillTime = 10f;
    private float normalLimitSkillTime = 1f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.2f;
    public Vector3 throwDirection;
    public Vector3 lastDirection;
    public float throwDistance = 1.2f;
    [SerializeField] private  float areaAttackRadius = 5f;
    [SerializeField] private  LayerMask enemyLayer;

    public bool skillActive = false;


    private void Update()
    {
        
        if (skillActive == true) 
        {
            skillTime = skillTime + Time.deltaTime;
        }
        if (skillTime >= limitSkillTime)
        {
            skillActive = false;
        }

        throwDirection = new Vector3(rb.velocity.normalized.x, 0f, rb.velocity.normalized.z);
        if (throwDirection != Vector3.zero)
        {
            lastDirection = throwDirection.normalized;
        }
    }

    // Zeus Skills
    private void Dash() 
    {
        //if (energy >= 1)
        {
            // energy - 1
            skillActive = true;
            position = transform.position;

            Vector3 rayDirection = rb.velocity.normalized;
            rayDirection.x = Input.GetAxis("Horizontal");

            RaycastHit hit;
            if (Physics.Raycast(rayCastPos.transform.position + rayDirection * 0.08f, rayDirection, out hit, dashDistance))
            {
            
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Walls"))
                {
                    Vector3 objetive = hit.point - (rayDirection * 0.2f);
                    StartCoroutine(MoveToPositionSmooth(objetive));

                    return;
                }
            }
            Vector3 destiny = new Vector3(position.x + (rayDirection.x * dashDistance), position.y, position.z);
            StartCoroutine(MoveToPositionSmooth(destiny));
            
        }
       
    }
    private IEnumerator MoveToPositionSmooth(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void SpearAttack() 
    {
        
        if (skillTime == 0f /*&& energy >= 2*/)
        {
            //energy - 2
            skillActive = true;
            // Obtiene la dirección de la velocidad del Rigidbody del jugador

            // Si el jugador no se está moviendo, utiliza la última dirección almacenada
            if (throwDirection == Vector3.zero)
            {
                throwDirection = lastDirection;
            }

            Vector3 spearDir = new Vector3(transform.position.x + (throwDirection.x * throwDistance), transform.position.y +0.7f, transform.position.z);
            Vector3 spearPosition = transform.position + (throwDirection * throwDistance);


            // Crea una lanza en la posición ajustada
            GameObject spear = Instantiate(spearPrefab, spearDir, Quaternion.identity);
            // Establece la dirección de la lanza según la dirección del jugador
            
            //spear.GetComponent<Spear>().SetDirection(throwDirection); (crear script Spear)


        }

    }
    

    private void AoE() 
    {
        if (skillTime == 0f /*&& energy >= 3*/)
        {
            // energy -3
            skillActive = true;
            Invoke(nameof(AoeDamage), 0.2f);

        }
    }

    private void AoeDamage()
    {
        Vector3 playerPosition = transform.position;
        float damage = 2f;
        Debug.Log("Daño");
        // Encuentra todos los colisionadores de enemigos en el rango especificado
        Collider[] hitColliders = Physics.OverlapSphere(playerPosition, areaAttackRadius, enemyLayer);
        // Itera a través de los colisionadores para realizar acciones
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy") || col.CompareTag("SubBoss") || col.CompareTag("Boss"))
            {
                EnemyController enemy = col.GetComponent<EnemyController>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
    
    // Kronos Skills
    private void JumpBoosted() 
    {
        if (skillTime == 0f)
        {
            skillActive = true;
            //boost jump force
        }
    }

    private void Strike()
    {
        if (skillTime == 0f /*&& energy >= 2*/)
        {
            //noop
        }
    }

    private void TimeSlow()
    {
        if (skillTime == 0f /*&& energy >= 3*/)
        {
            // energy -3
            skillActive = true;
            //noop
        }
        else if (skillActive)
        {
            skillActive = false;
            //ResetSkill();
        }
    }
}
