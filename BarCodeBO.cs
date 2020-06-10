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

        public void Print(string ipaddress, string zpl)
        {
            try
            {
                BarCodePrinterHandler barCodePrinterHandler =
                        barCodePrintHandlers.GetOrAdd(ipaddress, new BarCodePrinterHandler(ipaddress));
            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}
