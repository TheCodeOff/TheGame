using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace TheGame.Environment
{
    public class AudioController : DrawableGameComponent
    {
        private LeGame game;
        private Texture2D ControlTexture;
        private Texture2D MuteTexture;
        private Rectangle ControlDrawLocation;
        private Rectangle MuteDrawLocation;
        private Color InactiveColor = new Color(200, 0, 0, 255);
        private Color ActiveColor = Color.Wheat;
        private int Size = 20;
        private int spacing = 2;

        public bool muted;
        public bool playing;

        private Song song;

        public AudioController(Game game, bool muted, bool playing)
            : base(game)
        {
            this.muted = muted;
            this.playing = playing;
            this.game = (LeGame)game;
        }

        public override void Initialize()
        {
            ControlDrawLocation = new Rectangle((int)LeGame.CLIENT_SCREEN_WIDTH - Size - spacing, spacing, Size, Size);
            MuteDrawLocation = new Rectangle(ControlDrawLocation.X - Size - spacing, spacing, Size, Size);
            base.Initialize();
        }

        public void Load(ContentManager content)
        {
            ControlTexture = content.Load<Texture2D>("gui/audio_control");
            MuteTexture = content.Load<Texture2D>("gui/audio_mute");
            song = content.Load<Song>("music/theme");

            if (playing) MediaPlayer.Play(song);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime g)
        {
            if (playing) spriteBatch.Draw(ControlTexture, ControlDrawLocation, ActiveColor);
            else spriteBatch.Draw(ControlTexture, ControlDrawLocation, InactiveColor);
            if (muted) spriteBatch.Draw(MuteTexture, MuteDrawLocation, ActiveColor);
            else spriteBatch.Draw(MuteTexture, MuteDrawLocation, InactiveColor);

            base.Draw(g);
        }

        public void PlayerMouseClicked(Point mouse)
        {
            if (ControlDrawLocation.Contains(mouse))
            {
                if (!playing) // Play
                {
                    if (MediaPlayer.State == MediaState.Paused) MediaPlayer.Resume();
                    else MediaPlayer.Play(song);
                }
                else MediaPlayer.Pause();
                playing = !playing;
            }
            else if (MuteDrawLocation.Contains(mouse))
            {
                if (muted) MediaPlayer.Volume = 1.0f; else MediaPlayer.Volume = 0.0f;
                muted = !muted;
            }
        }

        public bool IsMute()
        {
            return muted;
        }

        public bool isPlaying()
        {
            return playing;
        }
    }
}
