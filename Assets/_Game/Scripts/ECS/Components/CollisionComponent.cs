using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using System;

namespace Arkanoid
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CollisionComponent : IComponent
    {
        public Collision collision;
    }
}