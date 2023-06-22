using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Model;
using Excel = Microsoft.Office.Interop.Excel.Application;

namespace TelegramBot
{
    internal class ExcelApplication
    {

        Excel application;
        Workbook book;
        Worksheet sheet;
        public ExcelApplication(string path) 
        {
            application = new Excel();
             book = application.Workbooks.Open(path);
            application.Visible = true;
        }
        public QuestionModel[] GetQuestions() 
        {
            if (book == null)
            {
                return new QuestionModel[0];
            }
             sheet = book.Sheets["Questions"];
            int count = Count;
            QuestionModel[] questions = new QuestionModel[count];
            for (int i = 0; i < count;i++)
            {
                questions[i] = new QuestionModel()
                {
                    Question= sheet.Cells[i+1,"A"].Text,
                    Response = sheet.Cells[i+1,"B"].Text,
                };
            }
            return questions;
        }


        public FilmModel[] GetFilms()
        {
            if (book == null)
            {
                return new FilmModel[0];
            }
            sheet = book.Sheets["Films"];
            int count = Count;
            FilmModel[] questions = new FilmModel[count];
            for (int i = 0; i < count; i++)
            {
                questions[i] = new FilmModel()
                {
                    Name = sheet.Cells[i + 1, "A"].Text,
                    Description = sheet.Cells[i + 1, "B"].Text,
                    Genre = sheet.Cells[i + 1, "C"].Text,
                    Rating = sheet.Cells[i + 1, "D"].Value2,
                     Show= sheet.Cells[i + 1, "E"].Value2,
                     Image= sheet.Cells[i + 1, "F"].Text,




                };
            }
            return questions;
        }
        public int Count
        {
            get
            {
                int count = 0;
                if (sheet == null)
                {
                    return -1;
                }
                while (true)
                {
                    var value1 = sheet.Cells[count + 1, "A"].Value2;
                    if (value1 == null)
                    {
                        break;
                    }
                    count++;
                }
                return count;
            }
        }
    }
}
