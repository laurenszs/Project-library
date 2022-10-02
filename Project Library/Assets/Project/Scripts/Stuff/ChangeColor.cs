using TMPro;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Material vapeMaterial;
    [SerializeField] private FlexibleColorPicker linePicker;
    [Header("Tiling")] [SerializeField] private TMP_InputField hor;
    [SerializeField] private TMP_InputField ver;

    private Vector2 _tiling;

    /*private ChangeColor()
    {
        InputNullChecker();
        LineModifiers();
    }*/

    private void Update()
    {
        InputNullChecker();
        LineModifiers();
    }

    private void LineModifiers()
    {
        vapeMaterial.SetColor("Vape_Color", linePicker.color);
        _tiling = new Vector2(int.Parse(hor.text), int.Parse(ver.text));
        vapeMaterial.SetVector("_tiling", _tiling);
    }

    private void InputNullChecker()
    {
        if (string.IsNullOrEmpty(hor.text)) hor.text = "0";
        if (string.IsNullOrEmpty(ver.text)) ver.text = "0";
    }
}