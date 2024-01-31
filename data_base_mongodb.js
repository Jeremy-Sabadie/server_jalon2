// Connexion à la base de données
use filrouge;

// Création de la collection "materiels" et insertion de données
db.materiels.insertMany([
  {
    "name": "Ordinateur portable",
    "serviceDate": ISODate("2023-01-01"),
    "endGuarantee": ISODate("2025-01-01"),
    "proprietaireId": ObjectId("idUtilisateur")
  },
  
]);

// Création de la collection "categories" et insertion de données
db.categories.insertMany([
  {
    "reference": 1,
    "name": "Laptops"
  },
  {
    "reference": 2,
    "name": "Desktops"
  },
  
]);

// Ajout de la référence entre "materiels" et "categories"
// (cette partie dépend de la manière dont tu souhaites gérer cette relation)

// Ajout du champ "LastUpdate" à chaque collection
db.materiels.updateMany({}, { $set: { "LastUpdate": new Date() } });
db.categories.updateMany({}, { $set: { "LastUpdate": new Date() } });
db.utilisateurs.updateMany({}, { $set: { "LastUpdate": new Date() } });
