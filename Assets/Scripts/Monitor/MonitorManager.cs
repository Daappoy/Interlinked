using UnityEngine;
using TMPro;
public class MonitorManager : MonoBehaviour
{
    public TextMeshProUGUI BottomText;
    public TextMeshProUGUI TopText;
    private int numtestT;
    private int numtestB;

    public void numplusT(){
        numtestT++;
        this.TopText.text = numtestT.ToString();
    }

    public void numplusB(){
        numtestB++;
        this.BottomText.text = numtestB.ToString();
    }
}