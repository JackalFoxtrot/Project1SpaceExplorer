using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerShip : MonoBehaviour
{

    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;
    [SerializeField] float _deathTimer = 2;

    [Header("Feedback")]
    [SerializeField] TrailRenderer _trail = null;
    [SerializeField] UIController _uiController = null;
    [SerializeField] GameObject _uiControllerObj = null;
    [SerializeField] AudioClip _deathSound = null;
    [SerializeField] GameObject _visualsToDeactivate = null;
    [SerializeField] GameObject _visualsToActivate = null;

    Collider _colliderToDeactivate = null;

    public GameObject goToEnable;
    public GameObject _rocketModel;

    Rigidbody _rb = null;
    bool _rockets = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _colliderToDeactivate = GetComponent<Collider>();

        _trail.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_colliderToDeactivate.enabled == true)
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

    public void Powerups()
    {   
        float spawnDistance = 3;
        Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 spawnPosition = playerPosition + transform.forward*spawnDistance;
        
        Quaternion spawnRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        
        GameObject newRocket = GameObject.Instantiate(_rocketModel, spawnPosition, spawnRotation);
        newRocket.GetComponent<RocketScript>().enabled = true;
        newRocket.SetActive(true);
    }

    public void Kill()
    {
        Debug.Log("Player has been killed.");
        
        _uiController.SetText("You have died.");
        _uiControllerObj.SetActive(true);
        
        AudioHelper.PlayClip2D(_deathSound, 1);

        _visualsToDeactivate.SetActive(false);
        _visualsToActivate.SetActive(true);
        this.SetSpeed(0);
        _colliderToDeactivate.enabled = false;
    }

    public void Win()
    {
        Debug.Log("Player has won the game.");
        this.gameObject.SetActive(false);
    }

    public float getDeathTimer()
    {
        return _deathTimer;
    }
    public bool getRocketsBool()
    {
        return _rockets;
    }
    public void SetSpeed(float speedChange)
    {
        _moveSpeed += speedChange;
    }

    public void SetBoosters(bool activateState)
    {
        _trail.enabled = activateState;
    }
    public void SetRockets(bool activateState)
    {
        Debug.Log("Rockets set to: " + activateState);
        _rockets = activateState;
    }
}
