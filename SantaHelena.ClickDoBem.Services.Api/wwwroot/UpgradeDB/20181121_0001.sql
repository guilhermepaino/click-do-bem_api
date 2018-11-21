-- ------------------------------------------------------------------------------------------------------------------------------------
-- Estruturas 1a Parte
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela CampanhaImagem
CREATE TABLE `CampanhaImagem` (
  `Id` char(36) NOT NULL,
  `CampanhaId` char(36) NOT NULL,
  `Caminho` varchar(2000) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY (`CampanhaId`),
  CONSTRAINT `FK_CampanhaImagem_Campanha` FOREIGN KEY (`CampanhaId`) REFERENCES `Campanha` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ------------------------------------------------------------------------------------------------------------------------------------
-- Dados
-- ------------------------------------------------------------------------------------------------------------------------------------

-- Tabela Item
UPDATE ItemImagem set Caminho = REPLACE(Caminho, 'images', 'images\/item') WHERE Caminho NOT LIKE '\/images\/item\/%';

-- Tabela __ControleVersaoUpgradeDB
INSERT INTO `__ControleVersaoUpgradeDB` VALUES (6, now(), 'Criação de estrutura para imagem da campanha.');