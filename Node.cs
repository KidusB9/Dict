namespace Dictionary
{
    
        public class Node
        {
            public DicIndex data = new DicIndex();
            public Node left; 
            public Node right;
            public Node(DicIndex data)
            {
                this.data.word = data.word;
                this.data.index = data.index;
            }
        public Node()
        {

        }
        }
}
 
