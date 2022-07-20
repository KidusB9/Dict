//kidus berhanu
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dictionary
{

    public class AVL 
    {

        Node root;
        public AVL()
        {
            

        }
        public void Add(DicIndex data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {

                root = RecursiveInsert(root, newItem);
            }
        }

        private int CompareNode(Node First, Node Second)
        {
            if (First == null)
                return 1;
            else if (Second == null)
                return -1;
            else
            {
                char[] FNode = First.data.word;

                char[] SNode = Second.data.word;


                string charsStr = new string(FNode);
                string charsStr2 = new string(SNode);
                int x = charsStr.CompareTo(charsStr2);

                return x;
            }
        }


        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (CompareNode(n, current) == 0)
            {
                current.data.index.Insert(n.data.index.ToDataArray()[0]);
                current = balance_tree(current);
            }
            else if (CompareNode(n, current) == -1)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (CompareNode(n, current) == 1)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }

        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
       
        public void Delete(Node target)
        {//and here
            root = Delete(root, target);
        }
        private Node Delete(Node current, Node target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                //left subtree
                if (CompareNode(target,current) == -1   )
                {
                    current.left = Delete(current.left, target);
                    if (balance_factor(current) == -2)//here
                    {
                        if (balance_factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                //right subtree
                else if (CompareNode(target, current) ==  1)
                {
                    current.right = Delete(current.right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //if target is found
                else
                {
                    if (current.right != null)
                    {
                        //delete its inorder successor
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent);
                        if (balance_factor(current) == 2)//rebalancing
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }
        /*   */
        public Node Find(Node key)
        {
            Node a = Find(key, root);
            if (CompareNode(a, key) == 0)
            {
                return a;
            }
            else
            {
                DicIndex d = new DicIndex();
                d.word = null;
                d.index.Insert(-1);
                Node ret = new Node(d);
                return ret;

            }
        }
        private Node Find(Node target, Node current)
        {
            if (current != null)
            {
                if (CompareNode(target, current) == -1)
                {
                    if (CompareNode(target, current) == 0)
                    {
                        return current;
                    }
                    else
                        return Find(target, current.left);
                }
                else
                {
                    if (CompareNode(target, current) == 0)
                    {
                        return current;
                    }
                    else
                        return Find(target, current.right);
                }
            }
            else
                return current;

        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {

                InOrderDisplayTree(current.left);
                foreach (long n in current.data.index.ToDataArray())
                {
                    Console.Write("Word - ({0}) Index -({1}) ", new string(current.data.word), n);
                }
                InOrderDisplayTree(current.right);
              
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }

        private void find_matches(Node node, string regex, List<Node> matches)
        {
            if (node != null)
            {
                find_matches(node.left, regex, matches);
                if (Regex.IsMatch(new string(node.data.word), regex))
                {
                    matches.Add(node);
                }
                find_matches(node.right, regex, matches);
            }
        }

        public void find_matches(string regex, List<Node> matches)
        {
            find_matches(root, regex, matches);
        }
    }
}
 
