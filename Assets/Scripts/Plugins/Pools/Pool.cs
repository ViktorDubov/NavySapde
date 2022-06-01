using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public class Pool<T>
    {
        public Pool(int size)
        {
            Elements = new T[size];
            Size = size;
            Pointer = 0;
        }
        private T[] elements;
        public T[] Elements
        {
            get
            {
                return elements;
            }
            protected set
            {
                elements = value;
            }
        }
        private int size;
        public int Size
        {
            get
            {
                return size;
            }
            protected set
            {
                size = value;
            }
        }
        private int pointer = 0;
        public int Pointer
        {
            get
            {
                return pointer;
            }
            protected set
            {
                if (value >= 0 && value < Size)
                {
                    pointer = value;
                }
                else
                {
                    pointer = 0;
                }
            }
        }
        public virtual T GetNext()
        {
            T element = Elements[Pointer];
            Pointer++;
            return element;
        }
    }
}