using JUnity;
using JUnity.Basic;
using JUnity.Graphics;

namespace Tankists.GameObjects.Surroundings
{
    class Plane : GameObject
    {
        SpriteRenderer spriteRenderer;
        float speed;
        Sprite sprite;
        float dropPoint;
        bool isFirst = true;
        bool isDropped;
        bool isAmmo;
        BoxesFactory factory = new BoxesFactory();

        public Plane()
        {
            speed = 500.0f;

            spriteRenderer = AddComponent<SpriteRenderer>();
            sprite = spriteRenderer.Sprites.AddByName(@"Textures\Same\Plane.png");
            sprite.ZOrder = 10.129f;
        }

        private void CheckBounds()
        {
            if (position.X + sprite.Width / 2.0f < -10.0f)
            {
                Destroy();
            }
        }

        public override void FixedUpdate(double deltaTime)
        {
            if (isFirst)
            {
                isFirst = false;
                position.X = Engine.ClientWidth + sprite.Width;
            }

            position.X -= speed * (float)deltaTime;

            position.Y = Engine.ClientHeight - sprite.Height / 2.0f - 10.0f;

            CheckBounds();
            Send();
        }

        private void Send()
        {
            if (position.X <= dropPoint && !isDropped)
            {
                if (isAmmo)
                {
                    Engine.RegisterObject(factory.GetAmmoBox(position));
                }
                else
                {
                    Engine.RegisterObject(factory.GetRandomBox(position));
                }
                
                isDropped = true;
            }
        }

        public static void SendPresent(Tank tank)
        {
            var plane = new Plane();
            plane.dropPoint = tank.position.X;
            Engine.RegisterObject(plane);
        }

        public static void SendAmmo(Tank tank)
        {
            var plane = new Plane()
            {
                isAmmo = true
            };
            plane.dropPoint = tank.position.X;
            Engine.RegisterObject(plane);
        }
    }
}
