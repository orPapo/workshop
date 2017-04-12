namespace TestProject
{
    public class ProjectTest
    {
        private Bridge bridge;

        public ProjectTest()
        {
            this.bridge = Driver.getBridge();
          
        }
        public object register(string username, string password)
        {
           return  this.bridge.register(username,password);
        }
        public object login(string username, string password)
        {
           return this.bridge.login(username, password);
        }
        public object getUserbyName(string username)
        {
            return this.bridge.getUserbyName(username);
        }
        public bool isUserExist(string username, string password)
        {
            return this.isUserExist(username, password);
        }
        public bool checkActiveGame(string statusGame)
        {
            return this.checkActiveGame(statusGame);
        }
        public bool logoutUser(string game, string user)
        {
            return this.logoutUser(game, user);
        }
        public object editProfile(string username)
        {
            return this.editProfile(username);
        }
        public bool editImage(string img)
        {
            return this.editImage(img);
        }
        public bool editName(string name)
        {
            return this.editName(name);
        }
        public bool editEmail(string email)
        {
            return this.editEmail(email);
        }

        public object creatGame(string gameDefinitions)
        {
            return this.creatGame(gameDefinitions);
        }
        public bool isLogin(string username)
        {
            return this.isLogin(username);
        }
        public bool isGameDefOK(string gameDefinithions)
        {
            return this.isGameDefOK(gameDefinithions);
        }
        public bool addPlayerToGame(string username, string game)
        {
            return this.addPlayerToGame(username, game);
        }
        public  object selectGametoJoin(string game)
        {
            return this.selectGametoJoin(game);
        }
        public bool checkAvailibleSeats(string game)
        {
            return this.checkAvailibleSeats(game);
        }
    }

}
