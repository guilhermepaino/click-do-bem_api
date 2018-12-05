-- ------------------------------------------------------------------------------------------------------------------------------------
-- Estruturas
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela TipoMatch
CREATE TABLE `TipoMatch` (
  `Id` char(36) NOT NULL,
  `DataInclusao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `Descricao` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UK_TipoMatch_Descricao` (`Descricao`),
  KEY `IX_TipoMatch_DtInclusao` (`DataInclusao`),
  KEY `IX_TipoMatch_DtAlteracao` (`DataAlteracao`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE ItemMatch ADD TipoMatchId char(36) NULL;
ALTER TABLE ItemMatch ADD Valor decimal(16,2) NOT NULL DEFAULT 0;
ALTER TABLE ItemMatch ADD Efetivado BIT NOT NULL DEFAULT 0;

ALTER TABLE Item ADD GeradoPorMatch BIT NOT NULL DEFAULT 0;

-- ------------------------------------------------------------------------------------------------------------------------------------
-- Dados
-- ------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO TipoMatch (Id, DataInclusao, Descricao) VALUES ('b69eed41-d87c-11e8-abfa-0e0e947bb2d6', NOW(), 'Doação');
INSERT INTO TipoMatch (Id, DataInclusao, Descricao) VALUES ('b69eed4f-d87c-11e8-abfa-0e0e947bb2d6', NOW(), 'Necessidade');
INSERT INTO TipoMatch (Id, DataInclusao, Descricao) VALUES ('a3412363-d87d-11e8-abfa-0e0e947bb2d6', NOW(), 'Escolha Combinada');

UPDATE ItemMatch SET TipoMatchId = 'a3412363-d87d-11e8-abfa-0e0e947bb2d6' WHERE TipoMatchId IS NULL;
UPDATE ItemMatch SET Efetivado = 1 WHERE TipoMatchId = 'a3412363-d87d-11e8-abfa-0e0e947bb2d6';

-- Tabela __ControleVersaoUpgradeDB
INSERT INTO `__ControleVersaoUpgradeDB` VALUES (3, now(), 'Criação de mecanismo de Match Unilateral.');

-- ------------------------------------------------------------------------------------------------------------------------------------
-- Foreign Keys
-- ------------------------------------------------------------------------------------------------------------------------------------
ALTER TABLE ItemMatch CHANGE TipoMatchId TipoMatchId char(36) NOT NULL;
ALTER TABLE ItemMatch ADD CONSTRAINT FK_ItemMatch_TipoMatch FOREIGN KEY (TipoMatchId) REFERENCES TipoMatch (Id);