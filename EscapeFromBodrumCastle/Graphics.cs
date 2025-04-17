using System.Security.Cryptography.X509Certificates;

namespace EscapeFromBodrumCastle
{
    public static class Graphics
    {


        #region Wall
        public static void PrintWall()
        {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"\u2588");
                Console.ResetColor();
        }
        public static void PrintWall(string attachment)
        {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"\u2588{attachment}");
                Console.ResetColor();
        }
        public static void PrintWall(string attachment,ConsoleColor color)
        {
                Console.ForegroundColor = color;
                Console.Write($"\u2588{attachment}");
                Console.ResetColor();
        }
        public static void PrintWall(int wallLenght) 
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(string.Concat(Enumerable.Repeat("\u2588",wallLenght)));
            Console.ResetColor();
        }
        public static void PrintWall(int wallLenght,string attachment) 
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(string.Concat(Enumerable.Repeat($"\u2588{attachment}",wallLenght)));
            Console.ResetColor();
        }  
        
        public static void PrintWall(int wallLenght,string attachment,ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(string.Concat(Enumerable.Repeat($"\u2588{attachment}",wallLenght)));
            Console.ResetColor();
        }
        public static void PrintWallWithNameMiddle(int wallLenght,string text,ConsoleColor wallColor,ConsoleColor textColor){
            double oneSideWallCount =(wallLenght - text.Length)/2; 
            Console.ForegroundColor = wallColor;
            Console.Write(string.Concat(Enumerable.Repeat("\u2588",Convert.ToInt32(Math.Ceiling(oneSideWallCount)))));
            Console.ForegroundColor=textColor;
            Console.Write(text);
            Console.ForegroundColor= wallColor;
            Console.Write(string.Concat(Enumerable.Repeat("\u2588",Convert.ToInt32(Math.Floor(oneSideWallCount)))));
            Console.ResetColor();
            

        }
        public static void PrintWallWithNameMiddle(int wallLenght,string text,ConsoleColor wallColor,ConsoleColor textColor,string EndLine){
            double oneSideWallCount =(wallLenght - text.Length)/2; 
            Console.ForegroundColor = wallColor;
            Console.Write(string.Concat(Enumerable.Repeat("\u2588",Convert.ToInt32(Math.Ceiling(oneSideWallCount)))));
            Console.ForegroundColor=textColor;
            Console.Write(text);
            Console.ForegroundColor= wallColor;
            Console.WriteLine(string.Concat(Enumerable.Repeat("\u2588",Convert.ToInt32(Math.Floor(oneSideWallCount)))));
            Console.ResetColor();

        }                        
        public static void PrintWallWithNameMiddle(int wallLenght,int oneSideWallCount,string text,ConsoleColor wallColor,ConsoleColor textColor){
            double oneSideSpaceLenght =(wallLenght-oneSideWallCount - text.Length)/2; 
            Console.ForegroundColor = wallColor;
            Console.Write("\u2588" + string.Concat(Enumerable.Repeat(" ",Convert.ToInt32(Math.Floor(oneSideSpaceLenght)))));
            Console.ForegroundColor=textColor;
            Console.Write(text);
            Console.ForegroundColor= wallColor;
            Console.Write(string.Concat(Enumerable.Repeat(" ",Convert.ToInt32(Math.Floor(oneSideSpaceLenght))))+ "\u2588");
            Console.ResetColor();

        }
        public static void PrintWallWithNameMiddle(int wallLenght,int oneSideWallCount,string text,ConsoleColor wallColor,ConsoleColor textColor,string EndLine){
            double oneSideSpaceLenght =(wallLenght-oneSideWallCount - text.Length)/2; 
            Console.ForegroundColor = wallColor;
            Console.Write("\u2588" + string.Concat(Enumerable.Repeat(" ",Convert.ToInt32(Math.Ceiling(oneSideSpaceLenght)))));
            Console.ForegroundColor=textColor;
            Console.Write(text);
            Console.ForegroundColor= wallColor;
            Console.WriteLine(string.Concat(Enumerable.Repeat(" ",Convert.ToInt32(Math.Floor(oneSideSpaceLenght))))+ "\u2588");
            Console.ResetColor();
            

        }
        #endregion
        
    }
}