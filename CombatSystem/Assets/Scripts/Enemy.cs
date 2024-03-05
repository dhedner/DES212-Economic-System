using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxHitPoints = 100.0f;
    public float MoveSpeed = 0.1f;
    public float OptimalRange = 5.0f;

    [HideInInspector]
    public float HitPoints = 100.0f;

    [HideInInspector]
    public Player Target;

    [HideInInspector]
    public StatBar HealthBar;

    [HideInInspector]
    public List<EnemyAbility> Abilities;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar = transform.Find("EnemyHealth").GetComponent<StatBar>();
        Abilities = new List<EnemyAbility>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<EnemyAbility>() != null)
            {
                Abilities.Add(child.GetComponent<EnemyAbility>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }
    }

    public void PerformMovement()
    {
        if (HitPoints <= 0.0f || Target == null)
        {
            return;
        }

        float distanceToTarget = Mathf.Abs(transform.position.x - Target.transform.position.x);

        // If the target is within 80% to 100% of the optimal range, stop moving
        if (Mathf.Abs(distanceToTarget) <= OptimalRange && Mathf.Abs(distanceToTarget) >= OptimalRange * 0.8f)
        {
            return;
        }
        
        if (Mathf.Abs(distanceToTarget) < OptimalRange * 0.8f)
        {
            distanceToTarget -= distanceToTarget;
        }

        float newX = transform.position.x;
        if (distanceToTarget > 0.0f)
        {
            newX -= MoveSpeed * Time.deltaTime; // Change to SimControl instead of Time
        }
        else
        {
            newX += MoveSpeed * Time.deltaTime; // Change to SimControl instead of Time
        }

        // Clamp so the enemy won't move past the edge of the arena
        newX = Mathf.Clamp(newX, -8.0f, 8.0f);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public void Initialize()
    {
        HitPoints = MaxHitPoints;

        foreach (EnemyAbility ability in Abilities)
        {
            ability.ResetCooldown();
        }

        Target = GameObject.Find("Player").GetComponent<Player>();
        HealthBar.InterpolateImmediate(HitPoints / MaxHitPoints);
    }

    public bool UseRandomAbility()
    {
        if (HitPoints <= 0.0f || Target == null)
        {
            return false;
        }

        List<EnemyAbility> availableAbilities = new List<EnemyAbility>();
        foreach (EnemyAbility ability in Abilities)
        {
            if (ability.IsReady())
            {
                availableAbilities.Add(ability);
            }
        }

        if (availableAbilities.Count == 0)
        {
            return false;
        }

        int randomIndex = Random.Range(0, availableAbilities.Count);
        availableAbilities[randomIndex].Use();
        return true;
    }

    public bool TakeDamage(float damage)
    {
        if (damage != 0.0f)
        {
            //HitPoints = Mathf.Min(Mathf.Max(damage, 0.0f), HitPoints);
            HitPoints = Mathf.Clamp(HitPoints - damage, 0.0f, MaxHitPoints);
            HealthBar.InterpolateToScale(HitPoints / MaxHitPoints, 0.5f);
        }

        return (HitPoints <= 0.0f);
    }

}
