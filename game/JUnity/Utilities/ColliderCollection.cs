using JUnity.Physics;
using JUnity.Physics.BaseColliderClasses;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JUnity.Utilities
{
    /// <summary>
    /// Collider collection
    /// </summary>
    public class ColliderCollection : ICollection<Collider>, IDisposable
    {
        private readonly List<Collider> colliders = new List<Collider>();

        /// <summary>
        /// Collection indexator
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Collider this[int index]
        {
            get => colliders[index];
            set => colliders[index] = value;
        }

        /// <summary>
        /// Elements count
        /// </summary>
        public int Count => colliders.Count;

        /// <summary>
        /// Is collection readonly
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Add item
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(Collider item)
        {
            colliders.Add(item);
        }

        /// <summary>
        /// Clear collection
        /// </summary>
        public void Clear()
        {
            colliders.Clear();
        }

        /// <summary>
        /// Is collection contains item
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>Troo if contains</returns>
        public bool Contains(Collider item)
        {
            return colliders.Contains(item);
        }

        /// <summary>
        /// Copy collection to array
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="arrayIndex">Start index in array</param>
        public void CopyTo(Collider[] array, int arrayIndex)
        {
            colliders.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<Collider> GetEnumerator()
        {
            return colliders.GetEnumerator();
        }

        /// <summary>
        /// Remove item from collection
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns></returns>
        public bool Remove(Collider item)
        {
            return colliders.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return colliders.GetEnumerator();
        }

        private bool isDisposed = false;

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    foreach (var collider in colliders)
                    {
                        collider.Dispose();
                    }
                }

                isDisposed = true;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~ColliderCollection()
        {
            Dispose(false);
        }
    }
}
