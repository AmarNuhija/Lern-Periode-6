using System;
using System.Collections.Generic;

public class Spieler
{
    private int _chips;
    private string _name;

    public Spieler(string name)
    {
        _name = name;
        _chips = 3; // Start with 3 chips
    }

    public string Name => _name;
    public bool HatNochChips => _chips > 0;
    public int AnzahlWuerfel => Math.Min(_chips, 3);

    public void ErhalteChip()
    {
        _chips++;
    }

    public void GebeChipAb()
    {
        if (_chips > 0)
            _chips--;
    }

    public void SpieleZug(Becher becher)
    {
        becher.Schuettle();
    }

    public void PrintNameUndChips()
    {
        Console.WriteLine($"{_name}: {_chips} Chips");
    }

    public void PrintWuerfel(List<int> wuerfe)
    {
        Console.WriteLine($"{_name} würfelt: {string.Join(", ", wuerfe)}");
    }
}