﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupRocket : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float _powerupDuration = 30;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;
    [SerializeField] AudioClip _pickUp = null;
    [SerializeField] AudioClip _powerDown = null;

    Collider _colliderToDeactivate = null;
    bool _poweredUp = false;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();
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
        if (playerShip != null)
        {
            playerShip.SetRockets(true);
        }
    }

    void DeactivatePowerUp(PlayerShip playerShip)
    {
        playerShip?.SetRockets(false);
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
