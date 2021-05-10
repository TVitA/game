namespace Game.GameObjects
{
    public class SecondRocket : Rocket
    {
        public SecondRocket()
            : base(@"Textures\Second player\Green_rocket.png")
        {
            Sprite.FlipX = true;

            Animator.AnimationFrames.SetFlipToAll(true, false);

            Animator.AnimationFrames.SetOffsetToAll(70.0f, 0.0f);
            Animator.AnimationFrames.SetRotationPointToAll(-70.0f, 0.0f);
            Animator.AnimationFrames.SetTextureRotationToAll(-90.0f);
        }
    }
}
