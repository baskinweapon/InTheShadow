using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SunVFX : MonoBehaviour
{
    public VisualEffect _visualEffect;

    public bool set = false;

    public Gradient gradient;
    private float time;
    public float value = 1;
    private int sign = 1;
    private GradientColorKey[] gck;
    private GradientAlphaKey[] gak;
    
    void Start()
    {
        time = 0;
       gck = gradient.colorKeys;
       gak = gradient.alphaKeys;
       print(gck[1].color.ToString());
    }

   
    // Update is called once per frame
    void Update()
    {
        _visualEffect.gameObject.transform.position += new Vector3(0, 0, Time.deltaTime);
        time += Time.deltaTime;
        value += sign * time * 0.1f;
        if (time >= 2f)
        {
            time = 0;
            sign *= -1;
            gck[0].color *= Color.white;
            gck[0].time = 0F;
            gck[1].color = new Color(Random.Range(0f, 5f), Random.Range(0f, 5f), Random.Range(0f, 5f));
            gck[1].time = 0.344f;
            gradient.SetKeys(gck, gak);
        }
        _visualEffect.SetGradient("Sparks Gradient", gradient);
        if (value >= 10f || value <= 0f)
            return;
        _visualEffect.SetInt("Intensity", (int)value);
    }
}
