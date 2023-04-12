using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using TMPro;
public class OnOffButton : MonoBehaviour
{
	// Use this for initialization
	//public TextMeshProUGUI CurrentCalculationUI;
	//public TextMeshProUGUI ShowResultUI;
	public GameObject NumberButton;

    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
			
	}
    public void TurnOnButton()
    {
		//CurrentCalculationUI.gameObject.SetActive(true);
		//ShowResultUI.gameObject.SetActive(true);
		NumberButton.SetActive(true);
    }
	public void TurnOffButton()
	{
		Application.Quit();
    }

}

