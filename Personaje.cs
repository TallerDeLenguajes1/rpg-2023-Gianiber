namespace espacioPersonaje
{
    enum Tipo
    {
        Elfo,
        Orco,
        Humano,
        Enano,
    }
    class FabricaDePersonajes
    {
        private int destreza;
        private int salud;
        private int armadura;
        private int nivel;
        private int fuerza;
        private int velocidad;
        private Tipo tipo;
        private string? nombre;
        private string? apodo;
        private DateTime fechanac;
        private int edad;
        public void CrearPj(FabricaDePersonajes pj)
        {
            Random random = new Random();
            pj.destreza = random.Next(1, 6);
            pj.salud = 100;
            pj.armadura = random.Next(1, 11);
            pj.nivel = random.Next(1, 11);
            pj.fuerza = random.Next(1, 11);
            pj.velocidad = random.Next(1, 11);
            Array valoresTipos = Enum.GetValues(typeof(Tipo));
            Tipo tipoAleatorio = (Tipo)valoresTipos.GetValue(random.Next(valoresTipos.Length));
            pj.tipo = tipoAleatorio;
            Console.WriteLine("Ingrese el nombre del pj:");
            pj.nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apodo del pj:");
            pj.apodo = Console.ReadLine();
            DateTime start = new DateTime(1723, 1, 1);
            DateTime end = DateTime.Now;

            // Calcular un número de días aleatorio entre las dos fechas
            int range = (end - start).Days;
            DateTime randomDate = start.AddDays(random.Next(range));
            pj.fechanac = randomDate;
            pj.edad = end.Year - randomDate.Year;
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
        internal Tipo Tipo { get => tipo; set => tipo = value; }
    }
}
