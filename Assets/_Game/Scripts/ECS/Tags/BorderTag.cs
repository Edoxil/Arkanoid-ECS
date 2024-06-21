using UnityEngine;

namespace Arkanoid
{
    public class BorderTag : MonoBehaviour
    {
        [field: SerializeField] public BorderType BorderType { get; private set; }

    }
}