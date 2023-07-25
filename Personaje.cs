using System.Text.Json;
using System.IO;
using System.Collections.Generic;
namespace espacioPersonaje
{
    enum Tipo
    {
        Elfo,
        Orco,
        Humano,
        Enano
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
        public FabricaDePersonajes CrearPj()
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
            DateTime start = new DateTime(1723, 1, 1);
            DateTime end = DateTime.Now;

            // Calcular un número de días aleatorio entre las dos fechas
            int range = (end - start).Days;
            DateTime randomDate = start.AddDays(random.Next(range));
            pj.fechanac = randomDate;
            pj.edad = end.Year - randomDate.Year;
            return pj;
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
    }

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

        public List<FabricaDePersonajes> LeerPersonajes(string nombreArchivo)
        {
            if (File.Exists(nombreArchivo))
            {
                string pjs = File.ReadAllText(nombreArchivo);
                List<FabricaDePersonajes> Personajes = JsonSerializer.Deserialize<List<FabricaDePersonajes>>(pjs);
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
        public void MostrarPersonaje(List<FabricaDePersonajes> Lista)
        {
            Console.WriteLine($"{"Nombre",-10}\t{"Tipo",-6}\tEdad\tD\tF\tN\tA\tV\tS");
            foreach (FabricaDePersonajes personajeX in Lista)
            {
                Console.WriteLine($"{personajeX.Nombre,-10}\t{personajeX.Tipo,-6}" + "\t" + personajeX.Edad + "\t" + personajeX.Destreza + "\t" + personajeX.Fuerza + "\t" + personajeX.Nivel + "\t" + personajeX.Armadura + "\t" + personajeX.Velocidad + "\t" + personajeX.Salud);
            }
        }
    }

    class CombatePersonajes
    {
        public void Pelea(ref List<FabricaDePersonajes> pj)
        {
            Random aleatorio = new Random();
            int turno = 0;
            int daño, pj1, pj2;
            pj1 = aleatorio.Next(0, pj.Count);
            do
            {
                pj2 = aleatorio.Next(0, pj.Count);
            } while (pj1 == pj2);
            FabricaDePersonajes Personaje1 = pj[pj1];
            int salud1 = Personaje1.Salud;
            FabricaDePersonajes Personaje2 = pj[pj2];
            int salud2 = Personaje2.Salud;
            Console.WriteLine("En el coliseo ahora se encuentran " + Personaje1.Nombre + " del tipo " + Personaje1.Tipo + " y " + Personaje2.Nombre + " del tipo " + Personaje2.Tipo);
            while (Personaje1.Salud >= 0 && Personaje2.Salud! >= 0)
            {
                turno++;
                if (turno % 2 == 0)
                {
                    daño = (Personaje2.Destreza * Personaje2.Fuerza * Personaje2.Nivel * aleatorio.Next(0, 101) - (Personaje1.Armadura * Personaje1.Velocidad)) / 500;
                    Personaje1.Salud = Personaje1.Salud - daño;
                }
                else
                {
                    daño = (Personaje1.Destreza * Personaje1.Fuerza * Personaje1.Nivel * aleatorio.Next(0, 101) - (Personaje2.Armadura * Personaje2.Velocidad)) / 500;
                    Personaje2.Salud = Personaje2.Salud - daño;
                }
            }
            if (Personaje1.Salud <= 0)
            {
                Personaje2.Salud = salud2;
                Console.WriteLine("El ganador de este combate fue " + Personaje1.Nombre + " de la raza " + Personaje1.Tipo + " en " + turno + " turnos");
                pj.Remove(pj[pj1]);
                switch (turno)
                {
                    case int n when n < 5:
                        Personaje2.Nivel = Personaje2.Nivel + 1;
                        break;
                    case int n when n >= 5 && n < 10:
                        Personaje2.Nivel = Personaje2.Nivel + 1;
                        Personaje2.Destreza = Personaje2.Destreza + 1;
                        break;
                    case int n when n >= 10 && n < 15:
                        Personaje2.Nivel = Personaje2.Nivel + 1;
                        Personaje2.Destreza = Personaje2.Destreza + 1;
                        Personaje2.Velocidad = Personaje2.Velocidad + 2;
                        break;
                    case int n when n > 15:
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
                Console.WriteLine("El ganador de este combate fue " + Personaje1.Nombre + " de la raza " + Personaje1.Tipo + " en " + turno + " turnos");
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
    }
}
