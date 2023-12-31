using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Net;
namespace espacioPersonaje
{
    //Pool de nombres para generar automáticamente un torneo
    enum Tipo
    {
        Elfos,
        Orcos,
        Humanos,
        Enanos
    }
    enum Nombres
    {
        Pablo,
        Luis,
        Julian,
        Marcos,
        Andres,
        Sofia,
        Marina,
        Luz,
        Delfina,
        Agostina
    }

    enum Apodos
    {
        AveAtascada,
        CaranchoBlanco,
        GranRataQuemada,
        ColadePerro,
        PlumadeChamuco,
        IguanaBlanca,
        LagartijaMontada,
        DolordeTesticulo,
        QuirquinchoRigido,
        BatatadeMonte
    }
    //Clase que me genera los elementos alrededor de 1 personaje
    class FabricaDePersonajes
    {
        private int destreza;
        private int salud;
        private int armadura;
        private int nivel;
        private int fuerza;
        private int velocidad;
        private string? tipo;
        private string? nombre;
        private string? apodo;
        private DateTime fechanac;
        private int edad;
        private DateTime fechaganada;
        private FabricaDePersonajes CrearPj() //Devolvera un personaje con todas las stats definidas
        {
            FabricaDePersonajes pj = new FabricaDePersonajes();
            Random random = new Random();
            pj.destreza = random.Next(1, 6);
            pj.salud = 100;
            pj.armadura = random.Next(1, 11);
            pj.nivel = random.Next(1, 11);
            pj.fuerza = random.Next(1, 11);
            pj.velocidad = random.Next(1, 11);
            pj.tipo = Enum.GetName(typeof(Tipo), random.Next(0, Enum.GetNames(typeof(Tipo)).Length));
            pj.nombre = Enum.GetName(typeof(Nombres), random.Next(0, Enum.GetNames(typeof(Nombres)).Length));
            pj.apodo = Enum.GetName(typeof(Apodos), random.Next(0, Enum.GetNames(typeof(Apodos)).Length));
            DateTime end = DateTime.Now;
            DateTime start = new DateTime(end.Year - 300, 1, 1);

            // Calcular un número de días aleatorio entre las dos fechas
            int range = (end - start).Days;
            DateTime randomDate = start.AddDays(random.Next(range));
            pj.fechanac = randomDate;
            pj.edad = end.Year - randomDate.Year;
            return pj;
        }

        public List<FabricaDePersonajes> CrearLista()
        {
            FabricaDePersonajes Nuevo = new FabricaDePersonajes();
            List<FabricaDePersonajes> ListaPj = new List<FabricaDePersonajes>();
            for (int i = 0; i < 10; i++)
            {
                Nuevo = Nuevo.CrearPj();
                bool bandera = true;
                foreach (FabricaDePersonajes personajeX in ListaPj)
                {
                    if (Nuevo.Nombre == personajeX.Nombre && Nuevo.Apodo == personajeX.Apodo)
                    {
                        i--;
                        bandera = false;
                    }
                }
                if (bandera)
                {
                    ListaPj.Add(Nuevo);
                }
            }
            return ListaPj;
        }

        public int Destreza { get => destreza; set => destreza = value; }
        public int Salud { get => salud; set => salud = value; }
        public int Armadura { get => armadura; set => armadura = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Apodo { get => apodo; set => apodo = value; }
        public DateTime Fechanac { get => fechanac; set => fechanac = value; }
        public int Edad { get => edad; set => edad = value; }
        public string? Tipo { get => tipo; set => tipo = value; }
        public DateTime Fechaganada { get => fechaganada; set => fechaganada = value; }
    }

    //Clase que maneja los archivos JSON, ya sea para lectura o escritura
    class PersonajesJson
    {
        public void GuardarPersonajes(List<FabricaDePersonajes> pj, string nombreArchivo)
        {
            string personajesJson = JsonSerializer.Serialize(pj);
            using FileStream fs = File.Create(nombreArchivo);
            using StreamWriter sr = new StreamWriter(fs);
            sr.WriteLine("{0}", personajesJson);
            sr.Close();
            Console.WriteLine("Personajes guardados en formato JSON.");
        }
        public void GuardarUltimoGanador(FabricaDePersonajes pj, string nombreArchivo)
        {
            Console.WriteLine("\nFELICIDADES " + pj.Nombre + " REPRESENTANDO A LOS " + pj.Tipo + " POR OBTENER EL TRONO DE HIERRO. QUE SUS HAZAÑAS JAMAS SEAN OLVIDADAS.");
            pj.Fechaganada = DateTime.Now;
            string personajesJson = JsonSerializer.Serialize(pj);
            using FileStream fs = File.Create(nombreArchivo);
            using StreamWriter sr = new StreamWriter(fs);
            sr.WriteLine("{0}", personajesJson);
            sr.Close();
            Console.WriteLine("Ganador guardado en formato JSON.");
        }

        public List<FabricaDePersonajes> LeerPersonajes(string nombreArchivo)
        {
            string pjs = File.ReadAllText(nombreArchivo);
            if (pjs != null)
            {
                List<FabricaDePersonajes>? Personajes = JsonSerializer.Deserialize<List<FabricaDePersonajes>>(pjs);
                return Personajes;
            }
            else
            {
                return null;
            }
        }

        public bool Existe(string nombreArchivo)
        {
            if (File.Exists(nombreArchivo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class Flags
    {
        public bool nsfw { get; set; }
        public bool religious { get; set; }
        public bool political { get; set; }
        public bool racist { get; set; }
        public bool sexist { get; set; }
        public bool @explicit { get; set; }
    }

    public class Chiste
    {
        public bool error { get; set; }
        public string? category { get; set; }
        public string? type { get; set; }
        public string? joke { get; set; }
        public Flags? flags { get; set; }
        public int id { get; set; }
        public bool safe { get; set; }
        public string? lang { get; set; }
    }

    //Clase que trabaja con la lista de personajes que van a pelear. Me permite mostrar los personajes y realizar el combate
    class CombatePersonajes
    {

        public void MostrarPersonaje(List<FabricaDePersonajes> Lista)
        {
            Console.WriteLine($"{"Nombre",-10}\t{"Tipo",-6}\tEdad\tD\tF\tN\tA\tV\tS");
            foreach (FabricaDePersonajes personajeX in Lista)
            {
                Console.WriteLine($"{personajeX.Nombre,-10}\t{personajeX.Tipo,-6}" + "\t" + personajeX.Edad + "\t" + personajeX.Destreza + "\t" + personajeX.Fuerza + "\t" + personajeX.Nivel + "\t" + personajeX.Armadura + "\t" + personajeX.Velocidad + "\t" + personajeX.Salud);
            }
        }
        public void Pelea(List<FabricaDePersonajes> pj)
        {
            Random aleatorio = new Random();
            int turno = 0;
            int pj1, pj2;
            //Elijo dos elementos al azar de la lista y trabajo con ellos, trato de que no sean los mismo elementos,
            //a su vez guardo la salud inicial del personaje para restaurarla luego de la pelea (puede contener buffs)
            pj1 = aleatorio.Next(0, pj.Count);
            do
            {
                pj2 = aleatorio.Next(0, pj.Count);
            } while (pj1 == pj2);
            FabricaDePersonajes Personaje1 = pj[pj1];
            int salud1 = Personaje1.Salud;
            FabricaDePersonajes Personaje2 = pj[pj2];
            int salud2 = Personaje2.Salud;
            Console.WriteLine("\nEn el coliseo ahora se encuentran " + Personaje1.Nombre + " del tipo " + Personaje1.Tipo + " y " + Personaje2.Nombre + " del tipo " + Personaje2.Tipo);
            //En turnos impares atacara el PJ1 y en los pares el PJ2
            while (Personaje1.Salud >= 0 && Personaje2.Salud! >= 0)
            {
                turno++;
                if (turno % 2 == 0)
                {
                    Personaje1.Salud = Personaje1.Salud - DañoInflingido(aleatorio, Personaje2, Personaje1);
                }
                else
                {
                    Personaje2.Salud = Personaje2.Salud - DañoInflingido(aleatorio, Personaje1, Personaje2);
                }
            }
            //El vencedor obtendra buffs en sus stats y el perdedor será eliminado de la lista
            FinDelCombate(pj, turno, pj1, pj2, Personaje1, salud1, Personaje2, salud2);
        }
        private static string Frase()
        {
            var url = $"https://v2.jokeapi.dev/joke/Any?type=single";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null)
                        {
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                Chiste? Respuesta = JsonSerializer.Deserialize<Chiste>(responseBody);
                                return Respuesta.joke;
                            }
                        }
                        else
                        {
                            return "No hay chiste";
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                string respuesta = "Problemas de acceso a la API";
                return respuesta;
            }
        }
        private static void FinDelCombate(List<FabricaDePersonajes> pj, int turno, int pj1, int pj2, FabricaDePersonajes Personaje1, int salud1, FabricaDePersonajes Personaje2, int salud2)
        {
            if (Personaje1.Salud <= 0)
            {
                Personaje2.Salud = salud2;
                Console.WriteLine("\nEl ganador de este combate fue " + Personaje2.Nombre + " de la raza " + Personaje2.Tipo + " en " + turno + " turnos, que luego le dijo al perdedor:");
                Console.WriteLine("\"" + Frase() + "\"\n");
                pj.Remove(pj[pj1]);
                switch (turno)
                {
                    case int n when n < 5:
                        Personaje2.Nivel = Personaje2.Nivel + 1;
                        break;
                    case int n when n >= 5 && n <= 7:
                        Personaje2.Nivel = Personaje2.Nivel + 1;
                        Personaje2.Destreza = Personaje2.Destreza + 1;
                        break;
                    case int n when n > 7 && n <= 10:
                        Personaje2.Nivel = Personaje2.Nivel + 1;
                        Personaje2.Destreza = Personaje2.Destreza + 1;
                        Personaje2.Velocidad = Personaje2.Velocidad + 2;
                        break;
                    case int n when n > 10:
                        Personaje2.Nivel = Personaje2.Nivel + 1;
                        Personaje2.Destreza = Personaje2.Destreza + 1;
                        Personaje2.Velocidad = Personaje2.Velocidad + 2;
                        Personaje2.Salud = Personaje2.Salud + 10;
                        break;
                }
            }
            else
            {
                Personaje1.Salud = salud1;
                Console.WriteLine("El ganador de este combate fue " + Personaje1.Nombre + " de la raza " + Personaje1.Tipo + " en " + turno + " turnos, que luego le dijo al perdedor:");
                Console.WriteLine("\"" + Frase() + "\"\n");
                pj.Remove(pj[pj2]);
                switch (turno)
                {
                    case int n when n <= 5:
                        Personaje1.Nivel = Personaje1.Nivel + 1;
                        break;
                    case int n when n > 5 && n <= 10:
                        Personaje1.Nivel = Personaje1.Nivel + 1;
                        Personaje1.Destreza = Personaje1.Destreza + 1;
                        break;
                    case int n when n > 10 && n <= 15:
                        Personaje1.Nivel = Personaje1.Nivel + 1;
                        Personaje1.Destreza = Personaje1.Destreza + 1;
                        Personaje1.Velocidad = Personaje1.Velocidad + 2;
                        break;
                    case int n when n > 15:
                        Personaje1.Nivel = Personaje1.Nivel + 1;
                        Personaje1.Destreza = Personaje1.Destreza + 1;
                        Personaje1.Velocidad = Personaje1.Velocidad + 2;
                        Personaje1.Salud = Personaje1.Salud + 10;
                        break;
                }
            }
        }
        private static int DañoInflingido(Random aleatorio, FabricaDePersonajes Atacante, FabricaDePersonajes Defensor)
        {
            int daño = (Atacante.Destreza * Atacante.Fuerza * Atacante.Nivel * aleatorio.Next(1, 101) - (Defensor.Armadura * Defensor.Velocidad)) / 500;
            return daño;
        }
    }
}