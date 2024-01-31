

USE FilRougeIntegrationTest;
-- Manual creation of  user table instead of using fortify.
CREATE TABLE `users` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `email_verified_at` timestamp NULL DEFAULT NULL,
  `password` varchar(255) NOT NULL,
  `two_factor_secret` text DEFAULT NULL,
  `two_factor_recovery_codes` text DEFAULT NULL,
  `remember_token` varchar(100) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  `prenom` varchar(10) DEFAULT NULL,
  `tel` varchar(10) DEFAULT NULL,
  `role` varchar(10) DEFAULT NULL,
  `LastUpdate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `users_email_unique` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
INSERT INTO users (id, name, email, email_verified_at, password, two_factor_secret, two_factor_recovery_codes, remember_token, created_at, updated_at, prenom, tel, role, LastUpdate)
VALUES 
    (1, 'User1 Victor', 'user1@example.com', NULL, 'password1', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL),
    (2, 'User2 Leon', 'user2@example.com', NULL, 'password2', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL),
    (3, 'User3 Bruce', 'user3@example.com', NULL, 'password3', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL),
    (4, 'User4 Corben', 'user4@example.com', NULL, 'password4', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL),
    (5, 'User5 Jérémy', 'user5@example.com', NULL, 'password5', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL),
    (6, 'User6 Michel', 'user6@example.com', NULL, 'password6', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL),
    (7, 'User7 Marcel', 'user7@example.com', NULL, 'password7', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL),
    (8, 'User8 Rachid', 'user8@example.com', NULL, 'password8', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL),
    (9, 'User9 Hubert', 'user9@example.com', NULL, 'password9', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL),
    (10, 'User10 Lucien', 'user10@example.com', NULL, 'password10', NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL, NULL);
VALUES('Michel', '', NULL, '$2y$10$dU58A1qEW4pTBbyeUoOP6usPDrB6Jh3Ds/4906huT4BYvH7ulu9my', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);




-- TABLE MATERIEL:  
CREATE TABLE `materiel` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `serviceDat` datetime DEFAULT NULL,
  `endGarantee` datetime DEFAULT NULL,
  `proprietaireId` bigint(20) unsigned DEFAULT NULL,
  `LastUpdate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `proprietaireId` (`proprietaireId`),
  CONSTRAINT `materiel_ibfk_1` FOREIGN KEY (`proprietaireId`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=125 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

 
   -- PEUPLEMENT DE LA TABLE MATERIEL AVEC 5 ELEMENTS:
INSERT INTO materiel (id, name, serviceDat, endGarantee, proprietaireId) VALUES 

(1, 'Ordinateur portable', '2023-01-01', '2025-01-01', 5),
(2,'Imprimante', '2022-05-15', '2024-05-15', 5),
(3,'Projecteur', '2023-03-10', '2025-03-10', 5),
(4,'Casque audio', '2023-07-20', '2025-07-20', 5),
(5,'Écran LCD', '2022-12-10', '2024-12-10', 5);
 
--  Cette instruction sql crée une table appelée category:
CREATE TABLE `category` (
  `reference` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `LastUpdate` datetime DEFAULT NULL,
  PRIMARY KEY (`reference`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

   -- PEUPLEMENT DE LA TABLE category AVEC 4 ELEMENTS:
INSERT INTO category (reference, name) VALUES (1, 'Laptops');
INSERT INTO category (reference, name) VALUES (2, 'Desktops');
INSERT INTO category (reference, name) VALUES (3, 'Servers');
INSERT INTO category (reference, name) VALUES (4, 'Network Equipment');

-- TABLE DE LIAISON CATEGORYMATERIEL:
CREATE TABLE `CategoryMateriel` (
  `refCat` int(11) DEFAULT NULL,
  `idMat` int(11) DEFAULT NULL,
  KEY `refCat` (`refCat`),
  KEY `idMat` (`idMat`),
  CONSTRAINT `CategoryMateriel_ibfk_1` FOREIGN KEY (`refCat`) REFERENCES `category` (`reference`),
  CONSTRAINT `CategoryMateriel_ibfk_2` FOREIGN KEY (`idMat`) REFERENCES `materiel` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


