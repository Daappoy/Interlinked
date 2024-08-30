using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  [Header("Audio Source")]
  [SerializeField] AudioSource musicSource;
  [SerializeField] AudioSource SFXSource;

  [Header("Audio Clip")]
  public AudioClip background;
  public AudioClip Pause;
  public AudioClip ClickOnPause;
  public AudioClip GeneralClick;
  public AudioClip ButtonClick;
  public AudioClip Buzzer;
  public AudioClip DoorClose;
  public AudioClip DiscepancyFound;
  public AudioClip SwitchChara;
  public AudioClip Thump;
  public AudioClip Walking;

  private void Start()
  {
    musicSource.clip = background;
    musicSource.Play();
  }

  public void PlaySFX(AudioClip clip)
  {
    SFXSource.PlayOneShot(clip);
  }
}

