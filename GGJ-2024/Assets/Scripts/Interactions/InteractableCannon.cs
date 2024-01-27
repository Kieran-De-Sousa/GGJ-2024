using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCannon : Interactable
{

    public GameObject ammo;
    public Transform barrel;
    public float ammoLifespan;

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

    public override void Interact(TestPlayerScript initiator)
    {
        Debug.Log("Cannon Fired");

        // TODO: Add cooldown timer
        GameObject cannonBall = Instantiate(ammo, barrel.position, barrel.rotation);
        cannonBall.GetComponent<Rigidbody>().velocity = barrel.forward * force * Time.deltaTime;
        cannonBall.GetComponent<ComedicActionHit>().Initiator = initiator;
        AudioPlaySettings playSettings = AudioPlaySettings.Default;
        playSettings.Position = transform.position;
        AudioManager.Instance.PlayEffect(AudioID.CannonFire, AudioMixerID.SFX, playSettings);
        Destroy(cannonBall, ammoLifespan);
    }
}
