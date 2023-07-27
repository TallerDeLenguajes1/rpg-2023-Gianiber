using espacioPersonaje;
using System.Collections.Generic;
string Archivo = "PersonajesRPG.json";
PersonajesJson Archivados = new PersonajesJson();
CombatePersonajes Coliseo = new CombatePersonajes();
List<FabricaDePersonajes> ListaPj;


if (Archivados.Existe(Archivo))
{
    Console.WriteLine("¿Desea iniciar una nueva partida o continuar con la guardada? (N:NUEVA - C:CONTINUAR)");
    if (Console.ReadLine() == "N")
    {
        FabricaDePersonajes Nuevo = new FabricaDePersonajes();
        ListaPj = new List<FabricaDePersonajes>();
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
        Archivados.GuardarPersonajes(ListaPj, Archivo);
    }
    else
    {
        ListaPj = Archivados.LeerPersonajes(Archivo);
    }
}
else
{
    FabricaDePersonajes Nuevo = new FabricaDePersonajes();
    ListaPj = new List<FabricaDePersonajes>();
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
    Archivados.GuardarPersonajes(ListaPj, Archivo);
}



while (ListaPj.Count >= 2)
{
    Coliseo.MostrarPersonaje(ListaPj);
    Coliseo.Pelea(ListaPj);
}

Console.WriteLine("\nFELICIDADES " + ListaPj[0].Nombre + " REPRESENTANDO A LOS " + ListaPj[0].Tipo + " POR OBTENER EL TRONO DE HIERRO. QUE SUS HAZAÑAS JAMAS SEAN OLVIDADAS.");