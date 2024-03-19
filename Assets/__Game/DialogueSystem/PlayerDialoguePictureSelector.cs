using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialoguePictureSelector : MonoBehaviour
{
    [SerializeField] Sprite FemaleImage;
    [SerializeField] Sprite MaleImage;

    private void Start()
    {
        Image imageComponent = GetComponent<Image>();
        if(GameManager.Instance.CurrentPlayer.transform.name == "PlayerFemale")
        {
            imageComponent.sprite = FemaleImage;
        }
        else
        {
           imageComponent.sprite = MaleImage;
        }
    }
}
