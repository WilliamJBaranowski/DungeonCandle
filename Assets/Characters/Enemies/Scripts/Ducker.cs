using UnityEngine;
using System.Collections;

public class Ducker : MonoBehaviour {
    private SpriteRenderer sr;
    private Animator anim;
    private BoxCollider col;
    private PlayerController pc;
    private SceneController sc;

    public float duckedWaitTime;
    public float riseTime;
    public float risenWaitTime;
    public float duckTime;

    public bool invulnWhenDucked;

    private float phaseTimestamp = 0f;
    private int phase = 0;
    // phase = 0 means ducked
    //       = 1 means rising
    //       = 2 means risen
    //       = 3 means ducking

    void Awake () {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Use this for initialization
    void Start () {
        sc = GameObject.Find("Scene Controller").GetComponent<SceneController>();
    }
    
    // Update is called once per frame
    void Update () {
        if (phase == 0 && sc.gameTime >= phaseTimestamp + duckedWaitTime) {
            col.enabled = true;
            anim.SetTrigger("rise");
            phase = 1;
            phaseTimestamp = sc.gameTime;
        } else if (phase == 1 && sc.gameTime >= phaseTimestamp + riseTime) {
            phase = 2;
            phaseTimestamp = sc.gameTime;
        } else if (phase == 2 && sc.gameTime >= phaseTimestamp + risenWaitTime) {
            anim.SetTrigger("duck");
            phase = 3;
            phaseTimestamp = sc.gameTime;
        } else if (phase == 3 && sc.gameTime >= phaseTimestamp + duckTime) {
            if (invulnWhenDucked == true) {
                col.enabled = false;
            }
            phase = 0;
            phaseTimestamp = sc.gameTime;
        }

        if (pc.transform.position.x <= transform.position.x && sr.flipX == true) {
            TurnAround();
        } else if (pc.transform.position.x > transform.position.x && sr.flipX == false) {
            TurnAround();
        }
    }

    void TurnAround () {
        sr.flipX = !sr.flipX;

        foreach (Transform child in transform) {
            child.localPosition = new Vector3(-1 * child.localPosition.x, child.localPosition.y, child.localPosition.z);
        }
    }
}
