-- ------------------------------------------------------------------------------------------------------------------------------------
-- Estruturas 1a Parte
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela ValorFaixa
CREATE TABLE `ValorFaixa` (
  `Id` char(36) NOT NULL,
  `Descricao` varchar(50) NOT NULL,
  `ValorInicial` decimal(16,2) NOT NULL,
  `ValorFinal` decimal(16,2) NOT NULL,
  `Inativo` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UK_ValorFaixa_Descricao` (`Descricao`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `Campanha` (
  `Id` char(36) NOT NULL,
  `DataInclusao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `Descricao` varchar(150) NOT NULL,
  `DataInicial` datetime NOT NULL,
  `DataFinal` datetime NOT NULL,
  `Prioridade` int(1) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UK_Campanha_Descricao` (`Descricao`),
  KEY `IX_Campanha_DtInclusao` (`DataInclusao`),
  KEY `IX_Campanha_DtAlteracao` (`DataAlteracao`),
  KEY `IX_Campanha_DtInicial` (`DataInicial`),
  KEY `IX_Campanha_DtFinal` (`DataFinal`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE Item 
  ADD ValorFaixaId char(36) NULL,
  ADD KEY `IX_Item_ValorFaixa` (ValorFaixaId),
  ADD CONSTRAINT `FK_Item_ValorFaixa` FOREIGN KEY (`ValorFaixaId`) REFERENCES `ValorFaixa` (`Id`);

ALTER TABLE Item 
  ADD CampanhaId char(36) NULL,
  ADD KEY `IX_Item_Campanha` (CampanhaId),
  ADD CONSTRAINT `FK_Item_Campanha` FOREIGN KEY (`CampanhaId`) REFERENCES `Campanha` (`Id`);

ALTER TABLE ItemMatch
  ADD ValorFaixaId char(36) NULL,
  ADD KEY `IX_ItemMatch_ValorFaixa` (ValorFaixaId),
  ADD CONSTRAINT `FK_ItemMatch_ValorFaixa` FOREIGN KEY (`ValorFaixaId`) REFERENCES `ValorFaixa` (`Id`);

-- ------------------------------------------------------------------------------------------------------------------------------------
-- Dados
-- ------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO ValorFaixa (Id, Descricao, ValorInicial, ValorFinal, Inativo) VALUES ('f97c08c3-e69e-11e8-9ebb-0242ac110006', '- Nenhum -',            -1.00,    -0.01, 0);
INSERT INTO ValorFaixa (Id, Descricao, ValorInicial, ValorFinal, Inativo) VALUES ('3174c9d9-e69e-11e8-9ebb-0242ac110006', 'Até 49,00',              0.00,    49.99, 0);
INSERT INTO ValorFaixa (Id, Descricao, ValorInicial, ValorFinal, Inativo) VALUES ('35d06ebd-e69e-11e8-9ebb-0242ac110006', 'De 50,00 a 99,99',      50.00,    99.99, 0);
INSERT INTO ValorFaixa (Id, Descricao, ValorInicial, ValorFinal, Inativo) VALUES ('3a7cff65-e69e-11e8-9ebb-0242ac110006', 'De 100,00 a 499,99',   100.00,   499.99, 0);
INSERT INTO ValorFaixa (Id, Descricao, ValorInicial, ValorFinal, Inativo) VALUES ('3fb1cbf4-e69e-11e8-9ebb-0242ac110006', 'De 500,00 a 999,99',   500.00,   999.99, 0);
INSERT INTO ValorFaixa (Id, Descricao, ValorInicial, ValorFinal, Inativo) VALUES ('43ae0ad6-e69e-11e8-9ebb-0242ac110006', 'Acima de 999,99',     1000.00, 99999.99, 0);

UPDATE Item SET ValorFaixaId = '3174c9d9-e69e-11e8-9ebb-0242ac110006' WHERE Valor >    0.00 AND Valor <=  49.99 AND ValorFaixaId IS NULL;
UPDATE Item SET ValorFaixaId = '35d06ebd-e69e-11e8-9ebb-0242ac110006' WHERE Valor >=  50.00 AND Valor <=  99.99 AND ValorFaixaId IS NULL;
UPDATE Item SET ValorFaixaId = '3a7cff65-e69e-11e8-9ebb-0242ac110006' WHERE Valor >= 100.00 AND Valor <= 499.99 AND ValorFaixaId IS NULL;
UPDATE Item SET ValorFaixaId = '3fb1cbf4-e69e-11e8-9ebb-0242ac110006' WHERE Valor >= 500.00 AND Valor <= 999.99 AND ValorFaixaId IS NULL;

UPDATE ItemMatch SET ValorFaixaId = '3174c9d9-e69e-11e8-9ebb-0242ac110006' WHERE Valor >    0.00 AND Valor <=  49.99 AND ValorFaixaId IS NULL;
UPDATE ItemMatch SET ValorFaixaId = '35d06ebd-e69e-11e8-9ebb-0242ac110006' WHERE Valor >=  50.00 AND Valor <=  99.99 AND ValorFaixaId IS NULL;
UPDATE ItemMatch SET ValorFaixaId = '3a7cff65-e69e-11e8-9ebb-0242ac110006' WHERE Valor >= 100.00 AND Valor <= 499.99 AND ValorFaixaId IS NULL;
UPDATE ItemMatch SET ValorFaixaId = '3fb1cbf4-e69e-11e8-9ebb-0242ac110006' WHERE Valor >= 500.00 AND Valor <= 999.99 AND ValorFaixaId IS NULL;

-- ------------------------------------------------------------------------------------------------------------------------------------
-- Estruturas 2a Parte
-- ------------------------------------------------------------------------------------------------------------------------------------
ALTER TABLE Item DROP Valor;

ALTER TABLE ItemMatch DROP Valor;

-- Tabela __ControleVersaoUpgradeDB
INSERT INTO `__ControleVersaoUpgradeDB` VALUES (5, now(), 'Implementacao de faixa de valor e campanhas.');