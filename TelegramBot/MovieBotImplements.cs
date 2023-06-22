using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBot.Enums;
using TelegramBot.Interface;
using TelegramBot.Model;
using static System.Net.Mime.MediaTypeNames;

namespace TelegramBot
{
    partial class MovieBot : IMovieBot
    {
        static Random random = new Random();
        public FilmModel GetByTags(string search, SearchMode mode)
        {
            throw new NotImplementedException();
        }

        public FilmModel GetRandomFilm()
        {
            int randomFilm = random.Next(0,Films.Length);
            return Films[randomFilm];
        }

        public string GetResponse(string message)
        {
            string text = message.ToLower();

            var question = Questions.FirstOrDefault(q => q.Question.Contains(text));

            if (question != null)
            {
                return question.Response;
            }
            else
            {
                return $"Я тебя понял, а что значит {text} ?";

            }
        }

        public string GetStatisticView()
        {
            throw new NotImplementedException();
        }

        public FilmModel[] GetTopFilms(int count)
        {
            return Films.OrderByDescending(f=>f.Rating).Take(count).ToArray();
        }
    }
}
