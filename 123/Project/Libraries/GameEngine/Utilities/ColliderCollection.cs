using System;
using System.Collections;
using System.Collections.Generic;

using GameEngine.Physics.BaseColliderClasses;

namespace GameEngine.Utilities
{
    /// <summary>
    /// Collider collection
    /// </summary>
    public class ColliderCollection : Object, ICollection<Collider>, IDisposable
    {
        /// <summary>
        /// Checker of using dispose method.
        /// </summary>
        private Boolean isDisposed;

        /// <summary>
        /// List of colliders.
        /// </summary>
        private readonly List<Collider> colliders = new List<Collider>();

        /// <summary>
        /// ColliderCollection constructor.
        /// </summary>
        public ColliderCollection()
            : base()
        {
            isDisposed = false;
        }

        /// <summary>
        /// Colliders count.
        /// </summary>
        public Int32 Count => colliders.Count;

        /// <summary>
        /// Is collection readonly.
        /// </summary>
        public Boolean IsReadOnly => false;

        /// <summary>
        /// Collection indexator.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>Collider.</returns>
        public Collider this[Int32 index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return colliders[index];
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }    

                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                colliders[index] = value;
            }
        }

        /// <summary>
        /// Add collider in collection.
        /// </summary>
        /// <param name="collider">Collider to add.</param>
        public void Add(Collider collider)
        {
            if (collider == null)
            {
                throw new ArgumentNullException();
            }    

            colliders.Add(collider);
        }

        /// <summary>
        /// Is collection contains collider.
        /// </summary>
        /// <param name="collider">Collider to check.</param>
        /// <returns>True if contains collider, else false.</returns>
        public Boolean Contains(Collider collider)
        {
            return colliders.Contains(collider);
        }

        /// <summary>
        /// Remove collider from collection.
        /// </summary>
        /// <param name="collider">Collider to remove.</param>
        /// <returns>True if removing, else false.</returns>
        public Boolean Remove(Collider collider)
        {
            return colliders.Remove(collider);
        }

        /// <summary>
        /// Clear collection.
        /// </summary>
        public void Clear()
        {
            colliders.Clear();
        }

        /// <summary>
        /// Copy collection to array.
        /// </summary>
        /// <param name="array">Array.</param>
        /// <param name="arrayIndex">Start index in array.</param>
        public void CopyTo(Collider[] array, Int32 arrayIndex)
        {
            colliders.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns enumerator.
        /// </summary>
        /// <returns>Enumerator.</returns>
        public IEnumerator<Collider> GetEnumerator()
        {
            return colliders.GetEnumerator();
        }

        /// <summary>
        /// Returns enumerator.
        /// </summary>
        /// <returns>Enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return colliders.GetEnumerator();
        }

        /// <summary>
        /// Releasing resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releasing resources.
        /// </summary>
        /// <param name="disposing">Releasing managed resources.</param>
        public void Dispose(Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    foreach (Collider collider in colliders)
                    {
                        collider.Dispose();
                    }
                }

                isDisposed = true;
            }
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~ColliderCollection()
        {
            Dispose(false);
        }
    }
}
