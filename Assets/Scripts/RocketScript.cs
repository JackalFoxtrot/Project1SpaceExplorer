using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [Header("Rockets's Base Stats")]
    [SerializeField] float _moveSpeed = 20f;

    float _rocketDuration = 2;
    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        DelayHelper.DelayAction(this, DestroyRocket,_rocketDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
        HazardVolume hv = other.gameObject.GetComponent<HazardVolume>();
        if (playerShip != null)
        {
            
        }
        else if(hv != null)
        {
            hv.Stun();
            Object.Destroy(this.gameObject);
        }
        else
        {
            Object.Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (this.isActiveAndEnabled)
        {
            MoveRocket();
        }
    }
    void MoveRocket()
    {
        float moveAmountThisFrame = _moveSpeed;

        Vector3 moveDirection = transform.forward * moveAmountThisFrame;

        _rb.AddForce(moveDirection);
    }
    void DestroyRocket()
    {
        Object.Destroy(this.gameObject);
    }
}
