namespace FileExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string curPath = @"C:\";
            while(true)
            {
                Console.WriteLine($"Current path: {curPath}");
                Console.WriteLine("Select directory or file(type in 'q' to go back or 'p' to exit app): ");

                //Directories
                Console.WriteLine("\nDirectories: ");
                var dirs = Directory.GetDirectories(curPath);
                WriteTab(dirs);

                //Files
                Console.WriteLine("\n\nFiles: ");
                var files = Directory.GetFiles(curPath);
                WriteTab(files);
                Console.Write("\n");


                var selectIndex = Console.ReadLine().Trim().ToLower();
                Console.Clear();

                char mode = selectIndex[0];
                selectIndex = selectIndex.Substring(1,selectIndex.Length - 1);
                if(mode == 'p') Environment.Exit(0);
                else if(mode == 'q')
                {
                    if (curPath != @"C:\")
                    {
                        curPath = Directory.GetParent(curPath).ToString();
                    }
                    else
                    {
                        Console.WriteLine("Error: Can't go back");
                    }
                }
                else if(mode == 'd')
                {
                    if (IsInt(selectIndex))
                    {
                        int x = Convert.ToInt32(selectIndex);
                        if (x >= dirs.Length) continue;
                        curPath = dirs[x];
                    }
                    else
                    {
                        Console.WriteLine("Error: Bad index");
                    }
                }
                else if(mode == 'f')
                {
                    if (IsInt(selectIndex))
                    {
                        int x = Convert.ToInt32(selectIndex);
                        if(x >= files.Length) continue;
                        string filePath = files[x];

                        //File control
                        while(true)
                        {
                            Console.Clear();
                            Console.WriteLine($"Path: {filePath}");
                            Console.WriteLine($"File name: {Path.GetFileNameWithoutExtension(filePath)}");
                            Console.WriteLine($"File extension: {Path.GetExtension(filePath)}");
                            Console.WriteLine("Choose option: ");
                            Console.WriteLine("1) Read file(if txt)");
                            Console.WriteLine("2) Delete file");
                            Console.WriteLine("3) Copy file");
                            Console.WriteLine("4) Move file");
                            Console.WriteLine("5) Go back");
                            string odp = Console.ReadLine();
                            if (!IsInt(odp)) continue;
                            int option = Convert.ToInt32(odp);

                            //Read if txt
                            if (option == 1)
                            {
                                string ext = Path.GetExtension(filePath);
                                ext = ext.Substring(1,ext.Length - 1);
                                if(ext == "txt")
                                {
                                    Console.Clear();
                                    Console.WriteLine(File.ReadAllText(filePath));
                                    Console.WriteLine("Press any key to exit");
                                    Console.ReadKey();
                                }
                            }
                            //Delete
                            else if (option == 2)
                            {
                                Console.Clear();
                                Console.Write("Are you sure(y/n): ");
                                string res = Console.ReadLine().Trim().ToLower();
                                if(res == "y")
                                {
                                    File.Delete(filePath);
                                    break;
                                }
                            }
                            //Copy
                            else if (option == 3)
                            {
                                Console.Clear();
                                Console.Write("Type in destination(without file name and extension if you don't want to change it): ");
                                string des = Console.ReadLine();
                                if (!des.Contains('.'))
                                {
                                    if (des[des.Length - 1] != '\\') des += '\\';
                                    des += Path.GetFileName(filePath);
                                }
                                if(File.Exists(des))
                                {
                                    Console.WriteLine("File in this destination exits. Overwrite? (y/n)");
                                    string res = Console.ReadLine().Trim().ToLower();
                                    if(res == "y") File.Copy(filePath, des, true);
                                }
                                else
                                {
                                    File.Copy(filePath, des, false);
                                }
                            }
                            //Move
                            else if (option == 4)
                            {
                                Console.Clear();
                                Console.Write("Type in destination(without file name and extension if you don't want to change it): ");
                                string des = Console.ReadLine();
                                if (!des.Contains('.'))
                                {
                                    if (des[des.Length - 1] != '\\') des += '\\';
                                    des += Path.GetFileName(filePath);
                                }
                                if (File.Exists(des))
                                {
                                    Console.WriteLine("File in this destination exits. Overwrite? (y/n)");
                                    string res = Console.ReadLine().Trim().ToLower();
                                    if (res == "y") File.Move(filePath, des, true);
                                }
                                else
                                {
                                    File.Move(filePath, des, false);
                                }
                            }
                            //Go back
                            else if (option == 5)
                            {
                                Console.Clear();
                                break;
                            }
                        }

                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Bad index");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                else
                {

                }
            }
        }
        private static bool IsInt(string word)
        {
            char[] ints = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach(char c in word)
            {
                if (!ints.Contains(c)) return false;
            }
            return true;
        }
        private static void WriteTab(string[] tab)
        {
            int maxLength = 15;
            int n = 0;
            foreach(string s in tab)
            {
                string fileName = Path.GetFileName(s);
                if (n <= 9) fileName = ' ' + fileName;
                if(fileName.Length > maxLength)
                {
                    fileName = fileName.Substring(0, maxLength);
                    fileName += "...";
                }
                else
                {
                    for(int i=fileName.Length;i<maxLength+3;i++)
                    {
                        fileName += ' ';
                    }
                }
                Console.Write($"{n++}) {fileName}\t");
                if ((n) % 4 == 0) Console.Write("\n");
            }

        }
    }
}