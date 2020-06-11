using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            BarCodeBO barCodeBO = new BarCodeBO();

            // printer 1 => 10.229.5.12
            Thread process1 = new Thread(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    string opdr = $"print opdracht: {i}";
                    await barCodeBO.PrintAsync("10.229.5.12", opdr);
                }
                Console.WriteLine("Alle print opdrachten voor printer 10.229.5.12 verstuurd.");
            });

            // printer 2 => 10.115.40.1
            Thread process2 = new Thread(async () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    string opdr = $"print opdracht: {i}";
                    await barCodeBO.PrintAsync("10.115.40.1", opdr);
                }
                Console.WriteLine("Alle print opdrachten voor printer 10.115.40.1 verstuurd.");

            });
                     
            process1.Start();
            process2.Start();

            Console.ReadLine();
        }
    }
}
