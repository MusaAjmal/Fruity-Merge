using UnityEngine;

public class MyADSInitializer : MonoBehaviour
{
    private void Start()
    {
        AdController.Instance.ShowBannerAd(AdController.BannerAdTypes.BANNER);
    }
}
