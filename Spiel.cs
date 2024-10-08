using System;
using System.Collections.Generic;

public class Spiel
{
    private Spieler _aktuellerSpieler;
    private List<Spieler> _spielerListe;
    private GUI _gui;
    private Becher _becher;
    private int _chipsInDerMitte = 0;  // Pot in der Mitte

    public Spiel()
    {
        _gui = new GUI();
        _spielerListe = _gui.FrageSpielerEingabe();
        _becher = new Becher();
    }

    public void Spielen()
    {
        SetzeStartSpieler();
        while (MehrAlsEinSpielerHatChips())
        {
            _aktuellerSpieler.SpieleZug(_becher);
            List<int> wuerfe = _becher.GetZahlen(_aktuellerSpieler.AnzahlWuerfel);
            _aktuellerSpieler.PrintWuerfel(wuerfe);
            GewuerfelteZahlenVerarbeiten(wuerfe);
            _aktuellerSpieler = SpielerRechtsVonAktuellemSpieler();
            _gui.PrintRangliste(_spielerListe);
        }
        _gui.PrintGewinner(_spielerListe);
    }

    private void SetzeStartSpieler()
    {
        Random rnd = new Random();
        _aktuellerSpieler = _spielerListe[rnd.Next(_spielerListe.Count)];
    }

    private Spieler SpielerRechtsVonAktuellemSpieler()
    {
        int index = (_spielerListe.IndexOf(_aktuellerSpieler) + 1) % _spielerListe.Count;
        return _spielerListe[index];
    }

    private Spieler SpielerLinksVonAktuellemSpieler()
    {
        int index = (_spielerListe.IndexOf(_aktuellerSpieler) - 1 + _spielerListe.Count) % _spielerListe.Count;
        return _spielerListe[index];
    }

    private bool MehrAlsEinSpielerHatChips()
    {
        int count = 0;
        foreach (var spieler in _spielerListe)
        {
            if (spieler.HatNochChips)
                count++;
            if (count > 1)
                return true;
        }
        return false;
    }

    // Verarbeite die Würfelergebnisse basierend auf den LCR-Regeln
    private void GewuerfelteZahlenVerarbeiten(List<int> zahlen)
    {
        foreach (int zahl in zahlen)
        {
            if (zahl == 4)        // 1 bedeutet, einen Chip nach links zu geben
                ChipNachLinksWeitergeben();
            else if (zahl == 6)   // 2 bedeutet, einen Chip nach rechts zu geben
                ChipNachRechtsWeitergeben();
            else if (zahl == 5)   // 3 bedeutet, einen Chip in die Mitte zu legen
                ChipInDieMitteLegen();
            // Zahlen 4, 5, 6: Keine Aktion, behalte den Chip
        }
    }

    private void ChipNachLinksWeitergeben()
    {
        _aktuellerSpieler.GebeChipAb();
        SpielerLinksVonAktuellemSpieler().ErhalteChip();
    }

    private void ChipNachRechtsWeitergeben()
    {
        _aktuellerSpieler.GebeChipAb();
        SpielerRechtsVonAktuellemSpieler().ErhalteChip();
    }

    private void ChipInDieMitteLegen()
    {
        _aktuellerSpieler.GebeChipAb();
        _chipsInDerMitte++;  // Erhöhe die Chips im Center-Pot
    }
}
