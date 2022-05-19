using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ArenaMagnetField : NetworkBehaviour
{
    public float magneticForce;

    public GameObject arenaField;
    public GameObject Playermodel;

    Rigidbody trompo;

    Transform PlayerPosition;
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

    /*void OnTriggerExit(Collider other)
    {
        //Aqui se tomara en cuenta si algun player sale del arena y la fuerza magnetica se dejara de aplicar y lo detendra.
        if (other.CompareTag("Player"))
        {
            magneticForce = 0;
            trompo.velocity = Vector3.zero;
            trompo.angularVelocity = Vector3.zero;
        }
    }*/
}
