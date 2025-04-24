using Unity.Collections;
using Unity.Entities;

namespace Dots.Attacks.Components
{
    public struct DamagedEntitites : IComponentData
    {
         public FixedList512Bytes<Entity> Entities;
    }
}