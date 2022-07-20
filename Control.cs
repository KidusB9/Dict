using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Dictionary
{
    class Control
    {
        AVL  tree = new AVL();
        const string fileName = "Dictiona1.dat";
        const string tempfileName = "temp.dat";
        public void show_menu()
        {

            Console.Clear();
            Console.WriteLine("Menu");
            Console.WriteLine("1. Add Item ");
            Console.WriteLine("2. Search ");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. View all");
            Console.WriteLine("5. Edit");
            Console.WriteLine("6. Exit");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            { 
                
                additem();
                
            }
            else if (choice == 4)
            {
                view_all( );
            }
            else if (choice == 3)
            {
                delete_node();
            }
            else if (choice == 2)
            {
                find_node();
            }

            else if (choice == 5)
            {
                edit_node();
            }
            else if (choice == 6)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid Input");
                show_menu();
            }

        }
        void delete_node()
        {
            Console.WriteLine("Word - > ");
            DicIndex s = new DicIndex();
            s.word = Console.ReadLine().ToCharArray();
            Node search = new Node(s);


            Node result = tree.Find(search);

            if (result.data.word == null)
                Console.WriteLine("Nth Found");
            else
            {

                foreach (long n in result.data.index.ToDataArray())
                {
                    // Console.WriteLine("Word" + new string(result.data.word));
                    // Console.WriteLine("Index" + n);
                    view_one(n);
                }
            }
            Console.WriteLine("Enter Num for Delete- >");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num <= result.data.index.ToDataArray().Length)
            {
                long delete = result.data.index.ToDataArray()[num - 1];
                  if(result.data.index.Remove(delete))
                    Console.WriteLine("Deleted ");
                if (result.data.index.IsEmpty())
                    tree.Delete(result);

                Dictionary input = new Dictionary();
                input.word = "null".ToCharArray();
               
                word_edit_file(input, delete);
                Console.WriteLine("del Pos - > " + delete);

            }

            Console.ReadKey();
            show_menu();


        }
        void edit_node()
        {
            Console.WriteLine("Word - > ");
            DicIndex s = new DicIndex();
            s.word = Console.ReadLine().ToCharArray();
            Node search = new Node(s);


            Node result = tree.Find(search);

            if (result.data.word == null)
                Console.WriteLine("Nth Found");
            else
            {
                
                foreach (long n in result.data.index.ToDataArray())
                {  
                    view_one(n);
                }
            }
            Console.WriteLine("Enter Num for Edit- >");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num <= result.data.index.ToDataArray().Length)
            {
                long edit=result.data.index.ToDataArray()[num - 1];
                Dictionary input = new Dictionary();
                input.word = result.data.word;
                Console.WriteLine("pronounce - > ");
                input.pron= Console.ReadLine().ToCharArray();

                Console.WriteLine("Type - > ");
                input.type = Console.ReadLine().ToCharArray();

                Console.WriteLine("Meaning - > ");
                input.meaning = Console.ReadLine().ToCharArray();

                word_edit_file(input, edit);
                Console.WriteLine("Edit Pos - > " + edit);

            }

            Console.ReadKey();
            show_menu();
        }
        void find_node( )
        {
             Console.WriteLine("Word - > ");            
             DicIndex s = new DicIndex();
            //   s.word = Console.ReadLine().ToCharArray();
            string str = Console.ReadLine();
            s.word = str.ToCharArray();
            Node search = new Node(s);

            // string str = new string(s.word);
         
            if (str != null)
            {
                if (str.Contains("*"))
                {
                    find_similar(str);
                    return;
                }
            }

            Node result = tree.Find(search);

            if (result.data.word == null)
                Console.WriteLine("Nth Found");
            else
            {

                foreach (long n in result.data.index.ToDataArray())
                {
                   // Console.WriteLine("Word" + new string(result.data.word));
                   // Console.WriteLine("Index" + n);
                    view_one(n);
                }
            }
            Console.ReadKey();
            show_menu();
        }

        void find_similar(string str)
        {
            int lastCharIndex = str.Length - 1;
            string regex;
            List<Node> matches = new List<Node>();

            // Case: *abc*
            if (str[0] == '*' && str[lastCharIndex] == '*')
            {
                regex = ".*" + str.Substring(1, lastCharIndex - 1) + ".*";
            }
            // Case *abc
            else if (str[0] == '*')
            {
                regex = ".*" + str.Substring(1);
            }
            // Case abc*
            else if (str[lastCharIndex] == '*')
            {
                regex = str.Substring(0, str.Length - 1);
            }
            // Case a*bC
            else
            {
                Console.WriteLine("Invalid wildcard placement");
                regex = null;
            }
            
            if (!string.IsNullOrEmpty(regex))
            {
                tree.find_matches(regex, matches);
                if (matches.Count > 0)
                {
                    foreach (Node n in matches)
                    {

                        foreach (long p in n.data.index.ToDataArray())
                        {
                            view_one(p);
                        }

                    }
                }
            }
            Console.ReadKey();
            show_menu();
        }

        void additem( )
        {
            string temp; 
            // Create A dictionary object and recive input from user
             Dictionary input = new Dictionary();
             Console.WriteLine("Word - > ");
             temp = Console.ReadLine();
             input.word=temp.ToCharArray();
             Console.WriteLine("pronounce - > ");
             temp = Console.ReadLine();
             input.pron = temp.ToCharArray();
             Console.WriteLine("Type - > ");
             temp = Console.ReadLine();
             input.type =temp.ToCharArray();
             Console.WriteLine("Meaning - > ");
             temp = Console.ReadLine();
             input.meaning =temp.ToCharArray();

             DicIndex Dindex = new DicIndex();
             Dindex.word = input.word; 
             Dindex.index.Insert(word_add(input));
             tree.Add(Dindex);


            Console.WriteLine("Item Added ");
            Console.ReadKey();
            show_menu();


        }
         
        long word_add(Dictionary input)
        {
            long index;
           
                if (!File.Exists(fileName))
                {
                    FileStream fileStream = new FileStream(fileName, FileMode.Create);
                    fileStream.Close();
                }
                using (FileStream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                { 
                    
                    BinaryFormatter formatter = new BinaryFormatter();
                     index = stream.Position;
                    formatter.Serialize(stream, input);
                    stream.Close();
                }
            
            return index;
        }


        long word_edit_file(Dictionary input, long pos)
        {
            long index;

            if (!File.Exists(fileName))
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                fileStream.Close();
            }
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Write))
            {

                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = pos;
                index = stream.Position;
                formatter.Serialize(stream, input);
                stream.Close();
            }

            return index;
        }

        void view_one(long Pos )
        {
            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                     

                        stream.Position = Pos;
                        Dictionary read = (Dictionary)formatter.Deserialize(stream);



                        Console.WriteLine("\t [+] Word  =" + new string(read.word));
                        Console.WriteLine("\t\t\t[-]Pronounce =" + new string(read.pron));
                        Console.WriteLine("\t\t\t[-]Type =" + new string(read.type));
                        Console.WriteLine("\t\t\t[-]Mean = " + new string(read.meaning));
                   
                    stream.Close();

                }




            }
            else
                Console.WriteLine("Emp File");




        }


        void view_all()
        {
            

            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    while (stream.Position < stream.Length)
                    {

                        Console.WriteLine("start =" + stream.Position);
                        Dictionary read = (Dictionary)formatter.Deserialize(stream);



                        Console.WriteLine("\t [+]Word  =" + new string(read.word));
                        Console.WriteLine("\t\t\t[-]Pronounce =" + new string(read.pron));
                        Console.WriteLine("\t\t\t[-]Type =" + new string(read.type));
                        Console.WriteLine("\t\t\t[-]Mean = " + new string(read.meaning));
                    }

                    stream.Close();

                }



                 
                }
 

            Console.WriteLine("================ Treeee ============= "  );
            tree.DisplayTree();
            Console.WriteLine("Tree End ");
            Console.ReadKey();
            show_menu();
        }

       public void init_avl()
         {
            
            if (File.Exists(fileName))
            {

                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    while (stream.Position < stream.Length)
                    {
                        DicIndex load = new DicIndex();
                       // Console.WriteLine("start =" + stream.Position);
                        load.index.Insert(stream.Position);

                        Dictionary read = (Dictionary)formatter.Deserialize(stream);
                        load.word = read.word;
                        tree.Add(load);



                    }

                    stream.Close();

                }




            }
            else
                Console.WriteLine("File init 0 ");


            Console.WriteLine("================ Treeee ============= ");
           tree.DisplayTree();
          Console.WriteLine("Tree End ");
            Console.ReadKey();
            show_menu();
        }

        public void init_clean()
        {      // ReCreate Temporary File
                FileStream fileStream1 = new FileStream(tempfileName, FileMode.Create);
                fileStream1.Close();


            if (File.Exists(fileName))
            {
                Dictionary ck = new Dictionary();
                ck.word = "null".ToCharArray();

                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {

                    FileStream stream2 = new FileStream(tempfileName, FileMode.Append, FileAccess.Write);


                    BinaryFormatter formatter = new BinaryFormatter();
                    BinaryFormatter formatter2 = new BinaryFormatter();
                    while (stream.Position < stream.Length)
                    {
                                       

                        Dictionary read = (Dictionary)formatter.Deserialize(stream);

                        if (new string(read.word) == new string(ck.word))
                            Console.WriteLine("Cleaning  ---> ");
                        else
                           formatter2.Serialize(stream2, read);
                        
                        
                    }

                    stream2.Close();


                }
                System.IO.File.Delete(fileName);
                System.IO.File.Move(tempfileName, fileName);
            }


                }
        }




    }


    

 
