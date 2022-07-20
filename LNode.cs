using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary
{
    [Serializable] // file 
    public class LNode<T>
    {
        public T Data;
        public LNode<T> Next;

        public LNode(T data) // Constructor 
        {
            Data = data;
            Next = null;
        }
    }
}
