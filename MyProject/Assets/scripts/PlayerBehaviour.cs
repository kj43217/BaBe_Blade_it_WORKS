using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBehaviour : NetworkBehaviour
{
    //Manages the velocity of the player per coordinate
    private Vector3 playerVelocity = Vector3.zero;

    //Value to easily change the player's speed
    public float speed = 10;
    public float magneticForce;

    public GameObject Playermodel;
    public GameObject arenaField;

    Rigidbody Trompo;

    Transform PlayerPosition;
    Transform Magnet;


    void Start()
    {
        Trompo = Playermodel.GetComponent<Rigidbody>();
        Magnet = arenaField.GetComponent<Transform>();
        PlayerPosition = Playermodel.GetComponent<Transform>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            Vector3 direction = new Vector3(0, 0, 0);

            //Player movement controller
            if (Input.GetKey("right"))
            {
                direction = new Vector3(1, 0, 0);
            }
            if (Input.GetKey("left"))
            {
                direction = new Vector3(-1, 0, 0);
            }
            if (Input.GetKey("up"))
            {
                direction = new Vector3(0, 0, 1);
            }
            if (Input.GetKey("down"))
            {
                direction = new Vector3(0, 0, -1);
            }
            if (Input.GetKey("up") && Input.GetKey("right"))
            {
                direction = new Vector3(1, 0, 1);
            }
            if (Input.GetKey("up") && Input.GetKey("left"))
            {
                direction = new Vector3(-1, 0, 1);
            }
            if (Input.GetKey("down") && Input.GetKey("right"))
            {
                direction = new Vector3(1, 0, -1);
            }
            if (Input.GetKey("down") && Input.GetKey("left"))
            {
                direction = new Vector3(-1, 0, -1);
            }
            SmoothMovement(direction);
    }
}
    void FixedUpdate()
    {
        Trompo.AddForce((Magnet.position - PlayerPosition.position) * magneticForce * Time.deltaTime);
    }

    //This function manages the player's movement
    private void SmoothMovement(Vector3 direction)
    {
        //This variable sets the speed to the desired direction
        Vector3 desiredVelocity = direction.normalized * speed;

        //This variable pushes the player to the desired direction
        Vector3 steeringForce = desiredVelocity - playerVelocity;
        playerVelocity += steeringForce * Time.deltaTime;
        transform.position += playerVelocity * Time.deltaTime;
    }

    void OnTriggerExit(Collider other)
    {
        //Si el player sale del arena eliminara el control del player sobre el trompo.
        Playermodel.GetComponent<PlayerBehaviour>().enabled = false;

        if (other.CompareTag("Player"))
        {
            magneticForce = 0;
            Trompo.velocity = Vector3.zero;
            Trompo.angularVelocity = Vector3.zero;
            playerVelocity = Vector3.zero;
        }
    }
}
