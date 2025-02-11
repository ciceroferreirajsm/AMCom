using Newtonsoft.Json;
using Questao1.Services;
using Questao2.Domain;

public class Program
{
    private static async Task Main(string[] args)
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.Write("Enter the year: ");            

            int year = int.Parse(Console.ReadLine());

            Console.Write("Enter the team name: ");
            string team = Console.ReadLine();

            var teamGoals = await GoalsDataService.GetGoalsScoredByTeam(year, team);

            if (teamGoals != null && teamGoals.Any())
                Console.WriteLine($"Team {teamGoals.FirstOrDefault().Team} scored {teamGoals.Sum(x => x.Goals)} goals in {teamGoals.FirstOrDefault().Team}");

            Console.WriteLine("\nDo you want to search again? (y/n): ");
            string userChoice = Console.ReadLine().ToLower();

            if (userChoice != "y")
            {
                keepRunning = false;
                Console.WriteLine("Exiting the program...");
            }
        }
    }

}