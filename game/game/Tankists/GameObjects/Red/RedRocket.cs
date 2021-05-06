using JUnity.Basic;

namespace Tankists.GameObjects
{
    class RedRocket : Rocket
    {
        public RedRocket()
            : base(@"Textures\Red\Red_rocket.png")
        {
            animator.AnimationFrames.SetOffsetToAll(-70.0f, 0.0f);
            animator.AnimationFrames.SetRotationPointToAll(70.0f, 0.0f);
            animator.AnimationFrames.SetTextureRotationToAll(90.0f);
        }
    }
}
