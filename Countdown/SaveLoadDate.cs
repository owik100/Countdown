using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Countdown
{
    class SaveLoadDate
    {
        private string myDocumentspatch;
        private string finalPatch;

        public SaveLoadDate()
        {
            myDocumentspatch = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            finalPatch = myDocumentspatch + @"\Countdown data";
        }

        public void Save(DateTime date)
        {
            if (!Directory.Exists(finalPatch))
            {
                Directory.CreateDirectory(finalPatch);
                File.WriteAllText(finalPatch + @"\data.txt", date.ToString());
            }
            else
            {
                File.WriteAllText(finalPatch + @"\data.txt", date.ToString());
            }
           
        }

        public DateTime Load()
        {
            string dateFromFile;
            DateTime dateTime;

            try
            {
               dateFromFile = File.ReadAllText(finalPatch + @"\data.txt");
               dateTime = DateTime.Parse(dateFromFile);
               return dateTime;
            }
            catch
            {
                DateTime data = DateTime.Now;
                return data;
            }
        }
    }
}
