﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats {
    Slider HPSlider;

    Animator anim;

    public int multiplier = 1;
    public int level = 1;
    int experience;

    public GameObject ExpPrefab;
    public GameObject CBTprefabs;
    public GameObject HealthSlider;

    PlayerStats playerStats;

    EnemyController EnemyController;
    CharacterCombat EnemyCombat;
    EnemyInteract EnemyInteract;



    protected override void Awake()
    {
        HPSlider = transform.Find("EnemyCanvas").Find("HealthSlider").GetComponent<Slider>();

        experience = (int)(5 * Mathf.Log(level+1, 1.1F) * multiplier);
        currentHealth = maxHealth = (int)((100 + 10 * level * Mathf.Sqrt(level)) * multiplier);
        damage.SetValue((int)((Mathf.Log(level+6,1.3F) * level / 2) *multiplier));


        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        // HealthSlider = transform.Find("HealthSlider").gameObject;

        anim = transform.Find("Cube").GetComponent<Animator>();

        EnemyCombat = gameObject.GetComponent<CharacterCombat>();
        EnemyController = gameObject.GetComponent<EnemyController>();
        EnemyInteract = gameObject.GetComponent<EnemyInteract>();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        InitCBT(amount.ToString(), CBTprefabs);
        float normalizedHealth = (currentHealth / (float)maxHealth);
        HPSlider.value = normalizedHealth;
        playerStats.combatCooldown = 5f;
    }

    void InitCBT(string text, GameObject Prefab)
    {
        GameObject temp = Instantiate(Prefab) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(transform.Find("EnemyCanvas"));
        tempRect.transform.localPosition = Prefab.transform.localPosition;
        tempRect.transform.localScale = Prefab.transform.localScale;
        tempRect.transform.localRotation = Prefab.transform.localRotation;

        temp.GetComponent<Text>().text = text;
    }

    public override void Die()
    {
        base.Die();

        anim.SetTrigger("Die");

        Destroy(HealthSlider, 0.5f);

        EnemyCombat.enabled = false;
        EnemyController.enabled = false;
        EnemyInteract.enabled = false;

        playerStats.Experience += experience;
        InitCBT("+" + experience.ToString() + " XP", ExpPrefab);

        Destroy(gameObject, 3);
    }
}