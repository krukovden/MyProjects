using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HUB
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // для тестирование и дальнейшего прокидивания пакетов в сеть надо сначала отключить службу маршрутизации Виндоус
            // суть мы анализируем пакеты которые приходят на одну сетевую карту и прокидываем их после анализа на другую
            int PortSSL = 443;
           
            IPAddress ipSourceAddress = IPAddress.Parse("192.168.10.10");

            IPAddress ipDestinationAddress = IPAddress.Parse("192.168.20.20");

            PhysicalAddress MACsource = new PhysicalAddress(new byte[] { 0x00, 0x0C, 0x29, 0xC2, 0x7E, 0xF3 }); // честные маки для прокидывания дальше

            PhysicalAddress MACdestination = new PhysicalAddress(new byte[] { 0x00, 0x0C, 0x29, 0xB6, 0x4F, 0x25 });
           
            Router router = new Router(ipSourceAddress, ipDestinationAddress, MACsource, MACdestination, PortSSL);

            router.Capture += Router_Capture;

            router.Start();

            while (true) ;


        }

        private static void Router_Capture(object arg1, IpPacket arg2)
        {
            Console.WriteLine(arg2);
        }
    }
}
