using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private Transform _vehicle;
    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;
    [SerializeField] private float _moveSpeed;
    private float _journeyLength;
    private float _startTime;
    private Transform _target;
    private bool _changeTarget;
    private Vector3 _currentPosition;
    private void Start()
    {
        Vector3 firsPos = _leftTarget.position;
        firsPos.y = _vehicle.transform.position.y;
        _vehicle.transform.position = firsPos;
        _journeyLength = Vector3.Distance(_leftTarget.position,_rightTarget.position);
    }
    private void Update()
    {
#if UNITY_EDITOR
        SetInputTurn();
#endif
       MoveCar();
    }
    private void SetInputTurn()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _changeTarget = true;
            _target = _leftTarget;
            _startTime = Time.time;
            _currentPosition = _vehicle.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _changeTarget = true;
            _target = _rightTarget;
            _startTime = Time.time;
            _currentPosition = _vehicle.transform.position;
        }

        
    }

    public void OnClickLeft()
    {
        _changeTarget = true;
        _target = _leftTarget;
        _startTime = Time.time;
        _currentPosition = _vehicle.transform.position;
    }

    public void OnClickRight()
    {
        _changeTarget = true;
        _target = _rightTarget;
        _startTime = Time.time;
        _currentPosition = _vehicle.transform.position;
    }
    private void MoveCar()
    {
        if (_changeTarget)
        {
            float distanceCovered = (Time.time - _startTime) * _moveSpeed;
            float fractionOfJourney = distanceCovered / _journeyLength;
            _vehicle.transform.position = Vector3.Lerp(_currentPosition, _target.position, fractionOfJourney);
            if (fractionOfJourney > 1)
            {
                _changeTarget = false;
            }
        }
    }
}
