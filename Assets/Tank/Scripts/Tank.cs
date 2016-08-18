using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

    [SerializeField]
    private int playerID;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private string forward;
    [SerializeField]
    private string backward;
    [SerializeField]
    private string left;
    [SerializeField]
    private string right;
    [SerializeField]
    private string fire;

    [SerializeField]
    private Bullet bulletPrefab;

    private int health;
    private TankManager tankManager;
    private const float movementSpeed = 1f;
    private const float rotationSpeed = 70f;
    private const int cooldown = 100;
    private const float bulletSpeed = 500f;
    private bool moveForward;
    private bool moveBackward;
    private bool rotateLeft;
    private bool rotateRight;
    private bool canFire;
    private int fireCD;

    public void Initialize(TankManager tankManager) {
        this.tankManager = tankManager;
    }

    public void takeDamage(int amount, int player) {
        health -= amount;
        if (health > 0)
            tankManager.changeScore(amount, player);
        else {
            tankManager.changeScore(amount + health, player);
            health = 5; //TODO: remove
        }
    }

    public int getHealth() {
        return health;
    }

	// Use this for initialization
	void Start () {
        health = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        checkMovement();
        move();
        if (checkFire())
            fireBullet();
	}

    private void checkMovement() {
        if (Input.GetKeyDown(forward))
            moveForward = true;
        else if (Input.GetKeyUp(forward))
            moveForward = false;
        if (Input.GetKeyDown(backward))
            moveBackward = true;
        else if (Input.GetKeyUp(backward))
            moveBackward = false;
        if (Input.GetKeyDown(left))
            rotateLeft = true;
        else if (Input.GetKeyUp(left))
            rotateLeft = false;
        if (Input.GetKeyDown(right))
            rotateRight = true;
        else if (Input.GetKeyUp(right))
            rotateRight = false;
    }

    private void move() {
        if (rotateLeft)
            transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
        if (rotateRight)
            transform.Rotate(new Vector3(0f, 0f, -rotationSpeed * Time.deltaTime));
        if (moveForward)
            transform.position += transform.up * Time.deltaTime * movementSpeed;
        if (moveBackward)
            transform.position -= transform.up * Time.deltaTime * movementSpeed;
        if ((moveForward || moveBackward) && GetComponent<Rigidbody2D>().velocity.magnitude < 1)
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private bool checkFire() {
        if (Input.GetKeyDown(fire))
            canFire = true;
        else if (Input.GetKeyUp(fire))
            canFire = false;
        if (fireCD > 0) {
            fireCD--;
            return false;
        }
        return canFire;
    }

    private void fireBullet() {
        fireCD = cooldown;
        Bullet b = (Bullet)Instantiate(bulletPrefab, transform.position + transform.up * .5f, transform.rotation);
        b.Initialize(playerID);
        b.fire(bulletSpeed);
        b.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
    }
}
