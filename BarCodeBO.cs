using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class BarCodeBO
    {
        private readonly ConcurrentDictionary<string, BarCodePrinterHandler> barCodePrintHandlers =
            new ConcurrentDictionary<string, BarCodePrinterHandler>();


        public BarCodeBO()
        {
        }

        public async Task PrintAsync(string ipaddress, string zpl)
        {
            try
            {
                //bool itemFound = barCodePrintHandlers.ContainsKey(ipaddress);
                //Console.WriteLine(itemFound ? $"{ipaddress} gevonden" : $"{ipaddress} niet gevonden");

                BarCodePrinterHandler barCodePrinterHandler =
                        barCodePrintHandlers.GetOrAdd(ipaddress, _ => new BarCodePrinterHandler(ipaddress));
                
                await barCodePrinterHandler.ProducePrintOpdracht(zpl);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
