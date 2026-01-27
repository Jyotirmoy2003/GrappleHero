using UnityEngine;

public class FeelManager : MonoSingleton<FeelManager>
{
    [SerializeField] FeedBackManager litZoomFeedback;
    public void LitZoom()
    {
       litZoomFeedback.PlayFeedback(); 
    }
}
