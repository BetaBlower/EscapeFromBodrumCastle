namespace EscapeFromBodrumCastle
{
    class Map
    {
        
        public int[,] currentLevel = {
                { 0, 1, 1, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0 }, 
                { 3, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0 }, 
                { 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0 }, 
                { 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1 }, 
                { 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 }, 
                { 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 }, 
                { 1, 0, 3, 1, 1, 0, 3, 0, 1, 1, 1, 1, 1, 3, 0, 0 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
        private int[,] Level_1 = {
                { 0, 1, 1, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0 }, 
                { 3, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0 }, 
                { 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0 }, 
                { 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 6 }, 
                { 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 }, 
                { 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 }, 
                { 1, 0, 3, 1, 1, 0, 3, 0, 1, 1, 1, 1, 1, 3, 0, 0 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
        
        public void CreateMap(){
            for (int i = 0;i<Level_1.GetLength(0);i++) {
                for (int j = 0;j<Level_1.GetLength(1);j++) {
                    currentLevel[i,j] = Level_1[i,j];
                } 
            }
        }
        public void UpdateMap(int[,] map,int x,int y,int Value) {
            map[x,y]=Value;

        }
        public void ShowMap(int[,] map)
        {
            Graphics.PrintWall(map.GetLength(1) + 2);
            for (int i = 0; i < map.GetLength(0); i++) {

                Graphics.PrintWall();
                for (int j = 0; j < map.GetLength(1); j++) {
                    
                    if (map[i, j] == 0){
                        Console.Write(" ");
                    }
                    else if (map[i, j] == 1){
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\u2593");
                    }
                    else if (map[i, j] == 2){
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("\u25A0");
                        Console.ResetColor();
                    }      
                    else if (map[i, j] == 3){
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\u2593");
                        Console.ResetColor();
                    }                      
                    else if (map[i, j] == 4){
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\u2588");
                        Console.ResetColor();
                    }
                    else if (map[i, j] == 5){
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("\u2593");
                        Console.ResetColor();                        
                    }
                    else if (map[i, j] == 6){
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\u2593");
                        Console.ResetColor();
                    }

                    
                }
                Graphics.PrintWall("\n");
            }

        }
    }
}
