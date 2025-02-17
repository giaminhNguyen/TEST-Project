using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 5f; // Speed of the player movement
    private CharacterController controller; // Reference to the CharacterController component

    [SerializeField] private List<GameObject> projectileToShootList;
    void Start()
    {
        // Get the CharacterController component attached to the GameObject
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction based on input
        Vector3 movementDirection = new Vector3(-verticalInput, 0f, horizontalInput).normalized;

        // Move the player
        if (movementDirection != Vector3.zero)
        {
            // Rotate the player to face the direction of movement
            transform.rotation = Quaternion.LookRotation(movementDirection);

            // Move the player using CharacterController
            controller.Move(movementDirection * speed * Time.deltaTime);
        }

        // Check for attack input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            Attack(0);
        }
        else if ( Input.GetKeyDown(KeyCode.Alpha2))
        {
            Attack(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Attack(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Attack(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Attack(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Attack(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Attack(6);
        }
    }

    void Attack(int key)
    {
        // spawn projectile

        Instantiate(projectileToShootList[key], transform.position,Quaternion.identity);
    }
}
