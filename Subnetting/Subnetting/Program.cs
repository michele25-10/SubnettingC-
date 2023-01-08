using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetting
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] ip = new byte[4];
            byte[] sm = new byte[4];

            bool check;
            string ottetto;

            IPv4 ipv4 = new IPv4();         //Dichiarazione classe

            for (int i = 0; i < 4; i++)     //Inserisco i 4 ottetti del primo Ip
            {
                do
                {
                    Console.WriteLine($"Inserisci il {i + 1} otteto dell'ip");
                    ottetto = Convert.ToString(Console.ReadLine());
                } while (!Byte.TryParse(ottetto, out ip[i]));     //Controllo sull'ottetto inserito che non sia maggiore del previsto
            }
            ipv4.SetIp_address(ip);     //Setto le variabili della classe

            Console.WriteLine("\n\n\n\n");

                for (int i = 0; i < 4; i++)     //Inse risco i 4 ottetti della subnet mask
                {
                    do
                    {
                        Console.WriteLine($"Inserisci il {i + 1} otteto della subnet mask");
                        ottetto = Convert.ToString(Console.ReadLine());
                        check = Byte.TryParse(ottetto, out sm[i]);
                    } while (!check || !ipv4.CheckSubOct(sm[i]));   //Ripeti finche il contollo non è true e controllo sulla subnet mask
                }
                ipv4.setSubnetMask(sm);         //Setto la variabile

            Console.Clear();

            //Comunico in output i risultati presi dall'ipv4
            Console.WriteLine($"\n{ipv4.toString()}\n");

            Console.WriteLine("-----------------------------------------------------------\n");

            Console.WriteLine($"Ip in bit: {ipv4.GetIP_addbool()}\n");
            Console.WriteLine($"Subnet Mask in bit: {ipv4.GetCIDR()}\n\n");
            Console.WriteLine($"Indirizzo di rete: {String.Join(".",ipv4.GetNetworkAddress())}\n");
            Console.WriteLine($"Indirizzo di broadcast: {String.Join(".", ipv4.GetBroadcast())}\n");
            Console.WriteLine($"Wildcard mask: {String.Join(".", ipv4.GetWildcard())}\n\n");
            Console.WriteLine($"Numero totale degli host: {ipv4.GetTotalNumberHost()}\n");
            Console.WriteLine($"Numero degli host utilizzabili: {ipv4.GetNumberUsableHost()}\n");
            Console.WriteLine($"Range: {String.Join(".", ipv4.GetFirstHostIP())} - {String.Join(".", ipv4.GetLastHostIP())}\n");

            Console.ReadKey();
        }
    }
}
