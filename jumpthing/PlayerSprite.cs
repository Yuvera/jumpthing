using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace jumpthing
{
    class PlayerSprite : Sprite
    {
        bool jumping, walking, falling, jumpIsPressed;
        float jumpSpeed = 150f;
        float walkSpeed = 100f;

        public PlayerSprite(Texture2D newSpriteSheet, Texture2D newCollisionTxr, Vector2 newLocation) 
            : base(newSpriteSheet, newCollisionTxr, newLocation)
        {
            spriteOrigin = new Vector2(0.5f, 1f);
            isColliding = true;
            //drawCollision = true;

            frameTime = 0.1f;
            animations = new List<List<Rectangle>>();

            //idle
            animations.Add(new List<Rectangle>());
            animations[0].Add(new Rectangle(0, 0, 48, 48));
            animations[0].Add(new Rectangle(48, 0, 48, 48));

            //walking
            animations.Add(new List<Rectangle>());
            animations[1].Add(new Rectangle(48, 0, 48, 48));
            animations[1].Add(new Rectangle(96, 0, 48, 48));
            animations[1].Add(new Rectangle(48, 0, 48, 48));
            animations[1].Add(new Rectangle(144, 0, 48, 48));

            //jumping from standing still
            animations.Add(new List<Rectangle>());
            animations[2].Add(new Rectangle(96, 0, 48, 48));

            //falling
            animations.Add(new List<Rectangle>());
            animations[3].Add(new Rectangle(0, 48, 48, 48));

            jumping = false;
            walking = false;
            falling = true;
            jumpIsPressed = false;
        }

        public override void Update(GameTime gameTime)
        {
            if ((falling || jumping) && spriteVelocity.Y < 500f) spriteVelocity.Y += 5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            spritePos += spriteVelocity;
        }

        public void ResetPlayer(Vector2 newPos)
        {
            spritePos = newPos;
            spriteVelocity = new Vector2(0, 0);
            jumping = false;
            walking = false;
            falling = true;
            
        }
    }
}
