
using PlayerStats.Models;
using PlayerStats.PlayerData;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PlayerStats.Controllers
{
    public class playersController : ApiController
    {
        public List<Player> allPlayers = new List<Player>();

        /// <summary>
        /// Constructor : read json file
        /// </summary>
        /// <returns></returns>
        #region playersController
        public playersController()
        {
            // read json file 
            allPlayers = new DataPlayers().GetData();
        } 
        #endregion


        /// <summary>
        /// get all players ordered by id
        /// </summary>
        /// <returns> List of Players</returns>
        [HttpGet]
        [ActionName("players")]
        #region players
        public HttpResponseMessage players()
        {


            return Request.CreateResponse(HttpStatusCode.OK, allPlayers.OrderBy(x => x.Id));

        } 
        #endregion
        /// <summary>
        /// retrive player by id. code 404 if not found
        /// </summary>
        /// <param name="if"> Player id</param>
        /// <returns>Player if found, 404 if not</returns>
        [HttpGet]
        [ActionName("players/id")]
        #region players
        public HttpResponseMessage players(int id)
        {
            Player _playerToFind = new Player();
            _playerToFind = allPlayers.Find(x => x.Id == id);

            if (_playerToFind == null)

                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, _playerToFind);

        } 
        #endregion

        /// <summary>
        /// Delete player by id, update json file
        /// </summary>
        /// <param name="id"> Player id</param>
        /// <returns>Ok code if found, 404 if not</returns>
        [HttpDelete]
        [ActionName("players/id")]
        #region deleteplayers
	    public HttpResponseMessage deleteplayers(int id)
            {
                Player _playerToFind = new Player();
                _playerToFind = allPlayers.Find(x => x.Id == id);

                if (_playerToFind == null)

                    return Request.CreateResponse(HttpStatusCode.NotFound);

                allPlayers.Remove(_playerToFind);


                // recreate json
                jsonPlayers jsonTocreate = new jsonPlayers();
                jsonTocreate.players = allPlayers;


                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(jsonTocreate);

            
                 System.IO.File.WriteAllText(DataPlayers.pathFile, json);
            

                return Request.CreateResponse(HttpStatusCode.OK);

            }
        #endregion
    }
}
