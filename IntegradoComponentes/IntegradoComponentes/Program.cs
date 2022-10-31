using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HardwareSO;

namespace IntegradoComponentes
{
    internal class Integrado
    {
        public void Inicial()
        {
            string serialnro = "";
            string nroSerie = Componentes.Serial(serialnro);
            Console.WriteLine("Serial del disco duro: " + nroSerie);
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            string nroDisco = Componentes.UnidadesDeDisco();
            Console.WriteLine(nroDisco);
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            string nroProcesadores = Componentes.NumeroDeProcesadores();
            Console.WriteLine(nroProcesadores);
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            string ram = Componentes.Ram();
            Console.WriteLine(ram);
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            string nic = Componentes.NIC();
            Console.WriteLine(nic);
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            string macAnddress = Componentes.MacAddress();
            Console.WriteLine("MacAnddress: " + macAnddress);
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            string patches = Componentes.Patches();
            Console.WriteLine("Patches: " + patches);
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
        }

        public void Registro()
        {
            bool ban = false;
            string path, nomClave, valorClave;
            while (!ban)
            {
                Console.WriteLine("Elige una opcion");
                Console.WriteLine("1. Crear clave");
                Console.WriteLine("2. Leer clave");
                Console.WriteLine("3. Modificar");
                Console.WriteLine("4. Eliminar");
                Console.WriteLine("5. Salir");
                int opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Crear clave");
                        path = Console.ReadLine();
                        nomClave = Console.ReadLine();
                        valorClave = Console.ReadLine();
                        Componentes.CrearClave(path, nomClave, valorClave);
                        Console.WriteLine("Clave creada correctamente");
                        break;

                    case 2:
                        Console.WriteLine("Leer clave");
                        path = Console.ReadLine();
                        nomClave = Console.ReadLine();
                        valorClave = Componentes.LeerClave(path, nomClave);
                        Console.WriteLine(valorClave);
                        break;

                    case 3:
                        Console.WriteLine("Modificar clave");
                        path = Console.ReadLine();
                        nomClave = Console.ReadLine();
                        valorClave = Console.ReadLine();
                        Componentes.EstablecerClave(path, nomClave, valorClave);
                        Console.WriteLine("Clave modificada correctamente");
                        break;

                    case 4:
                        Console.WriteLine("Eliminar clave");
                        path = Console.ReadLine();
                        nomClave = Console.ReadLine();
                        Componentes.EliminarClave(path, nomClave);
                        Console.WriteLine("Clave eliminada correctamente");
                        break;

                    case 5:
                        Console.WriteLine("Saliste del registro del sistema");
                        ban = true;
                        break;
                }
            }
        }

        public  void ObtenerProceso()
        {
            Process[] proc;
            proc = Process.GetProcesses();
            foreach (Process p in proc)
            {
                string pr = p.ProcessName;
                Console.WriteLine(pr);
            }

        }

        public void Proceso(string proceso)
        {
            
            Process[] process = Process.GetProcessesByName(proceso);
            foreach (var proce in process)
            {
                proce.Kill();
            }

        }

        static void Main(string[] args)
        {
            bool ban = false;
            string sn;
            Integrado integrado = new Integrado();
             while (!ban)
             {
                 Console.WriteLine("Elige que metodos deseas: ");
                 Console.WriteLine("1. Principal: \r\n - Leer el nùmero seri del Disco Duro / CD / DVD\r\n - ¿Cúantas unidades de disco tiene?\r\n - Balance general del sistema : Procesadores, RAM, NIC, patches.\r\n - Obtener MAC Address.");
                 Console.WriteLine("2. Acceso al Registro del Sistema");
                 Console.WriteLine("3. Obtener los procesos activos / *matar*  procesos.");
                 Console.WriteLine("4. Salir");
                 int opcion = int.Parse(Console.ReadLine());
                 switch (opcion)
                 {
                     case 1:
                         integrado.Inicial();
                         Console.WriteLine("Deseas limpiar la consola Si/No");
                         sn = Console.ReadLine();
                         if (sn == "SI" || sn == "Si" || sn == "si")
                         {
                             Console.Clear();
                         }

                         break;

                     case 2:
                         integrado.Registro();
                         Console.WriteLine("Deseas limpiar la consola Si/No");
                         sn = Console.ReadLine();
                         if (sn == "SI" || sn == "Si" || sn == "si")
                         {
                             Console.Clear();
                         }

                         break;

                     case 3:
                        Console.WriteLine("---------------Acceso a procesos---------------");
                        integrado.ObtenerProceso();
                        Console.WriteLine("----------------------------------------------------------");
                         Console.WriteLine("Ingrese el nombre del proceso a detener: ");
                         string procesos = Console.ReadLine();
                         integrado.Proceso(procesos);
                         Console.WriteLine("Deseas limpiar la consola Si/No");
                         sn = Console.ReadLine();
                         if (sn == "SI" || sn == "Si" || sn == "si")
                         {
                             Console.Clear();
                         }

                         break;

                     case 4:
                         Console.WriteLine("Saliste del programa");
                         ban = true;
                         break;
                 }
             }
            Console.ReadKey();
        }
    }
}
