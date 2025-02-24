using Api_Film_P1.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Film_P1.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        //Ne pas les mettre dans le constructeurs
        public UtilisateursControllerTests() 
        {
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>()
                        // Chaine de connexion à mettre dans les ( )
                        .UseNpgsql("Server=localhost;port=5432;Database=TheFOOL; uid=postgres; password=postgres;"); 
            FilmRatingsDBContext context = new FilmRatingsDBContext(builder.Options);
            UtilisateursController controller = new UtilisateursController(context);
        }
        

        [TestMethod()]
        public void GetAllTest()
        {

        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {

        }
    }
}