using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
public class TutorialPages : MonoBehaviour
{
    public Button PageOneButton;
    public Button PageTwoButton;
    public Button PageThreeButton;
    public Button PageFourButton;
    public Button PageFiveButton;
    public GameObject Pageone;
    public GameObject Pagetwo;
    public GameObject Pagethree;
    public GameObject Pagefour;
    public GameObject Pagefive;
    public void OnEnable()
    {
        Pageone.SetActive(true);
        Pagetwo.SetActive(false);
        Pagethree.SetActive(false);
        Pagefour.SetActive(false);
        Pagefive.SetActive(false);
    }
    public void PageOne()
    {
        Pageone.SetActive(true);
        Pagetwo.SetActive(false);
        Pagethree.SetActive(false);
        Pagefour.SetActive(false);
        Pagefive.SetActive(false);
    }
    public void PageTwo()
    {
        Pageone.SetActive(false);
        Pagetwo.SetActive(true);
        Pagethree.SetActive(false);
        Pagefour.SetActive(false);
        Pagefive.SetActive(false);
    }
    public void PageThree()
    {
        Pageone.SetActive(false);
        Pagetwo.SetActive(false);
        Pagethree.SetActive(true);
        Pagefour.SetActive(false);
        Pagefive.SetActive(false);
    }
    public void PageFour()
    {
        Pageone.SetActive(false);
        Pagetwo.SetActive(false);
        Pagethree.SetActive(false);
        Pagefour.SetActive(true);
        Pagefive.SetActive(false);
    }
    public void PageFive()
    {
        Pageone.SetActive(false);
        Pagetwo.SetActive(false);
        Pagethree.SetActive(false);
        Pagefour.SetActive(false);
        Pagefive.SetActive(true);
    }

}
