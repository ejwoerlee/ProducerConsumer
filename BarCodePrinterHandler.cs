using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class BarCodePrinterHandler
    {
        const int boundedQueueSize = 100;   // max aantal labels die (tegelijkertijd) kunnen worden verwerkt per printer
        private readonly string ipAddress; // welke printer (ip)

        private Channel<String> printerChannel = Channel.CreateBounded<string>(boundedQueueSize);

        private Random randomizer = new Random();

        public BarCodePrinterHandler(string ipAddress)
        {
            this.ipAddress = ipAddress;
            Console.WriteLine($"Create BarCodePrintHandler: {ipAddress}");
            Task.Run(ConsumePrintOpdracht);
        }

        public async Task ProducePrintOpdracht(string zpl)
        {
            Console.WriteLine($"Zend naar zebraprinter: {ipAddress} tekst: {zpl}");
            await Task.Delay(GetRandomDelay()); // simuleer processing tijd            
            await printerChannel.Writer.WriteAsync(zpl);
        }

        public async Task ConsumePrintOpdracht()
        {
            while (await printerChannel.Reader.WaitToReadAsync())
            {
                // TryRead: in het geval van meerdere consumers zou een item toch al weg kunnen zijn.
                while (printerChannel.Reader.TryRead(out string zpl))
                {
                    Console.WriteLine($"** Print naar zebraprinter: {ipAddress} tekst: {zpl}");
                    await Task.Delay(GetRandomDelay()); // simuleer processing tijd                 
                    // await PrintLabelToZebraPrinter(document);
                }
            }
        }

        //private async Task PrintLabelToZebraPrinter(string zpl)
        //{
        //    return;
        //}

        private int GetRandomDelay()
        {            
            return randomizer.Next(2, 11) * 1000; // random tussen 2 en 10 sec
        }
    }
}
