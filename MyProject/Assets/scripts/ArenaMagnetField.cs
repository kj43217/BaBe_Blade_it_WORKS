using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class ArenaMagnetField : NetworkBehaviour
{
    public float magneticForce;

    /*List<Rigidbody> players = new List<Rigidbody>();*/

    public GameObject Playermodel;


   

    Rigidbody trompo;
    Transform PlayerPosition;

    public GameObject arenaField;
    Transform Magnet;

    void Start()
    {
        Magnet = arenaField.GetComponent<Transform>();
        trompo = Playermodel.GetComponent<Rigidbody>();
        PlayerPosition = Playermodel.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        trompo.AddForce((Magnet.position - PlayerPosition.position) * magneticForce * Time.deltaTime);

       

    }
}
