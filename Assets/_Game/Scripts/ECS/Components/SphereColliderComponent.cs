using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Arkanoid
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct SphereColliderComponent : IComponent
    {
        public SphereCollider collider;
    }
}