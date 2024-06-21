using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class DirectionProvider : MonoProvider<DirectionComponent>
    {
    }
}