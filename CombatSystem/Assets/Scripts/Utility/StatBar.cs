using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBar : MonoBehaviour
{
    public bool Rotate = false;

    private float _maxRealValue;
    private Vector3 _originalPosition;
    private float _currentScale = 0.0f;
    private float _targetScale = 1.0f;
    private float _scaleTime = 0.0f;
    private float _interpolationTime = 0.0f;

    void Start()
    {
        _maxRealValue = transform.localScale.x;
        _originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (_interpolationTime > 0.0f)
        {
            _interpolationTime =Mathf.Clamp(_interpolationTime - Time.deltaTime, 0.0f, 1.0f);

            if (Rotate)
            {
                SetScaleRotated(Mathf.Lerp(_currentScale, _targetScale, 1.0f - (_interpolationTime / _scaleTime)));
            }
            else
            {
                SetScaleStandard(Mathf.Lerp(_currentScale, _targetScale, 1.0f - (_interpolationTime / _scaleTime)));
            }
        }
    }

    void SetScaleStandard(float value)
    {
        transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
        float positionAdjustment = (_maxRealValue - value) / 2.0f;
        transform.localPosition = new Vector3(_originalPosition.x - positionAdjustment, _originalPosition.y, _originalPosition.z);
    }

    public void SetScaleRotated(float value)
    {
        transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
        float positionAdjustment = (_maxRealValue - value) / 2.0f;
        transform.localPosition = new Vector3(_originalPosition.x, _originalPosition.y - positionAdjustment, _originalPosition.z);
    }

    public void InterpolateToScale(float percent, float time)
    {
        _currentScale = transform.localScale.x;
        _targetScale = percent * _maxRealValue;
        _scaleTime = time + 0.001f;
        _interpolationTime = time + 0.001f;
    }

    public void InterpolateImmediate(float percent)
    {
        _currentScale = transform.localScale.x;
        _targetScale = percent * _maxRealValue;
        _scaleTime = 0.0f;
        _interpolationTime = 0.0f;

        if (Rotate)
        {
            SetScaleRotated(_targetScale);
        }
        else
        {
            SetScaleStandard(_targetScale);
        }
    }
}
