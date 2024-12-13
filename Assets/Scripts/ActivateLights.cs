using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLights : MonoBehaviour
{
    [SerializeField] private GameObject _lightSpot;

    private void Start() {
        _lightSpot.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        _lightSpot.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {
        _lightSpot.SetActive(false);
    }
}
