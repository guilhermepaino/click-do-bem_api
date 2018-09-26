-- Criar usuário no servidor
CREATE USER 'shclickappuser'@'%' IDENTIFIED BY 'sh.click';
GRANT ALL PRIVILEGES ON clickdobem.* TO 'shclickappuser'@'%';

-- criar base de dados
CREATE DATABASE clickdobem;