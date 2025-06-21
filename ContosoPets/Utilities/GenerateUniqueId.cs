

namespace ContosoPets.Utilities;

public partial class GenerateUniqueId
{
    public static string GetFirstCharactersFromName(string input)
    {
        string[] name = input.Split(' ');

        char firstChar = name[0][0];
        char secondChar = name[1][0];

        return $"{firstChar}{secondChar}";
    }

    public static string CreateUniqueId(string prompt)
    {
        string name;

        Random random = new Random();
        int randomNumber = random.Next(1, 100000);

        name = GetFirstCharactersFromName(prompt) + randomNumber.ToString();

        return name;
    }

}
