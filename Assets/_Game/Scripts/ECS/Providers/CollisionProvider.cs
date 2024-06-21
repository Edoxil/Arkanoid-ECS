using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [RequireComponent(typeof(Collider))]
    public sealed class CollisionProvider : MonoProvider<CollisionComponent>
    {
        private void OnCollisionEnter(Collision collision)
        {
            ref CollisionComponent component = ref GetData();
            component.collision = collision;
        }

        private void OnCollisionExit(Collision collision)
        {
            ref CollisionComponent component = ref GetData();
            component.collision = null;
        }

    }
}