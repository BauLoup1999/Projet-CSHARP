CREATE TABLE Sites (
    ID INTEGER PRIMARY KEY,
    ville VARCHAR(255) NOT NULL  -- Création de la table Sites
); 

CREATE TABLE Services (
    ID INTEGER PRIMARY KEY,
    nom VARCHAR(255) NOT NULL -- Création de la table Services
);

CREATE TABLE Salaries (
    ID INTEGER PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    prénom VARCHAR(255) NOT NULL,
    téléphone_fixe VARCHAR(20),
    téléphone_portable VARCHAR(20),
    email VARCHAR(255),
    service_id INTEGER,
    site_id INTEGER,
    FOREIGN KEY (service_id) REFERENCES Services(ID),
    FOREIGN KEY (site_id) REFERENCES Sites(ID) -- Création de la table salarié avec les clés étrangeres
);


INSERT INTO Sites (ville) VALUES
    ('Paris'),
    ('Nantes'),
    ('Toulouse'),
    ('Nice'),
    ('Lille');



INSERT INTO Services (nom) VALUES
    ('Comptabilité'),
    ('Production'),
    ('Accueil'),
    ('Informatique'),
    ('Commercial');

INSERT INTO Salaries (nom, prénom, téléphone_fixe, téléphone_portable, email, service_id, site_id) VALUES
    ('Dupont', 'Jean', '0123456789', '0612345678', 'jean.dupont@email.com', 2, 1),
    ('Martin', 'Alice', '0234567890', '0678901234', 'alice.martin@email.com', 1, 4),
    ('Dubois', 'Paul', '0345678901', '0690123456', 'paul.dubois@email.com', 3, 2);

