using _Scripts_.Interfaces;
using UnityEngine;

public class EnemyEffectScript : MonoBehaviour, IHitTarget
{
    public void ApplyEffect(AbilitySO AppliedAbility)
    {
        Debug.Log("Applying Effect On Enemy");
        //Apply Effect :D
    }
}
