﻿using UnityEngine;
using System.Collections;

public class Vitality : MonoBehaviour {
    private Animator anim;
    private SceneController sc;

    public int maxLife;
    public int currentLife;

    private float deathTimestamp;
    private bool dying;
    public float deathTime;

    private float hitTimestamp;
    public float flinchTime;

    public bool deathOnTerrainTouch;

    public GameObject deathObject;

    void Awake () {
        anim = GetComponent<Animator>();
        hitTimestamp = -1 * flinchTime;
    }

    // Use this for initialization
    void Start () {
        sc = GameObject.Find("Scene Controller").GetComponent<SceneController>();
    }
    
    void FixedUpdate () {
        if (dying == true && sc.gameTime >= deathTimestamp + deathTime) {
            Die();
        }
    }

    void OnTriggerEnter (Collider other) {
        if (deathOnTerrainTouch == true && other.gameObject.tag == "Terrain") {
            StartDeath();
        }
    }

    public void TakeDamage (int amount) {
        if (sc.gameTime >= hitTimestamp + flinchTime && dying == false) {

            hitTimestamp = sc.gameTime;
            currentLife = Mathf.Max(currentLife - amount, 0);

            if (currentLife <= 0) {
                StartDeath();
            } else {
                Flinch();
            }
        }
    }

    void StartDeath () {
        dying = true;
        anim.SetTrigger("die");
        deathTimestamp = sc.gameTime;
        foreach (Transform child in transform) {
            if (child.gameObject.name.Contains("Hitbox")) {
                Destroy(child.gameObject);
            }
        }

        if (deathObject != null) {
            Instantiate(deathObject, transform.position, Quaternion.identity);
        }
    }

    void Flinch () {
        
    }

    void Die () {     
        Destroy(gameObject);
    }
}
