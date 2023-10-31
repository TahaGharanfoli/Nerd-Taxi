using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private CarController _carController;
    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;
    [SerializeField] private float _moveSpeed;
    private float _journeyLength;
    private float _startTime;
    private Transform _target;
    private bool _changeTarget;
    private Vector3 _firstPosition;
    private void Start()
    {
        Vector3 firsPos = _leftTarget.position;
        firsPos.y = _carController.transform.position.y;
        _carController.transform.position = firsPos;
        _journeyLength = Vector3.Distance(_leftTarget.position,_rightTarget.position);
    }

    private void Update()
    {
       SetInputTurn();
       MoveCar();
    }

    private void SetInputTurn()
    {
        _changeTarget = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _target = _leftTarget;
            _startTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _target = _rightTarget;
            _startTime = Time.time;
        }

        _firstPosition = _carController.transform.position;
    }

    private void MoveCar()
    {
        if (_changeTarget)
        {
            float distanceCovered = (Time.time - _startTime) * _moveSpeed;
            float fractionOfJourney = distanceCovered / _journeyLength;
            _carController.transform.position = Vector3.Lerp(_firstPosition, _target.position, fractionOfJourney);
            if (fractionOfJourney > 1)
            {
                _changeTarget = false;
            }
        }
    }

}
