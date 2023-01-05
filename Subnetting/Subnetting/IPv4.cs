using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetting
{
    class IPv4
    {
        protected byte [] ipAddress;
        protected byte [] subnetMask;

        public IPv4()
        {
        }

        public byte[] GetIP_addr()
        {
            return this.ipAddress;
        }

        public string GetIP_addbool()      // ritorna l'ip in formato binario
        {
            string IPbinario = "";
            foreach(byte oct in this.ipAddress)
            {
                IPbinario += Convert.ToString(oct, 2).PadLeft(8, '0') + " ";
                //ToString(oct, 2) --> converte in binario
                //PadLeft(8,'0')--> Riempie l'ottetto nel caso in cui sia inferiore ad 8 del carattere '0' 
            }

            return IPbinario;
        }

        public byte[] GetSubnetMask()
        {
            return this.subnetMask;     //Torno la subnet
        }

        public bool CheckSubOct(byte octect)
        {
            string binarioSub = Convert.ToString(octect, 2);    //Converto in binario la stringa digitata
            if (binarioSub.Length > 1)                          //Se la lunghezza della stringa è maggiore di uno
            {
                for (int i = 0; i < 7; i++)
                {
                    if (binarioSub[i] == '0' && binarioSub[i + 1] == '1')       //se trovo uno 01111111 --> allora errore non può essere subnet mask
                    {
                        return false;           
                    }
                }
            }
            return true;
        }

        public bool checkFullSub()          
        {
            for (int i = 0; i < 3; i++)
            {
                if (this.subnetMask[i] < this.subnetMask[i+1])
                {
                    //Se l'ottetto successivo è maggiore del precedente torna un errore
                    return false;
                }
            }

            return true;
        }

        public void SetIp_address(byte[] Indirizzoip)        //Setto variabili della classe ipv4
        {
            this.ipAddress = Indirizzoip;
        }

        public void setSubnetMask(byte[] SubnetMask)        //Setto variabili della classe ipv4
        {
            this.subnetMask = SubnetMask;
        }

        public byte[] GetNetworkAddress()
        {
            byte[] networkAddress = new byte[4];

            for (int i = 0; i < this.ipAddress.Length; i++)
            {
                networkAddress[i] = (byte)(this.ipAddress[i] & this.subnetMask[i]);         //tipo di dato in uscita è un byte,Operatore logico and tra subnet mask e inidirizzi nei vari otteti
            }
            return networkAddress;
        }

        public byte[] GetBroadcast()
        {
            byte[] broadcastAddress = new byte[4];

            for (int i = 0; i < this.ipAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(this.GetNetworkAddress()[i] | this.GetWildcard()[i]);      //operazione di or tra inidirizzo Ip e wild mask
            }
            return broadcastAddress;
        }

        public double GetTotalNumberHost()
        {
            return Math.Pow(2, Convert.ToDouble(this.GetCIDR().Count(bitHost => bitHost == '0')));

            //Elevo al quadrato
            //il getCidr e conto dove bitHost == 0 e sarà il numero che verrà elevato al quadrato
        }

        public double GetNumberUsableHost()
        {
            return this.GetTotalNumberHost() - 2;       
            
            //Tolgo 2 al numero totale di host dal momento che quest'ultimi sono broadcast ed indirizzo di rete
        }

        public byte[] GetWildcard() 
        {
            string tmp = "";
            byte[] wildcard = new byte[4];
            int i=0;

            //Definisco le variabili

            foreach (byte oct in this.subnetMask)
            {
                tmp += Convert.ToString(oct, 2).Replace('0', '~').Replace('1', '0').Replace('~', '1').PadLeft(8,'1');           

                //Converte in binario l'ottetto in esame, rimpiazzo i vari caratteri con i numeri prestabiliti.
                //Se non arrivo a 8 caratteri aggiungo 1 fino ad arrivarci a partire da sinistra

                wildcard[i] = Convert.ToByte(tmp.Substring(8 * i, 8), 2);

                //converto in byte la stringa tmp
                //Applico il metodo SubString per prelevare delle sotto stringhe composte da 8 bit, e partendo dalla posizione 8*i
                //Ritorno il byte in binario

                i++;

                //Incremento la nostra variabile contatore
            }
            return wildcard;
        }

        public string GetCIDR() // ritorna maschera in bit
        {
            string SubnetBinario = "";

            foreach (byte oct in this.subnetMask)
            {
                SubnetBinario += Convert.ToString(oct, 2).PadLeft(8, '0') + " "; 
                //ToString(oct, 2) --> converte in binario
                //PadLeft(8,'0')--> Riempie l'ottetto nel caso in cui sia inferiore ad 8 del carattere '0' 
            }

            return SubnetBinario;
        }

        public byte[] GetFirstHostIP() 
        {
            byte[] firstIpAddress = new byte[4];
            firstIpAddress = this.GetNetworkAddress();
            firstIpAddress[3] += 1;

            //All'indirizzo di rete sommo uno nell'ultimo ottetto

            return firstIpAddress;
        }

        public byte[] GetLastHostIP() 
        {
            byte[] lastIpAddress = new byte[4];
            lastIpAddress = this.GetBroadcast();
            lastIpAddress[3] -= 1;

            //Tolgo uno al broadcast nell'ultimo otteto

            return lastIpAddress;
        }

        public string toString()
        {
            return $"Ip address: {this.ipAddress[0]}.{this.ipAddress[1]}.{this.ipAddress[2]}.{this.ipAddress[3]}\nSubnet Mask: {this.subnetMask[0]}.{this.subnetMask[1]}.{this.subnetMask[2]}.{this.subnetMask[3]}";
        }
    }
}
