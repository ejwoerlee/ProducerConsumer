using System;
using System.Collections.Generic;
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

        public BarCodePrinterHandler(string ipAddress)
        {
            this.ipAddress = ipAddress;
            Task.Run(ConsumePrintOpdracht);
        }

        public async Task ProducePrintOpdracht(string zpl)
        {
            await printerChannel.Writer.WriteAsync(zpl);
        }

        public async Task ConsumePrintOpdracht()
        {
            while (await printerChannel.Reader.WaitToReadAsync())
            {
                // in het geval van meerdere consumers zou een item toch al weg kunnen zijn.
                while (printerChannel.Reader.TryRead(out string zpl))
                {
                    // Debug.WriteLine($"printing document to zebraprinter: {zpl}");
                    // await PrintLabelToZebraPrinter(document);
                }
            }
        }

        public async Task PrintLabelToZebraPrinter(string zpl)
        {
            return;
        }

    }
}
