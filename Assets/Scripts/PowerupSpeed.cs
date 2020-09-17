using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PowerupSpeed : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float _speedIncreaseAmount = 20;
    [SerializeField] float _powerupDuration = 5;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;
    [SerializeField] AudioClip _pickUp = null;
    [SerializeField] AudioClip _powerDown = null;

    Collider _colliderToDeactivate = null;
    bool _poweredUp = false;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        EnableObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();

        if (playerShip != null && _poweredUp == false)
        {
            AudioHelper.PlayClip2D(_pickUp,1);
            StartCoroutine(PowerupSquence(playerShip));
        }
    }

    IEnumerator PowerupSquence(PlayerShip playerShip)
    {
        _poweredUp = true;

        ActivatePowerup(playerShip);
        DisableObject();

        yield return new WaitForSeconds(_powerupDuration);

        DeactivatePowerUp(playerShip);
        AudioHelper.PlayClip2D(_powerDown, 1);
        EnableObject();

        _poweredUp = false;
    }

    void ActivatePowerup(PlayerShip playerShip)
    {
        if(playerShip != null)
        {
            playerShip.SetSpeed(_speedIncreaseAmount);
            playerShip.SetBoosters(true);
        }
    }

    void DeactivatePowerUp(PlayerShip playerShip)
    {
        playerShip?.SetSpeed(-_speedIncreaseAmount);
        playerShip?.SetBoosters(false);
    }

    public void DisableObject()
    {
        _colliderToDeactivate.enabled = false;
        _visualsToDeactivate.SetActive(false);
    }

    public void EnableObject()
    {
        _colliderToDeactivate.enabled = true;
        _visualsToDeactivate.SetActive(true);
    }
}
