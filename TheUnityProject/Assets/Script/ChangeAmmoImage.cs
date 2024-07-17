using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAmmoImage : MonoBehaviour
{
    ShotgunTrigger shotgunScript;
    public List<Sprite> ammoImageList;
    Image imageRenderer;
    void Start()
    {
        shotgunScript = GameObject.FindWithTag("Shotgun").GetComponent<ShotgunTrigger>();
        imageRenderer = GetComponent<Image>();
    }

    void Update()
    {
        imageRenderer.sprite = ammoImageList[shotgunScript.selectedSpell];
    }
}
