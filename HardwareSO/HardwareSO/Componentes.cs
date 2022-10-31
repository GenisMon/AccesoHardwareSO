using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using WUApiLib;
using Microsoft.Win32;
using System.Diagnostics;

namespace HardwareSO
{
    public class Componentes
    {
        public static string Serial(string NroSerial)
        {
            ManagementObjectSearcher buscador = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMedia");
            foreach (ManagementObject objeto in buscador.Get())
            {
                if (objeto["SerialNumber"] != null)
                {
                    NroSerial = objeto["SerialNumber"].ToString();
                    return NroSerial;
                }
            }
            return null;
        }

        public static string UnidadesDeDisco()
        {
            DriveInfo[] Unidades = DriveInfo.GetDrives();
            string Disco = "";
            foreach (DriveInfo Unidad in Unidades)
            {
                Disco+= ($"Unidad {Unidad.Name}\n " + $"Tipo: {Unidad.DriveType}\n " + $"Espacio libre: {(Unidad.AvailableFreeSpace) / 1073741824 + " GB"}\n");

            }
            return Disco;
        }

        public static string NumeroDeProcesadores()
        {
            return ($"Numero de procesadores: {Environment.ProcessorCount}");
        }

        public static string Ram()
        {
            ManagementObjectSearcher Busqueda = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");
            foreach (ManagementObject Objeto in Busqueda.Get())
            {
                double Ram_Bytes = (Convert.ToDouble(Objeto["TotalPhysicalMemory"]));
                return ($"RAM Utilizable: {(Ram_Bytes) / 1073741824} GB");
            }
            return null;
        }

        public static string NIC()
        {
            NetworkInterface[] interfaz = NetworkInterface.GetAllNetworkInterfaces();
            string red = "";
            foreach (NetworkInterface adaptador in interfaz)
            { 
                red += ($"Name: {adaptador.Name}\n " + $"Descripcion: {adaptador.Description}\n " + $"Tipo de interfaz: {adaptador.NetworkInterfaceType}\n " + $"Estado operacional: {adaptador.OperationalStatus}\n " + "--------\n");
            }

            return red;
        }

        public static string MacAddress()
        {
            foreach (NetworkInterface NIC in NetworkInterface.GetAllNetworkInterfaces())
            {
                return NIC.GetPhysicalAddress().ToString();
            }
            return null;
        }

        public static string Patches()
        {
            var sesionActualización = new UpdateSession();
            var actualizarBuscador = sesionActualización.CreateUpdateSearcher();
            var cont = actualizarBuscador.GetTotalHistoryCount();
            var historia= actualizarBuscador.QueryHistory(0, cont);
            string mostrar = "";

            for (int i = 0; i < cont; ++i)
            {
                mostrar += (historia[i].Title + "\n");
            }
            return mostrar;
        }

        public static string LeerClave(string num1, string num2)
        {
            return Registry.GetValue(num1, num2, "Registro no encontrado.").ToString();
        }
        public static void EstablecerClave(string num1, string num2, string num3)
        {
            Registry.SetValue(num1, num2, num3);
        }
        public static string CrearClave(string num1, string num2, string num3)
        {
            Registry.SetValue(num1, num2, num3);
            return Registry.GetValue(num1, num2, "Registro no encontrado.").ToString();
        }
        public static void EliminarClave(string num1, string num2)
        {
            if (num2 != null)
            {
                File.WriteAllText("sys.reg", "Windows Registry Editor Version 5.00" + "\n" + "\n" + "[" + num1 + "]" + "\n" + "\"" + num2 + "\"=-");
                Process.Start(@"sys.reg");
            }
            else
            {
                 File.WriteAllText("sys.reg", "Windows Registry Editor Version 5.00" + "\n" + "\n" + "[-" + num1 + "]");
                Process.Start(@"sys.reg");
            }
        }

    }
}
