using Icu;

namespace MyParking.Helpers;

public static class CyrillicToLatinConverter
{
    public static string CyrillicToLatin(string input)
    {
        var dict = new Dictionary<string, string>
        {
            { "А", "A" }, { "а", "a" },
            { "Б", "B" }, { "б", "b" },
            { "В", "V" }, { "в", "v" },
            { "Г", "G" }, { "г", "g" },
            { "Д", "D" }, { "д", "d" },
            { "Ѓ", "Gj" }, { "ѓ", "gj" },
            { "Е", "E" }, { "е", "e" },
            { "Ж", "Z" }, { "ж", "z" },
            { "З", "Z" }, { "з", "z" },
            { "И", "I" }, { "и", "i" },
            { "Ј", "J" }, { "ј", "j" },
            { "К", "K" }, { "к", "k" },
            { "Л", "L" }, { "л", "l" },
            { "Љ", "Lj" }, { "љ", "lj" },
            { "М", "M" }, { "м", "m" },
            { "Н", "N" }, { "н", "n" },
            { "Њ", "Nj" }, { "њ","nj" },
            { "О", "O" }, { "о", "o" },
            { "П", "P" }, { "п", "p" },
            { "Р", "R" }, { "р", "r" },
            { "С", "S" }, { "с", "s" },
            { "Т", "T" }, { "т", "t" },
            { "Ќ", "Kj" }, { "ќ", "kj" },
            { "У", "U" }, { "у", "u" },
            { "Ф", "F" }, { "ф", "f" },
            { "Х", "H" }, { "х", "h" },
            { "Ц", "C" }, { "ц", "c" },
            { "Ч", "Č" }, { "ч", "č" },
            { "Џ", "Dž" }, { "џ", "dž" },
            { "Ш", "Sh" }, { "ш", "Sh" },
            
        };

        
        foreach (var kvp in dict)
        {
            input = input.Replace(kvp.Key, kvp.Value);
        }

        return input;
    }
}