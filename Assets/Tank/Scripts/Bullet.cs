using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int damage;

    private int player;

    public void Initialize(int player) {
        this.player = player;
    }

    public void fire(float movementSpeed) {
        GetComponent<Rigidbody2D>().AddForce(transform.up * movementSpeed * GetComponent<Rigidbody2D>().mass);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<Tank>().takeDamage(damage, player);
        Destroy(this.gameObject);
    }
}
