using System;

namespace HedgehogPopulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the counts of Red, Green, and Blue hedgehogs (space-separated):");
            if (!TryParseInput(Console.ReadLine(), out int[] counts) || counts.Length != 3)
            {
                Console.WriteLine("Invalid input. Please provide three non-negative integers.");
                return;
            }

            Console.WriteLine("Enter the desired color (0 - Red, 1 - Green, 2 - Blue):");
            if (!int.TryParse(Console.ReadLine(), out int desiredColor) || desiredColor < 0 || desiredColor > 2)
            {
                Console.WriteLine("Invalid input. Desired color must be 0, 1, or 2.");
                return;
            }

            int result = CalculateMinimumMeetings(counts, desiredColor);
            Console.WriteLine(result == -1
                ? "It is impossible to make all hedgehogs the same color."
                : $"Minimum number of meetings: {result}");
        }

        static bool TryParseInput(string? input, out int[] counts)
        {
            counts = Array.Empty<int>();
            if (string.IsNullOrWhiteSpace(input)) return false;

            string[] parts = input.Split();
            if (parts.Length != 3) return false;

            counts = new int[3];
            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i], out int value) || value < 0)
                {
                    return false;
                }
                counts[i] = value;
            }

            return true;
        }

        static int CalculateMinimumMeetings(int[] counts, int desiredColor)
        {
            int totalHedgehogs = counts[0] + counts[1] + counts[2];
            if (counts[desiredColor] == totalHedgehogs) return 0;

            int color1 = (desiredColor + 1) % 3;
            int color2 = (desiredColor + 2) % 3;

            if ((counts[color1] - counts[color2]) % 3 != 0) return -1;

            int meetings = 0;
            while (counts[color1] > 0 && counts[color2] > 0)
            {
                int pairMeetings = Math.Min(counts[color1], counts[color2]);
                counts[desiredColor] += pairMeetings * 2;
                counts[color1] -= pairMeetings;
                counts[color2] -= pairMeetings;
                meetings += pairMeetings;
            }

            while (counts[color1] >= 3)
            {
                counts[color1] -= 3;
                counts[desiredColor] += 3;
                meetings++;
            }

            while (counts[color2] >= 3)
            {
                counts[color2] -= 3;
                counts[desiredColor] += 3;
                meetings++;
            }

            if (counts[color1] > 0 || counts[color2] > 0) return -1;

            return meetings;
        }
    }
}
