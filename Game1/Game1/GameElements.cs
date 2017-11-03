using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace nicegame

{
    static class GameElements
    {
        static Background background;
        static Background background2;
        static Background purchasebackground;
        static Background backgroundcontrols;
        static Background storebackground;
        static Menu menu;
        static Menu menu2;
        static Menu store;
        static Menu purchasemenu;
        static Menu controls;
        static Player player;
      
        static List<Enemy> enemies;
        static List<HighScore> highScoreList;
        static int lastScore = 0;
       
        static List<GoldCoin> goldCoins;
        static List<Heart> hearts;
        static List<Purchase> purchaze;
        static Texture2D heartSprite;
        static Texture2D goldCoinSprite;
      
        static PrintText printText;
        static List<Portal> portals;
        static Texture2D portalSprite;
        static Texture2D purchaseSprite;
        static Texture2D pShotSprite;
        public static SoundEffect effect;
        
       
        static Texture2D texture;
        static Rectangle rectangle;
        static Vector2 position;

        public static Texture2D Ctexture;
        public static Rectangle Crectangle;
        public static Vector2 Cposition;

        public static double timeSinceLastPress = 0;
        public static double timeSinceLastPurchase = 0;


        




        static Song song;
        public enum State { Menu, Controls, As1, TpCd1, moregold, Menu2, Purchase, Run, Run2, Store, Highscore, Quit };
        public static State currentState;
        public static bool isAlive
    
        {
            get { return isAlive; }
            set { isAlive = value; }
        }


        static GameTime lastTime;
  

        public static void Initialize()
        {
            goldCoins = new List<GoldCoin>();
            portals = new List<Portal>();
            hearts = new List<Heart>();
            purchaze = new List<Purchase>();

        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {

            song = content.Load<Song>("Sounds/Song");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            texture = content.Load<Texture2D>("images/Healthbar");
            position = new Vector2(320, 0);
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Ctexture = content.Load<Texture2D>("images/Cooldown");
            Cposition = new Vector2(550, 0);
            Crectangle = new Rectangle(0, 0, texture.Width, texture.Height);


            pShotSprite = content.Load<Texture2D>("images/player/Bullets/pShot");
          
    


            player = new Player(content.Load<Texture2D>("images/player/player_right"), 0, 500, 4f, 4f, content.Load<Texture2D>("images/player/Bullets/Bullet1"));

            if (player.Level == 5)
            {
                player = new Player(content.Load<Texture2D>("images/player/player_right"), 0, 500, 4f, 4f, content.Load<Texture2D>("images/player/Bullets/Bullet1"));
                player.X = 0;
                player.Y = 500;
        
            }
            

       
         
             enemies = new List<Enemy>();
             Random random = new Random();
             Texture2D tmpSprite = content.Load<Texture2D>("images/enemies/NOMNOM/Nomnom");
            for (int i = 0; i < 2; i++)
            {

                int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                int rndY = random.Next(window.ClientBounds.Height - 150, window.ClientBounds.Height - 50) - tmpSprite.Height;
                NOMNOM temp = new NOMNOM(tmpSprite, rndX, rndY);
                enemies.Add(temp);
          
               
            }

            tmpSprite = content.Load<Texture2D>("images/enemies/fedora/fedora_left1");
            
         


           
            for (int i = 0; i < 2; i++)
            {
                int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                int rndY = random.Next(window.ClientBounds.Height - 200, window.ClientBounds.Height) - tmpSprite.Height;
                Fedora temp = new Fedora(tmpSprite, rndX, rndY);
                enemies.Add(temp);
                
            }

          

            tmpSprite = content.Load<Texture2D>("images/enemies/Bosses/Penguin");
            tmpSprite = content.Load<Texture2D>("images/player/Teleport/Teleport1");



            portalSprite = content.Load<Texture2D>("images/Portal");
            purchaseSprite = content.Load<Texture2D>("images/Purchase2");
            goldCoinSprite = content.Load<Texture2D>("powerups/coin");
            heartSprite = content.Load<Texture2D>("powerups/Heart");

            printText = new PrintText(content.Load<SpriteFont>("myFont"));



            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("images/menu/start"), (int)State.Run);
            menu.AddItem(content.Load<Texture2D>("images/menu/highscore"), (int)State.Highscore);
            menu.AddItem(content.Load<Texture2D>("images/menu/controls"), (int)State.Controls);
            menu.AddItem(content.Load<Texture2D>("images/menu/exit"), (int)State.Quit);
            

            menu2 = new Menu((int)State.Menu2);
            menu2.AddItem(content.Load<Texture2D>("images/menu/continue"), (int)State.Run2);
            menu2.AddItem(content.Load<Texture2D>("images/menu/store"), (int)State.Store);
            menu2.AddItem(content.Load<Texture2D>("images/menu/exit"), (int)State.Quit);

            store = new Menu((int)State.Store);
            
            store.AddItem(content.Load<Texture2D>("images/menu/moregold"), (int)State.moregold);
            store.AddItem(content.Load<Texture2D>("images/menu/attackspeed1"), (int)State.As1);
            store.AddItem(content.Load<Texture2D>("images/menu/exit"), (int)State.Menu2);
            

            controls = new Menu((int)State.Controls);
            controls.AddItem(content.Load<Texture2D>("images/menu/exit"), (int)State.Menu);

            purchasemenu = new Menu((int)State.Controls);
            purchasemenu.AddItem(content.Load<Texture2D>("images/menu/exit"), (int)State.Store);


           

            background = new Background(content.Load<Texture2D>("images/background"), window);
            background2 = new Background(content.Load<Texture2D>("images/background2"), window);
            
            storebackground = new Background(content.Load<Texture2D>("images/storebackground"), window);
            backgroundcontrols = new Background(content.Load<Texture2D>("images/Controls"), window);
            

            



        }
        public static State ControlsUpdate(GameTime gameTime)
        {
            return (State)controls.Update(gameTime);
        }
        public static void ControlsDraw(SpriteBatch spriteBatch)
        {
            backgroundcontrols.Draw(spriteBatch);
            controls.Draw(spriteBatch);

        }
    
     
        public static State As1Update(GameTime gameTime)
        {
            if (player.Cash > 199)
            {
                purchaze.Add(new Purchase(purchaseSprite, 700, 127));
          
                foreach (Bullet b in player.Bullets.ToList())
                {

                    b.Aspeed1 = 2;

                }
                player.Cash -= 200;
                
           
                return State.Store;
            }


            return (State)store.Update(gameTime);
        }
        
        public static void As1Draw(SpriteBatch spriteBatch)
        {
            
            storebackground.Draw(spriteBatch);
            printText.Print("CASH:" + player.Cash, spriteBatch, 350, 0);
            store.Draw(spriteBatch);


        }
      
        public static State PurchaseUpdate(GameTime gameTime)
        {
            return (State)purchasemenu.Update(gameTime);
        }
        public static void PurchaseDraw(SpriteBatch spriteBatch)
        {
            purchasebackground.Draw(spriteBatch);
            purchasemenu.Draw(spriteBatch);

        }
       
        
        public static State moregoldUpdate(GameTime gameTime)
        {
            if (player.Cash > 499)
            {
                player.Moregold = 2;
                player.Cash -= 500;
            }
                
            
            return (State)store.Update(gameTime);
        }
       
        public static State Menu2Update(GameTime gameTime)
        {
            return (State)menu2.Update(gameTime);
        }
            

        public static State MenuUpdate(GameTime gameTime)
        {

            return (State)menu.Update(gameTime);
           
        }
        public static void Menu2Draw(SpriteBatch spriteBatch)
        {
            background2.Draw(spriteBatch);
            menu2.Draw(spriteBatch);
        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            menu.Draw(spriteBatch);
         
        }


        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {
            lastTime = gameTime;
            KeyboardState keyboardState = Keyboard.GetState();
           
            Random random = new Random();
            int newCoin = random.Next(1, 100);
            if (newCoin == 1)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(290, window.ClientBounds.Height - goldCoinSprite.Height);
                goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }

            
            {
            }

            
            int newHeart = random.Next(1, 250);
            if (newHeart == 1)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - heartSprite.Width);
                int rndY = random.Next(290, window.ClientBounds.Height - heartSprite.Height);
                hearts.Add(new Heart(heartSprite, rndX, rndY, gameTime));
            }
           
        
           

        

            foreach (Portal p in portals.ToList())
            {
                
                if (p.CheckCollision(player) && player.Level == 1)
                {

                    
                    
                        player.X = 0;
                        player.Y = 500;
                        player.Level++;
                        enemies = new List<Enemy>();

                        Texture2D tmpSprite = content.Load<Texture2D>("images/enemies/fedora/fedora_left1");
                        for (int i = 0; i < 3; i++)
                        {
                            int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                            int rndY = random.Next(window.ClientBounds.Height - 200, window.ClientBounds.Height) - tmpSprite.Height;
                            Fedora temp = new Fedora(tmpSprite, rndX, rndY);
                            enemies.Add(temp);
                        }

                        tmpSprite = content.Load<Texture2D>("images/enemies/NOMNOM/Nomnom");
                        for (int i = 0; i < 4; i++)
                        {
                            int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                            int rndY = random.Next(window.ClientBounds.Height - 150, window.ClientBounds.Height -50) - tmpSprite.Height;
                            NOMNOM temp = new NOMNOM(tmpSprite, rndX, rndY);
                            enemies.Add(temp);
                        }
                        portals.Clear();
                        goldCoins.Clear();
                        hearts.Clear();
                    

                    }

                if (p.CheckCollision(player) && player.Level == 2)
                {
                    player.X = 0;
                    player.Y = 500;
                    player.Level++;
                    enemies = new List<Enemy>();

                    Texture2D tmpSprite = content.Load<Texture2D>("images/enemies/fedora/fedora_left1");
                    for (int i = 0; i < 5; i++)
                    {
                        int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                        int rndY = random.Next(window.ClientBounds.Height - 200, window.ClientBounds.Height) - tmpSprite.Height;
                        Fedora temp = new Fedora(tmpSprite, rndX, rndY);
                        enemies.Add(temp);
                    }

                    tmpSprite = content.Load<Texture2D>("images/enemies/NOMNOM/Nomnom");
                    for (int i = 0; i < 6; i++)
                    {
                        int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                        int rndY = random.Next(window.ClientBounds.Height - 150, window.ClientBounds.Height - 50) - tmpSprite.Height;
                        NOMNOM temp = new NOMNOM(tmpSprite, rndX, rndY);
                        enemies.Add(temp);
                    }
                    portals.Clear();
                    goldCoins.Clear();
                    hearts.Clear();

                }
                if (p.CheckCollision(player) && player.Level == 3)
                {
                    player.X = 0;
                    player.Y = 500;
                    player.Level++;
                    enemies = new List<Enemy>();
                    Texture2D tmpSprite = content.Load<Texture2D>("images/enemies/Bosses/Penguin");
                    for (int i = 0; i < 1; i++)
                    {
                        int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                        int rndY = random.Next(window.ClientBounds.Height - 200, window.ClientBounds.Height) - tmpSprite.Height;
                        Penguin temp = new Penguin(tmpSprite, rndX, rndY, pShotSprite);
                        enemies.Add(temp);
                    }
                    portals.Clear();
                    goldCoins.Clear();
                    hearts.Clear();

                }
                if (p.CheckCollision(player) && player.Level == 4)
                {
                    {
                        player.Level = 5;
                        player.X = 0;
                        player.Y = 500;
                        return State.Menu2;
                        

                    }
                    
                }
            }

       


            foreach (GoldCoin gc in goldCoins.ToList())
            {
                if (gc.IsAlive)
                {
                    gc.Update(gameTime);

                    if (gc.CheckCollision(player))
                    {
                        goldCoins.Remove(gc);
                        player.Cash+=5;
                    }
                }
                else
                    goldCoins.Remove(gc);
            }
            foreach (Heart h in hearts.ToList())
            {
                if (h.IsAlive)
                {
                    h.Update(gameTime);

                    if (h.CheckCollision(player))
                    {
                        hearts.Remove(h);
                        if (rectangle.Width < 199)
                        {
                            rectangle.Width += 15;
                        }

                    }
                }
                else
                    hearts.Remove(h);
            }
            background.Update(window);
            player.Update(window, gameTime, effect);





     
            
            foreach (Enemy e in enemies.ToList())
            {
                if(e is Penguin) {
                    foreach (Penguinshot ps in (e as Penguin).penguinshots.ToList())
                    {
                        if (ps.CheckCollision(player))
                        {
                            rectangle.Width -= 40;
                            ps.IsAlive = false;

                        }
                    }
                }

             
                foreach (Bullet b in player.Bullets.ToList())
                {
                    if (e.CheckCollision(b))
                    {
                        e.Health -= b.Damage;
                        b.IsAlive = false;
                      
                    }
                }

                if (e.CheckCollision(player))
                {
                    rectangle.Width -= 1;
                    
                }


                if (e.IsAlive)
                {

                    e.Update(window, gameTime);
                }
                else
                    enemies.Remove(e);
                
                if (rectangle.Width == 0 && player.IsAlive)
                {
                    player.IsAlive = false;
                    if (!File.Exists("highScoreList.txt"))
                    {
                        File.WriteAllText("highScoreList.txt", "");
                    }

                    String yeah = "PLAYER:" + player.Points;
                    var sw = File.AppendText("highScoreList.txt");
                    sw.WriteLine(yeah);
                    sw.Close();
                  
                }
                
                if (e.Health <= 0)
                {
                    e.IsAlive = false;
                    player.Cash +=10;
                    player.Points += 25;
                   
                    
                  
                }
             
               
               
            }
            if (enemies.Count == 0)
            {
               portals.Add(new Portal(portalSprite, 980, 200));
               goldCoins.Clear();
               hearts.Clear();
               
            }
            if (!player.IsAlive)
            {
                rectangle.Width = 199;
                player.Health = 100;
                Reset(window, content);
                return State.Menu;
              
            }
            return State.Run;
        
        }

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            
            player.Draw(spriteBatch);
            foreach (Portal p in portals)
                p.Draw(spriteBatch);
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
            foreach (GoldCoin gc in goldCoins)
                gc.Draw(spriteBatch);
            foreach (Heart h in hearts)
                h.Draw(spriteBatch);
           
            printText.Print("CASH:" + player.Cash, spriteBatch, 0, 0);
            printText.Print("POINTS:" + player.Points, spriteBatch, 100, 0);
            printText.Print("LEVEL:" + player.Level, spriteBatch, 230, 0);
          
            if(lastTime == null)
                return;

            int x = (int)(Crectangle.Width * ((lastTime.TotalGameTime.TotalMilliseconds - player.timeSinceLastTP) / 4000.0));

            spriteBatch.Draw(texture, position, rectangle, Color.White);
            spriteBatch.Draw(Ctexture, Cposition, new Rectangle(Crectangle.X, Crectangle.Y, Crectangle.Width - x, Crectangle.Height), Color.White);
        
      
           
        }
        public static State Run2Update(ContentManager content, GameWindow window, GameTime gameTime)
        {
            lastTime = gameTime;
            KeyboardState keyboardState = Keyboard.GetState();
            Random random = new Random();
            int newCoin = random.Next(1, 100);
            if (newCoin == 1)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(290, window.ClientBounds.Height - goldCoinSprite.Height);
                goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }


           


            int newHeart = random.Next(1, 250);
            if (newHeart == 1)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - heartSprite.Width);
                int rndY = random.Next(290, window.ClientBounds.Height - heartSprite.Height);
                hearts.Add(new Heart(heartSprite, rndX, rndY, gameTime));
            }




            
            
            
           
          
             






                foreach (Portal p in portals.ToList())
                {

                    if (p.CheckCollision(player) && player.Level == 5)
                    {



                        player.X = 0;
                        player.Y = 500;
                        player.Level++;

                        enemies = new List<Enemy>();
                        Texture2D tmpSprite = content.Load<Texture2D>("images/enemies/Rabbit");
                        for (int i = 0; i < 2; i++)
                        {
                            int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                            int rndY = random.Next(window.ClientBounds.Height - 200, window.ClientBounds.Height) - tmpSprite.Height;
                            Rabbit temp2 = new Rabbit(tmpSprite, rndX, rndY);
                            enemies.Add(temp2);

                        }
                        tmpSprite = content.Load<Texture2D>("images/enemies/fedora/fedora_left1");
                        for (int i = 0; i < 2; i++)
                        {
                            int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                            int rndY = random.Next(window.ClientBounds.Height - 200, window.ClientBounds.Height) - tmpSprite.Height;
                            Fedora temp2 = new Fedora(tmpSprite, rndX, rndY);
                            enemies.Add(temp2);
                        }

                        tmpSprite = content.Load<Texture2D>("images/enemies/NOMNOM/Nomnom");
                        for (int i = 0; i < 1; i++)
                        {
                            int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                            int rndY = random.Next(window.ClientBounds.Height - 150, window.ClientBounds.Height - 50) - tmpSprite.Height;
                            NOMNOM temp2 = new NOMNOM(tmpSprite, rndX, rndY);
                            enemies.Add(temp2);
                        }
                        portals.Clear();
                        goldCoins.Clear();
                        hearts.Clear();
                    }
                }


                    


                




                foreach (GoldCoin gc in goldCoins.ToList())
                {
                    if (gc.IsAlive)
                    {
                        gc.Update(gameTime);

                        if (gc.CheckCollision(player))
                        {
                            goldCoins.Remove(gc);
                            player.Cash += 5;
                        }
                    }
                    else
                        goldCoins.Remove(gc);
                }
                foreach (Heart h in hearts.ToList())
                {
                    if (h.IsAlive)
                    {
                        h.Update(gameTime);

                        if (h.CheckCollision(player))
                        {
                            hearts.Remove(h);
                            if (rectangle.Width < 199)
                            {
                                rectangle.Width += 15;
                            }

                        }
                    }
                    else
                        hearts.Remove(h);
                }
                background2.Update(window);
                player.Update(window, gameTime, effect);
          
                
                foreach (Enemy e in enemies.ToList())
                {
                   
                    

                    foreach (Bullet b in player.Bullets.ToList())
                    {
                        if (e.CheckCollision(b))
                        {
                            e.Health -= b.Damage;
                            b.IsAlive = false;

                        }
                    }

                    if (e.CheckCollision(player))
                    {
                        rectangle.Width -= 1;

                    }


                    if (e.IsAlive)
                    {

                        e.Update(window, gameTime);
                    }
                    else
                        enemies.Remove(e);

                    if (rectangle.Width == 0 && player.IsAlive)
                    {
                        player.IsAlive = false;

                        if (!File.Exists("highScoreList.txt"))
                        {
                            File.WriteAllText("highScoreList.txt", "");
                        }

                        String yeah = "PLAYER:" + player.Points;
                        var sw = File.AppendText("highScoreList.txt");
                        sw.WriteLine(yeah);
                        sw.Close();
                    }

                    if (e.Health <= 0 && player.Moregold == 1)
                    {
                        e.IsAlive = false;
                        player.Cash += 10;
                        player.Points += 25;
                    }
                    if (e.Health <= 0 && player.Moregold == 2)
                    {
                        e.IsAlive = false;
                        player.Cash += 15;
                        player.Points += 25;
                    }
                    if (!player.IsAlive)
                    {
                        e.IsAlive = false;
                        player.X = 0;
                        player.Y = 500;
                    }



                }
                if (enemies.Count == 0)
                {
                    portals.Add(new Portal(portalSprite, 980, 200));
                    goldCoins.Clear();
                    hearts.Clear();

                }
                if (!player.IsAlive)
                {
                    rectangle.Width = 199;
                    player.Health = 100;
                    portals.Clear();
                    hearts.Clear();
                    goldCoins.Clear();
                    
                    
                    return State.Menu2;
                 

                }
                
            
        
            return State.Run2;
        }

        public static void Run2Draw(SpriteBatch spriteBatch)
        {
            background2.Draw(spriteBatch);
            

            player.Draw(spriteBatch);
            foreach (Portal p in portals)
                p.Draw(spriteBatch);
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
            foreach (GoldCoin gc in goldCoins)
                gc.Draw(spriteBatch);
            foreach (Heart h in hearts)
                h.Draw(spriteBatch);

            printText.Print("CASH:" + player.Cash, spriteBatch, 0, 0);
            printText.Print("POINTS:" + player.Points, spriteBatch, 100, 0);
            printText.Print("LEVEL:" + player.Level, spriteBatch, 230, 0);

            if (lastTime == null)
                return;

            int x = (int)(Crectangle.Width * ((lastTime.TotalGameTime.TotalMilliseconds - player.timeSinceLastTP) / 4000.0));

            spriteBatch.Draw(texture, position, rectangle, Color.White);
            spriteBatch.Draw(Ctexture, Cposition, new Rectangle(Crectangle.X, Crectangle.Y, Crectangle.Width - x, Crectangle.Height), Color.White);



        }

        public static State HighScoreUpdate()
        {
            KeyboardState keyboardstate = Keyboard.GetState();
            if (keyboardstate.IsKeyDown(Keys.Escape))
            {
                highScoreList = null;
                return State.Menu;
            }
            if (highScoreList == null)
            {
                highScoreList = new List<HighScore>();
                if (!File.Exists("highScoreList.txt"))
                {
                    File.WriteAllText("highScoreList.txt", "");
                }
                String[] scores = File.ReadAllLines("highScoreList.txt");
                foreach (var score in scores)
                {

                    String[] data = score.Split(':');
                    HighScore hs = new HighScore();
                    hs.PlayerName = data[0];




                    hs.Score = Int32.Parse(data[1]);
                    highScoreList.Add(hs);
                }

            }
            return State.Highscore;
 
        }
       
        public static State StoreUpdate(GameTime gameTime)
        {
          
            
            return (State)store.Update(gameTime);

        }
        public static void StoreDraw(SpriteBatch spriteBatch)
        {
            storebackground.Draw(spriteBatch);
            store.Draw(spriteBatch);
            printText.Print("CASH:" + player.Cash, spriteBatch, 350, 0);
            foreach (Purchase p in purchaze)
                p.Draw(spriteBatch);
               
        }


        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {
            if (highScoreList == null)
              
                  printText.Print("LOADING HIGHSCORELIST....." , spriteBatch, 10, 10);
            else
            {
               
             printText.Print("RECENT: " , spriteBatch, 10, 10);

             printText.Print("HIGHSCORE: ", spriteBatch, 320, 10);
                int y = 0;
                foreach (var hs in highScoreList.Reverse<HighScore>().ToList())
                {
                    spriteBatch.DrawString(
                         printText.font,
                         hs.PlayerName + ": " + hs.Score,
                         new Vector2(20, 80 + y),
                         Color.White);
                    y += 20;
                }
                y = 0;
                foreach (var hs in highScoreList.Reverse<HighScore>().OrderBy(t => -t.Score).ToList())
                {
                    spriteBatch.DrawString(
                         printText.font,
                         hs.PlayerName + ": " + hs.Score,
                         new Vector2(330, 80 + y),
                         Color.White);
                    y += 20;
                }
            }
        }

        private static void Reset (GameWindow window, ContentManager content)
        {
            player.Reset( 0, 500, 4f, 4f);
            portals.Clear();
            player.Level = 1;

            enemies.Clear();
            Random random = new Random();
            
            Texture2D tmpSprite = content.Load<Texture2D>("images/enemies/fedora/fedora_left1");
            for (int i = 0; i < 2; i++)
            {
                int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                int rndY = random.Next(window.ClientBounds.Height - 200, window.ClientBounds.Height) - tmpSprite.Height;
                Fedora temp = new Fedora(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }

            tmpSprite = content.Load<Texture2D>("images/enemies/NOMNOM/Nomnom");
            for (int i = 0; i < 2; i++)
            {
                int rndX = random.Next(window.ClientBounds.Width - 200, window.ClientBounds.Width) - tmpSprite.Width;
                int rndY = random.Next(window.ClientBounds.Height - 150, window.ClientBounds.Height - 35) - tmpSprite.Height;
                NOMNOM temp = new NOMNOM(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }

    


}
    }
}
