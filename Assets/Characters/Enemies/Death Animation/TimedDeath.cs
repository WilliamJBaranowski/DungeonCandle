using UnityEngine;
using System.Collections;

public class TimedDeath : MonoBehaviour {
    private SceneController sc;
    private float startTimestamp;
    public float deathTime;

    // Use this for initialization
    void Start () {
        sc = GameObject.Find("Scene Controller").GetComponent<SceneController>();
        startTimestamp = sc.gameTime;
    }
    
    // Update is called once per frame
    void Update () {
        if (sc.gameTime >= startTimestamp + deathTime) {
            Destroy(gameObject);
        }
    }
}
