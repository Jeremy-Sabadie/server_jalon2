using DAL.Sessions.Implementations;
using Dapper;

namespace TestIntegrationProject.Fixture
{//Classe qui implémente l'interface IClassFixture fixant l'environnement en se servant de la factory: APIfactory.
    public class AbstractIntegrationTests : IClassFixture<APIfactory>
    {
        protected readonly APIfactory _factory;
        protected readonly HttpClient _httpClient;
        public AbstractIntegrationTests(APIfactory fixture)
        {
            _factory = fixture;
            _httpClient = _factory.CreateClient();

            //Création d'un objetStreanReader qui fait référence au fichier sql contenant le script sql de  la BDD de test.
            StreamReader create = new StreamReader("integration_tests_database_script.sql");
            StreamReader drop = new StreamReader("drop.sql");
            //Lecture jusq'ua la fin du fichier lié au stream:
            //Drop de la BDD de test:
            string dropp = drop.ReadToEnd();
            //Création d'une BDD de tests neuve:
            string query = create.ReadToEnd();
            //Récupération de la chaine de connection de la base de donnée dédié au tests d'intégration don la valeur est écrite dans le fichier de configuration de la fixture à la clef DBConnectionStrings.
            string connection = fixture.Configuration.GetSection("DBConnectionStrings").Value;
            //Création d'une nouvelle connection Maria DB grace à la chaine de connection contenu dans la variable connection.
            var db = new DBSessionMariaDB(connection);
            //Nouvelle transaction.
            var tr = db.Connection.BeginTransaction();
            //Exécution de la requête pour effacer les différentes tables existantes.
            db.Connection.Execute(dropp, transaction: tr);
            //Exécution de la requête pour créer les différentes tables necessaires aux tests d'intégration.
            db.Connection.Execute(query, transaction: tr);
            //Commit de la transaction.
            tr.Commit();

        }
    }
}
