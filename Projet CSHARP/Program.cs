class Program
{


    static double calculateSalary(int salary, double taxes)
    {
        return salary * (1 - taxes / 100);
    }

    static void Main(string[] args)
    {
        string[] month = new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "juillet", "Aout", "Septembre", "Octobre", "Novembre", "Décembre" };
        string closedMonth = "Aout";
        double tauxPrime = 0;

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Quel est votre Salaire annuel Brut : ");
        bool salaryInInt = int.TryParse(Console.ReadLine().Replace("€", ""), out int salary);
        Console.WriteLine("\nQuel est votre Taux d'imposition : ");
        double taxes = double.Parse(Console.ReadLine().Replace("%", ""));
        Console.WriteLine("\nQuel est le Taux de la prime de fin d'année : ");
        try
        {
            tauxPrime = double.Parse(Console.ReadLine().Replace("%", ""));
        }
        catch (FormatException)
        {
            Console.WriteLine("Le taux de la Prime de fin d'année doit être un entier");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("La division ne peux pas être par 0");
        }
        Console.WriteLine("\nVous avez un salaire de : " + salary + "€ Brut" + "\nImposable a " + taxes + "%" + "\n avec une prime de fin d'année de : " + tauxPrime + "%");
        double salaryNet = Math.Round(calculateSalary(salary, taxes), 2);
        Console.WriteLine("\nVous gagnez donc : " + salaryNet + "€ Net");
        switch (salary)
        {
            case >= 50000:
                Console.WriteLine("Je vous conseille de faire des dons à des associations tels que l'Oeuvre des Pupilles pour réduire votre Imposition");
                break;

            case <= 1500 * 12:
                Console.WriteLine("Ce salaire est normal pour un alternant");
                break;

            case <= 40000 when salary >= 30000:
                Console.WriteLine("Venez travailler chez CESI vous gagnerez 100000€ brut");
                break;

            default:
                Console.WriteLine("Vous avez un salaire correct");
                break;
        }

        foreach (string eachMonth in month)
        {
            if (eachMonth != closedMonth)
            {
                if (eachMonth == "Décembre")
                {
                    Console.WriteLine("\n" + eachMonth + " : " + ((salaryNet / 12) * (1 + tauxPrime)));
                }
                else
                {
                    Console.WriteLine("\n" + eachMonth + " : " + salaryNet / 12);
                }
            }
        }


        Console.ReadLine();
    }
}