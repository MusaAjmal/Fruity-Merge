using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using Voodoo.Utils;
public class SettingsCanvas : MonoBehaviour
{
    [Header("Toggle Button References")]
    [SerializeField] private Button togglebtn1; // Music Toggle
    [SerializeField] private Button togglebtn2; // SFX Toggle
    [SerializeField] private Button togglebtn3; // Haptic Toggle
    [SerializeField] private Button togglebtn4; //Dark Mode
    private bool darkModeOn;
    [Header("Toggle Sprites")]
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;


    public void Start()
    {
        if (togglebtn1 != null)
        {
            togglebtn1.image.sprite =
                SoundManager.instance.IsMusicMuted()
                ? offSprite
                : onSprite;
        }

        if (togglebtn2 != null)
        {
            togglebtn2.image.sprite =
                SoundManager.instance.IsSFXMuted()
                ? offSprite
                : onSprite;
        }

        if (togglebtn3 != null)
        {
            togglebtn3.image.sprite = GameManager.Instance.IsHapticToggled() ? onSprite : offSprite;
        }
    }


    public void ToggleMusic()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.ToggleMusicMute();
            UpdateMusicButtonSprite();
            //UIManager.Instance.PlaySound();
        }
    }
    public void ToggleSounds()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.ToggleSFXMute();
            UpdateSoundButtonSprite();
            //UIManager.Instance.PlaySound();
        }
    }

    private void UpdateMusicButtonSprite()
    {
        if (togglebtn1 != null && SoundManager.instance != null)
        {
            togglebtn1.image.sprite = SoundManager.instance.IsMusicMuted() ? offSprite : onSprite;
        }
    }

    private void UpdateSoundButtonSprite()
    {
        if (togglebtn2 != null && SoundManager.instance != null)
        {
            togglebtn2.image.sprite = SoundManager.instance.IsSFXMuted() ? offSprite : onSprite;
        }
    }

    private void UpdateHapticButtonSprite()
    {
        togglebtn3.image.sprite = GameManager.Instance.IsHapticToggled() ? onSprite : offSprite;
    }

    public void ToggleHaptic()
    {
        GameManager.Instance.ManageHaptic();
        UpdateHapticButtonSprite();
    }

    private bool isDarkMode()
    {
        return darkModeOn;
    }

    public void ToggleDarkMode()
    {
        darkModeOn = !darkModeOn;
        UpdateDarkModeButtonSprite();
    }

    private void UpdateDarkModeButtonSprite()
    {
        togglebtn4.image.sprite = isDarkMode() ? offSprite : onSprite;
    }



    public void Tutorial()
    {

    }

    public void TermsOfService()
    {

    }

    public void Langauge()
    {

    }


    public void RestorePurchase()
    {

    }
    public void PrivacyPolicy()
    {

    }
    public void CloseSettings()
    {
        GameManager.Instance.ChangeState(UIState.CLOSE_OVERLAYS);
        //UIManager.Instance.CloseOverlay();
    }
}
