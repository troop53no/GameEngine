/*
*   Half Life 3 Confirmed
*   Created by David Graham from 2013-2017
*   Music by Joseph Gibson
*
*   Contact troop53no@gmail.com with subject
*   "Half Life 3 Confirmed: HELP" for any 
*   information or help related to the code
*/

/*TODO:
*   Add Bombs
*       -requires mouse inputs
*       -look back at JAVA code for most of this step)
*   Finish the Sound Effect work
*       -make sure all instances of sound stuff is working correctly
*       -stop sounds while paused??
*   Add Asteroids
*       -collisions will reset player, just like alien/boss collisions
*       -take multiple hits or bombs to destroy
*       -place behind or in front of powerups/aliens???
*       -place powerups inside of them? rather than just collecting powerups normally?
*       -what size should they be?
*       -how fast should they be? slower/faster than aliens?
*       -what will they look like?
*   Make the Pause Screen better
*       -options/settings
*           -volume levels for music/sound effects
*           -quit game
*           -controls
*           -anything else????
*   Add a better Start Screen
*       -what should it include?
*           -control remapping?
*               -actual remapping? or just presets? (WASD, arrow keys, etc)
*           -Start button
*           -Quit Button
*           -Settings/Options (same as Pause Screen, or anything else???)
*           -Credits (replace the current credits screen maybe???)
*       -move instruction screen within this one probably???
*
*/



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections;
using System;
using System.Threading;
using System.Reflection;

namespace GameEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Monogame stuff
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Font stuff
        private SpriteFont font;//Arial font 14
        private SpriteFont font2;//Arial font 12
        private Vector2 instPosition;//position of the instruction screen font

        //Constants
        private const int bwidth = 1000;//width of the screen
        private const int bheight = 675;//height of the screen

        //Variables
        //TODO remove count
        private int count;//counter for timing things
        private int shotsHit;//shots that hit aliens
        private int shotsFired;//shots that were fired
        private int power;//power level of the player
        private int score;//score of the player
        private int level;//current level
        private int aliensKilled;//number of aliens the player killed
        private int bossPower;//power level of the boss
        private int bombCount;//number of bombs the player has
        private int high1;//highest score
        private int high2;//second highest score
        private int high3;//third highest score
        private Random rand;//random event variable
        private bool isPaused;//pauses the game
        private int activeItem;//current menu item


        //images
        private Texture2D whalepic;//picture of our Happy Whale Studios Logo
        private Texture2D logopic;//picture of the Half-Life 3 Logo
        private Texture2D craftpic;//picture of the player's ship
        private Texture2D alienpic;//picture of an alien ship
        private Texture2D missilepic;//picture of a missile
        private Texture2D powerpic;//picture of a powerup
        private Texture2D bosspic;//picture of the boss
        private Texture2D bossmissilepic;//picture of a boss missile
        private Texture2D starpic;//picture of a star
        
        //Objects
        private Craft craft;//the player's craft
        private ArrayList aliens;//array of enemy aliens
        private ArrayList boss;//array of enemy bosses
        private ArrayList stars;//array of stars in the background
        private ArrayList bombs;//array of bomb detonations
        private ArrayList powerup;//array of collectable powerups
        private ArrayList buttons;//array of menu buttons
        
        //Game state
        enum GameState
        {
            whaleScreen,//screen state with our logo
            instScreen,//screen state with instructions
            startScreen,//screen state for the start screen
            gameScreen,//screen state for the gameplay
            creditScreen,//screen state for the credits
        }
        GameState gameState;//current state of the game

        //Button Stuff
        //private Texture2D startButton;
        //private Texture2D exitButton;
        //private Texture2D pauseButton;
        //private Texture2D resumeButton;
        //private Texture2D loadingScreen;
        //private Vector2 startButtonPosition;
        //private Vector2 exitButtonPosition;
        //private Vector2 resumeButtonPosition;

        //Loading Stuff
        //private Thread backgroundThread;
        //private bool isLoading = false;

        //Input Stuff
        GamePadState gamePadState;//current state of the controller inputs
        //MouseState mouseState;//current state of the mouse inputs
        //MouseState previousMouseState;//previous state of the mouse inputs

        //Sounds
        private SoundEffect bomb_s;//sound effect for bomb detonation
        private SoundEffect boss_s;//ound effect for
        private SoundEffect bosshit_s;//sound effect for
        private SoundEffect hit_s;//sound effect for 
        private SoundEffect missile_s;//sound effect for firing a missile
        private SoundEffect powerup_s;//sound effect for acquiring a powerup
        private SoundEffect theme_s;//sound effect for the first theme
        private SoundEffect theme2_s;//sound effect for the second theme
        private SoundEffect theme3_s;//sound effect for the third theme
        private SoundEffect title_s;//sound effect for the first title theme
        private SoundEffect title2_s;//sound effect for the second title theme
        private SoundEffect whale_s;//sound effect for the Happy Whale screen
        //freesound.org/people/LittleRobotSoundFactory/sounds/270521/
        private SoundEffectInstance current_theme;//instance of the current background music
        
        public Game1()
        {
            //monogame stuff
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";//sets the name for the folder containing the assemblies and content

            //sets the size of the screen
            graphics.IsFullScreen = false;//prevents the game from starting in fullscreen mode
            graphics.PreferredBackBufferWidth = 1000;//the width of the game window
            graphics.PreferredBackBufferHeight = 675;//the height of the game window

            //set screen states
            gameState = GameState.whaleScreen;//starts the game at the Happy Whale screen
            isPaused = false;//starts the game in a non-paused state

            //initialize the ship
            craft = new Craft(power, 40, 340);

            //initialize arraylists
            aliens = new ArrayList();//creates a blank array of enemy aliens
            boss = new ArrayList();//creates a blank array of bosses
            bombs = new ArrayList();//creates a blank array of bomb detonations
            powerup = new ArrayList();//creates a blank array of powerups
            buttons = new ArrayList();//creates a blank array of buttons

            //create star array
            rand = new Random();//initializes the randomizer
            stars = new ArrayList();//creates an empty array of stars
            for (int i = 1000; i > 0; --i)//creates 1000 stars 
            {
                int sx = rand.Next(0, bwidth);//random x-coord for star
                int sy = rand.Next(0, bheight);//random y-coord for star
                Color sc = new Color((byte)(rand.Next(0,255)), (byte)(rand.Next(0, 255)), (byte)(rand.Next(0, 255)));//random color for the star
                stars.Add(new Star(sx, sy, sc, rand.Next(15,20)));//creates star with a random speed
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;//allows the mouse to be seen

            //startButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 200);//position of the start button
            //exitButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 250);//position of the exit button

            //gameState = GameState.whaleScreen;
            // TODO: how do I implement a loading screen before the Happy Whale screen??
            //isLoading = true;//the game initializes in a loading state
            
            base.Initialize();//continue with the regular Initialize() functions
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //load font files
            font = Content.Load<SpriteFont>("font");
            font2 = Content.Load<SpriteFont>("font2");
            //TODO: move the instPosition line to a different locations (possibly to Initialize?)
            instPosition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);//sets the location of the instructions on the screen


            //load image files
            whalepic = Content.Load<Texture2D>("HWSLogo");
            logopic = Content.Load<Texture2D>("3");
            craftpic = Content.Load<Texture2D>("craft");
            alienpic = Content.Load<Texture2D>("alien");
            missilepic = Content.Load<Texture2D>("missile");
            powerpic = Content.Load<Texture2D>("powerup");
            bosspic = Content.Load<Texture2D>("boss");
            bossmissilepic = Content.Load<Texture2D>("bossmissile");
            starpic = Content.Load<Texture2D>("star");

            //load sound files
            bomb_s = Content.Load<SoundEffect>("bomb_s");
            boss_s = Content.Load<SoundEffect>("boss_s");
            bosshit_s = Content.Load<SoundEffect>("bosshit_s");
            hit_s = Content.Load<SoundEffect>("hit_s");
            missile_s = Content.Load<SoundEffect>("missile_s");
            powerup_s = Content.Load<SoundEffect>("powerup_s");
            theme_s = Content.Load<SoundEffect>("theme_s");
            theme2_s = Content.Load<SoundEffect>("theme2_s");
            theme3_s = Content.Load<SoundEffect>("theme3_s");
            title_s = Content.Load<SoundEffect>("title_s");
            title2_s = Content.Load<SoundEffect>("title2_s");
            whale_s = Content.Load<SoundEffect>("whale_s");
        
    }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // TODO: get rid of unused images and sounds once they're no longer needed
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();//pressing escape at any time exits the game

            //gets the state of the XBox controller
            gamePadState = GamePad.GetState(PlayerIndex.One);

            // TODO: Add your update logic here
            if (gameState == GameState.whaleScreen)
            {
                if (count == 0)
                {
                    current_theme = title2_s.CreateInstance();
                    current_theme.IsLooped = true;
                    whale_s.Play();
                }
                //timer for the whale logo screen
                if (count <= -200)
                {
                    gameState = GameState.instScreen;
                    count = 0;
                    current_theme.Play();
                }
                else
                {
                    count--;
                }

            }
            else if (gameState == GameState.instScreen)
            {
                //move the stars
                for (int ii = 0; ii < stars.Count; ++ii)
                {
                    (stars[ii] as Star).move();//moves the star
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || gamePadState.Buttons.Start == ButtonState.Pressed)
                {
                    gameState = GameState.startScreen;
                    activeItem = 0;
                    count = -10;
                    buttons.Add(new Button(bwidth / 2 - 75, bheight / 2 + 30, "START"));
                    buttons.Add(new Button(bwidth / 2 - 75, bheight / 2 + 90, "OPTIONS"));
                    buttons.Add(new Button(bwidth / 2 - 75, bheight / 2 + 150, "CREDITS"));
                }
            }
            //if the game is on the start screen
            else if (gameState == GameState.startScreen)
            {
                //move the stars
                for (int ii = 0; ii < stars.Count; ++ii)
                {
                    (stars[ii] as Star).move();//moves the star
                }

                if (count == 0)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.S) || gamePadState.DPad.Down == ButtonState.Pressed)
                    {
                        activeItem++;
                        if (activeItem > buttons.Count - 1)
                        {
                            activeItem = 0;
                        }
                        count = -10;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.W) || gamePadState.DPad.Up == ButtonState.Pressed)
                    {
                        activeItem--;
                        if (activeItem < 0)
                        {
                            activeItem = buttons.Count - 1;
                        }
                        count = -10;
                    }
                }
                else
                {
                    count++;
                }

                //initializes the level
                if (count == 0 && activeItem == 0 && (Keyboard.GetState().IsKeyDown(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed))
                {
                    score = 0;//inital score is set (default = 0)
                    level = 1;//initial level is set (default = 1)
                    power = 3;//initial player power level is set (default = 3)
                    bossPower = 0;//initial boss power level is set (default = 0?????or 1?????)
                    shotsFired = 0;//initial number of shots fired is set (default = 0)
                    shotsHit = 0;//initial numbers of shots hit is set (default = 0)
                    aliensKilled = 0;//initial number of aliens killed (default = 0)
                    initAliens();//initialize the alien ships
                    craft = new Craft(power, 40, 340);//initialize the player's ship
                    gameState = GameState.gameScreen;//transition to the playable game

                    current_theme.Stop();
                    current_theme = theme3_s.CreateInstance();
                    current_theme.IsLooped = true;
                    current_theme.Play();
                    Thread.Sleep(150);
                }

                //go to credit screen
                if (count == 0 && activeItem == 2 && (Keyboard.GetState().IsKeyDown(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed))
                {
                    gameState = GameState.creditScreen;
                }
            }
            else if (gameState == GameState.gameScreen)
            {
                if (!isPaused)
                {
                    if ((aliens.Count < 1 && boss.Count < 1) && power > 0)
                    {//starts the next level
                        level++;//game level increases
                        if (power > 5)
                        {//sets a power cap
                            power = 5;
                        }
                        initAliens();//creates a new wave of aliens
                        int shipx = craft.getX();
                        int shipy = craft.getY();
                        craft = new Craft(power, shipx, shipy);//replaces old ship, this one has a greater power
                    }

                    ArrayList missilelist = craft.getMissiles();//creates missile array

                    for (int i = 0; i < missilelist.Count; ++i)
                    {//if the missile exists, it moves it, otherwise gets rid of it
                        Missile m = (missilelist[i] as Missile);
                        if (m.isVisible())
                        {
                            m.move();
                        }
                        else {
                            missilelist.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < powerup.Count; ++i)
                    {
                        Power p = (powerup[i] as Power);
                        if (p.isVisible())
                        {
                            p.move();
                        }
                        else {
                            powerup.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < boss.Count; ++i)
                    {
                        ArrayList bossmissilelist = (boss[i] as Boss).getMissiles();
                        for (int k = 0; k < bossmissilelist.Count; ++k)
                        {//if the missile exists, it moves it, otherwise gets rid of it
                            BossMissile m = (BossMissile)bossmissilelist[k];
                            if (m.isVisible())
                            {
                                m.move();
                            }
                            else {
                                bossmissilelist.RemoveAt(k);
                            }
                        }
                    }
                    for (int i = 0; i < aliens.Count; ++i)
                    {//if the alien exists, it moves it, otherwise gets rid of it
                        Alien a = (aliens[i] as Alien);
                        if (a.isVisible())
                        {
                            a.move();
                        }
                        else {
                            a = null;
                            aliens.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < boss.Count; ++i)
                    {
                        Boss b = (boss[i] as Boss);
                        if (b.isVisible())
                        {
                            b.act();
                        }
                        else {
                            boss.RemoveAt(i);
                        }
                    }
                    if (craft.getFire())
                    {
                        missile_s.Play();
                        shotsFired += power;
                    }
                    for (int i = 0; i < boss.Count; ++i)
                    {
                        Boss b = (boss[i] as Boss);
                        if (b.getFire())
                        {
                            missile_s.Play();
                        }
                    }

                    //move the stars
                    for (int ii = 0; ii < stars.Count; ++ii)
                    {
                        (stars[ii] as Star).move();//moves the star
                    }

                    //send the keyboard/gamepad state to the ship
                    if (!gamePadState.IsConnected)
                    {
                        craft.keyEvent(Keyboard.GetState());
                    }
                    else
                    {
                        craft.padEvent(gamePadState);
                    }

                    //move the ship
                    craft.move();
                    checkCollisions();//sees if objects hit each other
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) || gamePadState.Buttons.Start == ButtonState.Pressed)
                    {
                        isPaused = true;
                        Thread.Sleep(150);
                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) || gamePadState.Buttons.Start == ButtonState.Pressed)
                    {
                        isPaused = false;
                        Thread.Sleep(150);
                    }
                }
            }
            else if (gameState == GameState.creditScreen)
            {
                //move the stars
                for (int ii = 0; ii < stars.Count; ++ii)
                {
                    (stars[ii] as Star).move();//moves the star
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || gamePadState.Buttons.Start == ButtonState.Pressed)
                {
                    gameState = GameState.startScreen;
                    activeItem = 0;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (gameState == GameState.whaleScreen)//displays the Happy Whale Studios Logo
            {
                GraphicsDevice.Clear(Color.White);

                // TODO: Add your drawing code here
                spriteBatch.Begin();
                spriteBatch.Draw(whalepic, new Rectangle((bwidth / 2) - 420, (bheight / 2) - 309, 839, 618), Color.White);
                spriteBatch.End();
                
            }
            else if (gameState == GameState.instScreen)//displays the instructions for the game
            {
                GraphicsDevice.Clear(Color.Black);

                //strings for the intro screen
                //TODO make into one string. It'll look messy, but it doesn't get changed often, so that's ok
                string q = "Welcome to Half Life 3 Confirmed,\n";
                string w = "a game by Happy Whale Studios.\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
                string e = "Your goal is to defeat the enemy\n";
                string r = "fighters before they defeat you.\n";
                string t = "With each alien you defeat, you\n";
                string y = "will gain points based on your speed.\n";
                string u = "Along the way, you will gain power,\n";
                string i = "as well as fight menacing bosses.\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
                string o = "    Controls:\n";
                string p = "        W = Up\n";
                string a = "        S = Down\n";
                string s = "        A = Left\n";
                string d = "        D = Right\n";
                string f = "        Space = Fire Missile\n";
                string g = "        Left Click = Detonate Bomb\n";
                string h = "        Esc = Quit Game\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
                string j = "Good Luck!       Press Enter or Start to Continue...";

                // TODO: Add your drawing code here
                spriteBatch.Begin();
                //draws the stars
                for (int ii = 0; ii < stars.Count; ++ii)
                {
                    spriteBatch.Draw(starpic, new Rectangle((stars[ii] as Star).getX(), (stars[ii] as Star).getY(), 2, 2), (stars[ii] as Star).getColor());
                }
                
                //draws the instructions
                Vector2 fontOrigin = font.MeasureString(q + w + e + r + t + y + u + i + o + p + a + s + d + f + g + h + j) / 2;
                spriteBatch.DrawString(font, q + w + e + r + t + y + u + i + o + p + a + s + d + f + g + h + j, instPosition, Color.White, 0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.End();
            }
            else if (gameState == GameState.startScreen)
            {
                GraphicsDevice.Clear(Color.Black);


                //strings for the title screen
                string start1 = "Half Life 3 Confirmed";
                //string start2 = "To Start a New Game";
                //string start3 = "Press P or A";

                
                // TODO: Add your drawing code here
                spriteBatch.Begin();
                //draw the HL3C logo
                spriteBatch.Draw(logopic, new Rectangle(390, 150, 220, 200), Color.White);

                //draws the stars
                for (int ii = 0; ii < stars.Count; ++ii)
                {
                    spriteBatch.Draw(starpic, new Rectangle((stars[ii] as Star).getX(), (stars[ii] as Star).getY(), 2, 2), (stars[ii] as Star).getColor());
                }

                //draw the text
                spriteBatch.DrawString(font, start1, new Vector2(bwidth / 2, bheight / 2 + 15), Color.White, 0, (font.MeasureString(start1) / 2), 1.0f, SpriteEffects.None, 0.5f);
                //spriteBatch.DrawString(font, start2, new Vector2(bwidth / 2, bheight / 2 +75), Color.White, 0, (font.MeasureString(start2) / 2), 1.0f, SpriteEffects.None, 0.5f);
                //spriteBatch.DrawString(font, start3, new Vector2(bwidth / 2, bheight / 2 + 100), Color.White, 0, (font.MeasureString(start3) / 2), 1.0f, SpriteEffects.None, 0.5f);
                
                //draw the buttons
                for(int ii = 0; ii < buttons.Count; ii++)
                {
                    Color butt_color;
                    if(ii == activeItem)
                    {
                        butt_color = Color.Orange;
                    }
                    else
                    {
                        butt_color = Color.White;
                    }
                    spriteBatch.Draw(starpic, new Rectangle((buttons[ii] as Button).getX(), (buttons[ii] as Button).getY(), 150, 50), butt_color);
                    spriteBatch.Draw(starpic, new Rectangle((buttons[ii] as Button).getX()+5, (buttons[ii] as Button).getY()+5, 140, 40), Color.Black);
                    spriteBatch.DrawString(font, (buttons[ii] as Button).getName(), new Vector2(bwidth / 2, (buttons[ii] as Button).getY() + 25), butt_color, 0, (font.MeasureString((buttons[ii] as Button).getName()) / 2), 1.0f, SpriteEffects.None, 0.5f);
                }

                spriteBatch.End();
            }
            else if (gameState == GameState.gameScreen)
            {
                GraphicsDevice.Clear(Color.Black);

                // TODO: Add your drawing code here
                spriteBatch.Begin();
                //draws the ship if it's visible
                if (craft.isVisible())
                {
                    spriteBatch.Draw(craftpic, craft.getBounds(), Color.White);
                }

                //draws the missiles
                ArrayList missiles = craft.getMissiles();
                for(int ii = 0; ii < missiles.Count; ++ii)
                {
                    spriteBatch.Draw(missilepic, (missiles[ii] as Missile).getBounds(), Color.White);
                }

                //draws the powerups
                for(int ii = 0; ii < powerup.Count; ++ii)
                {
                    Power p = (powerup[ii] as Power);
                    spriteBatch.Draw(powerpic, p.getBounds(), Color.White);
                }

                //draws the aliens
                for (int ii = 0; ii < aliens.Count; ++ii)
                {
                    Alien a = (aliens[ii] as Alien);
                    if (a.isVisible())
                    {
                        spriteBatch.Draw(alienpic, a.getBounds(), Color.White);
                    }
                }

                //draws the bosses
                for (int ii = 0; ii < boss.Count; ++ii)
                {
                    Boss b = (boss[ii] as Boss);
                    if (b.isVisible())
                    {
                        spriteBatch.Draw(bosspic, b.getBounds(), Color.White);
                        spriteBatch.DrawString(font2, "Boss HP: ", new Vector2(800, 15), Color.White, 0, (font.MeasureString("Boss HP: ") / 2), 1.0f, SpriteEffects.None, 0.5f);
                        spriteBatch.Draw(starpic, new Rectangle(855, 5, (int)((((double)b.getHealth())/((double)b.getMaxHealth()))*100.0), 15), Color.White);
                    }
                    ArrayList bossmissilelist = (boss[ii] as Boss).getMissiles();
                    for (int k = 0; k < bossmissilelist.Count; ++k)
                    {//draws missiles
                        BossMissile m = (bossmissilelist[k] as BossMissile);
                        spriteBatch.Draw(bossmissilepic, m.getBounds(), Color.White);
                    }
                }

                //draws the stars
                for (int ii = 0; ii < stars.Count; ++ii)
                {
                    spriteBatch.Draw(starpic, new Rectangle((stars[ii] as Star).getX(), (stars[ii] as Star).getY(), 2, 2), (stars[ii] as Star).getColor());
                }

                //prints game info on the screen
                spriteBatch.DrawString(font2, "Level: " + level, new Vector2(5, 15), Color.White);//prints the current level
                spriteBatch.DrawString(font2, "Score: " + score, new Vector2(5, 30), Color.White);//prints your score
                if (shotsFired != 0)
                {
                    spriteBatch.DrawString(font2, "Accuracy: " + (shotsHit * 100 / shotsFired) + "%", new Vector2(5, 45), Color.White);//prints your accuracy once you have fired at least one shot
                }
                else {
                    spriteBatch.DrawString(font2, "Accuracy: 0%", new Vector2(5, 45), Color.White);//prints your accuracy if no shots have been fired
                }
                spriteBatch.DrawString(font2, "Bombs: " + bombCount, new Vector2(5, 60), Color.White);//prints your bomb amount
                spriteBatch.DrawString(font2, "Aliens left: " + aliens.Count, new Vector2(5, 75), Color.White);//prints how many aliens are remaining in the current level
                spriteBatch.End();
            }
            else if (gameState == GameState.creditScreen)
            {
                GraphicsDevice.Clear(Color.Black);

                // TODO: Add your drawing code here
                spriteBatch.Begin();
                //draws the stars
                for (int ii = 0; ii < stars.Count; ++ii)
                {
                    spriteBatch.Draw(starpic, new Rectangle((stars[ii] as Star).getX(), (stars[ii] as Star).getY(), 2, 2), (stars[ii] as Star).getColor());
                }
                
                //draws the various messages/credits
                string gameovermsg = "Game Over";
                string scoremsg = "Score: " + score;
                string accuracymsg = "";
                string mecredit = "Programming and Game Design provided by David Graham";
                string joecredit = "Musical Talent provided by Joe Gibson.  Thanks Joe!";
                string returnto = "To return to the start screen, press Enter or Start";
                string levelmsg = "Final Level: " + level;
                string highmsg = "High Scores:";
                string high1msg = "** 1- " + high1 + " ** 2- " + high2 + " ** 3- " + high3 + " **";
                string aliensmsg = "Total Aliens Killed: " + aliensKilled;
                if (shotsFired != 0)
                {
                    accuracymsg = "Accuracy: " + (shotsHit * 100 / shotsFired) + "%";
                }
                else {
                    accuracymsg = "Accuracy: 0%";
                }
                spriteBatch.DrawString(font, gameovermsg, new Vector2(bwidth / 2, bheight / 2 - 150), Color.White, 0, (font.MeasureString(gameovermsg) / 2), 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, accuracymsg, new Vector2(bwidth / 2, bheight / 2 - 120), Color.White, 0, (font.MeasureString(accuracymsg) / 2), 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, levelmsg, new Vector2(bwidth / 2, bheight / 2 - 90), Color.White, 0, (font.MeasureString(levelmsg) / 2), 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, aliensmsg, new Vector2(bwidth / 2, bheight / 2 - 60), Color.White, 0, (font.MeasureString(aliensmsg) / 2), 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, scoremsg, new Vector2(bwidth / 2, bheight / 2 - 30), Color.White, 0, (font.MeasureString(scoremsg) / 2), 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, highmsg, new Vector2(bwidth / 2, bheight / 2 + 30), Color.White, 0, (font.MeasureString(highmsg) / 2), 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, high1msg, new Vector2(bwidth / 2, bheight / 2 + 60), Color.White, 0, (font.MeasureString(high1msg) / 2), 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, mecredit, new Vector2(bwidth / 2, bheight / 2 + 150), Color.White, 0, (font.MeasureString(mecredit) / 2), 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, joecredit, new Vector2(bwidth / 2, bheight / 2 + 180), Color.White, 0, (font.MeasureString(joecredit) / 2), 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, returnto, new Vector2(bwidth / 2, bheight / 2 + 240), Color.White, 0, (font.MeasureString(returnto) / 2), 1.0f, SpriteEffects.None, 0.5f);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~EXPERIMENTAL~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public void LoadGame()
        {

        }

        //creates the aliens, boss, and powerup
        public void initAliens()
        {
            if (level % 10 == 0)
            {
                current_theme.Stop();
                current_theme = boss_s.CreateInstance();
                current_theme.IsLooped = true;
                current_theme.Play();
                bossPower++;//increments the boss's power
                boss = new ArrayList();//resets the boss array
                boss.Add(new Boss(bossPower));//creates a boss with 'bosspow' power
            }
            else {
                aliens = new ArrayList();//resets the alien array
                for (int i = 0; i < 60 * level; ++i)
                {//the "60" represents how many more aliens are added each level.  I suggest no more than 1000, otherwise it lags to death
                    aliens.Add(new Alien(rand.Next(1000,2000), rand.Next(5, bheight-105)));//adds aliens with random positions off to the right of the screen
                }
            }
            powerup = new ArrayList();
            powerup.Add(new Power(rand.Next(1000,2000), rand.Next(5,bheight-105)));
        }

        //accuracy bonus system
        public void pointBonus()
        {
            double nscore = 0;
            int accuracy = 0;
            if (shotsFired != 0)
            {
                accuracy = shotsHit * 100 / shotsFired;
            }
            else {
                accuracy = 0;
            }
            if (accuracy >= 0)
            {
                if (accuracy >= 25)
                {
                    if (accuracy >= 50)
                    {
                        if (accuracy >= 75)
                        {
                            if (accuracy >= 100)
                            {
                                if (accuracy >= 150)
                                {
                                    throw new NotImplementedException("Error with Accuracy Measurement");
                                }else {
                                    nscore = score * 2.5;
                                }
                            }else {
                                nscore = score * 2;
                            }
                        }else {
                            nscore = score * 1.75;
                        }
                    }else {
                        nscore = score * 1.5;
                    }
                }else {
                    nscore = score * 1.25;
                }
            }else {
                nscore = score;
            }
            score = (int) nscore;
            //TODO: improve these calculations (swap the order and go one thing at a time maybe)
            if (score >= high1)
            {
                high3 = high2;
                high2 = high1;
                high1 = score;
            }
            else if (score >= high2 && score < high1)
            {
                high3 = high2;
                high2 = score;
            }
            else if (score >= high3 && score < high2 && score < high1)
            {
                high3 = score;
            }
        }

        public void checkCollisions()
        {//sees if objects hit each other
            if (gameState == GameState.gameScreen)
            {
                Rectangle r3 = craft.getBounds();//gets the space your ship takes up
                for (int i = 0; i < bombs.Count; ++i)
                {//checks bomb collisions
                    Bomb r = (bombs[i] as Bomb);
                    Rectangle r1 = r.getBounds();
                    for (int j = 0; j < aliens.Count; ++j)
                    {//checks alien and bomb collisions
                        Alien a = (aliens[j] as Alien);
                        Rectangle r2 = a.getBounds();
                        if (r1.Intersects(r2))
                        {
                            aliensKilled++;
                            a.setVisible(false);
                            score += a.getPoints();
                        }
                    }
                    for (int j = 0; j < boss.Count; ++j)
                    {//checks boss and bomb collisions
                        Boss b = (boss[j] as Boss);
                        Rectangle r2 = b.getBounds();
                        if (r1.Intersects(r2))
                        {
                            for (int z = 30; z > 0; --z)
                            {
                                b.hit();
                            }
                            if (b.getHealth() < 0)
                            {
                                b.setVisible(false);
                                score += b.getPoints();

                                current_theme.Stop();
                                current_theme = theme3_s.CreateInstance();
                                current_theme.IsLooped = true;
                                current_theme.Play();
                            }
                        }
                    }
                    bombs.RemoveAt(i);
                }
                for (int i = 0; i < powerup.Count; ++i)
                {//checks powerup collisions
                    Power p = (powerup[i] as Power);
                    Rectangle r2 = p.getBounds();
                    if (r3.Intersects(r2))
                    {
                        powerup_s.Play();
                        ++power;
                        bombCount += 2;
                        if (power > 5)
                        {
                            power = 5;
                        }
                        int shipx = craft.getX();
                        int shipy = craft.getY();
                        craft = new Craft(power, shipx, shipy);
                        r3 = craft.getBounds();//gets the space your ship takes up
                        powerup.RemoveAt(i);
                    }
                }
                for (int j = 0; j < aliens.Count; ++j)
                {//checks alien collisions
                    Alien a = (aliens[j] as Alien);
                    Rectangle r2 = a.getBounds();//space alien takes up

                    if (r3.Intersects(r2))
                    {//if your ship and an alien ship collide...
                        --power;
                        hit_s.Play();
                        if (power < 1)
                        {//...and your power is gone, GAME OVER
                            pointBonus();
                            craft.setVisible(false);
                            a.setVisible(false);
                            gameState = GameState.creditScreen;

                            current_theme.Stop();
                            current_theme = title2_s.CreateInstance();
                            current_theme.IsLooped = true;
                            current_theme.Play();
                        }
                        else {//...and you still have power, resets your ship and the aliens' positions
                            int shipx = craft.getX();
                            int shipy = craft.getY();
                            craft = new Craft(power, shipx, shipy);
                            for (int i = 0; i < aliens.Count; ++i)
                            {
                                (aliens[i] as Alien).reset();
                            }
                        }
                    }
                }
                for (int j = 0; j < boss.Count; ++j)
                {//checks boss collisions
                    Boss b = (boss[j] as Boss);
                    Rectangle r2 = b.getBounds();
                    if (r3.Intersects(r2))
                    {//if your ship and a boss ship collide...
                        --power;
                        hit_s.Play();
                        bosshit_s.Play();
                        if (power < 1)
                        {//...and your power is gone, GAME OVER
                            craft.setVisible(false);
                            b.setVisible(false);
                            gameState = GameState.creditScreen;

                            current_theme.Stop();
                            current_theme = title2_s.CreateInstance();
                            current_theme.IsLooped = true;
                            current_theme.Play();
                        }
                        else {//...and you still have power, resets your ship and the aliens' positions
                            int shipx = craft.getX();
                            int shipy = craft.getY();
                            craft = new Craft(power, shipx, shipy);
                            for (int i = 0; i < boss.Count; ++i)
                            {
                                (boss[i] as Boss).reset();
                            }
                        }
                    }
                    ArrayList bossmissilelist = b.getMissiles();
                    for (int k = 0; k < bossmissilelist.Count; ++k)
                    {//checks boss missile collisions
                        BossMissile m = (bossmissilelist[k] as BossMissile);
                        Rectangle r1 = m.getBounds();
                        if (r1.Intersects(r3))
                        {
                            b.reset();
                            --power;
                            hit_s.Play();
                            if (power < 1)
                            {//...and your power is gone, GAME OVER
                                craft.setVisible(false);
                                b.setVisible(false);
                                gameState = GameState.creditScreen;

                                current_theme.Stop();
                                current_theme = title2_s.CreateInstance();
                                current_theme.IsLooped = true;
                                current_theme.Play();
                            }
                            else {//...and you still have power, resets your ship and the aliens' positions
                                int shipx = craft.getX();
                                int shipy = craft.getY();
                                craft = new Craft(power, shipx, shipy);
                                for (int i = 0; i < aliens.Count; ++i)
                                {
                                    (aliens[i] as Alien).reset();
                                }
                            }
                        }
                    }
                }

                ArrayList missilelist = craft.getMissiles();//creates missile list

                for (int i = 0; i < missilelist.Count; ++i)
                {//checks missile collisions
                    Missile m = (missilelist[i] as Missile);

                    Rectangle r1 = m.getBounds();//space missiles take up

                    for (int j = 0; j < aliens.Count; ++j)
                    {//if an alien and missile collide, it removes both and gives you points
                        Alien a = (aliens[j] as Alien);
                        Rectangle r2 = a.getBounds();

                        if (r1.Intersects(r2))
                        {
                            hit_s.Play();
                            aliensKilled++;
                            shotsHit++;
                            m.setVisible(false);
                            a.setVisible(false);
                            score += a.getPoints();
                        }
                    }
                    for (int j = 0; j < boss.Count; ++j)
                    {//if an alien and missile collide, it removes both and gives you points
                        Boss b = (boss[j] as Boss);
                        Rectangle r2 = b.getBounds();
                        if (r1.Intersects(r2))
                        {
                            shotsHit++;
                            bosshit_s.Play();
                            m.setVisible(false);
                            b.hit();
                            if (b.getHealth() < 0)
                            {
                                b.setVisible(false);
                                score += b.getPoints();

                                current_theme.Stop();
                                current_theme = theme3_s.CreateInstance();
                                current_theme.IsLooped = true;
                                current_theme.Play();
                            }
                        }
                    }
                }
            }
        }
    }
}