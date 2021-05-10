using System;
using System.Drawing;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using GameEngine.Basic;
using GameEngine.Input;
using GameEngine.Graphics;
using GameEngine.Physics.BaseColliderClasses;

namespace GameEngine
{
    /// <summary>
    /// Game engine class.
    /// </summary>
    public sealed class Engine : GameWindow
    {
        /// <summary>
        /// Drawing colliders flag.
        /// </summary>
        private readonly System.Boolean drawColliders;

        /// <summary>
        /// Queue of game objects that will be destroyed.
        /// </summary>
        private static Queue<GameObject> objectsToDelete;
        /// <summary>
        /// List of game objects.
        /// </summary>
        private static List<GameObject> gameObjects;

        /// <summary>
        /// Created game engine.
        /// </summary>
        private static Engine gameEngine;

        /// <summary>
        /// Width of client window.
        /// </summary>
        private static Int32 clientWidth;
        /// <summary>
        /// Height of client window.
        /// </summary>
        private static Int32 clientHeight;

        /// <summary>
        /// Elapsed time between frames.
        /// </summary>
        private Double elapsedTime;
        /// <summary>
        /// Time between physical interactions.
        /// </summary>
        private Double fixedUpdatePeriod;

        /// <summary>
        /// Background color.
        /// </summary>
        private Color clearColor;

        /// <summary>
        /// Engine constructor.
        /// </summary>
        public Engine()
            : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            fixedUpdatePeriod = 0.001;
            gameEngine = this;
            objectsToDelete = new Queue<GameObject>();
            gameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Engine constructor.
        /// </summary>
        /// <param name="drawColliders">Draw colliders.</param>
        public Engine(System.Boolean drawColliders)
            : this()
        {
            this.drawColliders = drawColliders;
        }

        /// <summary>
        /// Returns queue of game objects that will be destroyed.
        /// </summary>
        internal static Queue<GameObject> ObjectsToDelete => objectsToDelete;

        /// <summary>
        /// Returns list of game objects.
        /// </summary>
        public static List<GameObject> GameObjects => gameObjects;

        /// <summary>
        /// Returns time between physical interactions.
        /// </summary>
        public Double FixedUpdatePeriod => fixedUpdatePeriod;

        /// <summary>
        /// Returns width of client window.
        /// </summary>
        public static Int32 ClientWidth
        {
            get => clientWidth;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                clientWidth = value;
            }
        }

        /// <summary>
        /// Returns height of client width.
        /// </summary>
        public static Int32 ClientHeight
        {
            get => clientHeight;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                clientHeight = value;
            }
        }

        /// <summary>
        /// Returns clear color.
        /// </summary>
        public Color ClearColor
        {
            get => clearColor;

            set => clearColor = value;
        }

        /// <summary>
        /// Register new game object.
        /// </summary>
        /// <param name="gameObject">Game object to register.</param>
        public static void RegisterObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException();
            }    

            gameObjects.Add(gameObject);

            gameObject.OnRegisterObject();
        }

        /// <summary>
        /// Unregister game object.
        /// </summary>
        /// <param name="gameObject">Game object to unregister.</param>
        internal static void UnregisterObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }

        /// <summary>
        /// On load function.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(clearColor);
        }

        /// <summary>
        /// On update frame.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            for (var i = 0; i < gameObjects.Count; ++i)
            {
                gameObjects[i].OnUpdate(e.Time);
            }

            elapsedTime += e.Time;

            if (elapsedTime >= FixedUpdatePeriod)
            {
                InputManager.Update();

                for (var i = 0; i < gameObjects.Count; ++i)
                {
                    gameObjects[i].OnFixedUpdate(elapsedTime);
                }

                elapsedTime = 0.0;
            }

            while (objectsToDelete.Count > 0)
            {
                var gameObject = objectsToDelete.Dequeue();

                foreach (var component in gameObject.Components)
                {
                    component.Dispose();
                }

                foreach (var fixedComponent in gameObject.FixedComponents)
                {
                    fixedComponent.Dispose();
                }

                UnregisterObject(gameObject);
            }
        }

        /// <summary>
        /// On render frame function.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            SpriteRenderer.RenderScene();

            if (drawColliders)
            {
                for (var i = 0; i < Collider.AllColliders.Count; ++i)
                {
                    Collider.AllColliders[i].Draw();
                }
            }

            SwapBuffers();
        }

        /// <summary>
        /// On resize function.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, Width, 0.0, Height, 0.0, 1.0);

            clientWidth = ClientSize.Width;
            clientHeight = ClientSize.Height;

            GL.MatrixMode(MatrixMode.Modelview);
        }

        /// <summary>
        /// On unload function.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnUnload(EventArgs e)
        {
            while (gameObjects.Count > 0)
            {
                gameObjects[0].Destroy();

                UnregisterObject(gameObjects[0]);
            }

            Texture2D.Reset();
        }

        /// <summary>
        /// Stops engine.
        /// </summary>
        public static void Stop()
        {
            gameEngine.Close();
        }
    }
}
