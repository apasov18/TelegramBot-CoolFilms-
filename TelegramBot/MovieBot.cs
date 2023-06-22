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

namespace TelegramBot
{

    partial class MovieBot
    {

        static TelegramBotClient jarvis;
        public QuestionModel[] Questions { private get; set; }
        public FilmModel[] Films { private get; set; }







        public MovieBot(string token)
        {
            jarvis = new TelegramBotClient(token);
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
        }




        async Task BotTakeMassage(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            if (update.Type == UpdateType.Message)
            {
                Message message = update.Message;

                if (message.Type == MessageType.Text)

                {
                    if (message.Text == "Рандомный фильм")
                    {
                        var film = GetRandomFilm();
                         await ShowFilm(message.Chat.Id,film);
                        return;
                    }

                    if (message.Text.ToLower() ==  "топ фильмов" )
                    {
                        var films = GetTopFilms(7);

                        foreach (var film in films)
                        await ShowFilm(message.Chat.Id, film);
                        return;
                    }




                    string responce = GetResponse(message.Text);
                    await jarvis.SendTextMessageAsync(message.Chat.Id, responce);
                }


            }
        }

        public async Task ShowFilm ( long chat ,FilmModel film )
        {
            await jarvis.SendPhotoAsync(chatId: chat, photo: film.Image);
            await jarvis.SendTextMessageAsync(chatId: chat, "Жанр : " + film.Genre);
            await jarvis.SendTextMessageAsync(chatId : chat , film.Name + "\n" + film.Description);
            await jarvis.SendTextMessageAsync(chatId: chat,  "Рейтинг IMDb: " + film.Rating );
        } 








        async Task BotTakeError(ITelegramBotClient botClient, Exception ex, CancellationToken token)
        {

        }
    }

}
