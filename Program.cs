using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace QuickMemo
{
    class Program
    {
        static void New()
        {
            string entry="";
            Console.WriteLine("Type your memo and press enter");
            Prompt(ref entry);
            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(@"C:\QuickMemo\MyData.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Memo>("Memo");

                // Create your new Memo instance
                var memo = new Memo
                {
                    Data = entry,
                    SubmitDT = DateTime.Now

                };

                // Insert new Memo document (Id will be auto-incremented)
                col.Insert(memo);
                Console.WriteLine("Memo was added successfuly!");


            }
            
        }
        static void ListAll()
        {

            Console.WriteLine("Listing all memos...");

            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(@"C:\QuickMemo\MyData.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Memo>("Memo");

                // Query and select all
                var result = col.Find(Query.All(Query.Ascending), limit: 100);
                foreach (var x in result)
                {
                    Console.WriteLine("{0} {1}\t\t (Submitted on {2})", x.Id, x.Data, x.SubmitDT.ToString());
                }


            }
        }
        static void Remove()
        {
            string entry = "";
            Console.WriteLine("enter memo id to remove");
            Prompt(ref entry);
            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(@"C:\QuickMemo\MyData.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Memo>("Memo");

                
                col.Delete(x => x.Id == int.Parse(entry));


            }
            Console.WriteLine("Memo was deleted successfully!");
        }
            static void Prompt(ref string variable)
        {
            Console.Write(">>");
            variable = Console.ReadLine();
        }
        static void Help()
        {
            Console.WriteLine("Here's the list of commands you can use:");
            Console.WriteLine("new: Gets a new memo and inserts into database.");
            Console.WriteLine("ls: Lists all memos.");
            Console.WriteLine("remove: removes a specific memo by Id.");
            Console.WriteLine("clear: Clears the screen.");
            Console.WriteLine("exit: exits QuickMemo.");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t\tWelcome to QuickMemo by Ashkan Taravati");
            Console.WriteLine("Make sure to have a folder named QuickMemo in Drive C!");
            Console.WriteLine("use help to see available commands.");
           
            
            
            while(true)
            {
                string entry = "";
                Prompt(ref entry);
                switch (entry.ToLower())
                {
                    case "new":
                        {
                            New();
                            break;
                        }
                    case "ls":
                        {
                            ListAll();
                            break;
                        }
                    case "help":
                        {
                            Help();
                            break;
                        }
                    case "exit":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    case "clear":
                        {
                            Console.Clear();
                            break;
                        }
                    case "remove":
                        {
                            Remove();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong Command!");
                            break;
                        }
                }
            }
        }
    }
    public class Memo
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime SubmitDT { get; set; }
       
    }
}
