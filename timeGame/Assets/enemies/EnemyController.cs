
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator animator;

    [Header("Detection")]
    public float chaseRadius = 10f;
    public float attackRadius = 2f;

    [Header("Combat")]
    public float attackCooldown = 2f;
    public float chargeDuration = 0.8f;
    public float attackDuration = 0.7f;

    private NavMeshAgent agent;

    private bool isAttacking = false;
    private float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");

            if (obj != null)
                player = obj.transform;
        }
    }

    void Update()
    {
        if (player == null || !agent.isOnNavMesh)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // Let the attack sequence finish uninterrupted
        if (isAttacking)
        {
            FacePlayer();
            return;
        }

        // Outside detection range
        if (distance > chaseRadius)
        {
            StopMoving();
            return;
        }

        // Inside attack range
        if (distance <= attackRadius)
        {
            StopMoving();

            if (Time.time >= nextAttackTime)
                StartCoroutine(AttackSequence());

            return;
        }

        // Otherwise chase
        Chase();
    }

    void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        animator.SetBool("IsMoving", true);
    }

    void StopMoving()
    {
        agent.isStopped = true;
        animator.SetBool("IsMoving", false);
    }

    IEnumerator AttackSequence()
    {
        isAttacking = true;

        nextAttackTime = Time.time + attackCooldown;

        StopMoving();

        // Charge animation
        animator.ResetTrigger("Attack");
        animator.SetTrigger("AttackCharge");

        yield return new WaitForSeconds(chargeDuration);

        // Attack animation
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDuration);

        // End attack
        animator.ResetTrigger("AttackCharge");
        animator.ResetTrigger("Attack");

        isAttacking = false;

        // Let Update decide whether to chase or attack again
        if (player != null)
        {
            float distance = Vector3.Distance(
                transform.position,
                player.position
            );

            if (distance > attackRadius && distance <= chaseRadius)
                Chase();
            else
                StopMoving();
        }
    }

    void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotation,
                10f * Time.deltaTime
            );
        }
    }
}
