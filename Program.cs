using System;

namespace Promillerechner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Geschlecht (männlich/weiblich): ");
            string geschlecht = Console.ReadLine().Trim().ToLower();

            Console.Write("Gewicht in kg: ");
            double gewicht = Convert.ToDouble(Console.ReadLine().Trim());

            Console.Write("Größe in cm: ");
            double groesse = Convert.ToDouble(Console.ReadLine().Trim());

            Console.Write("Geburtsdatum (YYYY-MM-DD): ");
            DateTime geburtsdatum = DateTime.Parse(Console.ReadLine().Trim());
            int alter = DateTime.Now.Year - geburtsdatum.Year;

            Console.Write("Art des Alkohols (bier/wein/schnaps): ");
            string alkoholType = Console.ReadLine().Trim().ToLower();

            Console.Write("Trinkdatum und -zeit (YYYY-MM-DD HH:MM:SS): ");
            DateTime trinkZeit = DateTime.Parse(Console.ReadLine().Trim());

            Console.Write("Menge des getrunkenen Alkohols in Milliliter: ");
            double mengeMl = Convert.ToDouble(Console.ReadLine().Trim());

            Console.Write("Aktuelles Datum und -zeit (YYYY-MM-DD HH:MM:SS): ");
            DateTime jetzigeZeit = DateTime.Parse(Console.ReadLine().Trim());

            double bac = CalculateBAC(geschlecht, gewicht, groesse, alter, alkoholType, mengeMl, trinkZeit, jetzigeZeit);
            bool isFitToDrive = bac <= 0.3; // 0.3 Promille ist die Grenze für die Fahrtüchtigkeit, ich bin nicht nach dem Schweizer Gesetzt gegangen sondern ich habe ein Zahl erfunden.

            // Ausgabe
            Console.WriteLine($"Ihre BAC (Blutalkoholkonzentration) beträgt: {bac:F2} Promille");
            Console.WriteLine(isFitToDrive ? "Sie sind fahrfähig." : "Sie sind nicht fahrfähig.");
        }

        static double CalculateBAC(string geschlecht, double gewicht, double groesse, int alter, string alkoholType, double mengeMl, DateTime trinkZeit, DateTime jetzigeZeit)
        {
            // Berechnung des Gesamtkörperwassers (GKW)
            double gkw;
            if (geschlecht == "männlich")
            {
                gkw = 2.447 - 0.09516 * alter + 0.1074 * groesse + 0.3362 * gewicht;
            }
            else
            {
                gkw = 0.203 - 0.07 * alter + 0.1069 * groesse + 0.2466 * gewicht;
            }

            // Alkoholgehalt in Volumenprozent
            double alcoholPercentage;
            switch (alkoholType)
            {
                case "bier":
                    alcoholPercentage = 0.05; // 5% für Bier
                    break;
                case "wein":
                    alcoholPercentage = 0.10; // 10% für Wein
                    break;
                case "schnaps":
                    alcoholPercentage = 0.40; // 40% für Schnaps
                    break;
                default:
                    throw new ArgumentException("Unbekannter Alkoholtyp");
            }

            // Dichte von Ethanol
            double dichteAlkohol = 0.8;

            // Berechnung der Alkoholmasse in Gramm
            double alkoholmasse = mengeMl * alcoholPercentage * dichteAlkohol;

            // Anteil Wasser im Blut
            double anteilWasserImBlut = 0.8;

            // Dichte von Blut
            double dichteBlut = 1.055;

            // Blutalkoholgehalt (BAC) in Promille
            double bac = (anteilWasserImBlut * alkoholmasse) / (dichteBlut * gkw);

            // Zeit seit dem Trinken in Stunden
            double hoursSinceDrinking = (jetzigeZeit - trinkZeit).TotalHours;

            // Alkoholabbau (0.1 Promille pro Stunde)
            bac -= Math.Max(0, (hoursSinceDrinking - 1) * 0.1); // Eine Stunde für die Resorption

            return Math.Max(bac, 0);
        }
    }
}
