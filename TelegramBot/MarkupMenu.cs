using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    internal class MarkupMenu
    {
        public static IReplyMarkup MainMenu
        {
            get
            {
                KeyboardButton[][] buttons = new KeyboardButton[][]
                {
                    new KeyboardButton[] { new KeyboardButton("Топ фильмов") ,new KeyboardButton ("Рандомный фильм") },
                    new KeyboardButton[] {new KeyboardButton ("Статистика") },
                    new KeyboardButton[] {new KeyboardButton ("Найти фильм") },
                };
                return new ReplyKeyboardMarkup(buttons);
            }
        }

        public static IReplyMarkup SearchMenu
        {
            get
            {
                KeyboardButton[][] buttons = new KeyboardButton[][]
                {
                    new KeyboardButton[] { new KeyboardButton("По жанру ") ,new KeyboardButton ("По названию") },
                    new KeyboardButton[] {new KeyboardButton ("Меню") },
                };
                return new ReplyKeyboardMarkup(buttons);
            }
        }
    }
}
