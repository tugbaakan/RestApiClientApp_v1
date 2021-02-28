using RestApiClientApp_v1.Business;
using RestApiClientApp_v1.Models;
using RestApiClientApp_v1.Services;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RestApiClientApp_v1
{
    class Program
    {
        static void Main(string[] args)
        {
            GuessTheCountryGame game = new GuessTheCountryGame();
            game.StartGame();

        }


    }
}
