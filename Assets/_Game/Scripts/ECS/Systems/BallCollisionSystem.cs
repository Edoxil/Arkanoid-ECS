using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Arkanoid
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class BallCollisionSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;
        private Filter _platformFilter;

        public void OnAwake()
        {
            _filter = World.Filter.With<BallTag>()
                .With<CollisionComponent>()
                .With<DirectionComponent>()
                .Build();

            _platformFilter = World.Filter.With<PlayerTag>()
                .With<TransformComponent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref CollisionComponent collision = ref entity.GetComponent<CollisionComponent>();

                if (collision.collision == null || collision.collision.collider == null)
                    continue;

                ref DirectionComponent direction = ref entity.GetComponent<DirectionComponent>();
                ContactPoint contact = collision.collision.GetContact(0);

                if (collision.collision.collider.TryGetComponent(out PlayerTagProvider _))
                    CollideWithPlatform(contact, ref direction);

                if (collision.collision.collider.TryGetComponent(out BorderTag borderTag))
                    CollideWithBorder(borderTag.BorderType, ref direction);

                if (collision.collision.collider.TryGetComponent(out BlockTagProvider _))
                    CollideWithBlock(contact, ref direction);

                collision.collision = null;
            }

        }

        private void CollideWithPlatform(ContactPoint contact, ref DirectionComponent direction)
        {
            Vector3 platformPos = _platformFilter.GetEntity(0).GetComponent<TransformComponent>().transform.position;

            Vector3 dirToContact = (contact.point - platformPos).normalized;

            float dot = Vector3.Dot(Vector3.up, dirToContact);

            if (contact.point.x > platformPos.x)
            {
                direction.direction = new Vector3((1 - dot), -direction.direction.y, 0f);
            }
            else if (contact.point.x < platformPos.x)
            {
                direction.direction = new Vector3(-(1 - dot), -direction.direction.y, 0f);
            }
            else
            {
                direction.direction = -direction.direction;
            }
        }

        private void CollideWithBorder(BorderType borderType, ref DirectionComponent direction)
        {
            if (borderType == BorderType.Bot)
            {
                World.CreateEntity().AddComponent<LoseConditionComponent>();
                direction.direction = Vector3.zero;
            }

            if (borderType == BorderType.Left || borderType == BorderType.Right)
                direction.direction.x = -direction.direction.x;

            if (borderType == BorderType.Top)
                direction.direction.y = -direction.direction.y;
        }

        private void CollideWithBlock(ContactPoint contact, ref DirectionComponent direction)
        {
            Vector3 contactPoint = contact.point;
            Collider blockCollider = contact.otherCollider;
            Vector3 blockPos = blockCollider.transform.position;


            Vector3 leftBot = new Vector3(blockPos.x - blockCollider.bounds.size.x * 0.5f, blockPos.y - blockCollider.bounds.size.y * 0.5f);
            Vector3 leftTop = new Vector3(blockPos.x - blockCollider.bounds.size.x * 0.5f, blockPos.y + blockCollider.bounds.size.y * 0.5f);

            Vector3 rightBot = new Vector3(blockPos.x + blockCollider.bounds.size.x * 0.5f, blockPos.y - blockCollider.bounds.size.y * 0.5f);
            Vector3 rightTop = new Vector3(blockPos.x + blockCollider.bounds.size.x * 0.5f, blockPos.y + blockCollider.bounds.size.y * 0.5f);

            // Intersect left or right edge
            if (AreSegmentsIntersecting(blockPos, contactPoint, leftBot, leftTop) ||
                AreSegmentsIntersecting(blockPos, contactPoint, rightBot, rightTop))
            {
                direction.direction.x = -direction.direction.x;
            }

            // Intersect top or bot edge
            if (AreSegmentsIntersecting(blockPos, contactPoint, leftTop, rightTop) ||
                AreSegmentsIntersecting(blockPos, contactPoint, leftBot, rightBot))
            {
                direction.direction.y = -direction.direction.y;
            }

        }

        private bool AreSegmentsIntersecting(Vector3 segment1Start, Vector3 segment1End, Vector3 segment2Start, Vector3 segment2End)
        {
            Vector3 directionSegment1 = segment1End - segment1Start;
            Vector3 directionSegment2 = segment2End - segment2Start;

            float denominator = Vector3.Cross(directionSegment1, directionSegment2).magnitude;

            if (denominator == 0)
            {
                return false;
            }

            Vector3 segmentToSegment = segment2Start - segment1Start;
            float segment1Parameter = Vector3.Cross(segmentToSegment, directionSegment2).magnitude / denominator;
            float segment2Parameter = Vector3.Cross(segmentToSegment, directionSegment1).magnitude / denominator;

            return (segment1Parameter >= 0 && segment1Parameter <= 1) && (segment2Parameter >= 0 && segment2Parameter <= 1);
        }

        public void Dispose()
        {
        }
    }
}