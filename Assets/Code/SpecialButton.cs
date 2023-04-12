using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpecialButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PopupPanel;
    public Image BackgroundApp;
    public Sprite RickAstley;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "")
            PopupPanel.SetActive(true);
        else
        {
            BackgroundApp.sprite = RickAstley;
            SoundObject.Instance.BGM.clip = SoundObject.Instance.RickAstley;
            SoundObject.Instance.BGM.Play();
        }
    }
    public void PopupBackgroundClick()
    {
        PopupPanel.SetActive(false);
    }

}
