using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
public class TutorialPages : MonoBehaviour
{
    [Header("Buttons")]
    // public Button PageOneButton;
    // public Button PageTwoButton;
    // public Button PageThreeButton;
    // public Button PageFourButton;
    // public Button PageFiveButton;
    public GameObject next;
    public GameObject prev;

    [Header("Pages")]
    public GameObject Pageone;
    public GameObject Pagetwo;
    public GameObject Pagethree;
    public GameObject Pagefour;
    public GameObject Pagefive;

    [Header("dddddd")]
    public int pageIndex =1;


    public void NextPage()
    {
        pageIndex ++;
    }
    public void PrevPage()
    {
        pageIndex --;
    }
    public void Update()
    {
        if(pageIndex == 1)
        {
            prev.SetActive(false);
            Pageone.SetActive(true);
            Pagetwo.SetActive(false);
            Pagethree.SetActive(false);
            Pagefour.SetActive(false);
            Pagefive.SetActive(false);
        }
        if(pageIndex == 2)
        {
            prev.SetActive(true);
            Pageone.SetActive(false);
            Pagetwo.SetActive(true);
            Pagethree.SetActive(false);
            Pagefour.SetActive(false);
            Pagefive.SetActive(false);
        }
        if(pageIndex == 3)
        {
            Pageone.SetActive(false);
            Pagetwo.SetActive(false);
            Pagethree.SetActive(true);
            Pagefour.SetActive(false);
            Pagefive.SetActive(false);
        }
        if(pageIndex == 4)
        {
            next.SetActive(true);
            Pageone.SetActive(false);
            Pagetwo.SetActive(false);
            Pagethree.SetActive(false);
            Pagefour.SetActive(true);
            Pagefive.SetActive(false);
        }
        if(pageIndex == 5)
        {
            next.SetActive(false);
            Pageone.SetActive(false);
            Pagetwo.SetActive(false);
            Pagethree.SetActive(false);
            Pagefour.SetActive(false);
            Pagefive.SetActive(true);
        }
    }



}
