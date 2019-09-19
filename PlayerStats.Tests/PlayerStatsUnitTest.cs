using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayerStats.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using PlayerStats.Models;
using PlayerStats.PlayerData;
using System.Net;

namespace PlayerStats.Tests
{
    [TestClass]
    public class PlayerStatsUnitTest
    {
        public List<Player> allPlayers;
        public playersController controller;
        private string _urlApi = "http://localhost:55489/";

        public PlayerStatsUnitTest()
        {
            allPlayers = new List<Player>();
            allPlayers = new DataPlayers().GetData();
            controller = new playersController();
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage();

            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });


        }

        [TestMethod]
        public void players_ShouldReturnAllPlayers()
        {
            // Arrange
            controller.Request.RequestUri = new Uri($"{_urlApi}players");


            // Act 
            var response = controller.players();

            // Assert

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void playersNotFound_ShouldReturn404()
        {
            // Arrange
            controller.Request.RequestUri = new Uri($"{_urlApi}players/404");


            // Act 
            var response = controller.players(404);

            // Assert

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void playertFound_ShouldReturnPlayer()
        {
            // Arrange
            controller.Request.RequestUri = new Uri($"{_urlApi}players/{allPlayers[0].Id}");


            // Act 
            var response = controller.players(allPlayers[0].Id);

            // Assert
            Player player;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.TryGetContentValue<Player>(out player));
            Assert.AreEqual(52, player.Id);

           
        }
        [TestMethod]
        public void playerToDeleteNotFound_ShouldReturn404()
        {
            // Arrange
            controller.Request.RequestUri = new Uri($"{_urlApi}players/404");


            // Act 
            var response = controller.deleteplayers(404);

            // Assert

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        [TestMethod]
        public void playerToDeletetFound_ShouldReturnOK()
        {
            // Arrange
            controller.Request.RequestUri = new Uri($"{_urlApi}players/{allPlayers[0].Id}");


            // Act 
            var response = controller.deleteplayers(allPlayers[0].Id);

            // Assert

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }




    }
}
