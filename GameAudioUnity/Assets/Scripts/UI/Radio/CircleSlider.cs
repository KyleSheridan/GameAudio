using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSlider : MonoBehaviour
{
    [SerializeField] Transform handle;
    [SerializeField] Image fill;
    Vector3 mousePos;

    [Range(0,1)] public float startValue = 0.5f;

    public float value { get; private set; }

    private void Awake()
    {
        value = startValue;
        fill.fillAmount = (startValue * 0.75f) + 0.125f;
    }

    public void OnHandelDrag()
    {
        mousePos = Input.mousePosition;
        Vector2 dir = mousePos - handle.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;
        if (angle <= 225 || angle >= 315)
        {
            Quaternion r = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            handle.rotation = r;
            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
            fill.fillAmount = 0.75f - (angle / 360f) + 0.125f;
            CalculateValue();
        }
    }

    void CalculateValue()
    {
        float baseValue = (fill.fillAmount - 0.125f) / 0.75f;
        value = Mathf.Round(baseValue * 100) / 100f;
    }
}
