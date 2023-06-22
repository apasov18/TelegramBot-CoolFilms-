using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Enums;
using TelegramBot.Model;

namespace TelegramBot.Interface
{
    internal interface IMovieBot
    {
        FilmModel GetRandomFilm();
        FilmModel[] GetTopFilms(int count);
        FilmModel GetByTags(string search,SearchMode mode );
        string  GetStatisticView();
        string GetResponse(string question);

    }
}
