using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public float Cooldown = 1.0f;
    public float Damage = 1.0f;
    public float Range = 10.0f;
    public bool Active = true;

    [HideInInspector]
    public float CooldownRemaining = 0.0f;

    [HideInInspector]
    public StatBar CooldownBar;

    [HideInInspector]
    public TextMeshProUGUI AbilityNumber;

    [HideInInspector]
    public Player ParentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        ParentPlayer = transform.Find("Player").GetComponent<Player>();
        CooldownBar = transform.Find("Cooldown").GetComponent<StatBar>();
    }

    // Update is called once per frame
    void Update()
    {
        // Change to SimControl instead of Time
        CooldownRemaining = Mathf.Clamp(CooldownRemaining - Time.deltaTime, 0.0f, Cooldown);

        if (!Active || CooldownRemaining == 0.0f)
        {
            CooldownBar.InterpolateToScale(0.0f, 0.0f);
        }
        else
        {
            CooldownBar.InterpolateToScale(CooldownRemaining / Cooldown, 0.0f);
        }

        if (IsReady())
        {
            AbilityNumber.color = new Color(AbilityNumber.color.r, AbilityNumber.color.g, AbilityNumber.color.b, 1.0f);
        }
        else
        {
            AbilityNumber.color = new Color(AbilityNumber.color.r, AbilityNumber.color.g, AbilityNumber.color.b, 0.3f);
        }
    }

    public void ResetCooldown()
    {
        CooldownRemaining = Cooldown;
    }

    public float DistanceToTarget()
    {
        return Mathf.Abs(ParentPlayer.transform.position.x - ParentPlayer.Target.transform.position.x);
    }

    public bool IsReady()
    {
        // Add player at 0 hitpoints check
        if (!Active || ParentPlayer.HitPoints == 0.0f || ParentPlayer.Target == null || DistanceToTarget() > Range || CooldownRemaining > 0.0f)
        {
            return false;
        }

        return true;
    }

    public bool Use()
    {
        if (!IsReady())
        {
            return false;
        }

        // Apply damage to target (or negative damage if healing)
        // Add required flags for ability types (AoE, stun, etc.)

        CooldownRemaining = Cooldown;

        return false;
    }
}
