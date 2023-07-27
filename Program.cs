using espacioPersonaje;
using System.Collections.Generic;

string Archivo = "PersonajesRPG.json";
string Archivo2 = "GanadorRPG.json";
PersonajesJson Archivados = new PersonajesJson();
CombatePersonajes Coliseo = new CombatePersonajes();
FabricaDePersonajes Nuevo = new FabricaDePersonajes();
List<FabricaDePersonajes> ListaPj;

 
if (Archivados.Existe(Archivo))
{
    Console.WriteLine("¿Desea iniciar una nueva partida o continuar con la guardada? (N:NUEVA - C:CONTINUAR)");
    if (Console.ReadLine() == "N")
    {
        ListaPj = Nuevo.CrearLista();
        Archivados.GuardarPersonajes(ListaPj, Archivo);
    }
    else
    {
        ListaPj = Archivados.LeerPersonajes(Archivo);
    }
}
else
{
    ListaPj = Nuevo.CrearLista();
    Archivados.GuardarPersonajes(ListaPj, Archivo);
}



while (ListaPj.Count >= 2)
{
    Coliseo.MostrarPersonaje(ListaPj);
    Coliseo.Pelea(ListaPj);
}

Archivados.GuardarUltimoGanador(ListaPj[0],Archivo2);