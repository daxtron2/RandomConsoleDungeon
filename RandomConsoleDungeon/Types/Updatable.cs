namespace RandomConsoleDungeon
{
    internal class Updatable
    {
        protected static Screen Screen;
        protected static Game game;

        public Updatable()
        {
            if (Screen is null)
                Screen = Screen.Instance;
            if (game is null)
                game = Game.Instance;

            game.AddUpdatable(this);
        }
        public virtual void PreUpdate()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void PostUpdate()
        {

        }
    }
}