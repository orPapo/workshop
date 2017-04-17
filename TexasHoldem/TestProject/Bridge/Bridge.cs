﻿using System.Collections.Generic;

namespace TestProject
{
    interface Bridge
    {
        object register(string username, string password);
        object login(string username, string password);
        bool isLogin(string username);
        object getUserbyName(string username);
        bool isUserExist(string username, string password);
        bool checkActiveGame(string statusGame);
        bool logoutUser(string game, string user);
        object editProfile(string username);
        bool editImage(string img);
        bool editName(string name);
        bool editEmail(string email);
        object creatGame(string gameDefinitions);
        bool isGameDefOK(string gameDefinithions);
        bool addPlayerToGame(string username, string game);
        object selectGametoJoin(string game);
        bool checkAvailibleSeats(string game);
        bool spectateActiveGame(string game);
        bool exitGame(string game);
        int removeUserFromGame(string user, string game);
        object selectGameToReplay(string game);
        bool isWatchingReplay(string game);
        bool saveTurn(string game);
        List<string> findAllActive();
        List<string> filterByCriteria(string criteria);
        bool storeGameData();
        bool isGameOver(string game, string username);
        object joinLeaguePerPoints(int points);
      

    }
}
