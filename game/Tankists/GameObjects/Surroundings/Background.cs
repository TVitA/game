using JUnity;
using JUnity.Basic;
using JUnity.Graphics;

namespace Tankists.GameObjects
{
    class Background : GameObject
    {
        Sprite sprite;

        public Background(string filename)
        {
            var tmp = AddComponent<SpriteRenderer>();
            sprite = tmp.Sprites.AddByName(filename);
            sprite.ZOrder = -100.0f;
        }

        public override void Update(double deltaTime)
        {
            sprite.Width = Engine.ClientWidth;
            sprite.Height = Engine.ClientHeight;
            position = new OpenTK.Vector2(Engine.ClientWidth / 2.0f, Engine.ClientHeight / 2.0f);
        }
    }
}
