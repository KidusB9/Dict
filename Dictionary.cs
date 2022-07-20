namespace Dictionary
{
    [System.Serializable]
    class Dictionary
    {
           char[] Word = new char[50];
           char[] Pron = new char[60];
           char[] Type = new char[4];
           char[] Meaning = new char[200];


        public char[] word
        {
            get
            {
                return Word;
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    word[i] = value[i];
                }
            }
        }

        public char[] pron
        {
            get
            {
                return Pron;
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    Pron[i] = value[i];
                }
            }
        }

        public char[] type
        {
            get
            {
                return Type;
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    Type[i] = value[i];
                }
            }
        }

        public char[] meaning
        {
            get
            {
                return Meaning;
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    Meaning[i] = value[i];
                }
            }
        }




    }
}
    
