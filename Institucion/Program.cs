using System.Text;
using Institucion.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using Institucion.Misc;
using Institucion.DataAccess;

namespace Institucion
{
    class MainClass
    {
        const string fileName = "./Aux/AppSettings.dat";
        const string bmpFileName = "./Aux/archivo.bmp";

        public static void Main(string[] args)
        {
            ListaProfesoresDb();

            Console.ReadLine();
        }

        public static void ListaProfesoresDb()
        {
            var listaProfesores = CrearLista();
            var db = new InstitucionDB();

            db.Profesores.AddRange(listaProfesores);
            db.SaveChanges();
        }

        public static void DelegadosYEventos()
        {
            var profesor = new Profesor() { Id = 12, Nombre = "David", Apellido = "Fuentes", CodigoInterno = "PROFE_SMART" };

            var transmitter = new TransmisorDeDatos();
            transmitter.InformacionEnviada += Transmitter_InformacionEnviada;

            //Lambda 
            transmitter.InformacionEnviada += (obj, evtarg) =>
            {
                Console.WriteLine("Woaaaaaaaaa!");
            };


            transmitter.FormatearYEnviar(profesor, formatter, "DAVIDR");

            transmitter.FormatearYEnviar(profesor, Reverseformatter, "DAVIDR");

            // Eliminar Notificacion de Envio
            transmitter.InformacionEnviada -= Transmitter_InformacionEnviada;

            transmitter.FormatearYEnviar(profesor,
                (s) =>
                {
                    char[] arr = s.ToCharArray();
                    Array.Reverse(arr);
                    return new string(arr);
                }
                , "DAVIDR");
        }


        private static string Reverseformatter(string input)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private static string formatter(string input)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(stringBytes);
        }

        static void Transmitter_InformacionEnviada(object sender, EventArgs e)
        {
            Console.WriteLine("TRANSMISION DE INFORMACION");
        }


        public static void escribirArchivoBinario()
        {
            var listaProfes = new List<Profesor>();

            string[] lineas = File.ReadAllLines("./Aux/Profesores.txt");


            int localId = 0;
            foreach (var linea in lineas)
            {
                listaProfes.Add(new Profesor() { Nombre = linea, Id = localId++ });
            }

            foreach (var profesor in listaProfes)
            {
                Console.WriteLine($"Nombre: {profesor.Nombre}, Id: {profesor.Id}");
            }

            // Crear archivo Binario
            var archivo = File.Open("./Aux/Profesores.bin", FileMode.OpenOrCreate);
            var binaryFile = new BinaryWriter(archivo);

            foreach (var profe in listaProfes)
            {
                // var bytesNombre = Encoding.UTF8.GetBytes(profe.Nombre);
                //archivo.Write(bytesNombre, 0, bytesNombre.Length);
                binaryFile.Write(profe.Nombre);
                //binaryFile.Write(profe.Id);
            }

            binaryFile.Close();
            archivo.Close();
        }

        public void Rueda1()
        {
            Persona[] personas = new Persona[3];

            personas[0] = new Alumno("David", "Fuentes")
            {
                Id = 1,
                Edad = 36,
                Telefono = "5695114924",
                Correo = "ing.davidfuentes@gmail.com"
            };

            personas[1] = new Profesor()
            {
                Id = 2,
                Nombre = "David",
                Apellido = "Fuentes",
                Edad = 36,
                Telefono = "5695114924",
                Materia = "Matematicas"
            };

            personas[2] = new Profesor()
            {
                Id = 3,
                Nombre = "Mariela",
                Apellido = "Fuentes",
                Edad = 36,
                Telefono = "569274183",
                Materia = "Castellano"
            };

            foreach (var persona in personas)
            {
                Console.WriteLine($"Tipo de Clase: {persona.GetType()}");
                Console.WriteLine(persona.Resumen());
            }

        }

        public static void WriteDefaultValues()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                writer.Write(1.250F);
                writer.Write(@"c:\Temp");
                writer.Write(10);
                writer.Write(true);
            }
        }

        public static void DisplayValues()
        {
            float aspectRatio;
            string tempDirectory;
            int autoSaveTime;
            bool showStatusBar;

            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    aspectRatio = reader.ReadSingle();
                    tempDirectory = reader.ReadString();
                    autoSaveTime = reader.ReadInt32();
                    showStatusBar = reader.ReadBoolean();
                }

                Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                Console.WriteLine("Temp directory is: " + tempDirectory);
                Console.WriteLine("Auto save time set to: " + autoSaveTime);
                Console.WriteLine("Show status bar: " + showStatusBar);
            }
        }

        public static void leerArchivoBinario()
        {

            List<Profesor> listaProfes = new List<Profesor>();

            var file = File.Open("./Aux/profesores.bin", FileMode.Open);
            BinaryReader binaryFile = new BinaryReader(file);


            // Console.WriteLine(binaryFile.ReadString());
            while (binaryFile.BaseStream.Position < binaryFile.BaseStream.Length)
            {
                var profe = new Profesor();
                profe.Nombre = binaryFile.ReadString();
                listaProfes.Add(profe);
            }


            foreach (var profe in listaProfes)
            {
                Console.WriteLine($"Profesor: {profe.Nombre}");
            }
            // Console.WriteLine(binaryFile.ReadString());
            // Console.WriteLine(binaryFile.ReadString());
            // Console.WriteLine(binaryFile.ReadString());
            // Console.WriteLine(binaryFile.ReadString());


            binaryFile.Close();
            file.Close();
        }

        public static void leerBMPFile()
        {
            int width, height;
            short bitCount;
            using (FileStream archivo = File.Open(bmpFileName, FileMode.Open))
            {
                archivo.Seek(18, SeekOrigin.Begin);

				BinaryReader binaryReader = new BinaryReader(archivo);

                width = binaryReader.ReadInt32();
                height = binaryReader.ReadInt32();

                archivo.Seek(2, SeekOrigin.Current);

                bitCount = binaryReader.ReadInt16();

                Console.WriteLine($"Ancho: {width} Alto: {height} BitsXPixel: {bitCount}");
            }



            /*
            foreach (var item in signature)
            {
                Console.WriteLine(item);
            }
            */

        }

        private static List<Profesor> CrearLista()
        {
            Random rnd = new Random();
            var lista = new List<Profesor>();

            lista.Add(new Profesor() { Nombre = "Juan Carlos", Id = rnd.Next() });
            lista.Add(new Profesor()
            {
                Nombre = "Jeronimo",
                Materia = "Marketing",
                Id = rnd.Next()
            });
            lista.Add(new Profesor() { Nombre = "Yohanna", Id = rnd.Next() });
            lista.Add(new Profesor() { Nombre = "Martha", Materia = "Marketing", Id = rnd.Next() });
            lista.Add(new Profesor() { Nombre = "Jose Mauricio", Id = rnd.Next() });
            lista.Add(new Profesor() { Nombre = "Angela", Id = rnd.Next() });
            lista.Add(new Profesor() { Nombre = "Walter", Id = rnd.Next() });
            lista.Add(new Profesor() { Nombre = "Marco", Id = rnd.Next() });
            lista.Add(new Profesor() { Nombre = "Satya", Id = rnd.Next() });
            lista.Add(new Profesor() { Nombre = "Terry", Materia = "Marketing", Id = rnd.Next() });
            lista.Add(new Profesor() { Nombre = "Alexander", Id = rnd.Next() });
            lista.Add(new Profesor() { Nombre = "Sandra", Id = rnd.Next() });

            return lista;
        }

        public static void dibujar()
        {
            using (FileStream archivo = File.Open(bmpFileName, FileMode.Open))
            {


                archivo.Seek(54, SeekOrigin.Begin);
				// archivo.Seek(2, SeekOrigin.Begin);
                BinaryReader binaryReader = new BinaryReader(archivo);
                
                // Console.WriteLine(binaryReader.ReadInt32());

                // while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                //{
                var first = (Int16)binaryReader.ReadByte();
					var second = binaryReader.ReadByte();
					var third = binaryReader.ReadByte();
					var fourth = binaryReader.ReadByte();

                    // archivo.Seek(4, SeekOrigin.Current);

                Console.WriteLine($"First: {first} Second: {second} Third: {third} Fourth: {fourth}");
                //}



            }




            // Console.ForegroundColor = ConsoleColor.DarkBlue;
            // Console.WriteLine("Prueba");
        }
    }
}
