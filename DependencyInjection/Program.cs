using System;
using System.Collections.Generic;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sporter sporter = new Sporter(new GratisSchema());
            //sporter.StelDoel(5);
            //foreach (var item in sporter.Trainings)
            //{
            //    Console.WriteLine(item);
            //}
            //Sporter sporter2 = new Sporter(new BetaaldSchema());
            //sporter2.StelDoel(7);
            //foreach (var item in sporter2.Trainings)
            //{
            //    Console.WriteLine(item);
            //}
            Persoon kenny = new Persoon("Bruwier","Kenny",39);
            kenny.TicketGekocht(new TicketPukkelPop());
            kenny.TicketGekocht(new TicketTicketMaster());
            kenny.TicketGekocht(new TicketTweedeHands());
            kenny.PrintTickets();
        }
    }
    public class Training
    {
        public string Naam { get; set; }
        public double Afstand { get; set; }
        public Training(string naam, double afstand)
        {
            Naam = naam;
            Afstand = afstand;
        }
        public override string ToString()
        {
            return $"{Naam}: {Afstand}km";
        }

    }
    public interface ISchemaProvider
    {
        public List<Training> Lijst(int hoeveel);
    }
    public class GratisSchema : ISchemaProvider
    {
        public List<Training> Lijst(int hoeveel)
        {
            List<Training> trainings = new List<Training>();
            for (int i = 0; i < hoeveel; i++)
            {
                trainings.Add(new Training($"Training {i}", i * 1.5));
            }
            return trainings;
        }
    }
    public class BetaaldSchema : ISchemaProvider
    {
        public List<Training> Lijst(int hoeveel)
        {
            List<Training> trainings = new List<Training>();
            for (int i = 0; i < hoeveel; i++)
            {
                trainings.Add(new Training($"Custom training {i}", i * hoeveel / 4.0));
            }
            return trainings;
        }
    }
    public class Sporter
    {
        public List<Training> Trainings { get; set; }
        private ISchemaProvider schemaProvider;

        public Sporter(ISchemaProvider schemaProvider)
        {
            this.schemaProvider = schemaProvider;
        }

        public void StelDoel(int aantalLessen)
        {
            Trainings = schemaProvider.Lijst(aantalLessen);
        }
    }

    // Tickets verkoop

    public class Persoon
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public int Leeftijd { get; set; }
        public List<ITicketProvider> Tickets { get; set; }
        public Persoon(List<ITicketProvider> tickets)
        {
            Tickets = tickets;
        }
        public Persoon(string naam, string voornaam, int leeftijd)
        {
            Naam = naam;
            Voornaam = voornaam;
            Leeftijd = leeftijd;
        }
        public void TicketGekocht(ITicketProvider ticket)
        {
            if (Tickets == null) Tickets = new List<ITicketProvider>();
            ticket.Naam = Naam;
            ticket.Voornaam = Voornaam;
            ticket.Leeftijd = Leeftijd;
            Tickets.Add(ticket);
        }
        public void PrintTickets()
        {
            if (Tickets != null)
            {
                foreach (ITicketProvider ticket in Tickets)
                {
                    Console.WriteLine($"Ticket gekocht bij {ticket.ProviderNaam}");
                    Console.WriteLine($"    Naam: {ticket.Naam}");
                    Console.WriteLine($"    Voornaam: {ticket.Voornaam}");
                    Console.WriteLine($"    Leeftijd: {ticket.Leeftijd}");
                    Console.WriteLine($"    Prijs: {ticket.Prijs()}");
                }
                Console.WriteLine();
            }
        }
    }
    public interface ITicketProvider
    {
        public string ProviderNaam { get; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public int Leeftijd { get; set; }
        public double Prijs();
    }
    public class TicketPukkelPop : ITicketProvider
    {
        public string Naam { get; set ; }
        public string Voornaam { get ; set ; }
        public int Leeftijd { get  ; set ; }
        public string ProviderNaam { get { return "Pukkelpop website"; } }
        public double Prijs ()
        {
            return 75.5;
        }

        public TicketPukkelPop(string naam, string voornaam, int leeftijd)
        {
            Naam = naam;
            Voornaam = voornaam;
            Leeftijd = leeftijd;
        }
        public TicketPukkelPop()
        {

        }
    }
    public class TicketTicketMaster : ITicketProvider
    {
        public string ProviderNaam { get { return "TicketMaster"; } }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public int Leeftijd { get; set; }
        public TicketTicketMaster(string volledigeNaam, int leeftijd)
        {
            Naam = volledigeNaam.Substring(volledigeNaam.IndexOf(' ') + 1); 
            Voornaam = volledigeNaam.Split(" ")[0]; 
            Leeftijd = leeftijd;
        }
        public double Prijs()
        {
            return Leeftijd < 26 ? 90.75 : 100.75;
        }
        public TicketTicketMaster()
        {

        }
    }
    public class TicketTweedeHands : ITicketProvider
    {
        public string ProviderNaam { get { return "2dehands"; } }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public int Leeftijd { get; set; }
        public double Prijs()
        {
            return 55 + 1*Math.Round((DateTime.Now - new DateTime(2021, 6, 1)).TotalDays, 0);
        }
        public TicketTweedeHands()
        {

        }
    }
}
