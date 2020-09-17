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
        StartCoroutine(DestroyRocket());
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
            hv.setStun(true);
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
    IEnumerator DestroyRocket()
    {
        yield return new WaitForSeconds(_rocketDuration);
        Object.Destroy(this.gameObject);
    }
}
