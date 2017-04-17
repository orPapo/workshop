﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class Tests : ProjectTest    {

        string username = "Hadas";
        string usernameWrong = "Gil";
        string password = "1234";
        string statusGame = "Active";
        string statusGame2 = "notActive";
        string email = "gmail@gmail.com";
        string img = "img";
        string game = "Texas1";
        string game2 = "Texas1";
        string seatsNotAv = "none";
        List<string> activeGames = new List<string>();
        string criteria = "points";

        [TestMethod]
        public void TestRegister()
        {
            
            //User registared correctly
            Assert.AreEqual(this.register(username, password), this.getUserbyName(username));
            //check if User already registered
            Assert.IsTrue(this.isUserExist(username,password));
            Assert.IsFalse(this.isUserExist(usernameWrong, password));
            //User enter wrong input
            Assert.AreNotEqual(this.register(username, password), this.register(usernameWrong, password));
            Assert.AreNotEqual(this.register(username, password), this.register("238///", "...."));
        }

        [TestMethod]
        public void TestLogin()
        {
            //Hadas login
            Assert.AreEqual(this.login(username, password),this.getUserbyName(username));
            //Hadas not equal to another user
            Assert.AreNotEqual(this.login(username, password), this.getUserbyName(usernameWrong));
            //Wrong input
            Assert.AreNotEqual(this.login("1234", password), this.getUserbyName(username));
            //Hadas is already exist in the system
            Assert.IsTrue(this.isUserExist(username, password));
            //Gil didn't login yet
            Assert.IsFalse(this.isUserExist(usernameWrong, password));

        }

        [TestMethod]
        public void TestLogout()
        {
            //If it is an active game than user can logout
            Assert.IsTrue(this.checkActiveGame(statusGame));
            Assert.IsTrue(this.logoutUser(statusGame, username));
            //can't logout
            Assert.IsFalse(this.checkActiveGame(statusGame2));

            
        }

        [TestMethod]
        public void TestEditProfile()
        {
            //check if there is a user in the system
            Assert.IsTrue(this.isUserExist(username, password));
            Assert.IsTrue(this.isLogin(username));
            //check if there is a user that can be edited ****for security policy***
            Assert.IsNotNull(this.editProfile(username));
            //check if the user can be updated
            Assert.IsTrue(this.editName(username));
            Assert.IsTrue(this.editImage(img));
            Assert.IsTrue(this.editEmail(email));
            //check wrong input for update 
            Assert.IsFalse(this.editImage(username));
            Assert.IsFalse(this.editEmail(username));
            Assert.IsFalse(this.editName(img));
            //check if the user name is already taken or email
            Assert.AreNotEqual(this.editName(username), this.isUserExist(usernameWrong,password));
            Assert.AreNotEqual(this.editEmail(email), this.getUserbyName(username));

        }

        [TestMethod]
        public void TestCreatGame()
        {
            //before check if user is login
            Assert.AreNotEqual(this.login(username,password),null);
            Assert.AreEqual(this.login(username,password),this.getUserbyName(username));
            Assert.IsTrue(this.isLogin(username));
            Assert.AreNotEqual(this.creatGame(game), null);
            //check if perferneces ok
            Assert.IsTrue(this.isGameDefOK(game));
            Assert.IsTrue(this.addPlayerToGame(username, game));
            //check wrong input
            Assert.AreNotEqual(this.creatGame(password),this.creatGame(game));
            Assert.IsFalse(this.addPlayerToGame(password, game));
           
        }
        
        [TestMethod]
        public void TestjoinExistingGame()
        {
            Assert.IsTrue(this.isLogin(username));
            Assert.AreEqual(this.selectGametoJoin(game), this.selectGametoJoin(game2));
            //check if the game is exist
            Assert.AreNotEqual(this.selectGametoJoin(game), null);
            //check for not an existing game
            Assert.AreEqual(this.selectGametoJoin(seatsNotAv), null);
            Assert.IsTrue(this.checkAvailibleSeats(game));
            //no availble seats
            Assert.IsFalse(this.checkAvailibleSeats(seatsNotAv));

            Assert.IsTrue(this.addPlayerToGame(username, game));

        }

        [TestMethod]
        public void TestSpectateActiveGame()
        {
            //check user login
            Assert.IsTrue(this.isLogin(username));
            //check the game is active
            Assert.IsTrue(this.checkActiveGame(statusGame));
            //check the game is inactive
            Assert.IsFalse(this.checkActiveGame(statusGame2));
            //check the game is exist
            Assert.AreNotEqual(this.selectGametoJoin(game),null);
            Assert.AreEqual(this.spectateActiveGame(game), selectGametoJoin(game));
            Assert.AreNotEqual(this.spectateActiveGame(statusGame2), selectGametoJoin(game));
        }

        [TestMethod]
        public void TestLeaveGame()
        {
            //user can exit game
            Assert.IsTrue(this.checkActiveGame(statusGame));
            Assert.IsTrue(this.isLogin(username));
            Assert.IsTrue(this.exitGame(game));
            Assert.IsTrue(this.checkAvailibleSeats(game));
            Assert.IsTrue(this.removeUserFromGame(username, game) >= 0);
            //user can't exit game
            Assert.IsFalse(this.removeUserFromGame(username, statusGame2) < 0);
            Assert.IsFalse(this.checkActiveGame(statusGame2));
            Assert.IsFalse(this.isLogin(usernameWrong));
            Assert.IsFalse(this.exitGame(game));
        }

        [TestMethod]
        public void TestReplayGame()
        {
            //the game is not active and exist in the system
            Assert.IsFalse(this.checkActiveGame(statusGame2));
            Assert.AreNotEqual(this.selectGameToReplay(statusGame2), null);

            Assert.IsTrue(this.checkActiveGame(statusGame));

        }
        
        [TestMethod]
        public void TestSaveTurns()
        {
            //check if the user is watching a replay
            Assert.IsTrue(this.isWatchingReplay(game));
            //save success
            Assert.IsTrue(this.saveTurn(game));
            //already saved the turn
            Assert.IsFalse(this.saveTurn(game2));
            //check if the replay can be saved
            Assert.IsTrue(this.saveTurn((string)this.selectGameToReplay(statusGame)));
            //check if the turn can be replay
            Assert.IsTrue(this.saveTurn(game) && this.exitGame(game));
        }

        [TestMethod]
        public void TesstFindActiveGame()
        {
            Assert.IsTrue(this.isLogin(username));
            //there is at least one game that is active
            Assert.IsTrue(this.checkActiveGame(statusGame));
            Assert.AreEqual(this.findAllActive(), activeGames);
            //zero games when there are noy any active games
            Assert.IsFalse(this.checkActiveGame(statusGame2));
            Assert.AreEqual(this.findAllActive(), statusGame2);
        }

        [TestMethod]
        public void TestFilterActiveGame()
        {
            //the system was able to find a list with this criteria
            Assert.IsTrue(this.isLogin(username));
            Assert.AreEqual(this.filterByCriteria(criteria),activeGames);
            Assert.AreNotEqual(this.filterByCriteria(criteria), "empty list");
            //check if filter all active games
            Assert.AreNotEqual(this.findAllActive(), this.filterByCriteria(criteria));
        }

        [TestMethod]
        public void TestStoreGame()
        {
            Assert.IsTrue(this.isLogin(username));
            Assert.IsTrue(this.storeGameData());

        }

    }
}
