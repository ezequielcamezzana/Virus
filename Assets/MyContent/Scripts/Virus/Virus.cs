using System;
using UnityEngine;
using System.Collections;
using Photon.Pun;

public class Virus : MonoBehaviour, IDamageable, IDestructible
{
    public VirusProperties properties;

    private PlayerInput _playerInput;
    private MovementController _movementController;
    private ShootingController _shootingController;
    private AnimatorController _animatorController;
    private VirusBuilder _virusBuilder;
    private float _dashTime;
    private float _attackTime;

    private float LifePoints { get; set; }

    private void Start()
    {
        _virusBuilder = GetComponent<VirusBuilder>();
        //LLamo al server pido un virus random

        _virusBuilder.Build(_virusBuilder.seed);
        // StartCoroutine(APIService.Instance.GetRandomVirus((GetRandomVirusModel response) =>
        // {
        //     Debug.Log(response.virus.seed);
        //     _virusBuilder.Build(response.virus.seed);
        // }));

        //Creo las prperties

        //Genero la visuai

        //saco Foto

        //subo foto al sever


        _playerInput = GetComponent<PlayerInput>();
        _movementController = GetComponent<MovementController>();
        _shootingController = GetComponent<ShootingController>();
        _dashTime = properties.dashTime;
        LifePoints = properties.lifePoints;
    }

    private void Update()
    {
        _movementController.Rotate(_playerInput.DirectionVector);
        _movementController.Move(_playerInput.MovementVector, properties.speed);
        if (_playerInput.ShootAction && Time.time >= _attackTime)
        {
            _shootingController.Shoot(properties.projectileSpeed, properties.damage);
            _attackTime = Time.time + properties.timeBetweenAttack;
        }
        if (_playerInput.DashAction)
        {
            var direction = _playerInput.MovementVector != Vector2.zero ? _playerInput.MovementVector : (Vector2)transform.up;
            _movementController.Dash(direction, properties.speed, ref _dashTime);
        }
        else
        {
            _dashTime = properties.dashTime;
        }
    }

    public void TakeDamage(float damage)
    {
        LifePoints -= damage;
        if (LifePoints <= 0)
        {
            DestroyItSelf();
        }
    }

    public void DestroyItSelf()
    {
        Destroy(gameObject);
    }
}
