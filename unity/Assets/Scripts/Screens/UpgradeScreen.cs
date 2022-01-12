using System;
using UnityEngine.UI;

namespace Screens
{
    public class UpgradeScreen : Screen
    {
        public Button optionA;
        public Text   optionAText;
        public Button optionB;
        public Text   optionBText;
        public Button optionC;
        public Text   optionCText;

        public void ConfigureOptionA(string text, Action onClick)
        {
            optionAText.text = text;
            optionA.onClick.AddListener(onClick.Invoke);
        }
        
        public void ConfigureOptionB(string text, Action onClick)
        {
            optionBText.text = text;
            optionB.onClick.AddListener(onClick.Invoke);
        }
        
        public void ConfigureOptionC(string text, Action onClick)
        {
            optionCText.text = text;
            optionC.onClick.AddListener(onClick.Invoke);
        }
    }
}