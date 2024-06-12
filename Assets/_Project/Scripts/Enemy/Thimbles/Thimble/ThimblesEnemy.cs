using System;
using UnityEngine;

public class ThimblesEnemy : Enemy
{
    public Thimble FirstThimble { get; private set; }
    public Thimble SecondThimble { get; private set; }
    private ThimbleSelect _thimbleSelect;
    private ThimblesMovement _thimblesMovement;
    private Card _card;
    private Timer _timer;

    public bool ActivationThimbles { get; private set; }
    public bool ThimbleClick { get; set; }

    private void Start()
    {
        SubscribeToRespawn();
        _card = GetComponentInChildren<Card>(true);
        _thimbleSelect = GetComponent<ThimbleSelect>();
        _thimblesMovement = GetComponent<ThimblesMovement>();
        _timer = GetComponentInChildren<Timer>();
    }
    public override void Activate()
    {
        Debug.Log("Activate");
        _timer.Activate(_attackWaitingTime);
        ActivationThimbles = true;
    }

    public void ActivateGame()
    {
        if (ActivationThimbles)
        {
            Debug.Log("ActivatedGame");
            _timer.Deactivate();
            ThimbleActivateFirst();
            ThimbleActivateSecond();
            ActivationThimbles = false;
        }
    }

    public override void Deactivate()
    {
        SubscribeToRespawn();
    }

    public void Baff()
    {
        Deactivate();
        Debug.Log("Неверный наперсток!! Лови аплиуху!");
    }

    private void ThimbleActivateFirst()
    {
        FirstThimble = _thimbleSelect.ThimbleSelection();
        _card.TeleportCard(FirstThimble.transform.position);
        FirstThimble.AnimationSelected();
        FirstThimble.Animations += ThimblesMovementsStart;
    }
    public void ThimbleActivateSecond()
    {
        SecondThimble = _thimbleSelect.ThimbleSelection();
    }

    private void ThimblesMovementsStart()
    {
        FirstThimble.AddCardAndParent(_card);
        _thimblesMovement.Movements();
        FirstThimble.Animations -= ThimblesMovementsStart;
    }

    public void ActivationsThimbles(bool state)
    {
        ActivationThimbles = state;
    }






}
