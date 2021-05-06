using JUnity;
using JUnity.Basic;
using JUnity.Graphics;
using JUnity.Physics;
using JUnity.Physics.BaseColliderClasses;
using OpenTK;

namespace Tankists.GameObjects
{
    class Mountain : GameObject
    {
        Sprite sprite;

        public Mountain()
        {
            var sp = AddComponent<SpriteRenderer>();
            var rigid = AddComponent<Rigidbody>();
            rigid.UseGravity = false;

            rigid.Colliders.Add(new PolygonCollider(new[]
            {
                new Vector2(-140, -90),
                new Vector2(-140, -69),
                new Vector2(-93, 23),
                new Vector2(-75, 62),
                new Vector2(-46, 80),
                new Vector2(8, 87),
                new Vector2(92, 50),
                new Vector2(104, 33),
                new Vector2(139, -58),
                new Vector2(139, -90),
            }));

            sprite = sp.Sprites.AddByName(@"Textures\Same\Mountain.png", OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest, OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);
            sprite.ZOrder = 14.9f;
        }

        public override void Update(double deltaTime)
        {
            position = new Vector2(Engine.ClientWidth / 2.0f, sprite.Height / 2.0f);
        }
    }
}
