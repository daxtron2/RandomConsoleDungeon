namespace RandomConsoleDungeon
{
    internal class Updatable
    {
        protected Screen Screen;
        protected Game game;

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

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void PostUpdate()
        {

        }
    }
}