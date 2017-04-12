namespace TestProject
{
    class ProxyBridge : Bridge
    {
        private Bridge real;

        public ProxyBridge()
        {
            real = null;
        }
        public void setRealBridge(Bridge implementation)
        {
            if (real == null)
                real = implementation;
        }
        public object register(string username, string password)
        {
            //TODO
            return null;
        }
        public object login(string username, string password)
        {
            return null;
        }
        public object getUserbyName(string username)
        {
            return null;
        }
        public bool isUserExist(string username, string password)
        {
            return false;
        }
        public bool checkActiveGame(string satusGame)
        {
            return false;
        }
        public bool logoutUser(string game, string user)
        {
            return true;
        }
        public object editProfile(string username)
        {
            return null;
        }
        public bool editImage(string img)
        {
            return true;
        }
        public bool editName(string name)
        {
            return true;
        }
        public bool editEmail(string email)
        {
            return true;
        }
        public object creatGame(string gameDefinitions)
        {
            return null;
        }
        public bool isLogin(string username)
        {
            return true;
        }
        public bool isGameDefOK(string gameDefinithions)
        {
            return true;
        }
        public bool addPlayerToGame(string username, string game)
        {
            return true;
        }
        public object selectGametoJoin(string game)
        {
            return null;
        }
        public bool checkAvailibleSeats(string game)
        {
            return true;
        }
        public bool spectateActiveGame(string game)
        {
            return false;
        }
    }
}
