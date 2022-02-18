using UnityEngine;

[ExecuteInEditMode]
public class ChangeColor : MonoBehaviour
{
   [SerializeField] private Material vapeMaterial;
   [SerializeField] private Color _lineColor;
   public FlexibleColorPicker fcp;
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LineColor();
    }

    private void LineColor()
    {
       // vapeMaterial.SetColor("Vape_Color",_lineColor);
        vapeMaterial.SetColor("Vape_Color", fcp.color);
    }
}
