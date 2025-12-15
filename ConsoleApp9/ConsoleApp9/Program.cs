using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
struct Dimensions
{
    public string Title;
    public string Author;
    public int Year;
    public Dimensions(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;

    }

}

class Program
{
    void GetBookSummary(Dimensions a)
    {
        Console.WriteLine("Титл ", a.Title, "Автор ", a.Author, "Год ", a.Year);
    }
    static void Main(string[] args)
    {

        Dimensions ABC = new Dimensions("Дом", "Иван", 2025);
        GetBookSummary(ABC);
    }
}