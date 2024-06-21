using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private float underWaterDrag;
    [SerializeField] private float underWaterAngularDrag;
    [SerializeField] private float airDrag;
    [SerializeField] private float airAngularDrag;
    [SerializeField] private float waterPower;
    [SerializeField] private Transform[] floatPoints;

    private Rigidbody _rb;
    private bool _isUnderWater;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        int poinUnderWaterCount = 0;
        foreach (var point in floatPoints)
        {
            var diff = point.position.y - OcenManager.Instance.GetWaveHeight(point.position);
            if (diff < 0)
            {
                _rb.AddForceAtPosition(
                    Vector3.up * waterPower * Mathf.Abs(diff),
                    point.position,
                    ForceMode.Acceleration);
                poinUnderWaterCount++;
                if (!_isUnderWater)
                {
                    _isUnderWater = true;
                    Setstage(true);
                }
            }
        }
        if (_isUnderWater && poinUnderWaterCount == 0)
        {
            _isUnderWater = false;
            Setstage(underWater: false);
        }    
           
    }

    private void Setstage(bool underWater)
    {
        if (underWater)
        {
            _rb.drag = underWaterDrag;
            _rb.angularDrag = underWaterAngularDrag;
        }    
        else
        {
            _rb.drag = airDrag;
            _rb.angularDrag = airAngularDrag;
        }    
    }    
}
