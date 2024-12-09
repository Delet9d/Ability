using System.Collections.Generic;
using _Scripts_.Ability;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "ScriptableObjects/AbilitySO")]
public class AbilitySO : ScriptableObject
{
    [SerializeField] public EAbilityType AbilityType;
    [SerializeField] public int AbilityID;
    [SerializeField] public EAbilityEffectPolicy AbilityEffectPolicy;
     
    [SerializeField] public float Amount;

    [BoxGroup("Self Effect")]
    [ShowIf("AbilityType", EAbilityType.SelfEffect)]
    [SerializeField] public float EffectRadius;

    [BoxGroup("Ranged Ability")]
    [ShowIf("AbilityType", EAbilityType.Ranged)] 
    [SerializeField] public Transform ProjectilePrefab;
    
    [BoxGroup("Ranged Ability")]
    [ShowIf("AbilityType", EAbilityType.Ranged)] 
    [SerializeField] public int MultipleOverlaps = 1;
    
    [BoxGroup("Ranged Ability")]
    [ShowIf("AbilityType", EAbilityType.Ranged)] 
    [SerializeField] public List<LayerMask> LayerMasksToOverlap;
    
    [BoxGroup("Ranged Ability")]
    [ShowIf("AbilityType", EAbilityType.Ranged)] 
    [SerializeField] public List<LayerMask> LayerMasksToEnd;
    
    [BoxGroup("Ranged Ability")]
    [ShowIf("AbilityType", EAbilityType.Ranged)]
    [SerializeField] public bool ExplosionInTheEnd;

    private bool ExplosionRadiusFunc() { return ExplosionInTheEnd && AbilityType == EAbilityType.Ranged; }
    [BoxGroup("Ranged Ability")]
    [ShowIf("ExplosionRadiusFunc")] 
    [SerializeField] public float ExplosionRadius;

    [BoxGroup("Ranged Ability")]
    [ShowIf("ExplosionRadiusFunc")] 
    [SerializeField] public Transform ExplosionVFX;
        
    private bool DurationConditionFunc() { return !HasStacks && AbilityEffectPolicy == EAbilityEffectPolicy.Duration; }
    [ShowIf("DurationConditionFunc")]
    [SerializeField] public float Duration;

    [BoxGroup("Stack Policy")]
    [SerializeField] public bool HasStacks;
        
    [BoxGroup("Stack Policy")]
    [ShowIf("HasStacks")] 
    [SerializeField] public int MaxEffectStacks;
        
    private bool RemoveStacksConditionFunc() { return HasStacks && AbilityEffectPolicy == EAbilityEffectPolicy.Duration; }
    [BoxGroup("Stack Policy")]
    [ShowIf("RemoveStacksConditionFunc")]
    [SerializeField] public EAbilityRemoveStackPolicy AbilityRemoveStackPolicy;

    [BoxGroup("Stack Policy")]
    [ShowIf("RemoveStacksConditionFunc")] 
    [SerializeField] public float StackDuration;

    [BoxGroup("Entity Movement")] 
    [SerializeField] public float CastingVelocityChange;
    
    [BoxGroup("Entity Movement")] 
    [SerializeField] public float ActiveVelocityChange;

    [BoxGroup("Entity Movement")] [SerializeField]
    public EAbilityAngleOnRelease EntityRotationOnAbilityRelease;
    
    [BoxGroup("Entity Movement")]
    [ShowIf("EntityRotationOnAbilityRelease", EAbilityAngleOnRelease.Angle)]
    [SerializeField] public float EntityAngleOnAbilityRelease;
    
    [BoxGroup("Visual Part")]
    [SerializeField] public AnimationClip AnimationClip;
    [BoxGroup("Visual Part")]
    [SerializeField] public GameObject VisualEffectRelease;
    [BoxGroup("Visual Part")]
    [SerializeField] public GameObject VisualEffectHit;
    [BoxGroup("Visual Part")]
    [SerializeField] public GameObject VisualEffectEnd;
    [BoxGroup("Visual Part")]
    [SerializeField] public AudioClip AnimationSoundRelease;
    [BoxGroup("Visual Part")]
    [SerializeField] public AudioClip AnimationSoundHit;
    [BoxGroup("Visual Part")]
    [SerializeField] public AudioClip AnimationSoundEnd;
}


public enum EAbilityEffectPolicy
{
    Instant,
    Duration,
    Infinity,
}

public enum EAbilityRemoveStackPolicy
{
    RemoveSome,
    RemoveAll,
}

public enum EAbilityAngleOnRelease
{
    None,
    Angle,
    ToTarget,
}

public enum EAbilityType
{
    SelfEffect,
    Ranged,
}
