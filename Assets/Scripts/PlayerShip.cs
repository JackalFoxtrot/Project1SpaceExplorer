using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{

    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;
    [SerializeField] float _deathTimer = 10;

    [Header("Feedback")]
    [SerializeField] TrailRenderer _trail = null;

    public GameObject goToEnable;

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _trail.enabled = false;
    }

    private void FixedUpdate()
    {
        if (this.isActiveAndEnabled)
        {
            MoveShip();
            TurnShip();
            ParticleShip();
        }
    }

    void MoveShip()
    {
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;

        Vector3 moveDirection = transform.forward * moveAmountThisFrame;

        _rb.AddForce(moveDirection);
    }

    void TurnShip()
    {
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;

        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);

        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    void ParticleShip()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            goToEnable.SetActive(false);
        }
        else
        {
            goToEnable.SetActive(true);
        }
    }

    public void Kill()
    {
        Debug.Log("Player has been killed.");
        this.gameObject.SetActive(false);
    }

    public float getDeathTimer()
    {
        return _deathTimer;
    }

    public void SetSpeed(float speedChange)
    {
        _moveSpeed += speedChange;
    }

    public void SetBoosters(bool activateState)
    {
        _trail.enabled = activateState;
    }
}
