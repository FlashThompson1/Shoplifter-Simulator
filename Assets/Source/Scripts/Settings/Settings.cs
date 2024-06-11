using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;

    public void SetVolume(float volume) {

        audioMixer.SetFloat("volume", volume);
        
    }

    public void SetQuality(int qualityIndex) { 
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen) { 
    
        Screen.fullScreen = isFullscreen;
    }





}
