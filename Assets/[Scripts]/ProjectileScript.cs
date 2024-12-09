using System;
using System.Collections.Generic;
using _Scripts_.Interfaces;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float LifeTime;
    public float Speed;
    public int Overlaps;
    public event Action OnDestroy;
    
    public delegate void OnHitHandler(GameObject hitObject);
    public event OnHitHandler OnHit;

    public List<LayerMask> DestroyLayers;
    
    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        
        LifeTime -= Time.deltaTime;
        if(LifeTime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHitTarget target))
        {
            
            Overlaps--;
            if(Overlaps <= 0)
                Destroy(gameObject);
            else
            {
                OnHit?.Invoke(other.gameObject);
            }
        }

        if (DestroyLayers.Contains(other.gameObject.layer))
        {
            Destroy(gameObject);
            
        }
    }

    private void OnDisable()
    {
        OnDestroy?.Invoke();
    }
}
