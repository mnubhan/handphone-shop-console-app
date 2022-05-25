// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            char _ch;
            //only for one user
            // Handphone hp = new Handphone();
            Handphone hp;

            //make user list for json
            List<Handphone> HpList = new List<Handphone>();



            Console.WriteLine("Welcome to Z Gadget");

            Console.WriteLine("Press 'x' to exit or any key to continue....");

            while ((_ch = Console.ReadKey(true).KeyChar) != 'x')
            {

                hp = new Handphone();

                Console.WriteLine("Please enter your Name :");
                hp.Name = Console.ReadLine();

                Console.WriteLine("Please enter your HP Number :");
                hp.HpNumber = Console.ReadLine();

                Console.WriteLine("Please enter Handphone Name :");
                hp.HandphoneName = Console.ReadLine();

                Console.WriteLine("Please enter Handphone Brand:");
                hp.HandphoneBrand = Console.ReadLine();

                Console.WriteLine("Please enter Price:");
                hp.Price = int.Parse(Console.ReadLine());

                Console.WriteLine("Please enter your Quantity:");
                hp.Quantity = int.Parse(Console.ReadLine());

                //link event to class object
                hp.EventHandler+=OnQuantityCheck;
                HpList.Add(hp);

                Console.WriteLine("Press 'x' to exit or any key to continue....");
            }

            foreach (Handphone hpl in HpList)
            {
                Console.WriteLine("*******************************");
                Console.WriteLine($"UserID               : {hpl.UserID}");
                Console.WriteLine($"Name                 : {hpl.Name}");
                Console.WriteLine($"HP Number            : {hpl.HpNumber}");
                Console.WriteLine($"Handphone Name       : {hpl.HandphoneName}");
                Console.WriteLine($"Handphone Brand      : {hpl.HandphoneBrand}");
                Console.WriteLine($"Price                : {hpl.Price}");
                Console.WriteLine($"Quantity             : {hpl.Quantity}");
                Console.WriteLine($"Total Price          : {hpl.Total()}");
                Console.WriteLine("-------------------------------");
            }

            //Write to Json file
            string jsonObj = JsonConvert.SerializeObject(HpList, Formatting.Indented);
            File.WriteAllText(@"./HpList.json",jsonObj);

            jsonObj = JsonConvert.SerializeObject(HpList[0],Formatting.Indented);
            File.WriteAllText("singleUser.json",jsonObj);

            // string json = JsonConvert.SerializeObject(hp, Formatting.Indented);
            // Console.WriteLine(json);
            Console.ReadKey();
        }


        public static void OnQuantityCheck(object sender, QuantityEventArgs e)
        {
            Console.WriteLine(e.MessageEvent);
        }


        public class Handphone
        {
            public event EventHandler<QuantityEventArgs> EventHandler;
            //consturctor
            public Handphone()
            {
                UserID=0;
                HandphoneName = " ";
                HandphoneBrand = " ";
                HpNumber = "";
                Name = "";
                Price = 123;
                Quantity = 123;
            }
            //Object Properties
            // New Style
            public Int32 UserID { get;set; }
            // public static int GlobalUserID;
            public string Name { get; set; }
            public string HpNumber { get; set; }
            public string HandphoneName { get; set; }
            public string HandphoneBrand { get; set; }
            public int Price { get; set; }
            public int Quantity
            {
                get { return _quantity; }

                set
                {
                    if (CheckOnQuantity(value))
                    {
                        _quantity = value;
                    }
                }

            }
            private int _quantity;
            //Object Function
            public int Total()
            {
                return Quantity * Price;
            }

            // public int IncrementUserID()
            // {
            //     this.UserID = Interlocked.Increment(ref GlobalUserID);
            // }



            //for Event
            private bool CheckOnQuantity(int qty)
            {

                if ((this._quantity + qty) < 2)
                {
                    QuantityEventArgs args = new QuantityEventArgs();
                    args.MessageEvent = $"The quantity is allow to buy.";
                    OnQuantity(args);

                    return true;
                }
                else
                {
                    QuantityEventArgs args = new QuantityEventArgs();
                    args.MessageEvent = $"The quantity is not allow to buy. Maximum quantity buy is 2.";
                    OnQuantity(args);

                    return false;
                }
            }
            protected virtual void OnQuantity(QuantityEventArgs QuantityArg)
            {
                EventHandler<QuantityEventArgs> handle = EventHandler;

                if (handle != null)
                {
                    handle(this, QuantityArg);
                }
            }

        }
        public class QuantityEventArgs
        {
            public string MessageEvent { get; set; }
        }


    }
}

