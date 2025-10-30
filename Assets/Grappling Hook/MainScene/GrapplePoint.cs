using UnityEngine;
using Jy_Util;

public class GrapplePoint : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] SpriteRenderer knobSprite;
    public E_KnobState currentKnobState;
    public bool closest = false;
    
    
    public void KnobStatusChanged(E_KnobState knobState)
    {
        switch(knobState)
        {
            case E_KnobState.Idle:
                if (currentKnobState != E_KnobState.Connected && !closest)
                    knobSprite.color = GameAssets.Instance.knobNormalColor;
                if (closest) knobSprite.color = GameAssets.Instance.knobHoverColor;
                else knobSprite.color = GameAssets.Instance.knobNormalColor;
                break;
            case E_KnobState.Hover:
                if (currentKnobState != E_KnobState.Connected)
                    knobSprite.color = GameAssets.Instance.knobHoverColor;
                break;
            case E_KnobState.Connected:
                knobSprite.color = GameAssets.Instance.knobConnectedColor;
                break;
        }
        currentKnobState = knobState;
    }
}
