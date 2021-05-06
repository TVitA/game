using JUnity.Basic;
using JUnity.Graphics;
using JUnity.Physics;
using OpenTK;
using Tankists.GameObjects;

namespace Tankists
{
    /// <summary>
    /// Box base class
    /// </summary>
    public abstract class BoxBase : GameObject
    {
        Rigidbody rigidbody;
        SpriteRenderer spriteRenderer;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="emplemPath">Emblem sprite path</param>
        /// <param name="pos">Spawn position</param>
        public BoxBase(string emplemPath, Vector2 pos)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidbody = AddComponent<Rigidbody>();

            rigidbody.Colliders.Add(new BoxCollider(new Vector2(-20.0f, -20.0f), new Vector2(20.0f, 20.0f)));
            rigidbody.Colliders[0].IsTrigger = true;
            rigidbody.resistance.Y = 0.5f;
            rigidbody.Mass = 1.0f;
            rigidbody.OnTriggerEnter += Rigidbody_OnTriggerEnter;
            position = pos;

            var sprite = spriteRenderer.Sprites.AddByName(@"Textures\Same\Box.png");
            sprite.scale = new Vector2(0.15f, 0.15f);
            var emblem = spriteRenderer.Sprites.AddByName(emplemPath);
            emblem.Width = 30;
            emblem.Height = 30;
            emblem.ZOrder = 0.1f;
        }

        private void Rigidbody_OnTriggerEnter(object sender, TriggerEnterEventArgs e)
        {
            var tank = e.other.Rigidbody.owner as Tank;
            if (tank != null)
            {
                Decorate(tank);
                Destroy();
            }
        }

        /// <summary>
        /// Fixed update method
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void FixedUpdate(double deltaTime)
        {
            if (position.Y - rigidbody.Colliders[0].Heigth / 2.0f < 0.0f)
            {
                position.Y = rigidbody.Colliders[0].Heigth / 2.0f;
                rigidbody.UseGravity = false;
            }
        }

        /// <summary>
        /// Decorate method
        /// </summary>
        /// <param name="tank">Tank to decorate</param>
        public abstract void Decorate(Tank tank);
    }
}
