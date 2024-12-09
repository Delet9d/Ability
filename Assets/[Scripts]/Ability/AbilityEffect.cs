using System;
using System.Numerics;
using _Scripts_.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts_.Ability
{
    public class AbilityEffect : MonoBehaviour
    {
        private AbilitySO _AbilitySO;
        public event Action OnAbilityActivated;
        private Camera _Camera;
        private Transform Projectile;
        
        public delegate void OnSpawningObject(GameObject projectile);
        public event OnSpawningObject OnSpawning;
        
        
        public event Action OnHitEvent;
        public event Action OnDestroyEvent;
        public void Initialization(AbilitySO AbilitySO)
        {
            _AbilitySO = AbilitySO;
            
            _Camera = FindFirstObjectByType<Camera>();
        }

        public void AbilityActivated(GameObject player)
        {
            switch (_AbilitySO.AbilityType)
            {
                case EAbilityType.Ranged: 
                    ActivateRanged(player);
                    break;
                
                case EAbilityType.SelfEffect:
                    ActivateSelf(player);
                    break;
            }
        }

        public void ActivateRanged(GameObject player)
        {
            if (_AbilitySO.ProjectilePrefab != null)
            {
                Projectile = Instantiate(_AbilitySO.ProjectilePrefab, player.transform.position 
                                                                      + player.transform.forward * 0.5f
                                                                      + Vector3.up * 1f , _Camera.transform.rotation);
                OnSpawning?.Invoke(Projectile.gameObject);
            }
            else
            {
                Debug.LogError("Projectile Prefab is null, setup it in ScriptableObject");
                return;
            }
                
            if (Projectile.TryGetComponent(out ProjectileScript element))
            {
                element.Overlaps = _AbilitySO.MultipleOverlaps;
                element.OnHit += AbilityHit;
                
                if(_AbilitySO.ExplosionInTheEnd)
                    element.OnDestroy += AbilityEnd;
            }
        }
        
        //Make Include Self or Not for area effect
        private void ActivateSelf(GameObject player)
        {
            //Apply Effect To Player? or overlapped with AbilitySO.radius objects.
            transform.GetComponent<EntityAbilityManager>().ApplyEffect(_AbilitySO);
        }

        public void AbilityHit(GameObject hitObject)
        {
            hitObject.GetComponent<IHitTarget>().ApplyEffect(_AbilitySO);
            OnHitEvent?.Invoke();
        }

        public void AbilityEnd()
        {

            Collider[] hitColliders = Physics.OverlapSphere(Projectile.transform.position, _AbilitySO.EffectRadius);
            foreach (var e in hitColliders)
            {
                if (e.GetComponent<Collider>().gameObject.TryGetComponent(out IHitTarget element))
                {
                    element.ApplyEffect(_AbilitySO);
                }
            }
            
            OnDestroyEvent?.Invoke();
        }
    }
    
}