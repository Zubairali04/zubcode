//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class SpaceInvaders : Azul.Game
    {

        Azul.Sprite pAlien;
        Azul.Sprite pAlienWhite;
        Azul.Sprite pAlienSwap;
        Azul.Sprite pStitch;

        Azul.Sprite pRedBird;
        Azul.Sprite pWhiteBird;
        Azul.Sprite pYellowBird;
        Azul.Sprite pGreenBird;

        float redSpeed = 2.0f;
        float yellowSpeedX = 2.0f;
        float yellowSpeedY = 2.0f;
        float greenBirdSpeedX = 2.0f;
        float greenBirdSpeedY = 2.0f;
        float whiteBirdSpeed = 0.02f;
        bool yellowBird_state = true;

        Azul.SpriteBox pSpriteBox;
        Azul.SpriteBox pSpriteBox2;

        Azul.Texture pAliensTex;
        Azul.Texture pStitchTex;
        Azul.Texture pBirdsTex;

        float ScreenX = 0.0f;
        float ScreenWidth = 300.0f;
        float ScreenHeight = 100.0f;

        float blue = 0.0f;
        float red = 1.0f;

        float AlienPosX = 0.0f;
        float AlienPosY = 0.0f;
        float AlienAngle = 0.0f;

        int count = 0;

        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("SE456 SpaceInvaders");
            this.SetWidthHeight(800, 600);
            this.SetClearColor(0.4f, 0.4f, 0.8f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            // --------------------------------
            //  Texture
            // --------------------------------
            pAliensTex = new Azul.Texture("Aliens.t.azul");
            pStitchTex = new Azul.Texture("Stitch.t.azul");
            pBirdsTex = new Azul.Texture("Birds.t.azul");

            Debug.Assert(pAliensTex != null);
            Debug.Assert(pStitchTex != null);
            Debug.Assert(pBirdsTex != null);

            // --------------------------------
            //  Sprites
            // --------------------------------
            Azul.Rect pTexRect = new Azul.Rect();
            Azul.Rect pScreenRect = new Azul.Rect();
            Azul.Color pTmpColor = new Azul.Color();

            Debug.Assert(pTexRect != null);
            Debug.Assert(pScreenRect != null);
            Debug.Assert(pTmpColor != null);

            pTexRect.Set(136.0f, 64.0f, 85.0f, 63.0f);
            pScreenRect.Set(100.0f, 0.0f, 150.0f, 150.0f);
            pAlien = new Azul.Sprite(pAliensTex, pTexRect, pScreenRect);
            Debug.Assert(pAlien != null);

            AlienPosX = pAlien.x;
            AlienPosY = pAlien.y;

            pTexRect.Set(253.0f, 63.0f, 85.0f, 64.0f);
            pScreenRect.Set(500.0f, 300.0f, 150.0f, 150.0f);
            pTmpColor.Set(1.0f, 1.0f, 0.0f, 1.0f);
            pAlienWhite = new Azul.Sprite(pAliensTex, pTexRect, pScreenRect, pTmpColor);
            Debug.Assert(pAlienWhite != null);

            pTexRect.Set(253.0f, 63.0f, 85.0f, 64.0f);
            pScreenRect.Set(400.0f, 300.0f, 150.0f, 150.0f);
            pTmpColor.Set(1.0f, 1.0f, 0.0f, 1.0f);
            pAlienSwap = new Azul.Sprite(pAliensTex, pTexRect, pScreenRect, pTmpColor);
            Debug.Assert(pAlienSwap != null);

            pTexRect.Set(0.0f, 0.0f, 300.0f, 410.0f);
            pScreenRect.Set(150.0f, 500.0f, 100.0f, 100.0f);
            pStitch = new Azul.Sprite(pStitchTex, pTexRect, pScreenRect);
            Debug.Assert(pStitch != null);

            // --------------------------------
            //  SpriteBox
            // --------------------------------

            pScreenRect.Set(550.0f, 500.0f, 50.0f, 150.0f);
            pTmpColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            pSpriteBox = new Azul.SpriteBox(pScreenRect, pTmpColor);
            Debug.Assert(pSpriteBox != null);

            pScreenRect.Set(550.0f, 100.0f, 50.0f, 100.0f);
            pTmpColor.Set(1.0f, 1.0f, 0.0f);
            pSpriteBox2 = new Azul.SpriteBox(pScreenRect, pTmpColor);
            Debug.Assert(pSpriteBox2 != null);

            // --------------------------------
            //  Sprites
            // --------------------------------

            pTexRect.Set(47, 41, 48, 46);
            pScreenRect.Set(50, 500, 50, 50);
            pRedBird = new Azul.Sprite(pBirdsTex, pTexRect, pScreenRect);
            Debug.Assert(pRedBird != null);

            pTexRect.Set(124, 34, 60, 56);
            pScreenRect.Set(300, 400, 100, 100);
            pYellowBird = new Azul.Sprite(pBirdsTex, pTexRect, pScreenRect);
            Debug.Assert(pYellowBird != null);

            pTexRect.Set(139, 131, 84, 97);
            pScreenRect.Set(600, 200, 50, 50);
            pWhiteBird = new Azul.Sprite(pBirdsTex, pTexRect, pScreenRect);
            Debug.Assert(pWhiteBird != null);

            pTexRect.Set(246, 135, 99, 72);
            pScreenRect.Set(400, 200, 75, 75);
            pGreenBird = new Azul.Sprite(pBirdsTex, pTexRect, pScreenRect);
            Debug.Assert(pGreenBird != null);


        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        public override void Update()
        {

            // Demo of Time and software invoke quit
            if (this.GetTime() > 5.0f)
            {
                this.Quit();
            }

            // Keyboard test
            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_SPACE))
            {
                Console.WriteLine("key: <Space>");
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_LEFT))
            {
                Console.WriteLine("key: Left");
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_RIGHT))
            {
                Console.WriteLine("key: RIGHT");
            }

            //--------------------------------------------------------
            // Stitch - Scale
            //--------------------------------------------------------

            pStitch.sy = -1.0f;
            pStitch.Update();

            //--------------------------------------------------------
            // Swap Color
            //--------------------------------------------------------

            blue += 0.001f;
            red -= 0.002f;
            if (red <= 0.0f)
                red = 1.0f;

            Azul.Color pTmpColor = new Azul.Color(red, 0.0f, blue);
            pAlienWhite.SwapColor(pTmpColor);
            pAlienWhite.Update();

            //--------------------------------------------------------
            // Change Texture, TextureRect, Color
            //--------------------------------------------------------

            count++;
            if (count == 100)
            {
                pAlien.SwapTexture(pStitchTex);
                Azul.Rect pTextRect = new Azul.Rect(0.0f, 0.0f, 300.0f, 410.0f);
                pAlien.SwapTextureRect(pTextRect);

                Azul.Color pTmpColor2 = new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f);
                pSpriteBox.SwapColor(pTmpColor2);
            }
            else if (count == 200)
            {
                pAlien.SwapTexture(pAliensTex);
                Azul.Rect pTextRect = new Azul.Rect(136.0f, 64.0f, 85.0f, 63.0f);
                pAlien.SwapTextureRect(pTextRect);

                Azul.Color pTmpColor2 = new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f);
                pSpriteBox.SwapColor(pTmpColor2);
                count = 0;
            }

            //--------------------------------------------------------
            // SpriteBox
            //--------------------------------------------------------

            pSpriteBox.Update();
            pSpriteBox2.Update();

            //--------------------------------------------------------
            // Swap Screen Rect
            //--------------------------------------------------------

            ScreenX += 2;
            ScreenWidth -= 1;
            ScreenHeight += 1;
            if (ScreenX > this.GetScreenWidth())
            {
                ScreenX = 0.0f;
                ScreenWidth = 300.0f;
                ScreenHeight = 100.0f;
            }
            Azul.Rect pScreenRect = new Azul.Rect(ScreenX, 100, ScreenWidth, ScreenHeight);
            pAlienSwap.SwapScreenRect(pScreenRect);
            pAlienSwap.Update();

            //--------------------------------------------------------
            // Alien - Angles,position
            //--------------------------------------------------------

            AlienAngle += 0.1f;
            AlienPosX += 2.0f;
            if (AlienPosX > 800.0f)
                AlienPosX = 0.0f;
            AlienPosY += 1.0f;
            if (AlienPosY > 600.0f)
                AlienPosY = 0.0f;

            pAlien.x = AlienPosX;
            pAlien.y = AlienPosY;

            pAlien.angle = AlienAngle;

            pAlien.Update();


            //--------------------------------------------------------
            // Red Bird
            //--------------------------------------------------------
            if (pRedBird.x > this.GetScreenWidth() || pRedBird.x < 0.0f)
            {
                redSpeed *= -1.0f;
            }
            pRedBird.x += redSpeed;
            pRedBird.Update();

            //--------------------------------------------------------
            // Yellow Bird
            //--------------------------------------------------------
            bool swapTex = false;
            if (pYellowBird.x > this.GetScreenWidth() || pYellowBird.x < 0.0f)
            {
                swapTex = true;
                yellowSpeedX *= -1.0f;
            }
            if (pYellowBird.y > this.GetScreenHeight() || pYellowBird.y < 0.0f)
            {
                swapTex = true;
                yellowSpeedY *= -1.0f;
            }
            if (swapTex)
            {
                Azul.Rect pTextRect = new Azul.Rect();

                if (yellowBird_state)
                {
                    pTextRect.Set(246, 135, 99, 72);
                    yellowBird_state = false;
                }
                else
                {
                    yellowBird_state = true;
                    pTextRect.Set(124, 34, 60, 56);
                }
                pYellowBird.SwapTextureRect(pTextRect);
            }

            pYellowBird.x += yellowSpeedX;
            pYellowBird.y += yellowSpeedY;

            pYellowBird.Update();

            //--------------------------------------------------------
            // Green Bird
            //--------------------------------------------------------
            if (pGreenBird.x > this.GetScreenWidth() || pGreenBird.x < 0.0f)
            {
                greenBirdSpeedX *= -1.0f;
            }
            if (pGreenBird.y > this.GetScreenHeight() || pGreenBird.y < 0.0f)
            {
                greenBirdSpeedY *= -1.0f;
            }
            pGreenBird.x += greenBirdSpeedX;
            pGreenBird.y += greenBirdSpeedY;
            pGreenBird.angle += 0.05f;

            pGreenBird.Update();

            //--------------------------------------------------------
            // White Bird
            //--------------------------------------------------------
            if (pWhiteBird.sx > 5.0f || pWhiteBird.sy < 1.0f)
            {
                whiteBirdSpeed *= -1.0f;
            }
            pWhiteBird.sx += whiteBirdSpeed;
            pWhiteBird.sy += whiteBirdSpeed;

            pWhiteBird.Update();

        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            pAlien.Render();
            pStitch.Render();

            pSpriteBox.Render();
            pSpriteBox2.Render();

            pAlienWhite.Render();
            pAlienSwap.Render();

            pGreenBird.Render();
            pRedBird.Render();
            pWhiteBird.Render();
            pYellowBird.Render();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {

        }


        public override void DisplayHeader()
        {
            Console.Write(this.Header());
        }

        public override void DisplayFooter()
        {
            Console.Write(this.Footer());
        }

    }
}

// --- End of File ---
