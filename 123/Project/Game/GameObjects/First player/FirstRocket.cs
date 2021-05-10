namespace Game.GameObjects
{
    /// <summary>
    /// First rocket class.
    /// </summary>
    public class FirstRocket : Rocket
    {
        /// <summary>
        /// First rocket constructor.
        /// </summary>
        public FirstRocket()
            : base(@"Textures\First player\Red_rocket.png")
        {
            Animator.AnimationFrames.SetOffsetToAll(-70.0f, 0.0f);
            Animator.AnimationFrames.SetRotationPointToAll(70.0f, 0.0f);
            Animator.AnimationFrames.SetTextureRotationToAll(90.0f);
        }
    }
}
