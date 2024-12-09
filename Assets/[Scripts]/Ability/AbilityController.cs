using System;
using _Scripts_.Ability;
using _Scripts_.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;


public enum EAbilityState
{
   None, 
   Casting,
   Release,
   End,
}

public class AbilityController : MonoBehaviour, IAbility
{
    [SerializeField] AbilityEffect _AbilityEffect;
    [SerializeField] AbilityMovement _AbilityMovement;
    [SerializeField] AbilityEntityData _AbilityEntityData;

    [SerializeField] AbilityAnimation _AbilityAnimation;
    [SerializeField] AbilitySound _AbilitySound;
    [SerializeField] AbilityVFX _AbilityVFX;

    [SerializeField] public AbilitySO _AbilitySO;
    public EAbilityState AbilityState = EAbilityState.None;
    
    private GameObject Player;
    public GameObject Projectile;
    public void AbilityStateFunc(EAbilityState newAbilityState)
    {
        AbilityState = newAbilityState;

        switch (newAbilityState)
        {
            case EAbilityState.Casting:
                //AnimationStart
                break;
            case EAbilityState.Release:
                _AbilityEffect.AbilityActivated(Player);
                _AbilityVFX.PlayActivateVFX(Player.transform.position + new Vector3(0, 1f, 0));
                //Sound Start
                break;
            case EAbilityState.End:
                if(_AbilitySO.AbilityType == EAbilityType.Ranged)
                    _AbilityVFX.PlayEndVFX(Projectile.transform.position);
                else 
                    _AbilityVFX.PlayEndVFX(Player.transform.position + new Vector3(0, 1f, 0));
                //Sound End
            break;
        }
    }
    

    private void AbilityEffectOnOnSpawning(GameObject projectile)
    {
        if(projectile != null)
            Projectile = projectile;
    }

    private void AbilityEffectOnOnDestroyEvent()
    {
        AbilityStateFunc(EAbilityState.End);
    }

    private void AbilityEffectOnOnHitEvent()
    {
        Debug.Log("HitHitVFx ");
        if(_AbilitySO.AbilityType == EAbilityType.Ranged)
            _AbilityVFX.PlayHitVFX(Projectile.transform.position);
    }

    public void AbilityActivated(GameObject player)
    {
        Player = player;
        _AbilityEntityData.Initialization(_AbilitySO);
        _AbilityEffect.Initialization(_AbilitySO);
        _AbilityMovement.Initialization(_AbilitySO);
        _AbilitySound.Initialization(_AbilitySO);
        _AbilityVFX.Initialization(_AbilitySO);
        _AbilityAnimation.Initialization(_AbilitySO, Player);
        
        AbilityStateFunc(EAbilityState.Casting);
        
        
        _AbilityEffect.OnHitEvent += AbilityEffectOnOnHitEvent;
        _AbilityEffect.OnDestroyEvent += AbilityEffectOnOnDestroyEvent;
        _AbilityEffect.OnSpawning += AbilityEffectOnOnSpawning;
    }
}
