using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class ButtonPanel : MonoBehaviour
{
    public List<Button> NumberButton;

    public TextMeshProUGUI CurrentCalculationUI;
    public TextMeshProUGUI ShowResultUI;
    string CurrentCalculationText;
    string CurrentNumber="";
    List<string> Expressions = new List<string>();
    string Ans;

    void Start()
    {
        foreach (Button i in NumberButton)
            i.onClick.AddListener(delegate { ButtonNumberClick(i); });
    }

    public void DisplayCurrentCalculation(string s)
    {
        CurrentCalculationUI.SetText(s);
    }
    public void ButtonNumberClick(Button i)
    {
        SoundObject.Instance.PlaySoundFx(SoundObject.Instance.ButtonNumberClick);
        string num = i.GetComponentInChildren<TextMeshProUGUI>().text;
        CurrentCalculationText += num;
        CurrentNumber += num;
        DisplayCurrentCalculation(CurrentCalculationText);
    }

    public void DeleteButton()
    {
        if (CurrentCalculationText.Length == 0) return;

        SoundObject.Instance.PlaySoundFx(SoundObject.Instance.DeleteButton);

        CurrentCalculationText = CurrentCalculationText.Remove(CurrentCalculationText.Length - 1);
        DisplayCurrentCalculation(CurrentCalculationText);
        if (CurrentNumber.Length > 0)
        {
            CurrentNumber = CurrentNumber.Remove(CurrentNumber.Length - 1);
            return;

        }

        if(CurrentNumber.Length == 0 && Expressions.Count > 0)
        {
            CurrentNumber = Expressions[Expressions.Count - 1];
            CurrentNumber = CurrentNumber.Remove(CurrentNumber.Length - 1);
            Expressions.RemoveAt(Expressions.Count - 1);
            string LastExp = Expressions[Expressions.Count - 1];
            if (CurrentNumber.Length == 0 && (LastExp != "+" && LastExp != "-" && LastExp != "*" && LastExp != "/"))
            {
                CurrentNumber = LastExp;
                Expressions.RemoveAt(Expressions.Count - 1);
            }
        }


    }

    public void ClearButton()
    {
        SoundObject.Instance.PlaySoundFx(SoundObject.Instance.ACButton);
        CurrentCalculationText = "";
        CurrentNumber = "";
        Expressions.Clear();
        DisplayCurrentCalculation(CurrentCalculationText);
    }
    public void DotButton()
    {
        //if have . in number => dont do anything
        if (CurrentNumber.Contains(".")) return;

        SoundObject.Instance.PlaySoundFx(SoundObject.Instance.ButtonNumberClick);
        //case . => 0. or -. => -0. or +. => +0.
        if (CurrentNumber.Length == 0 || CurrentNumber == "-" || CurrentNumber == "+")
        {
            CurrentNumber += "0";
            CurrentCalculationText += "0";
        }

        CurrentNumber += ".";
        CurrentCalculationText += ".";
        DisplayCurrentCalculation(CurrentCalculationText);

    }

    public void MathematicButton(string m)
    {
        //case (+,-,x,/) so cant use (x,/) and block case CurrentCalculationText = ""
        if (CurrentNumber.Length == 0 && (m == "*" || m == "/")) return;

        SoundObject.Instance.PlaySoundFx(SoundObject.Instance.MathButtonClick);

        //case 0. (+,-,x,/) => 0.0 (+,-,x,/)
        if (CurrentNumber.Length > 0 && CurrentNumber[CurrentNumber.Length - 1] == '.')
        {
            CurrentCalculationText += "0";
            CurrentNumber += "0";
        }

        //case -- = + or -+ = -
        if (CurrentNumber == "-")
        {
            CurrentNumber = (m == "-") ? "+" : "-";
            CurrentCalculationText = CurrentCalculationText.Substring(0,CurrentCalculationText.Length - 1) + CurrentNumber;
            DisplayCurrentCalculation(CurrentCalculationText);
            return;
        }
        //case +- = - or ++ = +
        if (CurrentNumber == "+")
        {
            CurrentNumber = m;
            CurrentCalculationText = CurrentCalculationText.Substring(0,CurrentCalculationText.Length - 1) + CurrentNumber;
            DisplayCurrentCalculation(CurrentCalculationText);
            return;
        }

        CurrentCalculationText += m;
        DisplayCurrentCalculation(CurrentCalculationText);

        //first mathematic button use 16(+,-,x,/)
        if (CurrentNumber.Length != 0)
        {
            Expressions.Add(CurrentNumber);
            Expressions.Add(m);
            CurrentNumber = "";
            return;
        }

        //second mathematic button use (+,-,x,/) and (-,+)
        CurrentNumber = m;
    }

    public void AnsButton()
    {
        if (CurrentNumber.Length == 0 || CurrentNumber == "-" || CurrentNumber == "+")
        {
            CurrentNumber += Ans;
            CurrentCalculationText += Ans;
            DisplayCurrentCalculation(CurrentCalculationText);
            return;
        }


        if (CurrentNumber[CurrentNumber.Length - 1] == '.')
        {
            CurrentNumber += "0";
            CurrentCalculationText += "0";
        }

        Expressions.Add(CurrentNumber);
        Expressions.Add("*");
        CurrentNumber = Ans;
        CurrentCalculationText += "*" + Ans;
        DisplayCurrentCalculation(CurrentCalculationText);

    }

    float ToFloat(string s)
    {
        return float.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
    }
    public void EqualButton()
    {
        if (CurrentNumber.Length == 0)
        {
            SoundObject.Instance.PlaySoundFx(SoundObject.Instance.Error);
            DisplayCurrentCalculation("ERROR");
            CurrentCalculationText = "";
            return;
        }
        if(CurrentNumber[CurrentNumber.Length - 1] == '-' || CurrentNumber[CurrentNumber.Length - 1] == '+')
        {
            SoundObject.Instance.PlaySoundFx(SoundObject.Instance.Error);
            DisplayCurrentCalculation("ERROR");
            CurrentCalculationText = "";
            return;
        }

        Expressions.Add(CurrentNumber);

        SoundObject.Instance.PlaySoundFx(SoundObject.Instance.EqualButton);

        List<string> Result = new List<string>();
        foreach (string i in Expressions)
            Result.Add(i);

        for (int i = 1; i < Expressions.Count-1; i+=2)
        {
            if (Expressions[i] == "*")
            {
                float rs = 0;
                rs = ToFloat(Expressions[i - 1]) * ToFloat(Expressions[i + 1]);
                Expressions[i] = (i > 3) ? Expressions[i - 2] : "+";
                Expressions[i + 1] = rs.ToString();
                Expressions[i - 1] = "0";
            }
            if (Expressions[i] == "/")
            {
                if (ToFloat(Expressions[i + 1]) == 0f)
                {
                    DisplayCurrentCalculation("ERROR! Divide for 0");
                    CurrentCalculationText = "";
                    return;
                }
                float rs = 0;
                rs = ToFloat(Expressions[i - 1]) / ToFloat(Expressions[i + 1]);
                Expressions[i] = (i > 3) ? Expressions[i - 2] : "+";
                Expressions[i + 1] = rs.ToString();
                Expressions[i - 1] = "0";
            }
        }

        for (int i = 1; i < Expressions.Count - 1; i += 2)
        {
            if (Expressions[i] == "+")
            {
                float rs = 0;
                rs = ToFloat(Expressions[i - 1]) + ToFloat(Expressions[i + 1]);
                Expressions[i] = "+";
                Expressions[i + 1] = rs.ToString();
                Expressions[i - 1] = "0";
            }
            if (Expressions[i] == "-")
            {
                float rs = 0;
                rs = ToFloat(Expressions[i - 1]) - ToFloat(Expressions[i + 1]);
                Expressions[i] = "+";
                Expressions[i + 1] = rs.ToString();
                Expressions[i - 1] = "0";
            }
        }

        HistoryExpressionPanel.Instance.AddHistoryExpression(CurrentCalculationText, Expressions[Expressions.Count - 1]);
        ShowResultUI.SetText(Expressions[Expressions.Count - 1]);
        Ans = Expressions[Expressions.Count - 1];
        for (int i = 0; i < Expressions.Count; i++)
        {
            Expressions[i] = Result[i];
        }
        CurrentNumber = Expressions[Expressions.Count - 1];
        Expressions.RemoveAt(Expressions.Count - 1);
    }
}
