﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SL;
using Backend.Game;
using System.Collections.Generic;
using Backend.User;
using Backend.Game.DecoratorPreferences;
using static Backend.Game.DecoratorPreferences.GamePolicyDecPref;
using ApplicationFacade;
using PeL;

namespace TestProject
{
    //filter active games
    [TestClass]
    public class FindActiveAvailableGamesTest
    {
        private SLInterface sl;
        private IPeL db;
        private GameCenter center = GameCenter.getGameCenter();
        private List<TexasHoldemGame> gamesList;

        [TestCleanup]
        public void Cleanup()
        {
            for (int i = 0; i < 4; i++)
                db.deleteUser(db.getUserByName("test" + i).id);
            center.shutDown();
        }

        [TestInitialize]
        public void SetUp()
        {
            db = new PeLImpl();
            for (int i = 0; i < 4; i++)
            {
                db.RegisterUser("test" + i, "" + i, "email" + i, null);
            }
            db.EditUserById(db.getUserByName("test0").id, null, null, null, null, 1000, 10, false);
            db.EditUserById(db.getUserByName("test1").id, null, null, null, null, 0, 15, false);
            db.EditUserById(db.getUserByName("test2").id, null, null, null, null, 700, 20, false);
            db.EditUserById(db.getUserByName("test3").id, null, null, null, null, 1500, 25, false);


            var userList = new List<SystemUser>
            {
                db.getUserByName("test0"),
                db.getUserByName("test1"),
                db.getUserByName("test2"),
                db.getUserByName("test3")
            };

            center = GameCenter.getGameCenter();

            //set the leagues
            center.maintainLeagues(userList);

            //get the league of user 3
            League l = center.getUserLeague(userList[3]);

            //setting the games
            //pref order: mustpref(spectate,league)->game type , buy in policy, starting chips, minimal bet, minimum players, maximum players.
            gamesList = new List<TexasHoldemGame>
            {
                //regular games
                new TexasHoldemGame(userList[0],new MustPreferences(new GamePolicyDecPref(GameTypePolicy.No_Limit,0,
                                                                    new BuyInPolicyDecPref(100,new StartingAmountChipsCedPref(500,
                                                                    new MinBetDecPref(20,new MinPlayersDecPref(2,
                                                                    new MaxPlayersDecPref (9,null) ))))),true),
                                                                    userIdDeltaRank => db.EditUserById(userIdDeltaRank[0], null, null, null, null, null, userIdDeltaRank[1], false),
                                                                    userIdLeaderB => db.EditUserLeaderBoardsById(userIdLeaderB[0], userIdLeaderB[1], userIdLeaderB[2])),
                new TexasHoldemGame(userList[0],new MustPreferences(new GamePolicyDecPref(GameTypePolicy.No_Limit,0,
                                                                    new BuyInPolicyDecPref(100,new StartingAmountChipsCedPref(500,
                                                                    new MinBetDecPref(20,new MinPlayersDecPref(2,
                                                                    new MaxPlayersDecPref (9,null) ))))),false),
                                                                    userIdDeltaRank => db.EditUserById(userIdDeltaRank[0], null, null, null, null, null, userIdDeltaRank[1], false),
                                                                    userIdLeaderB => db.EditUserLeaderBoardsById(userIdLeaderB[0], userIdLeaderB[1], userIdLeaderB[2])),
                new TexasHoldemGame(userList[1],new MustPreferences(new GamePolicyDecPref(GameTypePolicy.No_Limit,0,
                                                                    new BuyInPolicyDecPref(100,new StartingAmountChipsCedPref(500,
                                                                    new MinBetDecPref(20,new MinPlayersDecPref(2,
                                                                    new MaxPlayersDecPref (2,null) ))))),true),
                                                                    userIdDeltaRank => db.EditUserById(userIdDeltaRank[0], null, null, null, null, null, userIdDeltaRank[1], false),
                                                                    userIdLeaderB => db.EditUserLeaderBoardsById(userIdLeaderB[0], userIdLeaderB[1], userIdLeaderB[2])),
                new TexasHoldemGame(userList[1],new MustPreferences(new GamePolicyDecPref(GameTypePolicy.No_Limit,0,
                                                                    new BuyInPolicyDecPref(100,new StartingAmountChipsCedPref(500,
                                                                    new MinBetDecPref(20,new MinPlayersDecPref(2,
                                                                    new MaxPlayersDecPref (2,null) ))))),false),
                                                                    userIdDeltaRank => db.EditUserById(userIdDeltaRank[0], null, null, null, null, null, userIdDeltaRank[1], false),
                                                                    userIdLeaderB => db.EditUserLeaderBoardsById(userIdLeaderB[0], userIdLeaderB[1], userIdLeaderB[2])),
                new TexasHoldemGame(userList[2],new MustPreferences(new GamePolicyDecPref(GameTypePolicy.No_Limit,0,
                                                                    new BuyInPolicyDecPref(100,new StartingAmountChipsCedPref(500,
                                                                    new MinBetDecPref(20,new MinPlayersDecPref(2,
                                                                    new MaxPlayersDecPref (2,null) ))))),false),
                                                                    userIdDeltaRank => db.EditUserById(userIdDeltaRank[0], null, null, null, null, null, userIdDeltaRank[1], false),
                                                                    userIdLeaderB => db.EditUserLeaderBoardsById(userIdLeaderB[0], userIdLeaderB[1], userIdLeaderB[2])),
                new TexasHoldemGame(userList[2],new MustPreferences(new GamePolicyDecPref(GameTypePolicy.No_Limit,0,
                                                                    new BuyInPolicyDecPref(100,new StartingAmountChipsCedPref(500,
                                                                    new MinBetDecPref(20,new MinPlayersDecPref(2,
                                                                    new MaxPlayersDecPref (2,null) ))))),false),
                                                                    userIdDeltaRank => db.EditUserById(userIdDeltaRank[0], null, null, null, null, null, userIdDeltaRank[1], false),
                                                                    userIdLeaderB => db.EditUserLeaderBoardsById(userIdLeaderB[0], userIdLeaderB[1], userIdLeaderB[2])),
                //league games
                new TexasHoldemGame(userList[3],new MustPreferences(new GamePolicyDecPref(GameTypePolicy.No_Limit,0,
                                                                    new BuyInPolicyDecPref(100,new StartingAmountChipsCedPref(500,
                                                                    new MinBetDecPref(20,new MinPlayersDecPref(2,
                                                                    new MaxPlayersDecPref (2,null) ))))),false,l.minRank,l.maxRank),
                                                                    userIdDeltaRank => db.EditUserById(userIdDeltaRank[0], null, null, null, null, null, userIdDeltaRank[1], false),
                                                                    userIdLeaderB => db.EditUserLeaderBoardsById(userIdLeaderB[0], userIdLeaderB[1], userIdLeaderB[2])),
                new TexasHoldemGame(userList[3],new MustPreferences(new GamePolicyDecPref(GameTypePolicy.No_Limit,0,
                                                                    new BuyInPolicyDecPref(100,new StartingAmountChipsCedPref(500,
                                                                    new MinBetDecPref(20,new MinPlayersDecPref(2,
                                                                    new MaxPlayersDecPref (2,null) ))))),false,l.minRank,l.maxRank),
                                                                    userIdDeltaRank => db.EditUserById(userIdDeltaRank[0], null, null, null, null, null, userIdDeltaRank[1], false),
                                                                    userIdLeaderB => db.EditUserLeaderBoardsById(userIdLeaderB[0], userIdLeaderB[1], userIdLeaderB[2]))

            };

            for (int i = 0; i < gamesList.Count; i++)
            {
                gamesList[i].gameId = i;
                center.TexasHoldemGames.Add(gamesList[i]);
            }

            sl = new SLImpl();
        }
        [TestMethod]
        public void findActiveAvailableGamesSuccessTest()
        {
            List<TexasHoldemGame> active = center.getAllGames();
            Assert.AreEqual(active.Count, gamesList.Count);
        }

        [TestMethod]
        public void findActiveAvailableGamesFailTest()
        {
            Assert.IsTrue(center.endGame(gamesList[0].gameId).success);
            sl.removeGame(4);
            List<TexasHoldemGame> active = (List<TexasHoldemGame>)sl.findAllActiveAvailableGames();

            Assert.AreNotEqual(active.Count, gamesList.Count);
        }
    }
}