using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HUB
{
    class Router
    {
        public readonly int Port = 443;

        private ICaptureDevice GlobalDeviceClient = null;
        private ICaptureDevice GlobalDeviceServer = null;

        public readonly IPAddress ipSourceAddress;
        public readonly IPAddress ipDestinationAddress;


        public readonly PhysicalAddress MACsource;

        public readonly PhysicalAddress MACdestination;

        public event Action<object, IpPacket> Capture;

        private SyncQueue<EthernetPacket> query;//очередь для хранения пакетов

        private Thread proc;//поток для обработки пакетов в отдельном потоке


        public Router(IPAddress ipSource, IPAddress ipDestination, PhysicalAddress MAC_source, PhysicalAddress MAC_destination, int port_interest)
        {
            ipSourceAddress = ipSource;

            ipDestinationAddress = ipDestination;

            MACsource = MAC_source;

            MACdestination = MAC_destination;

            Port = port_interest;

            query = new SyncQueue<EthernetPacket>();//очередь для пакетов  

            proc = new Thread(ProcessPacket); //создаем поток для обработке пакетов

        }

       
        public void Start()
        {
            var deviceList = CaptureDeviceList.Instance;// захват всех сетевых устройств 

            GlobalDeviceClient = deviceList[0];
            GlobalDeviceServer = deviceList[1];

            proc.Start();// старт потока по обработке пакетов

            //// MAC1 -> MAC2
            GlobalDeviceClient.OnPacketArrival += new PacketArrivalEventHandler(Program_OnPacketArrival10);
            GlobalDeviceClient.Open();
            GlobalDeviceClient.StartCapture();


            //// MAC2 -> MAC1
            GlobalDeviceServer.OnPacketArrival += new PacketArrivalEventHandler(Program_OnPacketArrival10);
            GlobalDeviceServer.Open();
            GlobalDeviceServer.StartCapture();

        }


        void Program_OnPacketArrival10(object sender, CaptureEventArgs e)
        {
            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data) as EthernetPacket;


            // Проверка  наличия пакетов уровня IP
            var ipPacket = (IpPacket)packet.Extract(typeof(IpPacket));

            if (ipPacket == null)
                return;

            // критерий отбора по ip
            if ((ipPacket.SourceAddress == ipSourceAddress || ipPacket.DestinationAddress == ipDestinationAddress))
                query.PutValue(packet);
            else
            {
                //подмена МАС
                if (packet.SourceHwAddress.Equals(MACsource))
                {
                    packet.SourceHwAddress = GlobalDeviceServer.MacAddress;
                    packet.DestinationHwAddress = MACdestination;
                    GlobalDeviceServer.SendPacket(packet);
                }

                if (packet.SourceHwAddress.Equals(MACdestination))
                {
                    packet.SourceHwAddress = GlobalDeviceClient.MacAddress;
                    packet.DestinationHwAddress = MACsource;
                    GlobalDeviceClient.SendPacket(packet);
                }
            }

        }


        private void ProcessPacket()
        {
            foreach (EthernetPacket packet in query)
            {
                // Проверка  наличия пакетов уровня IP
                var ipPacket = (IpPacket)packet.Extract(typeof(IpPacket));

                try
                {
                    var send = Capture; if (send != null)
                        send(this, ipPacket);

                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }

                //подмена МАС
                if (packet.SourceHwAddress.Equals(MACsource))
                {
                    packet.SourceHwAddress = GlobalDeviceServer.MacAddress;
                    packet.DestinationHwAddress = MACdestination;
                    GlobalDeviceServer.SendPacket(packet);
                }

                if (packet.SourceHwAddress.Equals(MACdestination))
                {
                    packet.SourceHwAddress = GlobalDeviceClient.MacAddress;
                    packet.DestinationHwAddress = MACsource;
                    GlobalDeviceClient.SendPacket(packet);

                }
            }
        }

    }

}

