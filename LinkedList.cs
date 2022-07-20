using System;
using System.Collections.Generic;
using System.Text;
// size of Dictionary obj on file  = 529 
namespace Dictionary { 
    // SortedLinkedList
    [Serializable]
    public class LinkedList<T> where T : IComparable
    {
        protected LNode<T> Head;
        protected LNode<T> Tail;


        public LinkedList()
        {
            Head = Tail = null;
        }

        public LinkedList(LinkedList<T> list)
        {
            LNode<T> p = list.Head;

            while (p != null)
            {
                Insert(p.Data);
                p = p.Next;
            }
        }


        public bool IsEmpty()
        {
            return Head == null;
        }


        public LNode<T> Find(T data)
        {
            LNode<T> p = Head;

            while (p != null)
            {
                if (p.Data.CompareTo(data) == 0) // Workaraound for == not working with generics
                {
                    return p;
                }
                p = p.Next;
            }

            return null;
        }


        public bool Exists(T data)
        {
            return Find(data) != null;
        }


        public void Insert(T data)
        {
            LNode<T> insertedNode = new LNode<T>(data);

            if (IsEmpty())
            {
                Head = Tail = insertedNode;
                return;
            }

            LNode<T> current = Head;
            LNode<T> prev = null;

            while (current != null)
            {
                if (insertedNode.Data.CompareTo(current.Data) == 0) // data already exists. Not using Exists() method because it would iterate n times for every element
                {
                    break;
                }

                if (insertedNode.Data.CompareTo(current.Data) < 0) // data < current
                {
                    if (current == Head) // data is smaller than Head.Data
                    {
                        insertedNode.Next = Head;
                        Head = insertedNode;
                    }
                    else // Data is inserted anywhere between the head and tail
                    {
                        prev.Next = insertedNode;
                        insertedNode.Next = current;
                    }

                    break;
                }

                if (current == Tail) // Data is inserted at the end of the list
                {
                    Tail.Next = insertedNode;
                    Tail = insertedNode;
                    break;
                }

                prev = current;
                current = current.Next;
            }
        }


        public bool Remove(T data)
        {
            // Case 1: List is empty
            if (IsEmpty())
            {
                return false;
            }

            // Case 2: Node to delete is the head
            if (data.CompareTo(Head.Data) == 0)
            {
                Head = Head.Next ?? null;
                return true;
            }

            // Case 3: Node to delete is not the head
            LNode<T> nodeToDeletePrev = FindNodeToDeletePrev(data);
            if (nodeToDeletePrev != null)
            {
                nodeToDeletePrev.Next = nodeToDeletePrev.Next.Next;
                return true;
            }
            return false;
        }


        // Assumes the list is non empty and that the node to be deleted is not the head
        private LNode<T> FindNodeToDeletePrev(T data)
        {
            LNode<T> p = Head.Next;
            LNode<T> prev = Head;

            while (p != null)
            {
                if (p.Data.CompareTo(data) == 0) // Workaraound for == not working with generics
                {
                    return prev;
                }
                p = p.Next;
                prev = prev.Next;
            }

            return null;
        }


        public T[] ToDataArray()
        {
            if (Head == null)
            {
                return null;
            }
            int length = 0;
            LNode<T> node = Head;
            //count no of nodes 
            while (node != null)
            {
                length++;
                node = node.Next;
            }

            T[] dataArray = new T[length];
            node = Head;

            //copy the linked list into array
            for (int i = 0; i < length; i++)
            {
                dataArray[i] = node.Data;
                node = node.Next;
            }

            return dataArray;

        }

 
    }
}
