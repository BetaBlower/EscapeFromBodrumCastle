using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Net.Mail;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using EscapeFromBodrumCastle.Entities;


namespace EscapeFromBodrumCastle
{
    class GameMain
    {
        #region Values
        public Map map = new Map();
        public Player player = new Player();
        string Message= "";
        int powerUpTime = 0;
        string powerUpShortName="";
        public bool gameover = false;
        

        #endregion
        
        #region Items
        Item[] items = new Item[]{
                new Item() {ID = 0,Name = "Görünmezlik iskisiri",ShortName = "INVIS",Description= "2 tur görünmez yapar"},
                new Item() {ID = 1,Name = "Işınlanma iskisiri",ShortName = "TP",Description= "Haritada rastgele bir yere ışınlar"},
                new Item() {ID = 2,Name = "Zamanı Durdurma iksiri",ShortName = "TIMEFRZ",Description= "2 tur zamanı durdurur"},
                new Item() {ID = 3,Name = "Zaman bükücü iksir",ShortName = "TIMEBACK",Description= "2 tur görünmez yapar"},
                new Item() {ID = 4,Name = "Harita",ShortName = "MAP ",Description= "Haritanın bazı bölümlerini açar"},
                new Item() {ID = 5,Name = "Anahtar",ShortName = "KEY ",Description= "Sandıkları açar"},
                };
        #endregion

        #region Notes
        Note[] notes = new Note[]{
            new Note() {ID = 1,Name= "Mahmut'un notu",ShortName = "NOTE",Description="Birinden kalma bir not, içinde şunlar yazıyor:\nKalenin en tepesine gidiyorum çıkış orası",Coordinate = [6,8]}
        };
        #endregion
        
        #region Enemies
        Enemy[] enemies = new Enemy[]{
            new Enemy() {X = 0,Y = 13,StartX = 0,StartY = 13,Speed = 1},
            //new Enemy() {X = 3,Y = 10,StartX = 3,StartY = 10,Speed = 1},
            new Enemy() {X = 2,Y = 6,StartX = 2,StartY = 6,Speed = 1}
        };
        
        #endregion

        #region Scenes
        public void MainMenu(){
            Console.Clear();
            Console.WriteLine("Bodrum Kalesinden Kaçışa hoşgeldinizzz\nLütfen yapmak istediğiniz işlemin numarasını girin\n");
            Console.WriteLine("1-Yeni Oyun");
            Console.WriteLine("2-Emeği geçenler");
            Console.WriteLine("3-Hedefler ve açıklama");
            Console.WriteLine("4-Hızlı Kaçış");
            
            bool flag = true;
            int sceneId = 3;

            while   (flag){
                char? userInput = Console.ReadKey().KeyChar;
                if (!Int32.TryParse(Convert.ToString(userInput), out int userInt))
                {
                    Console.WriteLine($"Lütfen bir sayı giriniz");
                }
                else if (!(userInt <=4) || !(userInt>= 1) ){
                    Console.WriteLine("Lütfen 1 - 4 aralığında geçerli sayı giriniz ");
                }
                else
                {
                    flag = false;
                    sceneId = userInt;
                }
            }
            if (sceneId == 1)
                Game();
            else if (sceneId == 2)
                Cast();
            else if(sceneId == 3)
                Goal();
            else
                Exit();
                


        }

        public void Game(){
            Start();
            Uptade();
        }
        public void Cast(){
            ClearConsole();
            Console.WriteLine("ben(Yunus Emre Kara)\nSerdar Ortaç Ve Püren Al : Mental Destek");
            Console.ReadKey(true);
            MainMenu();

        }
        public void Goal(){
            Console.WriteLine(@"
Ödevi Unuttuğumdan mütevellit 36 saatte 6 saatlik uyku ile yapıldı.
projede ChatGpt veya herhangi bir yapay zekadan yararlanılmamıştır
Zamanım yetişseydi veya motivasyon verecek bir şey olsa(Koşulsuz 100 gibi :D)
Oyuna eklemeyi planladıklarım:

Karakter özellikleri : öldükçe yaşadığımız seviyeye göre öldükten sonra
karakterimize ek özellik seçiyoruz
Grafik arayüzü: normal bir ekran gibi
farklı düşmanlar: daha hızlı veya daha uzağı gören
Npcler: görev alabileceğimiz npcler
Optimizasyon:sistem kaynaklarını daha iyi kullanmak ve üzerinde çalışmayı
kolaylaştırmak için kodları iyileştirme
Daha fazla level: Oyunda sistemi bulnuyor sadece eklenmesi gerekiyor, aslında hepsinin
alt yapısı var :D
daha var da var ancak şuan o kadar uykususum ki aklıma gelmiyor :/
neyse Vakit ayırdığınız için teşekkür ederim.


");
            Console.ReadKey();
            MainMenu();
        } 
        public void Exit(){
            Console.WriteLine("Tebrikler Kaleden Kaçmayı Başardın");
            Console.WriteLine("Kaçmak istediğine emin misin?");
            Console.ReadLine();

        }




        #endregion
      



        #region Main Functions
       
        void Start(){
            map.CreateMap();
            gameover = false;

            ClearConsole();
            Console.WriteLine("karakterinizin ismi nedir");
            player.Name = Console.ReadLine();
            player.Position = [7,0];
            player.Visibility = true;
            player.MapLevel = 1;
            player.Inventory = new List<Item>{};
            player.Notes = new List<Note>{};
            map.currentLevel[player.Position[0],player.Position[1]] = 2;
            
            SpawnEnemies(map.currentLevel);
            SpawnNotes();
            


        }
        void Uptade(){
            
            while (!gameover){
                
                ClearConsole();
                Console.WriteLine("Yardım için 'h' yaz ");
                map.ShowMap(map.currentLevel);
                PrintInventory(map.currentLevel.GetLength(1)+2);
                PrintMessageAndDelete();
                bool nextTurn = AskController();
                EndTurn(nextTurn);
                
            }
        }
        
        #endregion    
        #region  Functions
        
       
        
        char AskInput(){
            bool flag = true;
            char userInput = ' ';
            Console.WriteLine("Ne yapmak istersin");
            while   (flag){
                userInput = Console.ReadKey(true).KeyChar;
                char.ToLower(userInput);
                if (userInput == null){
                    Console.WriteLine("Lütfen bir değer giriniz");
                }
                else if (userInput== 'h'){
                    Console.WriteLine(
                        @"Yazılabilecek Komutlar :
w = karakteri yukarı yönde hareket ettirir                        
s = karakteri aşağı yönde hareket ettirir
a = karakteri sol yönde hareket ettirir
d = karakteri sağ yönde hareket ettirir
n = notları açar
1,2,3,4,5 = etkileşim veya çantadan eşya seçmek için
p = durdur :D
b = ana menü");
                }
                else if (userInput== 'w')
                    flag = false;
                else if (userInput== 's')
                    flag = false;
                else if (userInput== 'a')
                    flag = false;
                else if (userInput== 'd')
                    flag = false;
                else if (userInput== 'n')
                    flag = false;
                else if (userInput== '1')
                    flag = false;
                else if (userInput== '2')
                    flag = false;
                else if (userInput== '3')
                    flag = false;
                else if (userInput== '4')
                    flag = false;
                else if (userInput== '5')
                    flag = false;
                else if (userInput== 'p')
                    flag = false;
                else if (userInput== 'b')
                    flag = false;
                else{
                    Console.WriteLine(
                        @"Yazılabilecek Komutlar :
w = karakteri yukarı yönde hareket ettirir                        
s = karakteri aşağı yönde hareket ettirir
a = karakteri sol yönde hareket ettirir
d = karakteri sağ yönde hareket ettirir
n = notları açar
1,2,3,4,5 = etkileşim veya çantadan eşya seçmek için
p = durdur :D
b = ana menü");                    
                }
            }
            return userInput;
            
        }
        void ClearConsole(){
            Console.Clear();
        }
        bool PlayerMovement(char direction){
            
            if (direction=='w'){
                if(!(player.Position[0] == 0))
                    if(!(map.currentLevel[player.Position[0]- 1,player.Position[1]] == 0)){
                        player.Position[0]--; 
                        Collision(player);
                        map.currentLevel[player.Position[0] + 1,player.Position[1]]= 1;
                        
                        map.currentLevel[player.Position[0],player.Position[1]]= 2;
                        Message += "Karakter Yukarı doğru hareket etti";
                        return true;
                    }
            } 
            else if (direction=='s'){
                if(!(player.Position[0] == map.currentLevel.GetLength(0)-1)){
                    Console.WriteLine("geri ");
                    if(!(map.currentLevel[player.Position[0]+ 1,player.Position[1]] == 0)){
                        player.Position[0]++; 
                        Collision(player);
                        map.currentLevel[player.Position[0]-1,player.Position[1]]= 1;
                        map.currentLevel[player.Position[0],player.Position[1]]= 2;
                        Message += "Karakter aşağı doğru hareket etti";
                        return true;
                    }
                
                }
            }
            else if (direction=='a'){   
                if(!(player.Position[1] == 0))
                    if(!(map.currentLevel[player.Position[0],player.Position[1] -1] == 0)){
                        player.Position[1]--; 
                        Collision(player);
                        map.currentLevel[player.Position[0],player.Position[1] + 1]= 1;
                        map.currentLevel[player.Position[0],player.Position[1]]= 2;
                        Message += "Karakter sola doğru hareket etti";
                        return true;
                    }
            }
            else if (direction=='d'){  
                if(!(player.Position[1] == map.currentLevel.GetLength(1)-1))
                    if(!(map.currentLevel[player.Position[0],player.Position[1]+1] == 0)){
                        player.Position[1]++;
                        Collision(player);
                        map.currentLevel[player.Position[0],player.Position[1] - 1]= 1;
                        map.currentLevel[player.Position[0],player.Position[1]]= 2;
                        Message += "Karakter sağa doğru hareket etti";
                        return true;
                    }
            }
            Message += "Karakter Buraya hareket edemiyor";
            return false;
        }
        void Collision(Player player_){
            int x = player_.Position[0];
            int y = player_.Position[1];
            if(!(map.currentLevel[x,y]== 1))
            {
                if(map.currentLevel[x,y] == 3){ //hazine
                    GiveItem();
                }
                else if(map.currentLevel[x,y] == 4){//düşman
                    RestartGame();
                }
                else if(map.currentLevel[x,y] == 5){//notlar
                    GiveNote();
                }
                else if(map.currentLevel[x,y] == 6){
                    EndDemo();
                }
            }
        }

        bool AskController(){
            char ask = AskInput();

            if(ask == 'w'|| ask == 's' || ask == 'a'|| ask == 'd'){
                if(PlayerMovement(ask)){
                    return true;
                }
                else{
                    return false;
                }
            }
            else if(ask == '1' || ask == '2' || ask == '3' || ask == '4' || ask == '5' ){
                
                UseItem(Convert.ToInt32(Convert.ToString(ask))-1);
            }
            else if(ask == 'n'){
                Message+= "Demoda bulunmuyor";

                return false;
            }
            else if(ask=='p'){
                Message = "Oyun Durdu :D";
                return false;
            }
            else if(ask=='b'){
                gameover = true;
                MainMenu();
            }
            return false;

        }
        void PrintInventory(int wallSize)
        {
            Graphics.PrintWallWithNameMiddle(wallSize,"Envanter",ConsoleColor.DarkGray,ConsoleColor.DarkGreen,"");
            if(!(player.Inventory.Count() == 0)){
                 for(int i = 0;i< player.Inventory.Count;i++){
                    Graphics.PrintWallWithNameMiddle(wallSize,1,$"{i+1}.{player.Inventory[i].ShortName}",ConsoleColor.DarkGray,ConsoleColor.White,"");
                }
            }
            else{
                Graphics.PrintWallWithNameMiddle(wallSize,"  Envanter Boş  ",ConsoleColor.DarkGray,ConsoleColor.White,"");
            }
            Graphics.PrintWall(wallSize);

        }
        void PrintMessageAndDelete() {
            Console.WriteLine(Message);
            Message = "";
        }
        void GiveItem() {
            
            if(player.Inventory.Count <= 5){
                int chance = Random.Shared.Next(0, 100);
                if(chance<40){
                    Item item = items[Random.Shared.Next(0,2/*items.Count()*/)];
                    player.Inventory.Add(item);
                    Message += $"{player.Name} girdiği odada bir {item.Name} buldu\n";
                }
                else{
                    if(40<chance &&chance<70 ){
                        Message += $"{player.Name} girdiği odada hiçbir şey bulamadı\n";
                    }
                    else{
                        Message += $"{player.Name} girdiği odada kötü bir koku ve cesetler dışında hiçbir şey bulamadı\n";
                    }
                }
                
                
            }
            else {
                Message +=$"{player.Name}, Elleri Dolu olduğu için eşyayı almaya çalışırken kırdı\n";
            }

           
            
        }
        void GiveNote() {
            for (int i = 0;i< notes.Length;i++) {
                if(notes[i].Coordinate[0] == player.Position[0] && notes[i].Coordinate[1] == player.Position[1]) {
                    player.Notes.Add(notes[i]);
                    Message += $"Odada Bir not Buldun!\n{notes[i].Description}\n";
                }

            }
        }
        void EnemyManager(){
            if(player.Visibility) 
            {
                for(int i = 0;i<enemies.Length;i++){
                    EnemyMove(player.Position[0],player.Position[1],map.currentLevel,enemies[i]);
                }
            }
            else{
                for(int i = 0;i<enemies.Length;i++){
                    EnemyMove(enemies[i].StartX,enemies[i].StartY,map.currentLevel,enemies[i]);
                }
            }       
        }
        void EnemyMove(int targetX,int targetY,int[,] map,Enemy enemy) 
        {
            int[] difference = [targetX - enemy.X,targetY- enemy.Y];
            if(Math.Abs(difference[0]) <=2 && Math.Abs(difference[1]) <=2)
            {

                int[] movement = [
                    difference[0] == 0 ? 0 : difference[0] < 0 ? -1:1,
                    difference[1] == 0 ? 0 : difference[1] < 0 ? -1:1];
                if(Math.Abs(difference[0]) > Math.Abs(difference[1])){
                    if(!(map[enemy.X + movement[0],enemy.Y] == 0)){
                        map[enemy.X,enemy.Y] = 1;
                        enemy.X += movement[0];
                        map[enemy.X,enemy.Y] = 4;
                        
                    }
                    else if(!(map[enemy.X ,enemy.Y + movement[1]] == 0))
                    {
                        map[enemy.X,enemy.Y] = 1;
                        enemy.Y += movement[1];
                        map[enemy.X,enemy.Y] = 4;
                    }

                }
                
                else
                {
                    if(!(map[enemy.X ,enemy.Y + movement[1]] == 0)){
                        map[enemy.X,enemy.Y] = 1;
                        enemy.Y += movement[1];
                        map[enemy.X,enemy.Y] = 4;
                        
                    }
                    else if(!(map[enemy.X + movement[0],enemy.Y] == 0))
                    {
                        map[enemy.X,enemy.Y] = 1;
                        enemy.X += movement[0];
                        map[enemy.X,enemy.Y] = 4;
                    }                

                }
            }
            
        }       
        void EndTurn(bool nextTurn){
            if(nextTurn){
                EnemyManager();
                Collision(player);
                
                Message += powerUpTime +" -önce- " + powerUpShortName + "\n";
                if(powerUpTime>0){
                    powerUpTime--;
                    Message += powerUpTime +"  " + powerUpShortName + "\n";
                    CheckPowerUps();
                }
                

            }

        }
        void SpawnEnemies(int[,] map){
            for(int i = 0;i<enemies.Length;i++) {
                enemies[i].X = enemies[i].StartX;
                enemies[i].Y = enemies[i].StartY;
                map[enemies[i].X,enemies[i].Y] = 4;
            }
        }
        void SpawnNotes() {
            for(int i = 0;i<notes.Length;i++){
                map.currentLevel[notes[i].Coordinate[0],notes[i].Coordinate[1]] = 5;
            }
            
        }
        void RestartGame(){
            Console.WriteLine("Şövalyeler seni yakaladı ve en başa döndün");
            //gameover = false;
            Console.ReadKey();
            Game();


        }
          void UseItem(int index) {      
            Console.WriteLine(index);      
            if(!(player.Inventory.Count <=index )){
                PowerUps(player.Inventory[index]);
                player.Inventory.RemoveAt(index);
            }
            else
            {
                Message += " O slotta eşyası yok\n";
            }
            
        }
        private void PowerUps(Item item) {
            if(item.ID == 0){//görünmezlik
                Message+=$"{player.Name} Görünmezlik iksiri kullandı, 2 tur boyunca görünmez\n";
                player.Visibility = false;
                powerUpShortName = item.ShortName;
                powerUpTime = 2;
            }
            else if(item.ID == 1){//ışınlanma
                Message+=$"{player.Name} ışınlanma iksiri kullandı ve {player.Name} rastgele bir yere ışınlandı\n";

                int mapxlenght = map.currentLevel.GetLength(0);
                int mapylenght = map.currentLevel.GetLength(0);

                bool flag = true;
                while(flag){
                    int x = Random.Shared.Next(0, mapxlenght);
                    int y = Random.Shared.Next(0,mapylenght);
                    if(map.currentLevel[x,y] == 1){
                        flag = false;
                        map.currentLevel[player.Position[0],player.Position[1]] = 1;
                        player.Position[0] = x;
                        player.Position[1] = y;
                        map.currentLevel[player.Position[0],player.Position[1]] = 2;
                    }
                }
                
                
            }
            else if(item.ID == 2){// zamanı durdurma iksiri

            }
            else if(item.ID == 3){//zaman bükücü iksir

            }
            else if(item.ID == 4){//harita

            }
            else if(item.ID == 5){//anahtar

            }
            
        }
        void CheckPowerUps() {
            if(powerUpTime <= 1) {
                if(powerUpShortName == "INVIS"){//görünmezlik
                    Message+=$"Görünmezlik iksirinin Etkisi geçti\n";
                    player.Visibility = true;
                    powerUpShortName = "";
                }
                else if(powerUpShortName =="TIMEFRZ"){// zamanı durdurma iksiri
                    Message+=$"zamanı durdurma iksirinin Etkisi geçti\n";
                }
            }
        }
        void EndDemo() {
            Console.WriteLine("Tebrikler demoyu bitirdiniz\nOyuna daha neler ekleneceğini görmek için lütfen hedefler kısmına bakın ");
            gameover = true;
        }
        #endregion

      


    }
    
}
