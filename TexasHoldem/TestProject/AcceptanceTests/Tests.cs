using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class Tests : ProjectTest    {

        string username = "Hadas";
        string usernameWrong = "Gil";
        string password = "1234";
        string statusGame = "Active";
        string email = "gmail@gmail.com";
        string img = "img";
        string game = "Texas1";

        [TestMethod]
        public void TestRegister()
        {
            
            //User registared correctly
            Assert.AreEqual(this.register(username, password), this.getUserbyName(username));
            //check if User already registered
            Assert.IsTrue(this.isUserExist(username,password));
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

            
        }

        [TestMethod]
        public void TestEditProfile()
        {
            //to check if there is a user in the system
            Assert.IsTrue(this.isUserExist(username, password));
            Assert.IsTrue(this.isLogin(username));
            //to check if there is a user that can be edited ****for security policy***
            Assert.IsNotNull(this.editProfile(username));
            //to check if the user can be updated
            Assert.IsTrue(this.editName(username));
            Assert.IsTrue(this.editImage(img));
            Assert.IsTrue(this.editEmail(email));
            //check wrong input for update 
            Assert.IsFalse(this.editImage(username));
            Assert.IsFalse(this.editEmail(username));
            Assert.IsFalse(this.editName(img));
            //check if the user name is already taken
            Assert.AreNotEqual(this.editName(username), this.isUserExist(usernameWrong,password));

        }

        [TestMethod]
        public void TestCreatGame()
        {
            //before check if user is login
            Assert.AreNotEqual(this.login(username,password),null);
            Assert.IsTrue(this.isLogin(username));
            Assert.AreNotEqual(this.creatGame(game), null);
            //check if perferneces ok
            Assert.IsTrue(this.isGameDefOK(game));
            Assert.IsTrue(this.addPlayerToGame(username, game));
            //check wrong input
            Assert.AreNotEqual(this.creatGame(password),this.creatGame(game));
            Assert.IsFalse(this.addPlayerToGame(password, game));
           
        }

    }
}
