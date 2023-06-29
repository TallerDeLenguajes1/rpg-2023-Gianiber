using espacioPersonaje;

FabricaDePersonajes Nuevo = new FabricaDePersonajes();
Nuevo.CrearPj(Nuevo);
Console.WriteLine("Velocidad = "+Nuevo.Velocidad);
Console.WriteLine("Destreza = "+Nuevo.Destreza);
Console.WriteLine("Fuerza = "+Nuevo.Fuerza);
Console.WriteLine("Nivel = "+Nuevo.Nivel);
Console.WriteLine("Armadura = "+Nuevo.Armadura);
Console.WriteLine("Salud = "+Nuevo.Salud);
Console.WriteLine("Tipo = "+Nuevo.Tipo);
Console.WriteLine("Nombre = "+Nuevo.Nombre);
Console.WriteLine("Apodo = "+Nuevo.Apodo);
Console.WriteLine("Fecha de nacimiento = "+Nuevo.Fechanac.ToString("dd-MM-yyyy"));
Console.WriteLine("Edad = "+Nuevo.Edad);