using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Model;

namespace TelegramBot
{
    internal class InlineMenu
    {
        public static IReplyMarkup SetRate(FilmModel film)
        {
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    new InlineKeyboardButton("Лучший из лучших")
                    {
                        CallbackData = film.Name + "|10"
                    }
                 },
                 new InlineKeyboardButton[]
                {
                     new InlineKeyboardButton("Очень хорший выбор")
                    {
                        CallbackData = film.Name + "|8"
                    }
                },
                 new InlineKeyboardButton[]
                 {
                      new InlineKeyboardButton("Хороший выбор")
                    {
                        CallbackData = film.Name + "|7"
                    }
                  },
                 new InlineKeyboardButton[]
                 {

                       new InlineKeyboardButton("Под сон пойдет")
                    {
                        CallbackData = film.Name + "|6"
                    }
                 },
                 new InlineKeyboardButton[]
                 {
                        new InlineKeyboardButton("Не советую")
                    {
                        CallbackData = film.Name + "|5"
                    }
                 },


            };
            return new InlineKeyboardMarkup(buttons);
        }
    }
}
