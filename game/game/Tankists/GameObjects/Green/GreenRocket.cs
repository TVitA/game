using JUnity.Basic;

namespace Tankists.GameObjects
{
    class GreenRocket : Rocket
    {
        public GreenRocket()
            : base(@"Textures\Green\Green_rocket.png")
        {
            sprite.FlipX = true;
            animator.AnimationFrames.SetFlipToAll(true, false);

            animator.AnimationFrames.SetOffsetToAll(70.0f, 0.0f);
            animator.AnimationFrames.SetRotationPointToAll(-70.0f, 0.0f);
            animator.AnimationFrames.SetTextureRotationToAll(-90.0f);
        }
    }
}
