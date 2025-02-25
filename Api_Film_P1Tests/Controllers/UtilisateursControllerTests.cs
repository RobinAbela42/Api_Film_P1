using Api_Film_P1.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Api_Film_P1.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {

        static DbContextOptionsBuilder<FilmRatingsDBContext> builder = new DbContextOptionsBuilder<FilmRatingsDBContext>()
                    // Chaine de connexion à mettre dans les ( )
                    .UseNpgsql("Server=localhost;port=5432;Database=TheChariot; uid=postgres; password=postgres;"); 
        static FilmRatingsDBContext context = new FilmRatingsDBContext(builder.Options);
        UtilisateursController controller = new UtilisateursController(context);
        

        [TestMethod()]
        public void GetAllTest()
        {
            //Arrange 
            var dbuser = context.Utilisateurs;

            //Act
            var controllerUser= controller.GetUtilisateurs();

            Thread.Sleep(200);
            //Assert
            CollectionAssert.Equals(dbuser , controllerUser);
        }

        [TestMethod()]
        public void GetUtilisateursByIdTest_Succes()
        {

            //Arrange
            var user = context.Utilisateurs.First(u => u.UtilisateurId == 1);

            //Act
            var ctrlUser = controller.GetUtilisateurById(1);
            Thread.Sleep(200);
            //Assert
            Assert.AreEqual<Utilisateur>(user, ctrlUser.Result.Value);
        }
        [TestMethod()]
        public void GetUtilisateursByIdTest_Fail()
        {

            //Arrange
            var user = context.Utilisateurs.First(u => u.UtilisateurId == 2);

            //Act
            var ctrlUser = controller.GetUtilisateurById(1);

            Thread.Sleep(200);
            //Assert
            Assert.AreNotEqual<Utilisateur>(user, ctrlUser.Result.Value);
        }

        [TestMethod()]
        public void GetUtilisateursByEmailTest_Succes()
        {

            //Arrange
            var user = context.Utilisateurs.First(u => u.Mail == "gdominguez0@washingtonpost.com");

            //Act
            var ctrlUser = controller.GetUtilisateurByEmail("gdominguez0@washingtonpost.com");

            Thread.Sleep(200);
            //Assert
            Assert.AreEqual<Utilisateur>(user, ctrlUser.Result.Value);
        }
        [TestMethod()]
        public void GetUtilisateursByEmailTest_Fail()
        {

            //Arrange
            var user = context.Utilisateurs.First(u => u.Mail == "rrichings1@naver.com");

            //Act
            var ctrlUser = controller.GetUtilisateurByEmail("gdominguez0@washingtonpost.com");

            Thread.Sleep(200);
            //Assert
            Assert.AreNotEqual<Utilisateur>(user, ctrlUser.Result.Value);
        }

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
            // Assert
            Utilisateur? userRecupere = context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
                    Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        [TestMethod()]
        [ExpectedException(typeof(System.AggregateException))]
        public void Postutilisateur_ModelValidated_Violated_Unique_Mail()
        {

            //Arrange
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "gdominguez0@washingtonpost.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            //Act
            var result = controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            //Assert

        }

        [TestMethod()]
        [ExpectedException(typeof(System.AggregateException))]
        public void Postutilisateur_ModelValidated_Violated_Prenom_null()
        {

            //Arrange
            Utilisateur userAtester = new Utilisateur()
            {
                Prenom = null,
                Mobile = "0606070809",
                Mail = "gyuvezbuyvdfz@bhucvdzbuvedz.op",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            //Act
            var result = controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            //Assert
            
        }

        [TestMethod()]
        public void Postutilisateur_ModelValidated_Mobile_Non_Valide()
        {

            //Arrange
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "Machin",
                Prenom = "Driad",
                Mobile = "1",
                Mail = "gyuvvdfz@bhucvdvedz.op",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            string PhoneRegex = @"^0[0-9]{9}$";
            Regex regex = new Regex(PhoneRegex);
            if (!regex.IsMatch(userAtester.Mobile))
            {
                controller.ModelState.AddModelError("Mobile", "Le n° demobile doit contenir 10 chiffres"); //On met le même message que dans la classe Utilisateur
            }
            //Act
            var result = controller.PostUtilisateur(userAtester).Result;

            //Assert
        }


        [TestMethod()]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DeleteUtilisateur_Success()
        {
            //Arrange
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "Silverhand",
                Prenom = "Jhonny",
                Mobile = "0666666666",
                Mail = "fckcorop@netwatch.co",
                Pwd = "fckingcorpo",
                Rue = "9 rue de l'Atlantis",
                CodePostal = "69669",
                Ville = "Night City",
                Pays = "California",
                Latitude = null,
                Longitude = null
            };

            //Act
            context.Add(userAtester);
            context.SaveChanges();
            int id = context.Utilisateurs.First(u => u.Nom == "Silverhand" && u.Prenom == "Jhonny").UtilisateurId;

            var result = controller.DeleteUtilisateur(id);

            //Assert
            Assert.IsTrue(context.Utilisateurs.First(u => u.Nom == "Silverhand" && u.Prenom == "Jhonny") == null);

        }
    }
}