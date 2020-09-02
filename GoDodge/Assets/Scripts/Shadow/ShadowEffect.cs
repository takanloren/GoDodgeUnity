using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEffect : MonoBehaviour
{
    public Vector3 offset = new Vector3(-3, -3);
    public Material shadowMaterial;
    public Color shadowColor;
    public int scale = 1;
    public Sprite circleSprite;

    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprRndShadow;

    private Transform transCaster;
    private Transform transShadow;

    // Start is called before the first frame update
    void Start()
    {
        transCaster = transform;
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "shadow";
        transShadow.localRotation = Quaternion.identity;
        

        sprRndCaster = GetComponent<SpriteRenderer>();
        sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        sprRndShadow.material = shadowMaterial;
        sprRndShadow.color = shadowColor;

        sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
        sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;

        transShadow.localScale = new Vector3(scale, 0.3f, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transShadow.position = new Vector3(transCaster.position.x + offset.x,
            transCaster.position.y + offset.y, transCaster.position.z + offset.z);

        sprRndShadow.flipX = sprRndCaster.flipX;

        if(circleSprite == null)
        {
            sprRndShadow.sprite = sprRndCaster.sprite;
        }else
        {
            sprRndShadow.sprite = sprRndCaster.sprite;
        }
        
    }
}
