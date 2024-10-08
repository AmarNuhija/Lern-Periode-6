using System;

public class Wuerfel
{
    public const int MAX_NUMMER = 6;
    private Random _random;
    private int _letzteZahl;

    public Wuerfel()
    {
        _random = new Random();
    }

    public int LetzteZahl => _letzteZahl;

    public void Wuerfle()
    {
        _letzteZahl = _random.Next(1, MAX_NUMMER + 1);
    }
}
