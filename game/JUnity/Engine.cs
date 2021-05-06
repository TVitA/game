using JUnity.Basic;
using JUnity.Graphics;
using JUnity.Input;
using JUnity.Physics.BaseColliderClasses;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace JUnity
{
    /// <summary>
    /// Main engine class
    /// </summary>
    public sealed class Engine : GameWindow
    {
        internal static Queue<GameObject> objectsToDelete;
        private static List<GameObject> gameObjects;
        private double elapsedTime;
        private static Engine engine;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Engine()
            : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            FixedUpdatePeriod = 0.001;
            engine = this;
            objectsToDelete = new Queue<GameObject>();
            gameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="drawColliders">Draw collider</param>
        public Engine(bool drawColliders)
            : this()
        {
            this.drawColliders = drawColliders;
        }

        /// <summary>
        /// All game objects
        /// </summary>
        public List<GameObject> GameObjects { get => gameObjects; }

        /// <summary>
        /// Drawing colliders flag
        /// </summary>
        public readonly bool drawColliders;

        /// <summary>
        /// Time between fixed updates periods
        /// </summary>
        public static double FixedUpdatePeriod { get; set; }

        /// <summary>
        /// Width of client window
        /// </summary>
        public static int ClientWidth { get; set; }

        /// <summary>
        /// Height of client width
        /// </summary>
        public static int ClientHeight { get; set; }

        /// <summary>
        /// Background color
        /// </summary>
        public Color ClearColor { get; set; }

        /// <summary>
        /// Register new game object
        /// </summary>
        /// <param name="object">Object to register</param>
        public static void RegisterObject(GameObject @object)
        {
            gameObjects.Add(@object);
            @object.OnRegisterObject();
        }

        /// <summary>
        /// Stops engine
        /// </summary>
        public static void Stop()
        {
            engine.Close();
        }

        internal static void UnregisterObject(GameObject @object)
        {
            gameObjects.Remove(@object);
        }

        /// <summary>
        /// On load function
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(ClearColor);
        }

        /// <summary>
        /// On update frame
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].OnUpdate(e.Time);
            }

            elapsedTime += e.Time;
            if (elapsedTime >= FixedUpdatePeriod)
            {
                InputManager.Update();
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].OnFixedUpdate(elapsedTime);
                }
                elapsedTime = 0.0;
            }

            while (objectsToDelete.Count != 0)
            {
                var tmp = objectsToDelete.Dequeue();
                foreach (var component in tmp.Components)
                {
                    component.Dispose();
                }
                foreach (var component in tmp.FixedComponents)
                {
                    component.Dispose();
                }

                UnregisterObject(tmp);
            }
        }

        /// <summary>
        /// On render frame function
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            
            SpriteRenderer.RenderScene();

            if (drawColliders)
            {
                for (int i = 0; i < Collider.allColliders.Count; i++)
                {
                    Collider.allColliders[i].Draw();
                }
            }

            SwapBuffers();
        }

        /// <summary>
        /// On resize function
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, Width, 0.0, Height, 0.0, 1.0);

            ClientWidth = ClientSize.Width;
            ClientHeight = ClientSize.Height;

            GL.MatrixMode(MatrixMode.Modelview);
        }

        /// <summary>
        /// On unload function
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnUnload(EventArgs e)
        {
            while (gameObjects.Count > 0)
            {
                gameObjects[0].Destroy();
                UnregisterObject(gameObjects[0]);
            }

            Texture2D.Reset();
        }
    }
}
