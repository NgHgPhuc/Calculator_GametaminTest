using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuPanel : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    string MenuState;

    public Button SFXButton;
    public Sprite SfxOn;
    public Sprite SfxOff;

    public Button BGMButton;
    public Sprite BgmOn;
    public Sprite BgmOff;
    void Start()
    {
        animator = GetComponent<Animator>();
        MenuState = "Pop Down";
        SFXButton.onClick.AddListener(SFXButtonClick);
        BGMButton.onClick.AddListener(BGMButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MenuButtonClick()
    {
        animator.Play(MenuState);
        MenuState = (MenuState == "Pop Up") ? "Pop Down" : "Pop Up";
    }
    public void SFXButtonClick()
    {
        float value = SoundObject.Instance.SoundFx.volume;
        value = (value == 0f) ? 1f : 0f;
        SoundObject.Instance.SetVolumnSoundFX(value);

        SFXButton.transform.GetChild(0).GetComponent<Image>().sprite = (value == 0f) ? SfxOff : SfxOn;

    }

    public void BGMButtonClick()
    {
        float value = SoundObject.Instance.BGM.volume;
        value = (value == 0f) ? 1f : 0f;
        SoundObject.Instance.SetVolumnBGM(value);

        BGMButton.transform.GetChild(0).GetComponent<Image>().sprite = (value == 0f) ? BgmOff : BgmOn;
    }

    public void HistoryButtonClick(GameObject HistoryPanel)
    {
        MenuButtonClick();
        //HistoryPanel.SetActive(true);
        //HistoryPanel.GetComponent<HistoryExpressionPanel>().

        HistoryExpressionPanel.Instance.TurnOn();
    }
}
