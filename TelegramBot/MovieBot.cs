using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBot.Model;
using TelegramBot.Interface;
using Telegram.Bot.Types.InputFiles;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace TelegramBot
{

    partial class MovieBot
    {

        static TelegramBotClient jarvis;
        public QuestionModel[] Questions { private get; set; }
        public FilmModel[] Films { private get; set; }
        public ExcelApplication StatisticApp { private get; set; }
        Dictionary<long, string> Context { get; set; }

        public MovieBot(string token)
        {
            jarvis = new TelegramBotClient(token);
            Context = new Dictionary<long, string>();
        }

        public void Start()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancel = source.Token;


            ReceiverOptions options = new ReceiverOptions()
            {
                AllowedUpdates = { }
            };

            jarvis.StartReceiving(BotTakeMassage, BotTakeError, options, cancel);
            Console.WriteLine("Ready Bot");
            Console.ReadKey();
            StatisticApp.Exit();

        }




        async Task BotTakeMassage(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            if (update.Type == UpdateType.Message)
            {
                Message message = update.Message;

                if (message.Type == MessageType.Text)
                {
                    await GetTextMessage(message);
                }
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                await GetCallback(update.CallbackQuery);
            }
        }


        public async Task GetCallback(CallbackQuery query)
        {
            if (query.Data == null)
            {
                return;
            }

            string[] messages = query.Data.Split('|');
            string filmName = messages[0];
            int rate = Int32.Parse(messages[1]);
            var film = Films.FirstOrDefault(f => f.Name == filmName);
            if (film != null)
            {
                double currentShow = film.Show++;
                double rating = (currentShow * film.Rating + rate) / film.Show;
                film.Rating = rating;
                await jarvis.SendTextMessageAsync(query.From.Id, "Рейтинг IMDb: " + film.Rating);



            }
        }
        public async Task GetTextMessage(Message message)
        {
            string text = message.Text.ToLower();



            if (Context.ContainsKey(message.Chat.Id))
            {
                string context = Context[message.Chat.Id];
                Context[message.Chat.Id] = text;

                if (context == "по жанру")
                {

                    FilmModel[] filmRequest = Films.Where(f => f.Genre.ToLower().Contains(text)).ToArray();
                    if (filmRequest.Length > 0)
                    {
                        foreach (var film in filmRequest)
                        {
                            await ShowFilm(message.Chat.Id, film);
                        }
                    }
                    else
                    {
                        await jarvis.SendTextMessageAsync(message.Chat.Id, "Нам не удалось найти фильм по такому жанру.", replyMarkup: MarkupMenu.SearchMenu);
                    }
                    return;
                }
                if (context == "по названию")
                {
                    FilmModel[] filmRequest = Films.Where(f => f.Name.ToLower().Contains(text)).ToArray();
                    if (filmRequest.Length > 0)
                    {
                        foreach (var film in filmRequest)
                        {
                            await ShowFilm(message.Chat.Id, film);
                        }
                        await jarvis.SendTextMessageAsync(message.Chat.Id, $"Нам удалось найти {filmRequest.Length} фильма", replyMarkup: MarkupMenu.SearchMenu);
                    }
                    else
                    {
                        await jarvis.SendTextMessageAsync(message.Chat.Id, "Нам не удалось найти фильм с таким названием.", replyMarkup: MarkupMenu.SearchMenu);
                    }
                    return;
                }
            }
            else
            {
                Context.Add(message.Chat.Id, text);
            }


            if (text == "рандомный фильм")
            {
                var film = GetRandomFilm();
                await ShowFilm(message.Chat.Id, film);
                return;
            }

            if (text == "топ фильмов")
            {
                var films = GetTopFilms(8);
                foreach (var film in films)
                {
                    await ShowFilm(message.Chat.Id, film);
                }
                return;
            }
            if (text == "найти фильм")
            {
                await jarvis.SendTextMessageAsync(message.Chat.Id, "Выберете тип поиска", replyMarkup: MarkupMenu.SearchMenu);
                return;

            }
            if (text == "меню")
            {
                await jarvis.SendTextMessageAsync(message.Chat.Id, "Вы вернулись в главное меню", replyMarkup: MarkupMenu.MainMenu);
                return;

            }
            if (text == "по жанру")
            {
                await jarvis.SendTextMessageAsync(message.Chat.Id, "Введите жанр фильма ", replyMarkup: MarkupMenu.SearchMenu);
                return;

            }
            if (text == "по названию")
            {
                await jarvis.SendTextMessageAsync(message.Chat.Id, "Введите название фильма", replyMarkup: MarkupMenu.SearchMenu);
                return;

            }

            if (text == "статистика")
            {
                var path = GetStatisticView();
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    var file = new InputOnlineFile(fs, "statistic.png");
                    await jarvis.SendPhotoAsync(message.Chat.Id, file, caption: "Статистика", replyMarkup: MarkupMenu.MainMenu);

                }
                return;
            }








            string responce = GetResponse(text);
            await jarvis.SendTextMessageAsync(message.Chat.Id, responce, replyMarkup: MarkupMenu.MainMenu);
        }

        public async Task ShowFilm(long chat, FilmModel film)
        {
            var buttons = InlineMenu.SetRate(film);
            await jarvis.SendPhotoAsync(chatId: chat, caption: "🎬" + film.Name + "🍿" + "\n" + "\n" + "Жанр: " + film.Genre + "\n" + film.Description, photo: film.Image, replyMarkup: buttons);

        }









        async Task BotTakeError(ITelegramBotClient botClient, Exception ex, CancellationToken token)
        {

        }
    }

}
