using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirector : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    public int energyFieldPower;
    public float progress;

    // Start is called before the first frame update
    void Start()
    {
        energyFieldPower = 3;
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        energyFieldPower = Mathf.Clamp(energyFieldPower, 0, 3);

        switch (energyFieldPower)
        {
            case 0:
                {
                    spriteRenderer.sprite = sprites[0];
                }
                break;
            case 1:
                {
                    spriteRenderer.sprite = sprites[1];
                }
                break;
            case 2:
                {
                    spriteRenderer.sprite = sprites[2];
                }
                break;
            case 3:
                {
                    spriteRenderer.sprite = sprites[3];
                }
                break;
        }

    }
}
