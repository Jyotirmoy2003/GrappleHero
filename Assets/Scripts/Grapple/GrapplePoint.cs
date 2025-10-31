using UnityEngine;
using Jy_Util;

public class GrapplePoint : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] SpriteRenderer knobSprite;
    public E_KnobState currentKnobState;
    public bool closest = false;
    [SerializeField] FeedBackManager hoverFeedback;
    [SerializeField] FeedBackManager unhoverFeedback;
    private bool isHoverd = false;
    
    
    public void KnobStatusChanged(E_KnobState knobState)
    {
        switch(knobState)
        {
            case E_KnobState.Idle:
                if (currentKnobState != E_KnobState.Connected && !closest)
                    knobSprite.color = GameAssets.Instance.knobNormalColor;
                if (closest) {
                    knobSprite.color = GameAssets.Instance.knobHoverColor;
                    isHoverd = true;
                    hoverFeedback.PlayFeedback();
                }
                else knobSprite.color = GameAssets.Instance.knobNormalColor;
                break;
            case E_KnobState.Hover:
                if (currentKnobState != E_KnobState.Connected)
                    knobSprite.color = GameAssets.Instance.knobHoverColor;

                hoverFeedback.PlayFeedback();
                isHoverd = true;
                break;
            case E_KnobState.Connected:
                knobSprite.color = GameAssets.Instance.knobConnectedColor;
                break;
        }
        currentKnobState = knobState;

        if(currentKnobState != E_KnobState.Hover && isHoverd)
        {
            //it is in hover state change it back
            unhoverFeedback.PlayFeedback();
            isHoverd = false;
        }
    }
}
