using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    internal class Program
    {
        const string TOKEM = "5997540668:AAFVduLPQ_f0YdGmLwkQfT9ztP6YK7Eg5cg";
        static void Main(string[] args)
        {
            string excelFile = Path.Combine(Environment.CurrentDirectory, "../../../base.xlsx");
            ExcelApplication app = new ExcelApplication(excelFile);
            var questions = app.GetQuestions();
            var films = app.GetFilms();



            MovieBot bot = new MovieBot(TOKEM);
            bot.Questions = questions;
            bot.Films = films;
            bot.StatisticApp = app;
             bot.Start();

        }

    }
}