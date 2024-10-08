using System;
using System.Collections.Generic;

public class Becher
{
    public const int ANZ_WUERFEL = 3;
    private List<Wuerfel> _wuerfel;

    public Becher()
    {
        _wuerfel = new List<Wuerfel>();
        for (int i = 0; i < ANZ_WUERFEL; i++)
        {
            _wuerfel.Add(new Wuerfel());
        }
    }

    public void Schuettle()
    {
        foreach (var wuerfel in _wuerfel)
        {
            wuerfel.Wuerfle();
        }
    }

    public List<int> GetZahlen(int anzahl)
    {
        List<int> zahlen = new List<int>();
        for (int i = 0; i < Math.Min(anzahl, _wuerfel.Count); i++)
        {
            zahlen.Add(_wuerfel[i].LetzteZahl);
        }
        return zahlen;
    }
}
