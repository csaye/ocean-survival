using UnityEngine;

namespace OceanSurvival.UI
{
    public class TooltipPanel : MonoBehaviour
    {
        private readonly Vector3 Offset = new Vector3(10, -10, 0);

        private void Update()
        {
            transform.position = Input.mousePosition + Offset;
        }
    }
}
