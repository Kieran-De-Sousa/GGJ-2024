using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCannon : Interactable
{

    public GameObject ammo;
    public Transform barrel;

    public float force;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Cannon Fired");

        // TODO: Add cooldown timer
        GameObject cannonBall = Instantiate(ammo, barrel.position, barrel.rotation);
        cannonBall.GetComponent<Rigidbody>().velocity = barrel.forward * force * Time.deltaTime;
    }
}
