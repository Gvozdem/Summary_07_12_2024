using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLastProvidence : MonoBehaviour
{

    [SerializeField] private GameObject _lightSpot;
    [SerializeField] private GameObject _lastProvidence;

    private void Start() {
        _lightSpot.SetActive(false);
        _lastProvidence.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        _lightSpot.SetActive(true);
        _lastProvidence.SetActive(true);
    }
}
