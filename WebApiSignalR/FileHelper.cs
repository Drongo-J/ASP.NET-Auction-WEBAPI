using Microsoft.AspNetCore.Mvc;

namespace WebApiSignalR
{
    public class FileHelper
    {
       

        public static void Write(double data)
        {
            File.WriteAllText("data.txt", data.ToString());
        }

        public static void Write(string room,double data)
        {
            File.WriteAllText("RoomsFiles\\" + room + ".txt", data.ToString());
        }

        public static double Read()
        {
            return double.Parse(File.ReadAllText("data.txt"));
        }
        public static double Read(string room)
        {
            return double.Parse(File.ReadAllText("RoomsFiles\\" + room + ".txt"));
        }

        public static List<string> GetRooms()
        {
            string folderPath = @"RoomsFiles";

            try
            {
                var fileNames = Directory.GetFiles(folderPath).Select(f => Path.GetFileName(f)).ToList();
                return fileNames;
            }
            catch 
            {
                return null!;
            }
        }
    }
}
