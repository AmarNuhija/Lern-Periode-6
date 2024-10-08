using System;
using System.Collections.Generic;

public class GUI
{
    public List<Spieler> FrageSpielerEingabe()
    {
        List<Spieler> spielerListe = new List<Spieler>();
        do
        {
            Console.Write("Geben Sie den Namen des Spielers ein: ");
            string name = Console.ReadLine();
            spielerListe.Add(new Spieler(name));
        } while (FrageNochEinSpieler());
        return spielerListe;
    }

    public bool FrageNochEinSpieler()
    {
        Console.Write("Möchten Sie einen weiteren Spieler hinzufügen? (1 für Ja, 2 für Nein): ");
        string input = Console.ReadLine();
        return input == "1";
    }

    public void PrintRangliste(List<Spieler> spielerListe)
    {
        Console.WriteLine("Aktueller Chipstand:");
        foreach (var spieler in spielerListe)
        {
            spieler.PrintNameUndChips();
        }
    }

    public void PrintGewinner(List<Spieler> spielerListe)
    {
        Console.WriteLine("Das Spiel ist beendet. Der Gewinner ist:");
        foreach (var spieler in spielerListe)
        {
            if (spieler.HatNochChips)
            {
                Console.WriteLine(spieler.Name);
                break;
            }
        }
    }
}
