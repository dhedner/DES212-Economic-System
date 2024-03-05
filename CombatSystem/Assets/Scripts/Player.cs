using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHitPoints = 100.0f;
    public float MoveSpeed = 0.1f;
    public float OptimalRange = 5.0f;

    [HideInInspector]
    public float HitPoints = 100.0f;

    [HideInInspector]
    public Enemy Target;

    [HideInInspector]
    public StatBar HealthBar;

    [HideInInspector]
    public List<PlayerAbility> Abilities;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GameObject.Find("PlayerResources/HealthBar").GetComponent<StatBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseAbility(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseAbility(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseAbility(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseAbility(4);
        }
    }

    public void PerformMovement()
    {
        if (HitPoints <= 0.0f || Target == null)
        {
            return;
        }

        float newX = transform.position.x;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newX -= MoveSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            newX += MoveSpeed;
        }

        newX = Mathf.Clamp(newX, -10.0f, 10.0f);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public Enemy FindTarget()
    {
        var enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length == 0)
        {
            return null;
        }

        Enemy target = null;
        if (Target != null && Target.HitPoints > 0.0f)
        {
            target = Target;
        }
        
        float lowestHitPoints = float.MaxValue;
        if (target)
        {
            lowestHitPoints = target.HitPoints;
        }

        foreach (var enemy in enemies)
        {
            if (enemy.HitPoints <= 0.0f)
            {
                continue;
            }

            if (enemy.HitPoints > 0 && enemy.HitPoints < lowestHitPoints)
            {
                target = enemy;
                lowestHitPoints = enemy.HitPoints;
            }
        }

        return target;
    }

    public void Initialize()
    {
        transform.position = new Vector3(-GameplayController.StartingX, transform.position.y, transform.position.z);
        HitPoints = MaxHitPoints;

        foreach (var ability in Abilities)
        {
            ability.ResetCooldown();
        }

        Target = FindTarget();

        HealthBar.InterpolateImmediate(HitPoints / MaxHitPoints);
    }

    public bool UseAbility(int abilityNumber)
    {
        if (abilityNumber < 1 || abilityNumber > 4)
        {
            return false;
        }

        var ability = Abilities[abilityNumber - 1];
        return ability.Use();
    }

    public bool TakeDamage(float damage)
    {
        if (damage != 0.0f)
        {
            HitPoints = Mathf.Clamp(HitPoints - damage, 0.0f, MaxHitPoints);
            HealthBar.InterpolateToScale(HitPoints / MaxHitPoints, 0.5f);
        }

        return (HitPoints <= 0.0f);
    }
}
