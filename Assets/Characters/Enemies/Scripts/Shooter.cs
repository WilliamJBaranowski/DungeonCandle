using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
    private Animator anim;
    private SpriteRenderer sr;

    private SceneController sc;

    public GameObject bulletObject;
    public Vector3 sourcePoint;
    public float initialCooldown;
    public float windup;
    public float cooldown;
    // Set cooldown to 0 to make object only fire when FireBullet is explicitly called by some other behaviour

    private float shootTimestamp = 0f;

    void Awake () {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        sc = GameObject.Find("Scene Controller").GetComponent<SceneController>();
        shootTimestamp = sc.gameTime - cooldown + initialCooldown;
    }
    
    // Update is called once per frame
    void Update () {
        if (cooldown > 0f && sc.gameTime >= shootTimestamp + cooldown) {
            FireBullet();
        }
    }

    public void FireBullet () {
        anim.SetTrigger("shoot");
        if (sr.flipX == true) {
            sourcePoint = new Vector3(-1 * Mathf.Abs(sourcePoint.x), sourcePoint.y, sourcePoint.z);
        } else {
            sourcePoint = new Vector3(Mathf.Abs(sourcePoint.x), sourcePoint.y, sourcePoint.z);
        }

        shootTimestamp = sc.gameTime;
        StartCoroutine(CreateBullet());
    }

    private IEnumerator CreateBullet () {
        yield return new WaitForSeconds(windup);

        GameObject bullet = Instantiate(bulletObject, transform.position + sourcePoint, Quaternion.identity) as GameObject;
    }
}
