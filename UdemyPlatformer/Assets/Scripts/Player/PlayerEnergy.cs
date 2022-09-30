using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private float maxEnergy;
    [SerializeField] private float recoveryRate;

    private float _currentEnergy;
    private bool _energyCheck;
    private void Start()
    {
        _currentEnergy = maxEnergy;
    }
    private void FixedUpdate()
    {
        RegenerationEnergy();
    }
    public void ReduceEnergy(float energyCosts)
    {
            _currentEnergy -= energyCosts;
    }
    public bool EnergyCheck(float energyCosts)
    {
        if (_currentEnergy < energyCosts)
            _energyCheck = false;
        else
            _energyCheck = true;
        return _energyCheck;
    }
    private void RegenerationEnergy()
    {
        if (_currentEnergy < maxEnergy)
        {
            _currentEnergy += recoveryRate;
            if (_currentEnergy > maxEnergy)
            {
                _currentEnergy = maxEnergy;
            }
        }

    }
}
