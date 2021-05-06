using JUnity.Basic;
using JUnity.Utilities;
using System;
using System.Collections.Generic;

namespace JUnity.Graphics
{
    /// <summary>
    /// Animator component
    /// </summary>
    public sealed class Animator : GameComponent
    {
        internal Animator(GameObject @object)
            : base(@object)
        {
            AnimationFrames = new SpriteCollection();
            AnimationFrames.OnCollectionChanged += AnimationFrames_OnCollectionChanged;
        }

        private void AnimationFrames_OnCollectionChanged(object sender, System.EventArgs e)
        {
            if (AnimationFrames.Count > 0)
            {
                enumerator = AnimationFrames.GetEnumerator();
                enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Animation frames
        /// </summary>
        public SpriteCollection AnimationFrames { get; }

        /// <summary>
        /// Animation delay
        /// </summary>
        public double Delay
        {
            get => delay;
            set
            {
                if (value > 0.0)
                {
                    delay = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Delay", "Delay cannot be less then 0");
                }
            }
        }

        /// <summary>
        /// Is animation paused
        /// </summary>
        public bool Paused { get; set; }

        /// <summary>
        /// Event on animation ended
        /// </summary>
        public event EventHandler OnAnimationEnded;

        /// <summary>
        /// Dispose interfase
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            AnimationFrames.Dispose();
        }

        internal override void CallComponent(double deltaTime)
        {
            if (enumerator != null)
            {
                if (!Paused)
                {
                    time += deltaTime;
                    if (time >= Delay)
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

                SpriteRenderer.renderOrders.Add(new RenderOrder(enumerator.Current, owner));
            }
        }

        private IEnumerator<Sprite> enumerator;
        private double time;
        private double delay;
    }
}
