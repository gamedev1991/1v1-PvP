using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ButtonType
{
LeftButton,
RightButton,
FireButton
}
public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Player playerScript;
    public ButtonType buttonType;
    float bulletDelay;
    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
        if (!ispressed)
            return;


        switch (buttonType)
        {
            case ButtonType.LeftButton:
            playerScript.LeftMove();
                break;
            case ButtonType.RightButton:
                playerScript.RightMove();
                break;
            case ButtonType.FireButton:
                bulletDelay += Time.deltaTime;
                if (bulletDelay > 0.2f)
                {
                    playerScript.Fire();
                    bulletDelay = 0;
                }
                break;
        }
    }
    bool ispressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        ispressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ispressed = false;
    }

}
