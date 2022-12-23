using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEditor;

public class uiMenu : MonoBehaviour
{
    public Slider musicSlider;

    public void MusicVolume()
    {
        AudioManeger.instance.MusicVolume(musicSlider.value);
    }

    public void SetQuality(int qualitiIndex)
    {
        QualitySettings.SetQualityLevel(qualitiIndex);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void PlayGame(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame() =>
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

}
