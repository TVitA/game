using System;
using System.Collections.Generic;

using GameEngine.Basic;
using GameEngine.Utilities;

namespace GameEngine.Graphics
{
    /// <summary>
    /// Animator component
    /// </summary>
    public sealed class Animator : GameComponent
    {
        /// <summary>
        /// Event on animation ended
        /// </summary>
        public event EventHandler OnAnimationEnded;

        /// <summary>
        /// Animation frames.
        /// </summary>
        private SpriteCollection animationFrames;

        /// <summary>
        /// Enumerator of sprite collection.
        /// </summary>
        private IEnumerator<Sprite> enumerator;

        /// <summary>
        /// Time between frames.
        /// </summary>
        private Double time;

        /// <summary>
        /// Delay between animation frames.
        /// </summary>
        private Double delay;

        /// <summary>
        /// Is animation paused.
        /// </summary>
        private Boolean paused;

        /// <summary>
        /// Animator constructor.
        /// </summary>
        /// <param name="owner">Owner.</param>
        internal Animator(GameObject owner)
            : base(owner)
        {
            animationFrames = new SpriteCollection();

            animationFrames.OnCollectionChanged += AnimationFrames_OnCollectionChanged;
        }

        /// <summary>
        /// Returns animation frames.
        /// </summary>
        public SpriteCollection AnimationFrames => animationFrames;

        /// <summary>
        /// Returns animation delay.
        /// </summary>
        public Double Delay
        {
            get => delay;

            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException("Delay", "Delay cannot be less then 0");
                }

                delay = value;
            }
        }

        /// <summary>
        /// Returns whether is animation paused.
        /// </summary>
        public Boolean Paused
        {
            get => paused;

            set => paused = value;
        }

        /// <summary>
        /// OnCollectionChanged event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">EventArgs.</param>
        private void AnimationFrames_OnCollectionChanged(Object sender, EventArgs e)
        {
            if (animationFrames.Count > 0)
            {
                enumerator = animationFrames.GetEnumerator();

                enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Call every frame.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        internal override void CallComponent(Double deltaTime)
        {
            if (enumerator != null)
            {
                if (!Paused)
                {
                    time += deltaTime;

                    if (time >= delay)
                    {
                        time = 0.0;

                        if (!enumerator.MoveNext())
                        {
                            enumerator.Reset();
                            enumerator.MoveNext();
                            OnAnimationEnded?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }

                SpriteRenderer.RenderOrders.Add(new RenderOrder(enumerator.Current, Owner));
            }
        }

        /// <summary>
        /// Dispose overloading.
        /// </summary>
        /// <param name="disposing">Clean up managed code.</param>
        private protected override void Dispose(Boolean disposing)
        {
            animationFrames.Dispose();
        }
    }
}
